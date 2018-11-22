using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleStarComponent : MovingOnSceneComponent {

	public override void AddForce()
	{
		Rigidbody.AddForce(new Vector3(app.Model.GameSceneModel.LineRendererRowModel.velocity * -50, 0, 0));
	}

	public override bool ShouldBeDisabled()
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			return Rigidbody.position.x <= app.View.GameSceneView.LineRendererRowView.LeftBorderView.transform.position.x;
		}
		else
		{
			return Rigidbody.position.x <= app.View.TechLevelView.KSScreenView.GameSceneCopyView.LineRendererRowView.LeftBorderView.transform.position.x;
		}
		
	}

	public override void Move()
	{
		Rigidbody.velocity = new Vector3(app.Model.GameSceneModel.LineRendererRowModel.velocity * -1, 0, 0);
	}
	
	public override Vector3 GetInstantiationPoint()
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			return app.View.GameSceneView.LineRendererRowView.RightBorderView.transform.position;
		}
		else
		{
			return app.View.TechLevelView.KSScreenView.GameSceneCopyView.LineRendererRowView.RightBorderView.transform.position;
		}
		
		
	}
	
	public override int GetRecordId()
	{
		return 3;
	}

}
