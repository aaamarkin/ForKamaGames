using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsRowModel : AppElement {

	public float boomRotationVelocity = 0.3f;
	
	public float boomStayTime = 2f;
	
	public bool directionClockWise = true;
	
	public Transform boomPrefab;
	
	public Dictionary<int, Dictionary<int, Tuple<Vector3, Quaternion>>> PhonePositionsAndRotation;

	public List<int> TransformTypesRecorded;
	
	public bool isPhoneRecorded;
	
	public bool isPhoneReplayed;

	public int PhoneRecordingFrameNumber;
	
	public int PhoneRecordingTransformCurrentType;
	
	public int MaxPhoneRecordFrameNumberTime;
	
}