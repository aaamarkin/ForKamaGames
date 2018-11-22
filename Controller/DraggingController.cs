using System.Collections;
using TouchScript;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Gestures;
using TouchScript.Gestures.TransformGestures.Base;
using TouchScript.Scripts.Gestures;
using UnityEngine;

public class DraggingController : AppElement
{
	private MetaGesture MetaGesture;
	
//	private ScreenTransformGesture ManipulationGesture;
	
//	private PressGesture PressGesture;
//	private ReleaseGesture ReleaseGesture;
	// Currently is used as a Non AR dragging helper
//	private LongPressGesture LongPressGesture;
//	private LongPressMetaGesture LongPressMetaGesture;

//	public float RotationCoefAroundVerticalAxe = 10f;
//	public float RotationCoefAroundForwardAxe = 30f;
	
	void Start()
	{
		MetaGesture = GetComponent<MetaGesture>();

//		MetaGesture.enabled = false;
		
//		PressGesture = GetComponent<PressGesture>();
//		ReleaseGesture = GetComponent<ReleaseGesture>();
//		ManipulationGesture = GetComponent<ScreenTransformGesture>();
//		LongPressGesture = GetComponent<LongPressGesture>();
//		LongPressMetaGesture = GetComponent<LongPressMetaGesture>();
			

//		if (app.Model.DraggingModel.useNonARLongGesture && !app.Model.DraggingModel.forceARGestures)
//		{
//			ManipulationGesture.Transformed += manipulationTransformedHandler;
		
//			PressGesture.Pressed += manipulationCompletedTransformedHandler;
//
//			LongPressGesture.LongPressed += manipulationStartedTransformedHandler;
//		}
//		else
//		{
//			ManipulationGesture.Transformed += manipulationTransformedHandler;
		
//			PressGesture.Pressed += manipulationStartedTransformedHandler;
//
//			ReleaseGesture.Released += manipulationCompletedTransformedHandler;
//		}
		
		PlayModeOff();

		app.Model.DraggingModel.playTimeAfterPress = 1;

		app.Model.DraggingModel.isDownJumpPressed = false;
	}

	public virtual void PlayModeOn()
	{
		if (app.Model.DraggingModel.useNonARLongGesture && !app.Model.DraggingModel.forceARGestures)
		{
//			PressGesture.Pressed -= manipulationCompletedTransformedHandler;

//			LongPressMetaGesture.LongPressedMeta -= editModeMetaManipulationStartedTransformedHandler;
//			
//			LongPressMetaGesture.LongPressedMeta += playModeMetaManipulationStartedTransformedHandler;
		}
		else
		{
			MetaGesture.PointerPressed -= editModeMetaManipulationStartedTransformedHandler;
		
			MetaGesture.PointerReleased -= editModeManipulationCompletedTransformedHandler;
		
			MetaGesture.PointerPressed += playModeMetaManipulationStartedTransformedHandler;
		
			MetaGesture.PointerReleased += playModeManipulationCompletedTransformedHandler;
		}
		
		
//		PressGesture.Pressed += playModeManipulationStartedTransformedHandler;

//		MetaGesture.enabled = true;

		
	}

	public virtual void PlayModeOff()
	{
		if (app.Model.DraggingModel.useNonARLongGesture && !app.Model.DraggingModel.forceARGestures)
		{
			MetaGesture.enabled = false;
			
//			PressGesture.Pressed -= playModeManipulationCompletedTransformedHandler;
//			
//			PressGesture.Pressed += editModeManipulationCompletedTransformedHandler;

//			LongPressMetaGesture.LongPressedMeta -= playModeMetaManipulationStartedTransformedHandler;
//			
//			LongPressMetaGesture.LongPressedMeta += editModeMetaManipulationStartedTransformedHandler;
		}
		else
		{
//			PressGesture.Pressed += manipulationStartedTransformedHandler;
//
//			ReleaseGesture.Released += manipulationCompletedTransformedHandler;

//			PressGesture.enabled = false;
//
//			ReleaseGesture.enabled = false;

//			LongPressMetaGesture.enabled = false;
			
			MetaGesture.PointerPressed -= playModeMetaManipulationStartedTransformedHandler;
		
			MetaGesture.PointerReleased -= playModeManipulationCompletedTransformedHandler;
		
			MetaGesture.PointerPressed += editModeMetaManipulationStartedTransformedHandler;
		
			MetaGesture.PointerReleased += editModeManipulationCompletedTransformedHandler;
		}
		
		
//		PressGesture.Pressed -= playModeManipulationStartedTransformedHandler;

	}
	
	public void manipulationStartedTransformedHandler(object sender, System.EventArgs e)
	{
		if (app.Model.BuildingModel.BuildModeOn)
		{
			if (app.Model.AimModel.AimSelected)
			{
				app.Controller.AimController.SelectAimedObject();
				app.Model.DraggingModel.IsDraggingSmth = true;
			}
		}
	}
	
