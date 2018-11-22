using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsRowController : AppElement {
	
	private Dictionary<int, Tuple<Vector3, Quaternion>> tmpRecordPhonePositionAndRotationDict;

	private void Start()
	{
		app.Model.GameSceneModel.VisualEffectsRowModel.PhonePositionsAndRotation = new Dictionary<int, Dictionary<int, Tuple<Vector3, Quaternion>>>();
		
		tmpRecordPhonePositionAndRotationDict = new Dictionary<int, Tuple<Vector3, Quaternion>>();
		
		app.Model.GameSceneModel.VisualEffectsRowModel.TransformTypesRecorded = new List<int>();

		app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber = 0;
		
		app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingTransformCurrentType = 0;

		app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneRecorded = false;
		
		app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneReplayed = false;
	}
	
	private void Update()
	{
		if (app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneRecorded)
		{
			tmpRecordPhonePositionAndRotationDict[app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber] =
				new Tuple<Vector3, Quaternion>(app.Model.CameraModel.MainCamera.transform.position,
					app.Model.CameraModel.MainCamera.transform.rotation);
				
			app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber += 1;
			
			if (app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber >=
			    app.Model.GameSceneModel.VisualEffectsRowModel.MaxPhoneRecordFrameNumberTime)
			{
				print("PhoneRecordingFrameNumber == MaxPhoneRecordFrameNumberTime");
				SetPhoneRecordingOff();
			}
		}
		
		if (app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneReplayed)
		{

			foreach (var transformType in app.Model.GameSceneModel.VisualEffectsRowModel.TransformTypesRecorded)
			{
				Transform tr = GetTransformByNumber(transformType);

				Dictionary<int, Tuple<Vector3, Quaternion>> transformPositionsAndRotations =
					app.Model.GameSceneModel.VisualEffectsRowModel.PhonePositionsAndRotation[transformType];

				Vector3 cameraPos = transformPositionsAndRotations
					[app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber].first;
				
				tr.position = new Vector3(cameraPos.x * 2.5f, cameraPos.y * 2.5f, cameraPos.z);
			
				tr.rotation = transformPositionsAndRotations
					[app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber].second;
			
//			app.Model.GameSceneModel.VisualEffectsRowModel.PhonePositionsAndRotation
//					[app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber] =
//				new Tuple<Vector3, Quaternion>(app.Model.CameraModel.MainCamera.transform.position,
//					app.Model.CameraModel.MainCamera.transform.rotation);
				
				
			}
			
			app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber += 1;

			if (app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber >=
			    app.Model.GameSceneModel.VisualEffectsRowModel.MaxPhoneRecordFrameNumberTime)
			{
				SetPhoneReplayingOff();
			}
			
//			print("Replay - PhoneRecordingFrameNumber = " + app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber);
			
		}
	}

	public void BoomOn()
	{
		Vector3 pos;
		
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			pos = new Vector3(app.View.GameSceneView.CharacterRowView.CharacterView.transform.position.x, app.View.GameSceneView.CharacterRowView.CharacterView.transform.position.y,
				app.View.GameSceneView.VisualEffectsRowView.LeftBorderView.transform.position.z);
		}
		else
		{
			pos = new Vector3(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.transform.position.x, app.View.GameSceneView.CharacterRowView.CharacterView.transform.position.y,
				app.View.GameSceneView.VisualEffectsRowView.LeftBorderView.transform.position.z);
		}
		
		Instantiate(app.Model.GameSceneModel.VisualEffectsRowModel.boomPrefab, pos, app.Model.GameSceneModel.VisualEffectsRowModel.boomPrefab.rotation);
	}
	
	public void SetPhoneRecodingOn()
	{
		print("SetPhoneRecodingOn()");
		
		tmpRecordPhonePositionAndRotationDict = new Dictionary<int, Tuple<Vector3, Quaternion>>();
		
		app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber = 0;
		
		app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneRecorded = true;
		
		app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneReplayed = false;
		
		print("Is phone recorded = " + app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneRecorded);
	}

	public void SetPhoneRecordingOff()
	{
		print("SetPhoneRecordingOff()");
		
		app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneRecorded = false;
		
		if (app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber - 1 <
		    app.Model.GameSceneModel.VisualEffectsRowModel.MaxPhoneRecordFrameNumberTime)
		{
			Tuple<Vector3, Quaternion> posAndRot =
				tmpRecordPhonePositionAndRotationDict[app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber - 1];
			
			for (int i = app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber;
				i < app.Model.GameSceneModel.VisualEffectsRowModel.MaxPhoneRecordFrameNumberTime;
				i++)
			{
				tmpRecordPhonePositionAndRotationDict[i] = posAndRot;
			}
		}

		app.Model.GameSceneModel.VisualEffectsRowModel.PhonePositionsAndRotation
				[app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingTransformCurrentType] =
			tmpRecordPhonePositionAndRotationDict;
		
		app.Model.GameSceneModel.VisualEffectsRowModel.TransformTypesRecorded.Add(app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingTransformCurrentType);

		app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingTransformCurrentType += 1;
	
	}
	
	public void SetPhoneReplayingOn()
	{
		
		print("SetPhoneReplayingOn()");
		
		app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber = 0;
		
		app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneReplayed = true;
	}
	
	public void SetPhoneReplayingOff()
	{
		
		print("SetPhoneReplayingOff()");
		
		app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneReplayed = false;
	}
	
	public void ReplayRecordedPhone()
	{
		
		SetPhoneReplayingOn();
		
		SetPhoneRecordingOff();

		app.Model.GameSceneModel.VisualEffectsRowModel.PhoneRecordingFrameNumber = 0;
	}

	private Transform GetTransformByNumber(int transformNumber)
	{
		
		
		switch (transformNumber)
		{
			case 0:
				return app.View.CameraSubstituesView.SubstituteCubeView.transform;
			case 1:
				return app.View.CameraSubstituesView.SubstituteCylinderView.transform;
			case 2:
				return app.View.CameraSubstituesView.SubstituteSphereView.transform;
			default:
				return null;
		}
	}
}
