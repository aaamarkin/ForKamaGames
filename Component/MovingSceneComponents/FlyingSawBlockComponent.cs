using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSawBlockComponent : MovingOnSceneComponent {


	public override void AddForce()
	{
		Rigidbody.AddForce(new Vector3(app.Model.GameSceneModel.CharacterRowModel.FlyingBlockVelocity * -50, 0, 0));
	}

	public override bool ShouldBeDisabled()
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			return Rigidbody.position.x <= app.View.GameSceneView.CharacterRowView.LeftBorderView.transform.position.x;
		}
		else
		{
			return Rigidbody.position.x <= app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.LeftBorderView.transform.position.x;
		}
		
	}

	public override void Move()
	{
		Rigidbody.velocity = new Vector3(app.Model.GameSceneModel.CharacterRowModel.FlyingBlockVelocity * -1, 0, 0);
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
		return 1;
	}
}
