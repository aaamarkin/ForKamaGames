using UnityEngine;

[System.Serializable] public class PositionalSpringTweaksAndState
{
	public Transform sourceObject;

	[TooltipAttribute("Damping dampingCriticality: 0..1-under damped, 1-critically damped, >1-over-damped. See http://en.wikipedia.org/wiki/Damping")]
	public float dampingCriticality = 1.0f;

	[TooltipAttribute("The higher the value the faster motion copying will be. In units of 1/second, roughly.")]
	public Vector3 naturalFrequency = new Vector3(4f, 4f, 4f); /// as in wikipedia

	[TooltipAttribute("If true the spring will be applied in the target object's r.f. This is good for objects that normally move as opposed to stay still.")]
	public bool applySpringInMovingReferenceFrame = true;
		
	public enum eFreezeType
	{
		DONT_FIX_INITIAL_RELATIVE_POSITION,
		IN_WORLD_FRAME,
		IN_SOURCE_LOCAL_FRAME,
	};
	public eFreezeType fixRelativePositionAtStart = eFreezeType.IN_WORLD_FRAME;

	[System.Serializable]
	public class ScalesAndLimits
	{
		public Vector3 displacementScale = Vector3.one;
		public Vector3 maxDisplacement = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		public Vector3 minDisplacement = new Vector3(-float.MaxValue, -float.MaxValue, -float.MaxValue);
	}
	[TooltipAttribute("Advanced settings")]
	public ScalesAndLimits scalesAndLimits = new ScalesAndLimits();

	[System.NonSerialized] public Vector3 frozenLocalOffset = Vector3.zero;
	[System.NonSerialized] public Vector3 frozenWorldlOffset = Vector3.zero;

	/// State
	[System.NonSerialized] public Vector3 lastSourcePosition;
	[System.NonSerialized] public Vector3 lastPosition;
		
	[System.NonSerialized] public Vector3 springVelocity = Vector3.zero;
}
	
/// Rotational stuff
[System.Serializable] public class RotationalSpringTweaksAndState
{
//		[TooltipAttribute("The object to copy the motion from via the spring.")]
	public Transform sourceObject;

	[TooltipAttribute("Damping dampingCriticality: 0..1-under damped, 1-critically damped, >1-over-damped. See http://en.wikipedia.org/wiki/Damping")]
	public float dampingCriticality = 1.0f;
		
	[TooltipAttribute("The higher the value the faster motion copying will be. In units of radians/second, roughly.")]
	public float naturalFrequency = 4f; /// as in wikipedia

	public bool applySpringInMovingReferenceFrame = true;
		
	public bool fixRelativeOrientationAtStart = true;
		
	[System.NonSerialized] public Quaternion frozenLocalOffset;

		/// State
	[System.NonSerialized] public Quaternion lastSourceRotation;
	[System.NonSerialized] public Quaternion lastRotation;
	[System.NonSerialized] public Quaternion startingSourceRotation;
			

	[System.NonSerialized] public Vector3 springVelocity = Vector3.zero;
}

public abstract class MotionCopierComponent : AppElement
{
    public static void Assert(bool _condition, string _text)
		{
			if(!_condition)
			{
				//UnityEngine.Debug.Break();
				UnityEngine.Debug.LogWarning("Assertion failed: " + _text);
			}
		}

		public static void Assert(bool _condition)
		{
			Assert(_condition, "");
		}

		public static Vector3[] MakeOrthonormalBasis(Vector3 _vec0)
		{
			Assert(_vec0.magnitude > 1e-6f);

			Vector3[] perps = new Vector3[2];
			float dp1 = Mathf.Abs(Vector3.Dot(_vec0, Vector3.right));
			float dp2 = Mathf.Abs(Vector3.Dot(_vec0, Vector3.up));
			float dp3 = Mathf.Abs(Vector3.Dot(_vec0, Vector3.forward));
			if(dp1 >= dp2 && dp1 >= dp3)
				perps[0] = Vector3.Cross(_vec0, Vector3.up).normalized;
			if(dp2 >= dp1 && dp2 >= dp3)
				perps[0] = Vector3.Cross(_vec0, Vector3.forward).normalized;
			else
				perps[0] = Vector3.Cross(_vec0, Vector3.right).normalized;

			perps[1] = Vector3.Cross(_vec0, perps[0]).normalized;

			return perps;
		}

