using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererRowModel : AppElement {

	public float velocity = 0.3f;
	
	public float instantiationPeriod = 1f;
	
//	public float smoothTime = 0.3f;
	
	public bool directionClockWise = true;
	
	public Transform block;
	
	public Dictionary<int, Tuple<Vector3, bool>> trailPositions;
	
	public bool isTrailRecorded;
	
	public bool isTrailPainting;

	public int TrailRecordingFrameNumber;

}
