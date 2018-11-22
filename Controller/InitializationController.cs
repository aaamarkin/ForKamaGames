using UnityEngine;
using UnityEngine.XR.iOS;

public class InitializationController : AppElement {

	
	private int calls = 0;
	private double dt = 0.0;

	public delegate void InitializationStateChanged(int state);
	public static event InitializationStateChanged InitializationStateEvent;

	// Use this for initialization
	void Start ()
	{
		app.Model.InitializationModel.State = InitializationState.None;
		if (InitializationStateEvent != null)
		{
			InitializationStateEvent(InitializationState.None);
		}
		app.Model.InitializationModel.PlaneUpdateCalls = 0;

		if (app.Model.CameraModel.useARCamera)
		{
			UnityARSessionNativeInterface.ARSessionTrackingChangedEvent += ARSessionTrackingChanged;
			UnityARSessionNativeInterface.ARAnchorAddedEvent += AddAnchor;
			UnityARSessionNativeInterface.ARAnchorUpdatedEvent += UpdateAnchor;
			UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
		}
		
	}
	
	private void ARSessionTrackingChanged(UnityARCamera arCamera)
	{

		app.Model.InitializationModel.ARTrackingState = arCamera.trackingState;
		app.Model.InitializationModel.ARTrackingStateReason = arCamera.trackingReason;

		if (arCamera.trackingState == ARTrackingState.ARTrackingStateNotAvailable &&
		    arCamera.trackingReason == ARTrackingStateReason.ARTrackingStateReasonInitializing)
		{
			app.Model.InitializationModel.State = InitializationState.Initializing;
//			if (InitializationStateEvent != null)
//			{
//				InitializationStateEvent(InitializationState.Initializing);
//			}
		}

		if (arCamera.trackingState == ARTrackingState.ARTrackingStateLimited)
		{
			app.Model.InitializationModel.State = InitializationState.Initializing;
//			if (InitializationStateEvent != null)
//			{
//				InitializationStateEvent(InitializationState.Initializing);
//			}
		}

		if (arCamera.trackingState == ARTrackingState.ARTrackingStateNormal)
		{
		
			app.Model.InitializationModel.State = InitializationState.Initializied;
//			if (InitializationStateEvent != null)
//			{
//				InitializationStateEvent(InitializationState.Initializied);
//			}
		}


	}
	
	public void AddAnchor(ARPlaneAnchor arPlaneAnchor)
	{

		app.Model.InitializationModel.State = InitializationState.PlaneCreated;
//		if (InitializationStateEvent != null)
//		{
//			InitializationStateEvent(InitializationState.PlaneCreated);
//		}
	}
	
	public void UpdateAnchor(ARPlaneAnchor arPlaneAnchor)
	{
		app.Model.InitializationModel.PlaneUpdateCalls += 1;
		calls++;
		
//		if (InitializationStateEvent == null && app.Model.InitializationModel.PlaneUpdateCalls > 200)
//		{
//			InitializationStateEvent(InitializationState.PlaneStabilized);
//		}
	}
	
	public void ARFrameUpdated(UnityARCamera camera)
	{
		if (camera.pointCloudData != null)
		{
			app.Model.InitializationModel.PointCloudDataLength = camera.pointCloudData.Length;
		}
	}
	

	void Update()
	{
		if (app.Model.generatePlanes)
		{
			dt += Time.deltaTime;
			if (dt > 1.0 / app.Model.InitializationModel.PlanesUpdatePeriod)
			{
				app.Model.InitializationModel.PlanesPerUpdatePeriod = calls / dt ;
				calls = 0;
				dt -= 1.0 / app.Model.InitializationModel.PlanesUpdatePeriod;
			}
		}	
	}	
}
