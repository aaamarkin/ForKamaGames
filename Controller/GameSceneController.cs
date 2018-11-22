//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameSceneController : AppElement {

	private GroundRowController _groundRowController;
	public GroundRowController GroundRowController {get { return _groundRowController; }}
	private CharacterRowController _characterRowController;
	public CharacterRowController CharacterRowController {get { return _characterRowController; }}
	private LineRendererRowController _lineRendererRowController;
	public LineRendererRowController LineRendererRowController {get { return _lineRendererRowController; }}
	private VisualEffectsRowController _visualEffectsRowController;
	public VisualEffectsRowController VisualEffectsRowController {get { return _visualEffectsRowController; }}


	private float timeCounter;

	private bool wasSaved;
	
	override public void LastInAwake() {

		_groundRowController = GetComponentInChildren<GroundRowController>();
		_characterRowController = GetComponentInChildren<CharacterRowController>();
		_lineRendererRowController = GetComponentInChildren<LineRendererRowController>();
		_visualEffectsRowController = GetComponentInChildren<VisualEffectsRowController>();

	}
	
	
	
		
	void Start()
	{
		app.Model.GameSceneModel.StarsCollected = 0;
		app.Model.GameSceneModel.FlyingBlocksHit = 0;
//		app.Model.GameSceneModel.CharacterNumOfJumpsAvailable = 300;
		app.Model.GameSceneModel.rotatableComponentVelocity = 0;
		app.Model.GameSceneModel.manipulatorDelta = Vector3.zero;
		app.Model.GameSceneModel.isCharacterJumping = false;
		app.Model.GameSceneModel.runAllMovingComponents = false;
		app.Model.GameSceneModel.ShouldCountGoal = false;
		app.Model.GameSceneModel.HitBasketBoard = false;

		wasSaved = false;
		
		if (app.Model.GameSceneModel.copyGameScene)
		{
			CopyGameScene();
		}
		

		SetGameSceneViewToOriginalGameSceneView();

//		SetGameSceneViewToCopyGameSceneView();

		timeCounter = 0;
	}

	public void SetPlayTime()
	{
		app.Model.GameSceneModel.TimePlayRemained = app.Model.GameSceneModel.TimePlayTotal;
	}

	public void SetPointerNum()
	{
		app.Model.GameSceneModel.CharacterPointNumber = 0;
		
		app.View.GameSceneView.LineRendererRowView.NumOfCharacterPointsView.Text.text = app.Model.GameSceneModel.CharacterPointNumber.ToString();
	}

	public void SetInitialMaxScore()
	{
		app.Model.GameSceneModel.TimePlayRemained = app.Model.GameSceneModel.TimePlayTotal;
		
		wasSaved = false;
		
		LoadMaxScore();
	}

	public void LoadMaxScore()
	{
		
		app.Controller.SaveController.Load();
//		print("LoadMaxScore() = " + l);
//		return l;
	}

	public void SaveMaxScore()
	{
		app.Controller.SaveController.Save(app.Model.GameSceneModel.MaxScore);
	}

	public void SetMaxScore(int maxScore)
	{
		app.Model.GameSceneModel.MaxScore = maxScore;
		app.View.GameSceneView.LineRendererRowView.MaxScoreView.Text.text = app.Model.GameSceneModel.MaxScore.ToString();
		
		app.View.GameSceneView.LineRendererRowView.MaxScoreView.Text.color = Color.green;
	}