		public static void ToAngleAndAxis(out float _angleRadians, out Vector3 _axis, Quaternion _Q)
		{
			///floating point inaccuracy can lead to w component > 1..., which breaks 
			float Qlength = Mathf.Sqrt(Quaternion.Dot(_Q, _Q));
			_Q.x /= Qlength;
			_Q.y /= Qlength;
			_Q.z /= Qlength;
			_Q.w /= Qlength;

			_axis = new Vector3(_Q.x, _Q.y, _Q.z);
			float len = _axis.magnitude;

			if(Mathf.Abs(_Q.w) > 0.99f)
			{
				_angleRadians = 2.0f * Mathf.Asin(len);
				if(len == 0.0f)
					_axis = new Vector3(1, 0, 0);
				else
					_axis /= len;
			}
			else
			{
				_angleRadians = 2.0f * Mathf.Acos(_Q.w);
				_axis /= len;
			}
		}

		public static Quaternion Conjugate(Quaternion q)
		{
			return new Quaternion(-q.x, -q.y, -q.z, q.w);
		}

		public static Vector3 ToAngularVelocity(Quaternion _dQ, float _dT)
		{
			float angle;
			Vector3 axis;
			ToAngleAndAxis(out angle, out axis, _dQ);
			return (angle / _dT) * axis;
		}

		public static Vector3 ToAngularVelocity(Quaternion _Qnow, Quaternion _Qbefore, float _dT)
		{
			Quaternion deltaQ = _Qnow * Conjugate(_Qbefore);
			return ToAngularVelocity(deltaQ, _dT);
		}

		/// This is a replacement for Quaternion.AngleAxis()
		/// the original function has a problem that it uses degrees
		public static Quaternion ToQuaternion(float _angleRadians, Vector3 _axis)
		{
			Vector3 axisN = _axis.normalized;
			float sine = Mathf.Sin(_angleRadians * 0.5f);
			return new Quaternion
			(
				axisN.x * sine,
				axisN.y * sine,
				axisN.z * sine,
				Mathf.Cos(_angleRadians * 0.5f)
			);
		}

		public static Quaternion QuaternionScale(Quaternion _Q, float _power)
		{
			/// See http://www.iquilezles.org/www/articles/quaternions/quaternions.htm
			float angle;
			Vector3 axis;
			ToAngleAndAxis(out angle, out axis, _Q);
			return ToQuaternion(angle * _power, axis);
		}

		public static Quaternion ToQuaternionFromAngularVelocityAndTime(Vector3 _omega, float _time)
		{
			float angle = _omega.magnitude * _time;
			if(Mathf.Abs(angle) > 1e-15f)
			{
				Vector3 axisN = _omega.normalized;
				float sin = Mathf.Sin(angle * 0.5f);
				return new Quaternion
				(
					axisN.x * sin,
					axisN.y * sin,
					axisN.z * sin,
					Mathf.Cos(angle * 0.5f)
				);
			}
			else
				return Quaternion.identity;
		}
		
		public static void DampedSpringGeneralSolution
		(
			out float _newX, out float _newV,
			float _currentX, float _currentV,
			float _time,
			float _criticality,/// 0..1 - under damped, 1 - critically damped, >1 - over-damped (this is called "damping ratio" on the wikipage)
			float _naturalFrequency
		)
		{///See http://en.wikipedia.org/wiki/Damping
			if(_criticality < 0.0f)
			{
				Debug.LogWarning("criticality must be >= 0. Using 0.");
				_criticality = 0f;
			}
			if(_naturalFrequency <= 0.0f)
			{
				Debug.LogWarning("natural frequency must be > 0. Using 1.");
				_naturalFrequency = 1f;
			}

			if(_criticality == 1.0f)
			{///Critically damped
				float omega_time = _naturalFrequency * _time;
				float expfactor = Mathf.Exp(-omega_time);
				float A = _currentX;
				float B = _currentV + _naturalFrequency * _currentX;
				_newX = expfactor * (A + B * _time);
				_newV = expfactor * (B * (1.0f - omega_time) - _naturalFrequency * A);
			}
			else if(_criticality < 1.0f)
			{///Under-damped
				float omega_d = _naturalFrequency * Mathf.Sqrt(1.0f - _criticality * _criticality);
				float A = _currentX;
				float zeta_omega = _criticality * _naturalFrequency;
				float B = 1.0f / omega_d * (zeta_omega * _currentX + _currentV);
				float expfactor = Mathf.Exp(-zeta_omega * _time);
				float omega_d_time = omega_d * _time;
				float cos_omega_d_time = Mathf.Cos(omega_d_time);
				float sin_omega_d_time = Mathf.Sin(omega_d_time);
				_newX = expfactor * (A * cos_omega_d_time + B * sin_omega_d_time);
				_newV = expfactor * (cos_omega_d_time * (B * omega_d - zeta_omega * A) - sin_omega_d_time * (A * omega_d + B * zeta_omega));
			}
			else
			{///Over-damped
				float S = Mathf.Sqrt(_criticality * _criticality - 1.0f);
				float gamma_plus = _naturalFrequency * (S - _criticality);
				float gamma_minus = -_naturalFrequency * (S + _criticality);
				float B = (gamma_plus * _currentX - _currentV) / (gamma_plus - gamma_minus);
				float A = _currentX - B;
				float exp_gamma_plus_time = Mathf.Exp(gamma_plus * _time);
				float exp_gamma_minus_time = Mathf.Exp(gamma_minus * _time);
				float A_exp_gamma_plus_time = A * exp_gamma_plus_time;
				float B_exp_gamma_minus_time = B * exp_gamma_minus_time;
				_newX = A_exp_gamma_plus_time + B_exp_gamma_minus_time;
				_newV = gamma_plus * A_exp_gamma_plus_time + gamma_minus * B_exp_gamma_minus_time;
			}
		}

