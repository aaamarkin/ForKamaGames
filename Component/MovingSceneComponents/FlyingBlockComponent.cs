using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBlockComponent : MovingOnSceneComponent
{

	private bool isGoingUp;
	
	public override void AddForce()
	{
		Rigidbody.AddForce(new Vector3(app.Model.GameSceneModel.CharacterRowModel.FlyingBlockVelocity * -50, 0, 0));
	}

	public override bool ShouldBeDisabled()
	{
//		if (app.Model.GameSceneModel.isOriginalGameScene)
//		{
//			return Rigidbody.position.x <= app.View.GameSceneView.CharacterRowView.LeftBorderView.transform.position.x;
//		}
//		else
//		{
//			return Rigidbody.position.x <= app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.LeftBorderView.transform.position.x;
//		}
		return false;
	}

	void OnEnable()
	{
		if (app.Model)
		{
			if (app.Model.GameSceneModel.useForceForObjects)
			{
				AddForce();
			}

			previousXPosition = Rigidbody.position.x;
//            app.Model.GameSceneModel.MovingOnSceneComponents.Add(gameObject.GetInstanceID(), this);

			isGoingUp = false;
		} 
	}
	
	public override void Move()
	{
		Vector3 velocity;

//		if (app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position.y - Rigidbody.position.y > 0)
//		{
//			velocity = new Vector3(0, app.Model.GameSceneModel.CharacterRowModel.FlyingBlockVelocity, 0);
//		}
//		else
//		{
//			velocity = new Vector3(0, app.Model.GameSceneModel.CharacterRowModel.FlyingBlockVelocity * -1, 0);	
//		}
		
		if (app.View.GameSceneView.CharacterRowView.LeftBorderView.transform.position.y >= Rigidbody.position.y && !isGoingUp)
		{
			isGoingUp = true;
		}
		else if (app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position.y <= Rigidbody.position.y && isGoingUp) 
		{
			isGoingUp = false;
		}
		
		if (isGoingUp)
		{
//			print("LeftBorderView >= Rigidbody.position.y ");
			velocity = new Vector3(0, app.Model.GameSceneModel.CharacterRowModel.FlyingBlockVelocity , 0);	
		}
		else
		{
//			print("RightBorderView <= Rigidbody.position.y ");
			velocity = new Vector3(0, app.Model.GameSceneModel.CharacterRowModel.FlyingBlockVelocity * -1, 0);
		}
		
		Rigidbody.velocity = velocity;
	}
	
	
	public override bool ShouldBeMoved()
	{
		return !app.Model.GameSceneModel.useForceForObjects && (app.Model.GameSceneModel.runAllMovingComponents || app.Model.GameSceneModel.CharacterRowModel.IsFlyingBlockRunning);
	}

	public override Vector3 GetInstantiationPoint()
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			return app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position;
		}
		else
		{
			return app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position;
		}	
	}

	public override int GetRecordId()
	{
		return 0;
	}

	// Unique methods

	public virtual void IsTouchedByPlayer()
	{
		ChangeColorToPlayerHit();	
	}
	
	public virtual void IsNotTouchedByPlayer()
	{
		ChangeColorToDefault();	
	}

	private void ChangeColorToPlayerHit()
	{
		Material[] mat = Renderer.materials;
		mat[0].color = Color.white;
		Renderer.materials = mat;
	}
	
	private void ChangeColorToDefault()
	{
		Material[] mat = Renderer.materials;
		mat[0].color = DefaultColor;
		Renderer.materials = mat;
	}

	
//	void FixedUpdate()
//	{
//
//		if (ShouldBeDisabled())
//		{
////            RemoveFromModelAndDestroy();
//			MoveToStartPoint();
//		}
//		else if (ShouldBeMoved())
//		{
//			Move();
//
////			singleFrameDistance = Mathf.Abs(Rigidbody.position.x - previousXPosition);
////
////			previousXPosition = Rigidbody.position.x;
//		} 
//		else if (ShouldBeStopped())
//		{
//			Rigidbody.velocity = Vector3.zero;
//		}
//
//	}
}
