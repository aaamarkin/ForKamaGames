using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotationComponent : AppElement
{

	public float speed = 200;
	
	private Rigidbody _rigidbody;
	
	// Use this for initialization
	void Start ()
	{

		_rigidbody = GetComponent<Rigidbody>();

	}
	
	
	
	void FixedUpdate () {
		
//		Vector3 eulerAngleVelocity = new Vector3 (0, speed, 0);
//		
//		Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * Time.deltaTime);
//		
//		_rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
		
		
//		_rigidbody.MoveRotation(Quaternion.AngleAxis(app.Model.GameModel.velocity, transform.up) * _rigidbody.rotation);
		
//		_rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);

		_rigidbody.angularVelocity = new Vector3(app.Model.GameSceneModel.rotatableComponentVelocity, 0, 0);

	}
	

}
