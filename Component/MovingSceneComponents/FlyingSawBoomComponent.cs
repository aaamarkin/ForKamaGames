using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSawBoomComponent : AppElement {

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
//			print("Saw collision");
		
			app.Controller.GameSceneController.VisualEffectsRowController.BoomOn();

			app.Model.GameSceneModel.runAllMovingComponents = false;

//		app.Controller.RecordController.StopPlaying();
		}
	}
}