//	public void CharacterJump(Vector3 normal, float coef)
//	{
//		if (app.Model.GameSceneModel.CharacterNumOfJumpsAvailable > 0)
//		{
////			print("JUMP");
//			
//			app.Model.GameSceneModel.CharacterNumOfJumpsAvailable -= 1;
//			
//			if (!app.Model.GameSceneModel.isCharacterJumping && app.Model.GameSceneModel.complexCharacterJump)
//			{
//				app.Model.GameSceneModel.isCharacterJumping = true;
//			}
//			else if (!app.Model.GameSceneModel.complexCharacterJump)
//			{
//
//				Vector3 force = Vector3.RotateTowards(app.Model.GameSceneModel.CharacterLeftJumpForce, normal, 7, 0.0F) * coef;
//				
//				if (app.Model.GameSceneModel.isOriginalGameScene)
//				{
//					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
//					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.AddForce(force);
//				}
//				else
//				{
//				
//					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
//					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.AddForce(force);
//				}
//			}
//		}
//	}
	
	public void CharacterJumpUpRight()
	{
		if (app.Model.GameSceneModel.CharacterNumOfThrowsAvailable > 0)
		{
//			print("JUMP");

//			Invoke("DecrementNumOfThrowsAvailable()", 1);
			
			DecrementNumOfThrowsAvailable();
			app.Model.GameSceneModel.ShouldCountGoal = true;
			
			if (!app.Model.GameSceneModel.isCharacterJumping && app.Model.GameSceneModel.complexCharacterJump)
			{
				app.Model.GameSceneModel.isCharacterJumping = true;
			}
			else if (!app.Model.GameSceneModel.complexCharacterJump)
			{
				if (app.Model.GameSceneModel.isOriginalGameScene)
				{
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
					Vector3 currentForce = app.Model.GameSceneModel.CharacterRightUpJumpForce;

					float rnd = Random.Range(0.96f, 1.04f);

//					rnd = 1f;
					
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.AddForce(currentForce * rnd);
				}
				else
				{
				
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.GameSceneModel.CharacterRightUpJumpForce);
				}
			}
		}
	}
	
	public void CharacterJumpUpLeft()
	{

//		if (app.Model.GameSceneModel.CharacterNumOfJumpsAvailable > 0)
		if (app.Model.GameSceneModel.TimePlayRemained > 0)
		{
//			app.Model.GameSceneModel.HitBasketBoard = false;
			
			app.Model.GameSceneModel.ShouldCountGoal = false;
			
//			DecrementNumOfJumpsAvailable();
			
			if (!app.Model.GameSceneModel.isCharacterJumping && app.Model.GameSceneModel.complexCharacterJump)
			{
				app.Model.GameSceneModel.isCharacterJumping = true;
			}
			else if (!app.Model.GameSceneModel.complexCharacterJump)
			{
				if (app.Model.GameSceneModel.isOriginalGameScene)
				{
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.GameSceneModel.CharacterLeftUpJumpForce);
				}
				else
				{
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.GameSceneModel.CharacterLeftUpJumpForce);
				}
			
			}
		}
		else
		{
			app.Controller.AudioController.PlayConfusion();
		}
	}

	public void CharacterJumpDownRight()
	{
//		if (app.Model.GameSceneModel.CharacterNumOfJumpsAvailable > 0)
		if (app.Model.GameSceneModel.TimePlayRemained > 0)
		{
//			print("JUMP");
			
			app.Model.GameSceneModel.ShouldCountGoal = false;


			if (!app.Model.GameSceneModel.isCharacterJumping && app.Model.GameSceneModel.complexCharacterJump)
			{
				app.Model.GameSceneModel.isCharacterJumping = true;
			}
			else if (!app.Model.GameSceneModel.complexCharacterJump)
			{
				if (app.Model.GameSceneModel.isOriginalGameScene)
				{
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.GameSceneModel.CharacterRightDownJumpForce);
				}
				else
				{
				
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.GameSceneModel.CharacterRightDownJumpForce);
				}
			}
		}
		else
		{
			app.Controller.AudioController.PlayConfusion();
		}
	}
	
	public void CharacterJumpDownLeft()
	{

//		if (app.Model.GameSceneModel.CharacterNumOfJumpsAvailable > 0)
		if (app.Model.GameSceneModel.TimePlayRemained > 0)
		{
			app.Model.GameSceneModel.ShouldCountGoal = false;
			
//			DecrementNumOfJumpsAvailable();
			
			if (!app.Model.GameSceneModel.isCharacterJumping && app.Model.GameSceneModel.complexCharacterJump)
			{
				app.Model.GameSceneModel.isCharacterJumping = true;
			}
			else if (!app.Model.GameSceneModel.complexCharacterJump)
			{
				if (app.Model.GameSceneModel.isOriginalGameScene)
				{
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
					app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.GameSceneModel.CharacterLeftDownJumpForce);
				}
				else
				{
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.velocity = Vector3.zero;
					app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.Model.GameSceneModel.CharacterLeftDownJumpForce);
				}
			
			}
		}
		else
		{
			app.Controller.AudioController.PlayConfusion();
		}
	}

	public void AddForceToCharacter(float coef)
	{
		app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.AddForce(app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity.normalized * coef);
	}


	public void RemoveEverythingFromScene()
	{
		app.Controller.GameSceneController.CharacterRowController.RemoveAllMovingComponents();
	}

	private void CopyGameScene()
	{
		Transform gameSceneCopy = Instantiate(app.View.GameSceneView.transform, app.View.TechLevelView.KSScreenView.transform.position,
			Quaternion.identity);

		gameSceneCopy.parent = app.View.TechLevelView.KSScreenView.transform;

		Destroy(gameSceneCopy.GetComponent<GameSceneView>());

		gameSceneCopy.gameObject.AddComponent<GameSceneCopyView>();

		gameSceneCopy.name = "GameSceneCopyView";
		
		foreach (var destroyAfterCopyingComponent in gameSceneCopy.GetComponentsInChildren<DestroyAfterCopyingComponent>())
		{
			Destroy(destroyAfterCopyingComponent.gameObject);
		}
		
		gameSceneCopy.localPosition = new Vector3(0, -7, -46.2f);
		
		foreach (var renderer in gameSceneCopy.GetComponentsInChildren<Renderer>())
		{
			Material[] mats = renderer.materials;
			mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
			renderer.materials = mats;
		}

		app.View.TechLevelView.KSScreenView.GameSceneCopyView = gameSceneCopy.GetComponent<GameSceneCopyView>();
	}

	public void SetGameSceneViewToOriginalGameSceneView()
	{
		app.Model.GameSceneModel.isOriginalGameScene = true;
	}
	
	public void SetGameSceneViewToCopyGameSceneView()
	{
		app.Model.GameSceneModel.isOriginalGameScene = false;
	}



	public void DecrementNumOfThrowsAvailable()
	{
		if (app.Model.GameSceneModel.CharacterNumOfThrowsAvailable > 0)
		{
			app.Model.GameSceneModel.CharacterNumOfThrowsAvailable -= 1;
		
			app.View.GameSceneView.LineRendererRowView.NumOfThrowsCounterView.Text.text = app.Model.GameSceneModel.CharacterNumOfThrowsAvailable.ToString();


			app.View.GameSceneView.LineRendererRowView.NumOfThrowsCounterView.Text.color = Color.red;
		}
	}
	
	public void IncrementNumOfThrowsAvailable()
	{
		if (app.Model.GameSceneModel.CharacterNumOfThrowsAvailable < 1)
		{
			app.Model.GameSceneModel.CharacterNumOfThrowsAvailable += 1;
		
			app.View.GameSceneView.LineRendererRowView.NumOfThrowsCounterView.Text.text = app.Model.GameSceneModel.CharacterNumOfThrowsAvailable.ToString();
		
			app.View.GameSceneView.LineRendererRowView.NumOfThrowsCounterView.Text.color = Color.green;
		}
	}
	
	public void DecrementNumOfJumpsAvailable()
	{
		if (app.Model.GameSceneModel.CharacterNumOfJumpsAvailable > 0)
		{
			app.Model.GameSceneModel.CharacterNumOfJumpsAvailable -= 1;
		
			app.View.GameSceneView.LineRendererRowView.NumOfJumpsCounterView.Text.text = app.Model.GameSceneModel.CharacterNumOfJumpsAvailable.ToString();
			
			app.View.GameSceneView.LineRendererRowView.NumOfJumpsCounterView.Text.color = Color.red;
		}
	}
	
	public void IncrementNumOfJumpsAvailable(int num)
	{
		app.Model.GameSceneModel.CharacterNumOfJumpsAvailable += num;
		
		app.View.GameSceneView.LineRendererRowView.NumOfJumpsCounterView.Text.text = app.Model.GameSceneModel.CharacterNumOfJumpsAvailable.ToString();
		
		app.View.GameSceneView.LineRendererRowView.NumOfJumpsCounterView.Text.color = Color.green;
	}
	
	public void DecrementNumOfCharacterPointsAvailable()
	{
		if (app.Model.GameSceneModel.CharacterPointNumber > 0)
		{
			app.Model.GameSceneModel.CharacterPointNumber -= 1;
		
			app.View.GameSceneView.LineRendererRowView.NumOfCharacterPointsView.Text.text = app.Model.GameSceneModel.CharacterPointNumber.ToString();
			
			app.View.GameSceneView.LineRendererRowView.NumOfCharacterPointsView.Text.color = Color.red;
		}
	}
	
	public void IncrementNumOfCharacterPointsAvailable(int num)
	{
		app.Model.GameSceneModel.CharacterPointNumber += num;
		
		app.View.GameSceneView.LineRendererRowView.NumOfCharacterPointsView.Text.text = app.Model.GameSceneModel.CharacterPointNumber.ToString();
		
		app.View.GameSceneView.LineRendererRowView.NumOfCharacterPointsView.Text.color = Color.green;
		
		if (app.Model.GameSceneModel.MaxScore < app.Model.GameSceneModel.CharacterPointNumber)
		{
			app.Model.GameSceneModel.MaxScore = app.Model.GameSceneModel.CharacterPointNumber;
			
			app.View.GameSceneView.LineRendererRowView.MaxScoreView.Text.text = app.Model.GameSceneModel.MaxScore.ToString();
		}
	}
	
	public void DecrementPlayTimeAvailable()
	{
		if (app.Model.GameSceneModel.TimePlayRemained > 0)
		{
			app.Model.GameSceneModel.TimePlayRemained -= 1;
		
			app.View.GameSceneView.LineRendererRowView.NumOfJumpsCounterView.Text.text = app.Model.GameSceneModel.TimePlayRemained.ToString();
			
			app.View.GameSceneView.LineRendererRowView.NumOfJumpsCounterView.Text.color = Color.red;
		}
	}
	
	public void IncrementPlayTimeAvailable(int num)
	{
		app.Model.GameSceneModel.TimePlayRemained += num;
		
		app.View.GameSceneView.LineRendererRowView.NumOfJumpsCounterView.Text.text = app.Model.GameSceneModel.TimePlayRemained.ToString();
			
		app.View.GameSceneView.LineRendererRowView.NumOfJumpsCounterView.Text.color = Color.green;
	}

	void Update()
	{
		if (app.Model.GameSceneModel.TimePlayRemained > 0)
		{
			timeCounter += Time.deltaTime;

			if (timeCounter > 1)
			{
				DecrementPlayTimeAvailable();
				
				timeCounter = 0;
			}
		}
		else
		{
			if (!wasSaved)
			{

				wasSaved = true;
				
				SaveMaxScore();
			}
		}
	}

	public bool ShouldCountGoal()
	{
		print("GameSceneModel.ShouldCountGoal = " + app.Model.GameSceneModel.ShouldCountGoal);
		print("app.Model.GameSceneModel.CharacterNumOfThrowsAvailable = " + app.Model.GameSceneModel.CharacterNumOfThrowsAvailable);
		print("!app.Model.GameSceneModel.ShouldCountGoal = " + !app.Model.GameSceneModel.ShouldCountGoal);
		return app.Model.GameSceneModel.ShouldCountGoal || (app.Model.GameSceneModel.CharacterNumOfThrowsAvailable > 0 && !app.Model.GameSceneModel.ShouldCountGoal);
	}

//	private void SmoothMoveTransformDown(Transform tr)
//	{
//		Vector3 currentPosition = tr.position;
//		
//		float smoothTime = 0.3F;
//		Vector3 velocity = Vector3.zero;
//		
//		Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y - 20, currentPosition.z);
//		tr.position = Vector3.SmoothDamp(tr.position, targetPosition, ref velocity, smoothTime);
//	}
//	
//	private void SmoothMoveTransformUp(Transform tr)
//	{
//		Vector3 currentPosition = tr.position;
//		
//		float smoothTime = 0.3F;
//		Vector3 velocity = Vector3.zero;
//		
//		Vector3 targetPosition = new Vector3(currentPosition.x, currentPosition.y + 20, currentPosition.z);
//		tr.position = Vector3.SmoothDamp(tr.position, targetPosition, ref velocity, smoothTime);
//	}
	
	
}