		public static void DampedSpringGeneralSolution
		(
			out Quaternion _newX, out Vector3 _newV,
			Quaternion _currentX, Vector3 _currentV,
			float _time,
			float _criticality,/// 0..1 - under damped, 1 - critically damped, >1 - over-damped (this is called "damping ratio" on the wikipage)
			float _naturalFrequency
		)
		{///See http://en.wikipedia.org/wiki/Damping

			if(_criticality < 0.0f)
			{
				Debug.LogWarning("criticality must be >= 0. Using 0.");
				_criticality = 0f;
			}
			if(_naturalFrequency <= 0.0f)
			{
				Debug.LogWarning("natural frequency must be > 0. Using 1.");
				_naturalFrequency = 1f;
			}


			if(_criticality == 1.0f) ///Critically damped
			{
				float omega_time = _naturalFrequency * _time;
				float expfactor = Mathf.Exp(-omega_time);
				Quaternion A = _currentX;
				Vector3 B = _currentV + ToAngularVelocity(_currentX, 1f / _naturalFrequency);
				_newX = QuaternionScale(ToQuaternionFromAngularVelocityAndTime(B, _time) * A, expfactor);
				_newV = expfactor * (B * (1.0f - omega_time) - ToAngularVelocity(A, 1f / _naturalFrequency));
			}
			else if(_criticality < 1.0f) ///Under-damped
			{
				float omega_d = _naturalFrequency * Mathf.Sqrt(1.0f - _criticality * _criticality);
				Quaternion A = _currentX;
				float zeta_omega = _criticality * _naturalFrequency;
				Vector3 B = 1.0f / omega_d * (ToAngularVelocity(_currentX, 1f / zeta_omega) + _currentV);
				float expfactor = Mathf.Exp(-zeta_omega * _time);
				float omega_d_time = omega_d * _time;
				float cos_omega_d_time = Mathf.Cos(omega_d_time);
				float sin_omega_d_time = Mathf.Sin(omega_d_time);
				_newX = QuaternionScale(
					ToQuaternionFromAngularVelocityAndTime(B, sin_omega_d_time) * QuaternionScale(A, cos_omega_d_time),
					expfactor
				);
				_newV = expfactor * (
					cos_omega_d_time * (B * omega_d - ToAngularVelocity(A, 1f / zeta_omega))
					- sin_omega_d_time * (ToAngularVelocity(A, 1f / omega_d) + B * zeta_omega)
				);
			}
			else ///Over-damped
			{
				float S = Mathf.Sqrt(_criticality * _criticality - 1.0f);
				float gamma_plus = _naturalFrequency * (S - _criticality);
				float gamma_minus = -_naturalFrequency * (S + _criticality);
				Vector3 omega = ToAngularVelocity (_currentX, 1f / gamma_plus);
				Vector3 B = (omega - _currentV) / (gamma_plus - gamma_minus);
				Quaternion A = _currentX * Conjugate(ToQuaternionFromAngularVelocityAndTime(B, 1f));
				float exp_gamma_plus_time = Mathf.Exp(gamma_plus * _time);
				float exp_gamma_minus_time = Mathf.Exp(gamma_minus * _time);
				Quaternion A_exp_gamma_plus_time = QuaternionScale(A, exp_gamma_plus_time);
				Vector3 A_exp_gamma_plus_time_V3 = ToAngularVelocity(A, 1f / exp_gamma_plus_time);
				Vector3 B_exp_gamma_minus_time = B * exp_gamma_minus_time;
				Quaternion B_exp_gamma_minus_time_Q = ToQuaternionFromAngularVelocityAndTime(B, exp_gamma_minus_time);
				_newX = B_exp_gamma_minus_time_Q * A_exp_gamma_plus_time;
				_newV = gamma_plus * A_exp_gamma_plus_time_V3 + gamma_minus * B_exp_gamma_minus_time;
			}
		}
	
	
	