	public void playModeMetaManipulationStartedTransformedHandler(object sender, MetaGestureEventArgs e)
    {

	    app.Model.DraggingModel.playTimeAfterPress = 1;
	    
	    
	    
		if (e.Pointer.Position.x < app.Model.CameraModel.ScreenCenter.x)
		{
			    
			if (e.Pointer.Position.y < app.Model.CameraModel.ScreenCenter.y)
			{
				app.Model.DraggingModel.isDownJumpPressed = true;
				
				app.Controller.GameSceneController.CharacterJumpDownRight();
				
			}
			else
			{
				app.Model.DraggingModel.isDownJumpPressed = true;
				
				app.Controller.GameSceneController.CharacterJumpDownLeft();
			}
		}
		else
		{
			if (e.Pointer.Position.y < app.Model.CameraModel.ScreenCenter.y)
			{
				app.Model.DraggingModel.isDownJumpPressed = false;
				
//				app.Controller.TimeController.SlowTime();
//				app.Controller.AudioController.DecreaseCurrentPLayPitch();
				
				app.Controller.GameSceneController.CharacterJumpUpRight();
				    
			}
			else
			{
				app.Model.DraggingModel.isDownJumpPressed = true;
				
				app.Controller.GameSceneController.CharacterJumpDownLeft();
			}
		}		    
    }
	
	public void playModeManipulationCompletedTransformedHandler(object sender, MetaGestureEventArgs e)
	{
		app.Model.DraggingModel.isDownJumpPressed = false;
		
		if (e.Pointer.Position.x >= app.Model.CameraModel.ScreenCenter.x)
		{
			if (e.Pointer.Position.y < app.Model.CameraModel.ScreenCenter.y)
			{
//				app.Controller.TimeController.DoOriginalTime();
//				app.Controller.AudioController.IncreaseCurrentPLayPitch();
				
//				app.Controller.GameSceneController.CharacterJumpUpRight();
			}
			
		}
		else
		{
			
		}
	}
	
	public void editModeMetaManipulationStartedTransformedHandler(object sender, MetaGestureEventArgs e)
	{

//		if (e.Pointer.Position.y < app.Model.CameraModel.ScreenCenter.y)
//		{
//			app.Controller.GameSceneController.CharacterJumpRight();
//		}
//		else
//		{
//			app.Controller.GameSceneController.CharacterJumpLeft();
//		}
		
		if (app.Model.BuildingModel.BuildModeOn)
		{
			
			print("e.pointer.position = " + e.Pointer.Position);
			
			app.Controller.AimController.TryRaycastHit(new Vector3(e.Pointer.Position.x, e.Pointer.Position.y, 0));
			
			
			
			if (app.Model.AimModel.AimSelected)
			{
				print("AimSelected");
				
				app.Controller.AimController.SelectAimedObject();
				app.Model.DraggingModel.IsDraggingSmth = true;
			}
		}
		
	}
	
	public void editModeManipulationCompletedTransformedHandler(object sender, MetaGestureEventArgs e)
	{
		if (app.Model.BuildingModel.BuildModeOn)
		{
			app.Controller.AimController.DeselectAimedObject();
			app.Model.DraggingModel.IsDraggingSmth = false;
		}
	}

	void FixedUpdate()
	{
		if (app.Model.DraggingModel.isDownJumpPressed && app.Model.DraggingModel.playTimeAfterPress < 2)
		{
			app.Model.DraggingModel.playTimeAfterPress += Time.fixedDeltaTime * 3;
			
//			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity =
//				app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity * 1.2f;
//			
			app.Controller.GameSceneController.AddForceToCharacter(app.Model.DraggingModel.playTimeAfterPress);
		}
		
	}
	
	

//	public void manipulationTransformedHandler(object sender, System.EventArgs e)
//	{
//		if (app.Model.BuildingModel.BuildModeOn)
//		{
//			if (app.Model.AimModel.IsSelectableComponentSelected())
//			{
//				Vector3 rightCameraVector = app.Model.CameraModel.MainCamera.transform.right;
//				Vector3 downCameraVector = app.Model.CameraModel.MainCamera.transform.up * -1;
//				
//				
//				
//				Vector3 forwardSourceVector = app.Model.AimModel.AimHitCollider.transform.parent.forward;
//
//				Vector3 downVector = Vector3.down;
//			
//				Vector3 forwardProjectedSourceVector = Vector3.ProjectOnPlane(forwardSourceVector, downVector);
//				
//				
//
//				var newRotation = Quaternion.AngleAxis(ManipulationGesture.DeltaPosition.y, forwardProjectedSourceVector) * Quaternion.AngleAxis(ManipulationGesture.DeltaPosition.x, downVector);
//
//				
//				var lookRotation = Quaternion.FromToRotation(forwardSourceVector, forwardProjectedSourceVector);
//			
//			
//				lookRotation *= newRotation;
//
//				
//				app.Model.AimModel.SetRotQuaternsToDampedSpringMotionCopier(lookRotation);
//			}
//		}
//		
//	}
}
