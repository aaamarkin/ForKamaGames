using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomComponent : AppElement {

	[HideInInspector]
	public Rigidbody Rigidbody;

	private Quaternion desiredRotation;
	
	private Quaternion defaultRotation;
	
	override public void LastInAwake()
	{
		Rigidbody = GetComponent<Rigidbody>();
//		Rigidbody.AddForce(new Vector3(app.Model.GameSceneModel.Row_1_Model.velocity * -50, 0, 0));
//		
//		Rigidbody.AddTorque();

		desiredRotation =  Rigidbody.rotation * Quaternion.Euler(0, 90, 0); // this adds a 90 degrees Y rotation
		
		Invoke("RemoveFromScene", app.Model.GameSceneModel.VisualEffectsRowModel.boomStayTime);
	}

	void RemoveFromScene()
	{
		Destroy(gameObject);
	}
	
	void FixedUpdate( ) {
 
		Rigidbody.MoveRotation( Quaternion.RotateTowards( Rigidbody.rotation, desiredRotation, app.Model.GameSceneModel.VisualEffectsRowModel.boomRotationVelocity * Time.deltaTime));
 
	}
}
