using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlockComponent : MovingOnSceneComponent {
	
	public override void AddForce()
	{
		Rigidbody.AddForce(new Vector3(app.Model.GameSceneModel.GroundRowModel.GroundBlockVelocity * -50, 0, 0));
	}

	public override bool ShouldBeDisabled()
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			return Rigidbody.position.x <= app.View.GameSceneView.GroundRowView.LeftBorderView.transform.position.x;
		}
		else
		{
			return Rigidbody.position.x <= app.View.TechLevelView.KSScreenView.GameSceneCopyView.GroundRowView.LeftBorderView.transform.position.x;
		}	
		
	}

	public override void Move()
	{
		Rigidbody.velocity = new Vector3(app.Model.GameSceneModel.GroundRowModel.GroundBlockVelocity * -1, 0, 0);
	}
	
	public override bool ShouldBeMoved()
	{
		return !app.Model.GameSceneModel.useForceForObjects && (app.Model.GameSceneModel.runAllMovingComponents || app.Model.GameSceneModel.GroundRowModel.IsGroundBlockRunning);
	}
	
	public override Vector3 GetInstantiationPoint()
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			return app.View.GameSceneView.GroundRowView.RightBorderView.transform.position;
		}
		else
		{
			return app.View.TechLevelView.KSScreenView.GameSceneCopyView.GroundRowView.RightBorderView.transform.position;
		}	
		
	}
	
	public override void RemoveFromModelAndDestroy()
	{
//        app.Model.GameSceneModel.MovingOnSceneComponents.Remove(gameObject.GetInstanceID());
        
		app.Model.GameSceneModel.GroundRowModel.GroundBlockDictionaryDisabled.Add(transform.gameObject.GetInstanceID(), transform);

		app.Model.GameSceneModel.GroundRowModel.GroundBlockDictionaryEnabled.Remove(transform.gameObject.GetInstanceID());
        
		gameObject.SetActive(false);
            
//        Destroy(gameObject);
	}
	
	public override int GetRecordId()
	{
		return 4;
	}
	
	void FixedUpdate()
	{

		if (ShouldBeDisabled())
		{
            RemoveFromModelAndDestroy();
//			MoveToStartPoint();
		}
		else if (ShouldBeMoved())
		{
			Move();
		} 
		else if (ShouldBeStopped())
		{
			Rigidbody.velocity = Vector3.zero;
		}

	}
}