	// -------------------------------------------------------------------------------- //
	[HideInInspector]
	public bool isAttached;
	
	[HideInInspector]
	public PositionalSpringTweaksAndState positionalSpring = new PositionalSpringTweaksAndState();
	[HideInInspector]
	public RotationalSpringTweaksAndState rotationalSpring = new RotationalSpringTweaksAndState();

	[HideInInspector]
	public SnappableComponent _snappableComponent;
	[HideInInspector]
	public OnSnapActionComponent _onSnapActionComponent;

	[HideInInspector]
	public Rigidbody _rigidbody;
	
	public virtual void Dettach()
	{	
		isAttached = false;
	}

	void Start()
	{
		_snappableComponent = TryGetComponentEverywhere<SnappableComponent>();
		_onSnapActionComponent = TryGetComponentEverywhere<OnSnapActionComponent>();

		_rigidbody = GetComponentInParent<Rigidbody>();
	}

	private bool ShouldSnappingComponentBeSnapped()
	{
		if (_snappableComponent == null)
		{
			return false;
		}
		else
		{
			return _snappableComponent.ShouldBeSnapped;
		}
	}
		
	private bool IsSnappingComponentSnapped()
	{
		if (_snappableComponent == null)
		{
			return false;
		}
		else
		{
			return _snappableComponent.IsSnapped();
		}
	}
	
	private void UnSnappSnappingComponent()
	{
		if (_snappableComponent != null)
		{
			_snappableComponent.ShouldBeSnapped = false;
		}
		if (_onSnapActionComponent != null)
		{
			_onSnapActionComponent.OnUnSnap(_snappableComponent);
		}
	}

	public void SetRotatableQuartanions(Quaternion quartanion)
	{
		rotationalSpring.frozenLocalOffset = Quaternion.Inverse(rotationalSpring.sourceObject.rotation) * (quartanion * transform.rotation);
			
	}
	

	private void SnapSnappingComponent()
	{
		SetSnapCenterPosition();
			
		_onSnapActionComponent.OnSnap(_snappableComponent);
	}
	
	public virtual void Attach(Transform sourceObject)
	{
		isAttached = true;

		positionalSpring.sourceObject = sourceObject;
		rotationalSpring.sourceObject = sourceObject;

		SetPositionalFrozenWorldlOffsetAndLastSource();
		
		SetPositionSpringLastPosition();

		SetPRotationalFrozenWorldlOffsetAndLastSource();

		rotationalSpring.startingSourceRotation = rotationalSpring.sourceObject.localRotation;
			
		SetPositionSpringLastRotation();

	}
	
