using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecordModel : AppElement
{
	[HideInInspector]
	public bool pressedForTheFirstTime;
	[HideInInspector]
	public bool firstPatternLoop;
	[HideInInspector]
	public int recordFlyingBlockFramesNumber;
	[HideInInspector]
	public int recordCharacterFramesNumber;
	[HideInInspector]
	public int lastRecordedFlyingBlockFrameNumber;
	[HideInInspector]
	public bool IncrementFrameNumber;
	[HideInInspector]
	public bool CharacterRecordingIsOn;
	[HideInInspector]
	public bool ExecuteRecordedActions;
	[HideInInspector]
	public bool PatterningIsOn;

		
	[HideInInspector]
	public Vector3 characterVelocityBeforePause;
	
	[HideInInspector]
	public Vector3 characterAngularVelocityBeforePause;
	
	[HideInInspector]
	public Dictionary<int, GameObject> recordableComponentMap;
	[HideInInspector]
	public Dictionary<int, Vector3> recordableCharacterPositions;
	[HideInInspector]
	public Dictionary<int, int> recordableCharacterEvents;
	[HideInInspector]
	public Dictionary<int, Tuple<float, Quaternion, int>> recordableMovingComponentsEvents;
	[HideInInspector]
	public Dictionary<int, int> currentMovingComponentsOnSceneToFrame;
	[HideInInspector]
	public Dictionary<int, Vector3> cameraPositions;
	[HideInInspector]
	public Dictionary<int, Quaternion> cameraRotations;
	[HideInInspector]
	public Vector3 characterReplayStartPosition;

	public override void LastInAwake()
	{
		recordableComponentMap = new Dictionary<int, GameObject>();
        
		recordableCharacterPositions = new Dictionary<int, Vector3>();
		
		recordableCharacterEvents = new Dictionary<int, int>();
		
		recordableMovingComponentsEvents = new Dictionary<int, Tuple<float, Quaternion, int>> ();
		
		currentMovingComponentsOnSceneToFrame = new Dictionary<int, int> ();
		
		cameraPositions = new Dictionary<int, Vector3>();
		
		cameraRotations = new Dictionary<int, Quaternion>();

		recordCharacterFramesNumber = 0;

		recordFlyingBlockFramesNumber = 0;

		lastRecordedFlyingBlockFrameNumber = 0;

		pressedForTheFirstTime = true;
			
		IncrementFrameNumber = false;

		ExecuteRecordedActions = false;

		PatterningIsOn = false;
		
		CharacterRecordingIsOn = false;

	}

}
