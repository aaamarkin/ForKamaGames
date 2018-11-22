using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : AppElement
{

	// Use this for initialization
	void Start () {

		app.Model.BuildingModel.ShowPreview = false;

	}



	public void PlacePreview()
	{

		Transform currentBuildingTransform = app.Model.CanvasModel.UIPanelModel.ScrollPanelModel.NearestButtonView
			.BuildingContainerComponent
			.BuildingTransform;
		
		Vector3 currentPosition = app.Model.CameraModel.MainCamera.ScreenToWorldPoint(new Vector3(app.Model.CameraModel.ScreenCenter.x, app.Model.CameraModel.ScreenCenter.y, app.Model.CameraModel.instantiationDistanceFromCamera));

		GameObject currentObject =
			Instantiate(currentBuildingTransform.gameObject, currentPosition, currentBuildingTransform.rotation) as GameObject;

		app.Model.BuildingModel.AddModeChangerComponent(currentObject.GetComponentInChildren<ModeChangerComponent>());



	}

	public void ShowPreviewOn()
	{
		app.Model.BuildingModel.ShowPreview = true;
	}
	
	public void ShowPreviewOff()
	{
		app.Model.BuildingModel.ShowPreview = false;
	}

	public void BuildingModeOn()
	{
			app.Controller.AimController.DeselectAimedObject();
//			app.Controller.AimController.AddAimImageToScreen();
		
			app.Model.BuildingModel.BuildModeOn = true;
			app.Model.BuildingModel.UpdateModeChangerComponentRotPos();
			foreach (var buildingComponent in app.Model.BuildingModel.AllModeChangerComponents())
			{
				buildingComponent.SetBuildMode();
			}

		
	}

	public void BuildingModeOff()
	{
		if (IsBuildable())
		{
//			app.Controller.AimController.RemoveAimImageFromScreen();
		
			app.Model.BuildingModel.BuildModeOn = false;
			app.Model.BuildingModel.SaveModeChangerComponentRotPos();
			foreach (var buildingComponent in app.Model.BuildingModel.AllModeChangerComponents())
			{
				buildingComponent.SetPlayMode();
			}
		}
	}

	public bool IsBuildable()
	{
		bool temp = true;

		foreach (var buildingComponent in app.Model.BuildingModel.AllModeChangerComponents())
		{

			temp = temp && buildingComponent.IsBuildabel();

		}

		return temp;
	}
}
