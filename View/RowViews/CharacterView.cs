using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : AppElement
{
	[HideInInspector]
	public Rigidbody Rigidbody;
	
	private Vector3 LastVelocity;
	
//	private float LastpositionX;
	
	public float OriginalZPosition;

//	private Vector3 acceleration;

	public override void LastInAwake()
	{
		Rigidbody = GetComponent<Rigidbody>();
		OriginalZPosition = Rigidbody.position.z;
	}
	
	void OnCollisionEnter(Collision collision)
	{
//		lastVelocity = Rigidbody.velocity;
		
		if (collision.collider.gameObject.tag == "FlyingBlock")
		{

//			print("Impulse = " + collision.impulse.sqrMagnitude + ", Velocity = " + Rigidbody.velocity.sqrMagnitude);

//			print("Acceleration = " + acceleration);
			
			FlyingBlockComponent block = collision.gameObject.GetComponent<FlyingBlockComponent>();
			
			block.IsTouchedByPlayer();

			app.Model.GameSceneModel.FlyingBlocksHit += 1;

//			app.Controller.GameSceneController.CharacterJump(collision.contacts[0].normal, Math.Abs(acceleration.y / 9.6f)) ;

//			Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
			
			Rigidbody.velocity = Vector3.Reflect(LastVelocity, collision.contacts[0].normal) * 1.1f;
			
			app.Controller.AudioController.PlayHitMan();
			
//			app.Controller.GameSceneController.DecrementNumOfJumpsAvailable();
			
			
//			if (block.leftJump)
//			{
//				app.Controller.GameSceneController.CharacterJumpLeft();
//			}
//			else
//			{
//				app.Controller.GameSceneController.CharacterJumpRight();
//			}

		} else if (collision.collider.gameObject.tag == "FlyingBlockStatic")
		{
			FlyingBlockComponent block = collision.gameObject.GetComponent<FlyingBlockComponent>();
			
			block.IsTouchedByPlayer();

//			app.Model.GameSceneModel.FlyingBlocksHit += 1;

//			app.Controller.GameSceneController.CharacterJump(collision.contacts[0].normal, Math.Abs(acceleration.y / 9.6f)) ;

//			Vector3.Reflect(lastVelocity, collision.contacts[0].normal);
			
			Rigidbody.velocity = Vector3.Reflect(LastVelocity, collision.contacts[0].normal) * 0.9f;
			
//			app.Controller.AudioController.PlayHitMan();
		}
		else if (collision.collider.gameObject.tag == "MiddleScene")
		{
			app.Controller.GameSceneController.IncrementNumOfThrowsAvailable();
			app.Model.GameSceneModel.HitBasketBoard = false;
			
			Rigidbody.velocity = Vector3.Reflect(LastVelocity, collision.contacts[0].normal) * 0.9f;
		}
		else if (collision.collider.gameObject.tag != "BasketRing")
		{
//			print("Acceleration = " + acceleration);

//			Rigidbody.velocity = LastVelocity * -0.99f;
			Rigidbody.velocity = Vector3.Reflect(LastVelocity, collision.contacts[0].normal) * 0.9f;
//			app.Controller.GameSceneController.CharacterJump(collision.contacts[0].normal, Math.Abs(acceleration.y / 9.6f)) ;

//			app.Model.GameSceneModel.ShouldCountGoal = true;

			if (app.Model.GameSceneModel.HitBasketBoard && app.Model.GameSceneModel.CharacterNumOfThrowsAvailable > 0)
			{
				print("app.Model.GameSceneModel.HitBasketBoard && app.Model.GameSceneModel.CharacterNumOfThrowsAvailable > 0");
				app.Controller.GameSceneController.DecrementNumOfThrowsAvailable();
			}
		}
		else
		{
//			print("Acceleration = " + acceleration);

//			Rigidbody.velocity = LastVelocity * -0.99f;
			Rigidbody.velocity = Vector3.Reflect(LastVelocity, collision.contacts[0].normal) * 0.9f;
//			app.Controller.GameSceneController.CharacterJump(collision.contacts[0].normal, Math.Abs(acceleration.y / 9.6f)) ;

//			app.Model.GameSceneModel.ShouldCountGoal = true;
		}

//		print("Player collision impulse = " + collision.impulse.sqrMagnitude);
//		print("Player collision relativeVelocity = " + collision.relativeVelocity.sqrMagnitude);
//		app.Controller.AudioController.PlayBallHit();
		app.Controller.AudioController.PlayBallHit( collision.relativeVelocity.sqrMagnitude / 26f);
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.collider.gameObject.tag == "FlyingBlock")
		{
			FlyingBlockComponent block = collision.gameObject.GetComponent<FlyingBlockComponent>();
			
			block.IsNotTouchedByPlayer();
		}
	}

	private void FixedUpdate()
	{
//		acceleration = (Rigidbody.velocity - lastVelocity) / Time.fixedDeltaTime;
		LastVelocity = Rigidbody.velocity;
		
		
//		print("Rigidody pos x = " + Rigidbody.position.x);
//		print("app.Model.CameraModel.ScreenCenter.y = " + app.Model.CameraModel.ScreenCenter.y);
		
		
		
//		LastpositionX = Rigidbody.position.x;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "FlyingBlock")
		{


			app.Model.GameSceneModel.CharacterRowModel.IsCharacterTriggeringFlyingBlock = true;
		}

//		if (other.gameObject.tag == "MiddleScene")
//		{
//			app.Controller.GameSceneController.DecrementNumOfThrowsAvailable();	
//		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "FlyingBlock")
		{

			app.Model.GameSceneModel.CharacterRowModel.IsCharacterTriggeringFlyingBlock = false;

		}
		
	}
}
