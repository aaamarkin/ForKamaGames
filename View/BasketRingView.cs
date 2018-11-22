using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketRingView : AppElement {

	public void Goal()
	{
//		print("GOAAAA-AAAA-AAAAAAAl !!!");
		app.Controller.AudioController.PlayGoal();
		
//		app.Controller.GameSceneController.IncrementNumOfJumpsAvailable(5);

		if (!app.Model.GameSceneModel.HitBasketBoard)
		{
			app.Controller.GameSceneController.IncrementNumOfCharacterPointsAvailable(30);

			app.Controller.GameSceneController.IncrementPlayTimeAvailable(30);
			
			app.Controller.GameSceneController.VisualEffectsRowController.BoomOn();
			
			app.Controller.AudioController.PlayGoalWow();
		}
		else
		{
			app.Controller.GameSceneController.IncrementNumOfCharacterPointsAvailable(10);
			
			app.Controller.GameSceneController.IncrementPlayTimeAvailable(10);
		}
		

		app.Model.GameSceneModel.ShouldCountGoal = false;
		
		app.Controller.GameSceneController.DecrementNumOfThrowsAvailable();
	}
	
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.gameObject.tag == "Player")
		{

//			print("BasketRingView = OnCollisionEnter");
		
			app.Model.GameSceneModel.HitBasketBoard = true;
			
			app.Controller.AudioController.PlayBasketHit();

		}	
	}
}
