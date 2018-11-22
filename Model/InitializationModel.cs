using UnityEngine;
using UnityEngine.XR.iOS;

public static class InitializationState
{
	public static int None = 0;
	public static int Initializing = 1;
	public static int Initializied = 2;
	public static int PlaneCreated = 3;
	public static int PlaneStabilized = 4;
	
}

public class InitializationModel : AppElement
{
	[HideInInspector]
	public int State;
	[HideInInspector]
	public ARTrackingState ARTrackingState;
	[HideInInspector]
	public ARTrackingStateReason ARTrackingStateReason;

	[HideInInspector]
	public int PointCloudDataLength;
	[HideInInspector]
	public int PlaneUpdateCalls;

	public float FadeInStep = 0.03f;
	
	public float FadeOutStep = 0.1f;
	
	public int PointCloudDataCeil = 50;
	
	public float PlanesUpdatePeriod = 5.0f;
	
	[HideInInspector]
	public double PlanesPerUpdatePeriod = 0.0;


	public override void LastInAwake() {

		PointCloudDataLength = 0;

		PlaneUpdateCalls = 0;
	
	}


}
