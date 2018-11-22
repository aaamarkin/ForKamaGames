using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChangerParentComponent : ModeChangerComponent {

	public bool useGravityInPlayMode;
   
	private Rigidbody _rigidbody;
	private ModeChangerChildComponent[] _modeChangerChildComponents;


	public override void LastInAwake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_modeChangerChildComponents = GetComponentsInChildren<ModeChangerChildComponent>();
	}

	public override void SetPlayMode()
	{
		if (useGravityInPlayMode)
		{
			_rigidbody.useGravity = true;
		}
		else
		{
			_rigidbody.isKinematic = true;
		}

		foreach (var modeChangerChildComponent in _modeChangerChildComponents)
		{
			modeChangerChildComponent.SetPlayMode();
		}
	}
    
	public override void SetBuildMode()
	{
		_rigidbody.useGravity = false;
		_rigidbody.velocity = Vector3.zero;
		_rigidbody.freezeRotation = true;
		_rigidbody.freezeRotation = false;
		_rigidbody.isKinematic = false;
		
		foreach (var modeChangerChildComponent in _modeChangerChildComponents)
		{
			modeChangerChildComponent.SetBuildMode();
		}

	}

	public override bool IsBuildabel()
	{
		foreach (var modeChangerChildComponent in _modeChangerChildComponents)
		{
			if (!modeChangerChildComponent.IsBuildabel())
			{
				return modeChangerChildComponent.IsBuildabel();
			}
		}
		return true;
	}
}
