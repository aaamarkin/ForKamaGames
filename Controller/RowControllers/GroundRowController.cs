using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GroundRowController : AppElement
{
	private float time1;
	
	private float time2;
	
	private float zScale;

	private Transform lastBlock;

	void Start()
	{
		zScale = app.Model.GameSceneModel.GroundRowModel.GroundBlock.localScale.z / 5;

		time1 = 0;
		time2 = app.Model.GameSceneModel.GroundRowModel.GroundBlockInstantiationPeriod / 2;
		
		app.Model.GameSceneModel.GroundRowModel.GroundBlockDictionaryDisabled = new Dictionary<int, Transform>();
		
		app.Model.GameSceneModel.GroundRowModel.GroundBlockDictionaryEnabled = new Dictionary<int, Transform>();
		
		for (int i = 0; i < 40; i++)
		{
			Transform tr = Instantiate(app.Model.GameSceneModel.GroundRowModel.GroundBlock, app.View.GameSceneView.GroundRowView.RightBorderView.transform.position, Quaternion.identity);
			
			app.Model.GameSceneModel.GroundRowModel.GroundBlockDictionaryDisabled.Add(tr.gameObject.GetInstanceID(), tr);
			
			tr.gameObject.SetActive(false);
		
		}
	}

	void FixedUpdate()
	{
		if (app.Model.GameSceneModel.runAllMovingComponents || app.Model.GameSceneModel.GroundRowModel.IsGroundBlockRunning)
		{
			time1 += Time.fixedDeltaTime;
			
			time2 += Time.fixedDeltaTime;

			if (time2 >= app.Model.GameSceneModel.GroundRowModel.GroundBlockInstantiationPeriod)
			{
				Vector3 pos;
				
				if (app.Model.GameSceneModel.isOriginalGameScene)
				{
					pos = new Vector3(app.View.GameSceneView.GroundRowView.RightBorderView.transform.position.x,
						app.View.GameSceneView.GroundRowView.RightBorderView.transform.position.y,
						app.View.GameSceneView.GroundRowView.RightBorderView.transform.position.z - zScale);
				}
				else
				{
					pos = new Vector3(app.View.TechLevelView.KSScreenView.GameSceneCopyView.GroundRowView.RightBorderView.transform.position.x,
						app.View.TechLevelView.KSScreenView.GameSceneCopyView.GroundRowView.RightBorderView.transform.position.y,
						app.View.TechLevelView.KSScreenView.GameSceneCopyView.GroundRowView.RightBorderView.transform.position.z - zScale);
				}
				
				EnableGroundBlock(pos, Quaternion.identity);

				time2 = 0;
			}

			if (time1 >= app.Model.GameSceneModel.GroundRowModel.GroundBlockInstantiationPeriod)
			{
				if (app.Model.GameSceneModel.isOriginalGameScene)
				{
					EnableGroundBlock(app.View.GameSceneView.GroundRowView.RightBorderView.transform.position, Quaternion.identity);
				}
				else
				{
					EnableGroundBlock(app.View.TechLevelView.KSScreenView.GameSceneCopyView.GroundRowView.RightBorderView.transform.position, Quaternion.identity);
				}
				time1 = 0;
			}
		}
	}
	
	private Transform EnableGroundBlock(Vector3 position, Quaternion rotation)
	{
		Transform tr = app.Model.GameSceneModel.GroundRowModel.GroundBlockDictionaryDisabled.First().Value;
		
		app.Model.GameSceneModel.GroundRowModel.GroundBlockDictionaryEnabled.Add(tr.gameObject.GetInstanceID(), tr);

		app.Model.GameSceneModel.GroundRowModel.GroundBlockDictionaryDisabled.Remove(tr.gameObject.GetInstanceID());

		tr.position = position;

		tr.rotation = rotation;
		
		tr.gameObject.SetActive(true);
		
//		return Instantiate(app.Model.GameSceneModel.GroundRowModel.FailBlock, position, rotation);
		
		return tr;
	}
}
