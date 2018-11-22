using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecordController : AppElement {
	
	void Start()
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.Model.RecordModel.characterReplayStartPosition = app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position;
		}
		else
		{
			app.Model.RecordModel.characterReplayStartPosition = app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.position;
		}
		

		app.Model.RecordModel.characterVelocityBeforePause = Vector3.zero;
		app.Model.RecordModel.characterAngularVelocityBeforePause = Vector3.zero;
		
	}
	
	/// ---- BUTTONS ---- ///


	// -- Editor -- //
	
	public void RightJumpButtonWasPressed()
	{
		SetExecuteRecordedActions(false);
		
		StartRightJump();
		
		AddJumpActionToFrameDictionary(0);
	}
	
	public void RightJumpButtonWasReleased()
	{
		PauseAllMovements();
	}
	
	public void LeftJumpButtonWasPressed()
	{	
		SetExecuteRecordedActions(false);
		
		StartLeftJump();
		
		AddJumpActionToFrameDictionary(1);
		
	}
	
	public void LeftJumpButtonWasReleased()
	{
		PauseAllMovements();
	}
	
	public void ContinueButtonWasPressed()
	{
		ContinueAllMovements();		
	}
	
	public void ContinueButtonWasReleased()
	{
		PauseAllMovements();
	}
	
	public void InstantiateFyingBlockButtonWasPressed()
	{
		SetExecuteRecordedActions(false);
		
		InstantiateFyingBlock();

//		InstantiateFyingBlockOnCharacterPath();
	}
	
	public void InstantiateFyingBlockButtonWasReleased()
	{

	}
	
	public void InstantiateFailBlockButtonWasPressed()
	{
		SetExecuteRecordedActions(false);
		
		InstantiateFailBlock();

	}
	
	public void InstantiateFailBlockButtonWasReleased()
	{

	}
	
	public void InstantiateBasketRingButtonWasPressed()
	{
		SetExecuteRecordedActions(false);
		
		InstantiateBasketRing();

	}
	
	public void InstantiateBasketRingButtonWasReleased()
	{

	}
	
	public void ReplayButtonWasPressed()
	{

		ReplayAllMovements();
		
	}
	
	public void ReplayButtonWasReleased()
	{

		PauseAllMovements();
		
	}
	
	public void PatternButtonWasPressed()
	{
		MakePatternFromExistingMovingComponents();
	}
	
	public void PatternButtonWasReleased()
	{

		SetPatterning(false);
		
		PauseAllMovements();
	}
	
	public void ToggleRunningFlyingPlatformButtonWasPressed()
	{	
		ToggleRunningFlyingPlatform();
	}
	
	public void ToggleRunningFlyingPlatformButtonWasReleased()
	{

	}
	
	public void ToggleRunningGroundPlatformButtonWasPressed()
	{	
		ToggleRunningGroundPlatform();
	}
	
	public void ToggleRunningGroundPlatformButtonWasReleased()
	{

	}
	
	public void RecordPhoneButtonWasPressed()
	{
	
		print("RecordPhoneButtonWasPressed");
		
		print("Is phone recorded = " + app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneRecorded);
		
		if (app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneRecorded)
		{
			StopRecordingPhone();
		}
		else
		{
			StartRecordingPhone();
		}

		
		
	}
	
	public void RecordPhoneButtonWasReleased()
	{

		
		
	}
	
	public void ReplayPhoneButtonWasPressed()
	{
		print("ReplayPhoneButtonWasPressed");
		
		if (app.Model.GameSceneModel.VisualEffectsRowModel.isPhoneReplayed)
		{
			StopReplayingPhone();
		}
		else
		{
			StartReplayingPhone();
		}

		
		
	}
	
	public void ReplayPhoneButtonWasReleased()
	{

		
		
	}
	
	// -- Game -- //
	
	public void LaunchGameButtonWasPressed()
	{
		
		LaunchGame();
	}
	
	public void LaunchGameButtonWasReleased()
	{
	}
	
	public void ChangeGameSceneButtonWasPressed()
	{
		
	}
	
	public void ChangeGameSceneButtonWasReleased()
	{

		ToggleGameScene();

	}

    public void SaveTrailButtonWasPressed()
    {
        SaveTrail();
    }

    public void SaveTrailButtonWasReleased()
    {



    }
	
	/// ---- BUTTONS ---- ///
	
	/// ---- RECORD FUNCTIONS --- ///

	private void StartRightJump()
	{
		
		print("StartRightJump");
		
		SetIncrementFrameNumber(true);
		
		SetFlyingBlocksRunning(true);
		
		SetBasketRingRunning(true);

		SetFirstLaunchParams();
		
		SetCharacterGravity(true);
		
		app.Controller.GameSceneController.CharacterJumpDownRight();
	
	}

	
	private void StartLeftJump()
	{
		
		print("StartLeftJump");

		SetIncrementFrameNumber(true);
		
		SetFlyingBlocksRunning(true);
		
		SetBasketRingRunning(true);

		SetFirstLaunchParams();
		
		SetCharacterGravity(true);
		
		app.Controller.GameSceneController.CharacterJumpDownLeft();
		
	}
	
	private void PauseAllMovements()
	{
		
		print("PauseRecord");

		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.Model.RecordModel.characterVelocityBeforePause =
				app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity;
		
			app.Model.RecordModel.characterAngularVelocityBeforePause =
				app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.angularVelocity;

			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
		
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.angularVelocity = Vector3.zero;
		}
		else
		{
			app.Model.RecordModel.characterVelocityBeforePause =
				app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity;
		
			app.Model.RecordModel.characterAngularVelocityBeforePause =
				app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.angularVelocity;

			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
		
			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.angularVelocity = Vector3.zero;
		}
		

		SetCharacterGravity(false);
		
		SetIncrementFrameNumber(false);
		
		SetFlyingBlocksRunning(false);
		
		SetBasketRingRunning(false);
		
	}
	
	private void ResetMovementsToOriginalState()
	{
		
		print("ResetMovementsToOriginalState");
		
		app.Model.RecordModel.characterVelocityBeforePause = Vector3.zero;
		
		app.Model.RecordModel.characterAngularVelocityBeforePause = Vector3.zero;

		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
		
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.angularVelocity = Vector3.zero;
		}
		else
		{
			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
		
			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.angularVelocity = Vector3.zero;
		}
		

		SetCharacterGravity(false);
		
		SetIncrementFrameNumber(false);
		
		SetFlyingBlocksRunning(false);

		SetBasketRingRunning(false);
		
		SetExecuteRecordedActions(false);
		
		SetPatterning(false);

		app.Model.RecordModel.pressedForTheFirstTime = true;
		
		app.Controller.GameSceneController.RemoveEverythingFromScene();
		
		app.Model.RecordModel.recordableMovingComponentsEvents = new Dictionary<int, Tuple<float, Quaternion, int>> ();
		
		app.Model.RecordModel.currentMovingComponentsOnSceneToFrame = new Dictionary<int, int> ();
	}
	
	private void ContinueAllMovements()
	{
		
		print("ContinueAllMovements");
		
		SetIncrementFrameNumber(true);
		
		SetFlyingBlocksRunning(true);

		SetBasketRingRunning(true);
		
		SetFirstLaunchParams();

		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity =
				app.Model.RecordModel.characterVelocityBeforePause;
			
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.angularVelocity = 
				app.Model.RecordModel.characterAngularVelocityBeforePause;
		}
		else
		{
			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity =
				app.Model.RecordModel.characterVelocityBeforePause;
			
			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.angularVelocity = 
				app.Model.RecordModel.characterAngularVelocityBeforePause;
		}
		
			
		SetCharacterGravity(true);
		
	}

	private void InstantiateFyingBlockFromRecord(float height, Quaternion rotation)
	{
		print("InstantiateFyingBlockFromRecord");

		
		app.Controller.GameSceneController.CharacterRowController.InstantiateFlyingBlock(rotation, height);
		
	}
	
	private void InstantiateFyingBlock()
	{
		print("InstantiateFyingBlock in frame = " + app.Model.RecordModel.recordFlyingBlockFramesNumber);

		Transform tr;
		
		tr = app.Controller.GameSceneController.CharacterRowController.InstantiateFlyingBlock();
			
		app.Model.RecordModel.recordableMovingComponentsEvents.Add(app.Model.RecordModel.recordFlyingBlockFramesNumber, new Tuple<float, Quaternion, int>(tr.position.y, tr.rotation, tr.GetComponent<MovingOnSceneComponent>().GetRecordId()));
		
		app.Model.RecordModel.currentMovingComponentsOnSceneToFrame.Add(tr.gameObject.GetInstanceID(), app.Model.RecordModel.recordFlyingBlockFramesNumber);
		
	}
	
	private void InstantiateFailBlockFromRecord(float height, Quaternion rotation)
	{
		print("InstantiateFailBlockFromRecord in frame = " + app.Model.RecordModel.recordFlyingBlockFramesNumber);

		app.Controller.GameSceneController.CharacterRowController.InstantiateFailBlock(rotation, height);
				
	}
	
	private void InstantiateFailBlock()
	{
		print("InstantiateFailBlock in frame = " + app.Model.RecordModel.recordFlyingBlockFramesNumber);

		Transform tr;
		
		tr = app.Controller.GameSceneController.CharacterRowController.InstantiateFailBlock();
			
		app.Model.RecordModel.recordableMovingComponentsEvents.Add(app.Model.RecordModel.recordFlyingBlockFramesNumber, new Tuple<float, Quaternion, int>(tr.position.y, tr.rotation, tr.GetComponent<MovingOnSceneComponent>().GetRecordId()));
		
		app.Model.RecordModel.currentMovingComponentsOnSceneToFrame.Add(tr.gameObject.GetInstanceID(), app.Model.RecordModel.recordFlyingBlockFramesNumber);
		
	}
	
	private void InstantiateBasketRingFromRecord(float height, Quaternion rotation)
	{
		print("InstantiateBasketRingFromRecord in frame = " + app.Model.RecordModel.recordFlyingBlockFramesNumber);

		app.Controller.GameSceneController.CharacterRowController.InstantiateBasketRing(rotation, height);
				
	}
	
	private void InstantiateBasketRing()
	{
		print("InstantiateBasketRing in frame = " + app.Model.RecordModel.recordFlyingBlockFramesNumber);

		Transform tr;
		
		tr = app.Controller.GameSceneController.CharacterRowController.InstantiateBasketRing();
			
		app.Model.RecordModel.recordableMovingComponentsEvents.Add(app.Model.RecordModel.recordFlyingBlockFramesNumber, new Tuple<float, Quaternion, int>(tr.position.y, tr.rotation, tr.GetComponent<MovingOnSceneComponent>().GetRecordId()));
		
		app.Model.RecordModel.currentMovingComponentsOnSceneToFrame.Add(tr.gameObject.GetInstanceID(), app.Model.RecordModel.recordFlyingBlockFramesNumber);
		
	}
	
	private void InstantiateFyingBlockOnCharacterPath()
	{
		print("InstantiateFyingBlockOnCharacterPath in frame = " + app.Model.RecordModel.recordFlyingBlockFramesNumber);

		Transform tr;

		Vector2 futureCharacterHeightAndWidth = CalculateCharacterFuturePosition(4);
			
		tr = app.Controller.GameSceneController.CharacterRowController.InstantiateFlyingBlock(futureCharacterHeightAndWidth.x, futureCharacterHeightAndWidth.y);
			
		app.Model.RecordModel.recordableMovingComponentsEvents.Add(app.Model.RecordModel.recordFlyingBlockFramesNumber, new Tuple<float, Quaternion, int>(tr.position.y, tr.rotation, 0));
		
		app.Model.RecordModel.currentMovingComponentsOnSceneToFrame.Add(tr.gameObject.GetInstanceID(), app.Model.RecordModel.recordFlyingBlockFramesNumber);
		
	}

	private void ReplayAllMovements()
	{
		print("ReplayAllMovements");

		int currentFrameNumber = app.Model.RecordModel.recordFlyingBlockFramesNumber;
		
		ReCalculateInstantiationHeightAndRotationForFlyingBlocks();
		
		ReCalculateInstantiationFrameForFlyingBlocks(currentFrameNumber);

		app.Model.RecordModel.recordFlyingBlockFramesNumber = 0;
		
		SetExecuteRecordedActions(true);

		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position = app.Model.RecordModel
				.characterReplayStartPosition;
		}
		else
		{
			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.position = app.Model.RecordModel
				.characterReplayStartPosition;
		}
		
		
		
		app.Controller.GameSceneController.RemoveEverythingFromScene();
			
		SetIncrementFrameNumber(true);
		
		SetFlyingBlocksRunning(true);

		SetBasketRingRunning(true);

		SetFirstLaunchParams();
	}
	
	private void MakePatternFromExistingMovingComponents()
	{
		print("MakePatternFromExistingMovingComponents");

		app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber = app.Model.RecordModel.recordFlyingBlockFramesNumber;

		ReplayAllMovements();
		
		SetPatterning(true);

		app.Model.RecordModel.firstPatternLoop = true;
	}

	private void ToggleRunningFlyingPlatform()
	{
		if (app.Model.GameSceneModel.CharacterRowModel.IsFlyingBlockRunning)
		{
			SetFlyingBlocksRunning(false);
			
			SetIncrementFrameNumber(false);
		}
		else
		{
			SetFlyingBlocksRunning(true);
			
			SetIncrementFrameNumber(true);
		}
	}
	
	private void ToggleRunningGroundPlatform()
	{
		if (app.Model.GameSceneModel.GroundRowModel.IsGroundBlockRunning)
		{
			SetGroundBlocksRunning(false);
		}
		else
		{
			SetGroundBlocksRunning(true);
		}
	}
	
	private void StartRecordingPhone()
	{
		print("StartRecordingPhone");
		app.Controller.GameSceneController.VisualEffectsRowController.SetPhoneRecodingOn();
		
		
	}
	
	private void StopRecordingPhone()
	{
		print("StopRecordingPhone");
		
		app.Controller.GameSceneController.VisualEffectsRowController.SetPhoneRecordingOff();
		
	}
	
	private void StartReplayingPhone()
	{
		print("StartReplayingPhone");
		app.Controller.GameSceneController.VisualEffectsRowController.SetPhoneReplayingOn();
		
	}
	
	private void StopReplayingPhone()
	{
		print("StopReplayingPhone");
		
		app.Controller.GameSceneController.VisualEffectsRowController.SetPhoneReplayingOff();
		
	}

    private void SaveTrail()
    {
        print("SaveTrail");

        app.Controller.GameSceneController.LineRendererRowController.SaveTrail();
    }

	// -- Game -- //

	private void LaunchGame()
	{
		
		print("LaunchGame");

		app.Model.RecordModel.recordCharacterFramesNumber = 0;
		
		app.Controller.CameraController.SetToOrthographicCameraSettings();

		app.Controller.DraggingController.PlayModeOn();
		
		app.Controller.GameSceneController.SetPlayTime();

		app.Controller.GameSceneController.SetPointerNum();

		app.Controller.GameSceneController.SetInitialMaxScore();

		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.Model.RecordModel.characterReplayStartPosition = app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position;
		}
		else
		{
			app.Model.RecordModel.characterReplayStartPosition = app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.position;
		}
		
		
		SetCharacterGravity(true);
		
		app.Controller.CanvasController.BuildButtonController.PlayModeOn();
		
