using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class AimController : AppElement
{

	private bool isSmthSelected;
	private bool wasSmthSelectedBefore;
	private bool wasNewObjectSelected;
	
	private void Start()
	{
		wasNewObjectSelected = true;
		wasSmthSelectedBefore = false;
		isSmthSelected = false;
		NthgIsSelected();
	}

	// Update is called once per frame
//	void Update ()
//	{
//		if (app.Model.BuildingModel.BuildModeOn && !app.Model.DraggingModel.IsDraggingSmth)
//		{
//			
//			TryRaycastHit(new Vector3(app.Model.CanvasModel.AimModel.AimCenter.x, app.Model.CanvasModel.AimModel.AimCenter.y, 0));
//			
//		}
//		
//	}

	public void TryRaycastHit(Vector3 screenPoint)
	{
		
//		print("screenPoint = " + screenPoint);
		
		RaycastHit hit;
		
		Ray ray = app.Model.CameraModel.MainCamera.ScreenPointToRay(screenPoint);
        
		if (Physics.Raycast(ray, out hit))
		{
			
			
			
			wasNewObjectSelected = app.Model.AimModel.LastAimInstanceID != hit.collider.GetInstanceID().ToString();

			app.Model.AimModel.LastAimInstanceID = hit.collider.GetInstanceID().ToString();

			if (wasNewObjectSelected)
			{
				app.Model.AimModel.UnhighlightAimableComponent();
			} 					
					
//			app.View.CanvasView.AimView.Image.sprite = app.Model.CanvasModel.AimModel.aimSelected;

			app.Model.AimModel.AimSelected = true;

			app.Model.AimModel.AimHitCollider = hit.collider;

			app.Model.AimModel.ScreenPointOfAimContact = screenPoint;
					
			app.Model.AimModel.HighlightAimableComponent();
					
			app.Model.AimModel.ChangeColorSelectableComponent();

			isSmthSelected = true;
		}
		else
		{
			if (wasSmthSelectedBefore)
			{
					
//				app.View.CanvasView.AimView.Image.sprite = app.Model.CanvasModel.AimModel.aimNotSelected;
				
				app.Model.AimModel.AimSelected = false;
					
				app.Model.AimModel.UnhighlightAimableComponent();
						
				app.Model.AimModel.ChangeColorSelectableComponent();

				app.Model.AimModel.ResetLastAimInstanceID();

			}
				
			isSmthSelected = false;
			
		}
		
		wasSmthSelectedBefore = isSmthSelected;
	}

//	public void AddAimImageToScreen()
//	{
//		app.View.CanvasView.AimView.gameObject.SetActive(true);
//	}
	
//	public void RemoveAimImageFromScreen()
//	{
//		app.View.CanvasView.AimView.gameObject.SetActive(false);
//	}


	public void DeselectAimedObject()
	{
		
		app.Model.AimModel.DettachDampedSpringMotionCopier();
		
		app.Model.AimModel.DeselectSelectableComponent();
		
		app.Model.AimModel.RemoveAimHitCollider();
		
		app.Model.AimModel.UnhighlightAimableComponent();
		

		app.Model.AimModel.AimSelected = false;
		
		NthgIsSelected();

	}

	public void SelectAimedObject()
	{
		app.Model.AimModel.MotionCopierComponent.Attach(app.Model.CameraModel.MainCamera.transform);
		if (app.Model.AimModel.SelectableComponent != null)
		{
			app.Model.AimModel.SelectableComponent.Select();
		}
		
	}

	private void SmthIsSelected()
	{
		wasSmthSelectedBefore = isSmthSelected;
		isSmthSelected = true;
//		app.View.CanvasView.AimView.Image.sprite = app.Model.CanvasModel.AimModel.aimSelected;
		
		
	}
	
	private void NthgIsSelected()
	{
		wasSmthSelectedBefore = isSmthSelected;
		isSmthSelected = false;
//		app.View.CanvasView.AimView.Image.sprite = app.Model.CanvasModel.AimModel.aimNotSelected;
		
	}
}