	public void RepositionAndRotate(float zDt)
	{
		Quaternion sourceRotation = rotationalSpring.sourceObject.rotation * rotationalSpring.frozenLocalOffset;

		Vector3 sourceRotVelocity = ToAngularVelocity(sourceRotation, rotationalSpring.lastSourceRotation, zDt);
		Quaternion currentRelativeRotation = rotationalSpring.lastRotation * Conjugate(sourceRotation);

		/// Moving r.f. does not work well with high damping. Apply a bit of hackery.
		bool useMovingRf = rotationalSpring.applySpringInMovingReferenceFrame && (rotationalSpring.dampingCriticality <= 1f);
		if(useMovingRf)
			currentRelativeRotation = sourceRotation * Conjugate(rotationalSpring.lastSourceRotation) * currentRelativeRotation;
		Vector3 rfVelocity = (useMovingRf ? sourceRotVelocity : Vector3.zero);
		
		Quaternion newRelativeRotation;
		Vector3 newSpringVelocity;

		DampedSpringGeneralSolution
		(
			out newRelativeRotation, out newSpringVelocity,
			currentRelativeRotation, rotationalSpring.springVelocity - rfVelocity,
			zDt,
			rotationalSpring.dampingCriticality,
			rotationalSpring.naturalFrequency
		);

		/// Limit the spring velocity so that over the timestep zDt it cannot rotate by more than 180 deg.
		float mag = newSpringVelocity.magnitude;
		if (mag * zDt > Mathf.PI)
			newSpringVelocity *= Mathf.PI / mag;

		rotationalSpring.springVelocity = newSpringVelocity + rfVelocity;

		Quaternion newRotation = newRelativeRotation * sourceRotation;
			
		
			
		Quaternion relative = rotationalSpring.sourceObject.localRotation * Quaternion.Inverse(rotationalSpring.startingSourceRotation);						
			

		Vector3 dir = positionalSpring.lastPosition - positionalSpring.sourceObject.transform.position; // get point direction relative to pivot

		Vector3 rotatedPoint = ( relative * dir ) + positionalSpring.sourceObject.transform.position; // calculate rotated point
			
			

		
			
			

		

		Vector3 sourcePosition = positionalSpring.frozenWorldlOffset
			+ positionalSpring.sourceObject.TransformPoint(positionalSpring.frozenLocalOffset);
		
		
		Vector3 sourcePosVelocity = (sourcePosition - positionalSpring.lastSourcePosition)/zDt;
		Vector3 targetPosition = sourcePosition;
		Vector3 currentRelativePosition = positionalSpring.lastPosition - targetPosition;
		if(positionalSpring.applySpringInMovingReferenceFrame)
			currentRelativePosition += sourcePosition - positionalSpring.lastSourcePosition;
		
		Vector3 newRelativePosition = Vector3.zero;
		
		for(int d=0; d<3; d++)
		{
			float newRelativePosition_d, newSpringVelocity_d;
			DampedSpringGeneralSolution
			(
				 out newRelativePosition_d, out newSpringVelocity_d,
				 currentRelativePosition[d],
				 positionalSpring.springVelocity[d]	- (positionalSpring.applySpringInMovingReferenceFrame ? sourcePosVelocity[d] : 0.0f),
				 zDt,
				 positionalSpring.dampingCriticality,
				 positionalSpring.naturalFrequency[d]
			);
			
			newRelativePosition[d] = newRelativePosition_d;
			positionalSpring.springVelocity[d] = newSpringVelocity_d + (positionalSpring.applySpringInMovingReferenceFrame ? sourcePosVelocity[d] : 0.0f);
		}
		
		positionalSpring.lastPosition = targetPosition + newRelativePosition;
		
		
		// Convert to sourceObject's local frame to apply scales and limits
		Vector3 delta = positionalSpring.sourceObject.transform.InverseTransformDirection(newRelativePosition);
		// Apply scale
		delta = Vector3.Scale(delta, positionalSpring.scalesAndLimits.displacementScale);
		// Apply limit
		delta = Vector3.Min(delta, positionalSpring.scalesAndLimits.maxDisplacement);
		delta = Vector3.Max(delta, positionalSpring.scalesAndLimits.minDisplacement);
		// Convert back
		delta = positionalSpring.sourceObject.transform.TransformDirection(delta);

		
		
		SetRotation(newRotation);

		rotationalSpring.lastRotation = newRotation;
		
		rotationalSpring.lastSourceRotation = sourceRotation;
		
		if (ShouldSnappingComponentBeSnapped())
		{
			if (!IsSnappingComponentSnapped())
			{
				SnapSnappingComponent();	
					
			}
			if (ShouldUnsnapSnappingComponent((rotatedPoint + delta).magnitude))
			{

				UnSnappSnappingComponent();
			}
		}
		else
		{
				
			SetPosition(rotatedPoint + delta);

		}

		positionalSpring.lastSourcePosition = sourcePosition;
	}

	public bool IsAttached()
	{
		return isAttached;
	}

	public abstract void SetSnapCenterPosition();
	
	public abstract void SetRotation(Quaternion newRotation);
	
	public abstract void SetPosition(Vector3 newPosition);
	
	public abstract bool ShouldUnsnapSnappingComponent(float magnitude);
	
	public abstract void SetPositionalFrozenWorldlOffsetAndLastSource();
	
	public abstract void SetPRotationalFrozenWorldlOffsetAndLastSource();
	
	public abstract void SetPositionSpringLastPosition();
	
	public abstract void SetPositionSpringLastRotation();

}