//		MakePatternFromExistingMovingComponents();
		
//		SetGroundBlocksRunning(true);
		
		SetFlyingBlocksRunning(true);
		
//		ReplayAllMovements();
		
        //app.Controller.GameSceneController.LineRendererRowController.SaveTrail();

		app.Controller.GameSceneController.LineRendererRowController.ReplayTrail();
		
		//app.Controller.GameSceneController.LineRendererRowController.DisableLineRendererView();
	}
	
	public void StopPlayingGame()
	{

		print("StopPlayingGame");
		
		app.Controller.CameraController.SetToDefaultCameraSettingsAndContinueSession();
		
		app.Controller.DraggingController.PlayModeOff();

		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position = app.Model.RecordModel.characterReplayStartPosition;
		}
		else
		{
			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.position = app.Model.RecordModel.characterReplayStartPosition;
		}
		
		SetGroundBlocksRunning(false);
		
		ResetMovementsToOriginalState();
		
		app.Controller.GameSceneController.LineRendererRowController.EnableLineRendererView();
		
		app.Controller.GameSceneController.LineRendererRowController.ClearTrail();

	}

	private void ToggleGameScene()
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.Controller.GameSceneController.SetGameSceneViewToCopyGameSceneView();
		}
		else
		{
			app.Controller.GameSceneController.SetGameSceneViewToOriginalGameSceneView();
		}
	}

	/// ---- RECORD FUNCTIONS --- ///
	 
	/// ---- CHARACTER RECORD --- ///

	public void StartCharacterRecord()
	{
				
		print("StartCharacterRecord");
		
		app.Model.RecordModel.recordableCharacterPositions = new Dictionary<int, Vector3>();
		
		foreach (var pair in app.Model.RecordModel.recordableComponentMap)
		{
			app.Model.RecordModel.recordableCharacterPositions.Add(pair.Key, pair.Value.transform.position);
		}
		
		app.Model.RecordModel.cameraPositions = new Dictionary<int, Vector3>();
		
		app.Model.RecordModel.cameraRotations = new Dictionary<int, Quaternion>();

		app.Model.RecordModel.recordCharacterFramesNumber = 0;
		
		app.Model.RecordModel.CharacterRecordingIsOn = true;	

	}

	public void StopCharacterRecord()
	{
		
		print("StopCharacterRecord");
		
		app.Model.RecordModel.CharacterRecordingIsOn = false;
	}
	
	/// ---- CHARACTER RECORD --- ///

	/// ---- HELPER FUNCTIONS --- ///
 
	
	private void ReCalculateInstantiationHeightAndRotationForFlyingBlocks()
	{
		foreach (var movComp in app.Model.GameSceneModel.MovingOnSceneComponents.Values)
		{
			int frame = app.Model.RecordModel.currentMovingComponentsOnSceneToFrame[movComp.gameObject.GetInstanceID()];

			app.Model.RecordModel.recordableMovingComponentsEvents[frame] = new Tuple<float, Quaternion, int>(movComp.transform.position.y, movComp.Rigidbody.rotation, movComp.GetRecordId());

		}
	}
	
	private void ReCalculateInstantiationFrameForFlyingBlocks(int currentFrameNumber)
	{
		foreach (var movComp in app.Model.GameSceneModel.MovingOnSceneComponents.Values)
		{
			print("CalculateFrameToStartForFlyingBlocks");
			
			float distance = movComp.GetCurrentDistanceFromStart();
			
			int previuoslyLaunchedFrame = app.Model.RecordModel.currentMovingComponentsOnSceneToFrame[movComp.gameObject.GetInstanceID()];
			
			
			int numOfFramesForDistance = (Mathf.RoundToInt(distance / movComp.GetSingleFrameDistance()) + 1);

			if (previuoslyLaunchedFrame != numOfFramesForDistance)
			{
				print("numOfFramesForDistance = " + numOfFramesForDistance);
				print("currentFrameNumber = " + currentFrameNumber);
				if (currentFrameNumber >= numOfFramesForDistance)
				{
					print("currentFrameNumber >= numOfFramesForDistance");

					Tuple<float, Quaternion, int> tuple = app.Model.RecordModel.recordableMovingComponentsEvents[previuoslyLaunchedFrame];
					
					print("previuoslyLaunchedFrame = " + previuoslyLaunchedFrame);
					
					app.Model.RecordModel.recordableMovingComponentsEvents.Remove(previuoslyLaunchedFrame);
					
					app.Model.RecordModel.recordableMovingComponentsEvents.Add(currentFrameNumber - numOfFramesForDistance, tuple);

					app.Model.RecordModel.currentMovingComponentsOnSceneToFrame[movComp.gameObject.GetInstanceID()] =
						currentFrameNumber - numOfFramesForDistance;
					
					print("new frame = " + (currentFrameNumber - numOfFramesForDistance));
				}
				else
				{
					print("currentFrameNumber < numOfFramesForDistance");
					Dictionary<int, Tuple<float, Quaternion, int>> tmpMovingDict = new Dictionary<int, Tuple<float, Quaternion, int>>();
					Dictionary<int, int> tmpCharacterDict = new Dictionary<int, int>();
					Dictionary<int, int> tmpObjectIdToFrameDict = new Dictionary<int, int>();

					int offset = numOfFramesForDistance - currentFrameNumber;

					foreach (var key in app.Model.RecordModel.recordableCharacterEvents.Keys)
					{
						tmpCharacterDict.Add(key + offset, app.Model.RecordModel.recordableCharacterEvents[key]);
					}
					
					app.Model.RecordModel.recordableCharacterEvents = tmpCharacterDict;

					Tuple<float, Quaternion, int> tuple = app.Model.RecordModel.recordableMovingComponentsEvents[previuoslyLaunchedFrame];
					
					tmpMovingDict.Add(0, tuple);
					
					app.Model.RecordModel.recordableMovingComponentsEvents.Remove(previuoslyLaunchedFrame);


					var frameList = app.Model.RecordModel.recordableMovingComponentsEvents.Keys;
				
					
				
					foreach (var frameId in frameList)
					{
						tmpMovingDict.Add(frameId + offset, app.Model.RecordModel.recordableMovingComponentsEvents[frameId]);
						
					}

					
					app.Model.RecordModel.recordableMovingComponentsEvents = tmpMovingDict;
					
					foreach (var goIdKey in app.Model.RecordModel.currentMovingComponentsOnSceneToFrame.Keys)
					{
						tmpObjectIdToFrameDict[goIdKey] = 
							app.Model.RecordModel.currentMovingComponentsOnSceneToFrame[goIdKey] + offset;
					}

					app.Model.RecordModel.currentMovingComponentsOnSceneToFrame = tmpObjectIdToFrameDict;
						
					app.Model.RecordModel.currentMovingComponentsOnSceneToFrame[movComp.gameObject.GetInstanceID()] = 0;
				
					currentFrameNumber += offset;
				}
			}
		}
	}
	
	private void IncrementFrameNumber()
	{

		app.Model.RecordModel.recordFlyingBlockFramesNumber += 1;
		
	}

	private void SetIncrementFrameNumber(bool incrementFrameNumber)
	{
		app.Model.RecordModel.IncrementFrameNumber = incrementFrameNumber;	
	}

	private void SetFlyingBlocksRunning(bool isRunning)
	{
		app.Model.GameSceneModel.CharacterRowModel.IsFlyingBlockRunning = isRunning;
	}
	
	private void SetBasketRingRunning(bool isRunning)
	{
		app.Model.GameSceneModel.CharacterRowModel.IsBasketRingRunning = isRunning;
	}
	
	private void SetGroundBlocksRunning(bool isRunning)
	{
		app.Model.GameSceneModel.GroundRowModel.IsGroundBlockRunning = isRunning;
	}
	
	private void SetPatterning(bool makingPattern)
	{
		app.Model.RecordModel.PatterningIsOn = makingPattern;	
	}
	
	private void SetCharacterGravity(bool useGravity)
	{
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.useGravity = useGravity;
		}
		else
		{
			app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.useGravity = useGravity;
		}
		
	}
	
	private void SetExecuteRecordedActions(bool replaying)
	{
		app.Model.RecordModel.ExecuteRecordedActions = replaying;
	}

	private void SetFirstLaunchParams()
	{
		if (app.Model.RecordModel.pressedForTheFirstTime)
		{
			app.Model.RecordModel.recordFlyingBlockFramesNumber = 0;

			app.Model.RecordModel.pressedForTheFirstTime = false;

			if (app.Model.GameSceneModel.isOriginalGameScene)
			{
				app.Model.RecordModel.characterReplayStartPosition = app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position;
			}
			else
			{
				app.Model.RecordModel.characterReplayStartPosition = app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.position;
			}
			
		}
		
	}

	private void AddJumpActionToFrameDictionary(int rightOrLeftJump)
	{
		if (app.Model.RecordModel.recordableCharacterEvents.ContainsKey(app.Model.RecordModel.recordFlyingBlockFramesNumber))
		{
			app.Model.RecordModel.recordableCharacterEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber] = rightOrLeftJump;
		}
		else
		{
			app.Model.RecordModel.recordableCharacterEvents.Add(app.Model.RecordModel.recordFlyingBlockFramesNumber, rightOrLeftJump);
		}
	}

	private void ExecuteLeftOrRightJumpMethod(int method)
	{
		switch (method)
		{
			case 0:
				StartRightJump();
				break;
			case 1:
				StartLeftJump();
				break;
			case 2:
//				StartDownJump();
				break;
			default:
				break;
		}
	}
	
	private void InstantiateMovingComponentFromRecord(float height, Quaternion rotation, int method)
	{
		print("InstantiateMovingComponentFromRecord = " + method);
		switch (method)
		{
			case 0:
				InstantiateFyingBlockFromRecord(height, rotation);
				break;
			case 1:
				InstantiateFailBlockFromRecord(height, rotation);
				break;
			case 2:
				InstantiateBasketRingFromRecord(height, rotation);
				break;
			default:
				break;
		}
	}

	private Vector3 CalculateCharacterFuturePosition(int frameNumber)
	{
		
		Vector3 predictedPoint = new Vector3(app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position.x, 
			                         app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position.y, app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position.z)
		                         + app.Model.RecordModel.characterVelocityBeforePause * (Time.fixedDeltaTime * frameNumber);
		return new Vector2(predictedPoint.x, predictedPoint.y);
	}

	/// ---- HELPER FUNCTIONS --- ///

	/// ---- MAIN LOOP --- ///
 
	void FixedUpdate()
	{	
		if (app.Model.RecordModel.IncrementFrameNumber)
		{
			if (app.Model.RecordModel.ExecuteRecordedActions && app.Model.RecordModel.PatterningIsOn)
			{
				
				if (app.Model.RecordModel.recordableCharacterEvents.ContainsKey(app.Model.RecordModel.recordFlyingBlockFramesNumber % app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber))
				{
					ExecuteLeftOrRightJumpMethod(app.Model.RecordModel.recordableCharacterEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber % app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber]);
				}
				
				if (app.Model.RecordModel.recordableMovingComponentsEvents.ContainsKey(app.Model.RecordModel.recordFlyingBlockFramesNumber % app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber))
				{
					InstantiateFyingBlockFromRecord(app.Model.RecordModel.recordableMovingComponentsEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber % app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber].first,
						app.Model.RecordModel.recordableMovingComponentsEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber % app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber].second);

					if (!app.Model.RecordModel.firstPatternLoop)
					{
						app.Model.RecordModel.recordableMovingComponentsEvents.Add(app.Model.RecordModel.recordFlyingBlockFramesNumber,
							new Tuple<float, Quaternion, int>(app.Model.RecordModel.recordableMovingComponentsEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber % app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber].first, 
								app.Model.RecordModel.recordableMovingComponentsEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber % app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber].second,
								app.Model.RecordModel.recordableMovingComponentsEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber % app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber].third
								));
					}
				}
				
				if (app.Model.RecordModel.recordFlyingBlockFramesNumber >= app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber)
				{
					print("recordFramesNumber >= app.Model.RecordModel.lastFrameNumber - " + app.Model.RecordModel.recordFlyingBlockFramesNumber + ", " + app.Model.RecordModel.lastRecordedFlyingBlockFrameNumber);

					app.Model.RecordModel.firstPatternLoop = false;
				}
				
			}
			else if (app.Model.RecordModel.ExecuteRecordedActions)
			{
//				print("ExecuteRecordedActions");
				
				if (app.Model.RecordModel.recordableCharacterEvents.ContainsKey(app.Model.RecordModel.recordFlyingBlockFramesNumber))
				{
					ExecuteLeftOrRightJumpMethod(app.Model.RecordModel.recordableCharacterEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber]);
				}
				
				if (app.Model.RecordModel.recordableMovingComponentsEvents.ContainsKey(app.Model.RecordModel.recordFlyingBlockFramesNumber))
				{
					
					
					InstantiateMovingComponentFromRecord(app.Model.RecordModel.recordableMovingComponentsEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber].first,
						app.Model.RecordModel.recordableMovingComponentsEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber].second, 
						app.Model.RecordModel.recordableMovingComponentsEvents[app.Model.RecordModel.recordFlyingBlockFramesNumber].third
						);
				}
				
			}
			
			IncrementFrameNumber();
		}
		
		if (app.Model.GameSceneModel.isCharacterJumping)
		{
			if (app.Model.RecordModel.recordableCharacterPositions.ContainsKey(app.Model.RecordModel.recordCharacterFramesNumber))
			{
//				Rigidbody.velocity = app.Model.RecordModel.recordableComponentPositions[frameNumber];

				if (app.Model.GameSceneModel.isOriginalGameScene)
				{
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.RecordModel.recordableCharacterPositions[app.Model.RecordModel.recordCharacterFramesNumber]);
				}
				else
				{
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.RecordModel.recordableCharacterPositions[app.Model.RecordModel.recordCharacterFramesNumber]);
				}
					
				
				
				
				app.Model.RecordModel.recordCharacterFramesNumber += 1;
			}
			else
			{
				app.Model.GameSceneModel.isCharacterJumping = false;
				
				app.Model.RecordModel.recordCharacterFramesNumber = 0;
			}
		}

	}
	
	/// ---- MAIN LOOP --- ///

}
