using System.Collections;
using System.Collections.Generic;
using TouchScript.Gestures;
using TouchScript.Pointers;
using UnityEngine;

public class DraggingControllerNative : DraggingController
{

	private bool playModeOn;
	
	// Use this for initialization
	void Start () {
		
		PlayModeOff();

		app.Model.DraggingModel.playTimeAfterPress = 1;

		app.Model.DraggingModel.isDownJumpPressed = false;
		
	}
	
	void Update()
	{
		
		#if !UNITY_EDITOR

		for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Pointer pointer = new Pointer(Input.GetTouch(i).position);
	
				pointer.INTERNAL_UpdatePosition();
				
				MetaGestureEventArgs eventArgs = new MetaGestureEventArgs(pointer);

				if (playModeOn)
				{
					playModeMetaManipulationStartedTransformedHandler(null, eventArgs);
				}
				else
				{
					editModeMetaManipulationStartedTransformedHandler(null, eventArgs);
				}
            } else if (Input.GetTouch(i).phase == TouchPhase.Ended) 
			{
				Pointer pointer = new Pointer(Input.GetTouch(i).position);
	
				pointer.INTERNAL_UpdatePosition();
				
				MetaGestureEventArgs eventArgs = new MetaGestureEventArgs(pointer);

				if (playModeOn)
				{
					playModeManipulationCompletedTransformedHandler(null, eventArgs);

				}
				else
				{
					editModeManipulationCompletedTransformedHandler(null, eventArgs);
				}
			}

        }

		#else
		
		if (Input.GetMouseButtonDown(0))
		{
//			Debug.Log(Input.mousePosition);
//
//			Debug.Log(app.Model.CameraModel.ScreenCenter);
//			
//			print("Input.mousePosition = " + Input.mousePosition);
			
			Pointer pointer = new Pointer(Input.mousePosition);

//			pointer.Position = Input.mousePosition;

			pointer.INTERNAL_UpdatePosition();
			
//			print("pointer = " + pointer.Position);
				
			MetaGestureEventArgs eventArgs = new MetaGestureEventArgs(pointer);
			
//			print("Posiotion = " + eventArgs.Pointer.Position);

			if (playModeOn)
			{
				playModeMetaManipulationStartedTransformedHandler(null, eventArgs);
			}
			else
			{
				editModeMetaManipulationStartedTransformedHandler(null, eventArgs);
			}
			
		} else if (Input.GetMouseButtonUp(0))
		{
//			Debug.Log(Input.mousePosition);
//			
//			Debug.Log(app.Model.CameraModel.ScreenCenter);
			
			Pointer pointer = new Pointer(Input.mousePosition);
				
			MetaGestureEventArgs eventArgs = new MetaGestureEventArgs(pointer);

			if (playModeOn)
			{
				playModeManipulationCompletedTransformedHandler(null, eventArgs);

			}
			else
			{
				editModeManipulationCompletedTransformedHandler(null, eventArgs);
			}
		}
		
		#endif
	}
	
//	void FixedUpdate()
//	{
//		if (app.Model.DraggingModel.isDownJumpPressed && app.Model.DraggingModel.playTimeAfterPress < 2)
//		{
//			app.Model.DraggingModel.playTimeAfterPress += Time.fixedDeltaTime * 3;
//			
////			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity =
////				app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity * 1.2f;
////			
//			app.Controller.GameSceneController.AddForceToCharacter(app.Model.DraggingModel.playTimeAfterPress);
//		}
//		
//	}

	public override void PlayModeOn()
	{
		playModeOn = true;
	}
	
	public override void PlayModeOff()
	{
		playModeOn = false;
	}
	
	
}
