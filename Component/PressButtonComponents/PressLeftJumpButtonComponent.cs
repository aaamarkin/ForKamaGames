using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressLeftJumpButtonComponent : PressButtonComponent {

	public override void PressButton()
	{
		transform.SetPositionAndRotation(new Vector3(defaulPos.x, defaulPos.y, defaulPos.z + 0.003f), transform.rotation);
		isPressed = true;
				
		app.Controller.RecordController.LeftJumpButtonWasPressed();
	}

	public override void ReleaseButton()
	{
		transform.SetPositionAndRotation(new Vector3(defaulPos.x, defaulPos.y, defaulPos.z), transform.rotation);
		isPressed = false;
		app.Controller.RecordController.LeftJumpButtonWasReleased();
	}
}
