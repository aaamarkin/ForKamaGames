using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketRingTriggersView : AppElement {

	private bool characterStartedFollowingThroughBasket;


	void Start()
	{
		characterStartedFollowingThroughBasket = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !app.Model.GameSceneModel.CharacterRowModel.IsCharacterGoingUp)
		{

			print("BasketRingTriggersComponent = OnTriggerEnter");
		
			characterStartedFollowingThroughBasket = true;
			
			print("characterStartedFollowingThroughBasket = " + characterStartedFollowingThroughBasket);
			
//			print("app.Model.GameSceneModel.ShouldCountGoal = " + app.Model.GameSceneModel.ShouldCountGoal);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player" && !app.Model.GameSceneModel.CharacterRowModel.IsCharacterGoingUp)
		{

			print("BasketRingTriggersComponent = OnTriggerExit");
			
			print("characterStartedFollowingThroughBasket = " + characterStartedFollowingThroughBasket);
			
			print("app.Controller.GameSceneController.ShouldCountGoal() = " + app.Controller.GameSceneController.ShouldCountGoal());

			if (characterStartedFollowingThroughBasket && app.Controller.GameSceneController.ShouldCountGoal())
			{
				print("HitBasketBoard = " + app.Model.GameSceneModel.HitBasketBoard);
				
				app.View.GameSceneView.CharacterRowView.BasketRingView.Goal();
			}
			
			characterStartedFollowingThroughBasket = false;

		}
		else if (other.gameObject.tag == "Player")
		{
			print("BasketRingTriggersComponent IsCharacterGoingUp = OnTriggerExit");
			
			characterStartedFollowingThroughBasket = false;
		}
		
		
	}
}
