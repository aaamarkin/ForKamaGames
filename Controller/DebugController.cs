using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugController : AppElement {

		


	void OnGUI()
	{
		
		GUI.Label(new Rect(300, 100, 1000, 1000),
			"PlanesNumber = " + app.Model.GeneratePlaneModel.PlanesNumber, guiStyle); 

		GUI.Label(new Rect(300, 150, 1000, 1000),
			app.Model.InitializationModel.ARTrackingStateReason.ToString(), guiStyle);
		
		GUI.Label(new Rect(100, 400, 1000, 1000),
			app.Model.InitializationModel.ARTrackingState.ToString(), guiStyle);
		
		GUI.Label(new Rect(100, 450, 1000, 1000),
			"State = " + app.Model.InitializationModel.State.ToString(), guiStyle);
		
		GUI.Label(new Rect(100, 500, 1000, 1000),
			"Camera position = " + app.Model.CameraModel.MainCamera.transform.position.ToString(), guiStyle);
		
		GUI.Label(new Rect(100, 550, 1000, 1000),
			"Rotational Velocity = " +  app.Model.CameraModel.cameraCurrentRotationVelocity.ToString(), guiStyle);
		
		GUI.Label(new Rect(100, 600, 1000, 1000),
			"Linear Velocity = " + app.Model.CameraModel.cameraCurrentLinearVelocity.ToString(), guiStyle);
		
	}
	
}
