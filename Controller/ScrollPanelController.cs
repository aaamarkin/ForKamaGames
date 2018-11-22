using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPanelController : AppElement
{

	private float _canvasLocalXOffset;
	private int minDistanceButtonIdx;
	private int numberOfButtons;

	private int indexForAutoDrag;
	
	// Use this for initialization
	void Start ()
	{
		indexForAutoDrag = -1;
		
//		_canvasLocalXOffset = app.View.CanvasView.transform.position.x;

		numberOfButtons = app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews.Length;        

		app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistances = new float[numberOfButtons];
		
		app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistanceRepositions = new float[numberOfButtons];

		app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.DistanceBetweenButtons = 
			(int) Mathf.Abs(app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[1].RectTransform.anchoredPosition.x - 
		                    app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[0].RectTransform.anchoredPosition.x);

		float minDistance = Mathf.Min(app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistances);
		
		for (int i = 0; i < numberOfButtons; i++)
		{
			if (minDistance == app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistances[i])
			{
				app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.NearestButtonView = app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i];
	
				minDistanceButtonIdx = i;
			}
		}
		
	}


	private void Update()
	{

		CalcDistances();

		float minDistance = Mathf.Min(app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistances);

		ScrollPanelButtonView tempNearestButtonView = app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.NearestButtonView;
		
		for (int i = 0; i < numberOfButtons; i++)
		{
			if (minDistance == app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistances[i])
			{
				tempNearestButtonView = app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i];
	
				minDistanceButtonIdx = i;
			}
			
		}

		float buttonLerpingSpeed = app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonLerpingSpeed;
		
		if (app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.IsAutoDragging)
		{
			minDistanceButtonIdx = indexForAutoDrag;

			buttonLerpingSpeed = buttonLerpingSpeed * 10;
		}
		
		if (!app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.IsDragging)
		{
			LerpToButton(-app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[minDistanceButtonIdx]
				.RectTransform.anchoredPosition.x, buttonLerpingSpeed);
			
		}
		
		bool wasButtonChanged = tempNearestButtonView.GetInstanceID() != app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.NearestButtonView.GetInstanceID();
		
		if (wasButtonChanged)
		{
			
//			app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.NearestButtonView.Button.interactable = false;
			
			app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.NearestButtonView = tempNearestButtonView;
				
//			app.Controller.BuildingController.ChangeCurrentPreview();
			
//			app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.NearestButtonView.Button.interactable = true;
		}
		
	}

	public void AutoLerpToButton(ScrollPanelButtonView buttonView)
	{

		indexForAutoDrag = GetButtonIndex(buttonView);

		app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.IsAutoDragging = true;
		
//		Vector2 oldPos = app.View.CanvasView.UIPanelView.ScrollPanelView.RectTransform.anchoredPosition;

//		print("oldPosX = " + oldPos.x);
//		print("ButtonDistanceReposition = " + app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistanceRepositions[buttonIndex]);
//		print("ButtonDistances = " + app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistances[buttonIndex]);

//		app.View.CanvasView.UIPanelView.ScrollPanelView.RectTransform.anchoredPosition = 
//			new Vector2(oldPos.x + app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistanceRepositions[buttonIndex], oldPos.y);

//		forceToAnchoredDistance = app.View.CanvasView.UIPanelView.ScrollPanelView.RectTransform.anchoredPosition.x;
	}

	private void CalcDistances()
	{
		for (int i = 0; i < numberOfButtons; i++)
		{
			app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistanceRepositions[i] =
				app.View.CanvasView.UIPanelView.CenterToCompareView.RectTransform.position.x -
				app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i].RectTransform.position.x;

			app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistances[i] =
				Mathf.Abs(app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistanceRepositions[i]);

			if (app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistanceRepositions[i] >
			    app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.MaxRepositionDistance)
			{
				float curX = app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i].RectTransform.anchoredPosition.x;
				float cury = app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i].RectTransform.anchoredPosition.y;
				
				Vector2 newAnchoredPosition = new Vector2(curX + (numberOfButtons * app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.DistanceBetweenButtons), cury);

				app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i].RectTransform.anchoredPosition =
					newAnchoredPosition;
			}
			
			if (app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.ButtonDistanceRepositions[i] <
			    -app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.MaxRepositionDistance)
			{
				float curX = app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i].RectTransform.anchoredPosition.x;
				float cury = app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i].RectTransform.anchoredPosition.y;
				
				Vector2 newAnchoredPosition = new Vector2(curX - (numberOfButtons * app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.DistanceBetweenButtons), cury);

				app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i].RectTransform.anchoredPosition =
					newAnchoredPosition;
			}
			
		}
	}

	private int GetButtonIndex(ScrollPanelButtonView buttonView)
	{
		int index = -1;
		
		for (int i = 0; i < numberOfButtons; i++)
		{
			
			if (app.View.CanvasView.UIPanelView.ScrollPanelView.ScrollPanelButtonViews[i].gameObject.GetInstanceID() ==
			    buttonView.gameObject.GetInstanceID())
			{
				index = i;
			}
		}

		return index;
	}

	private void LerpToButton(float buttonPosition, float buttonLerpingSpeed)
	{
		float newX = Mathf.Lerp(app.View.CanvasView.UIPanelView.ScrollPanelView.RectTransform.anchoredPosition.x, buttonPosition,
			Time.deltaTime * buttonLerpingSpeed);
		
		Vector2 newPosition = new Vector2(newX, app.View.CanvasView.UIPanelView.ScrollPanelView.RectTransform.anchoredPosition.y);

		app.View.CanvasView.UIPanelView.ScrollPanelView.RectTransform.anchoredPosition = newPosition;

		if (Mathf.Abs(app.View.CanvasView.UIPanelView.ScrollPanelView.RectTransform.anchoredPosition.x - buttonPosition) <=
		    10)
		{
			indexForAutoDrag = -1;

			app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.IsAutoDragging = false;
		}
	}

	public void StartDrag()
	{

		app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.IsDragging = true;
	}

	public void StopDrag()
	{

		app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.IsDragging = false;
	}

}
