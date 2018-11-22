using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButtonController : AppElement {
	
	private void Start()
	{
		BuildingModeOn();
		
//		app.View.CanvasView.UIPanelView.Animator.SetBool("StartUIPanelHide", true);
		
		
	}

	public void ToggleButton()
	{
		if (app.Model.BuildingModel.BuildModeOn)
		{
			PlayModeOn();
		}
		else
		{
			BuildingModeOn();
		}
		
	}

	public void BuildingModeOn()
	{
			app.View.CanvasView.BuildButtonView.BuildButtonImageView.Image.sprite =
				app.Model.CanvasModel.BuildButtonModel.buildOnImage;
		
			app.Controller.BuildingController.BuildingModeOn();
		
			app.Controller.RecordController.StopPlayingGame();
		
//			app.View.CanvasView.UIPanelView.Animator.SetBool("StartUIPanelHide", false);
	
	}
	
	public void PlayModeOn()
	{
//		print("app.Controller.BuildingController.IsBuildable() = " + app.Controller.BuildingController.IsBuildable());
		if (app.Controller.BuildingController.IsBuildable())
		{
			app.View.CanvasView.BuildButtonView.BuildButtonImageView.Image.sprite =
				app.Model.CanvasModel.BuildButtonModel.playOnImage;
		
			app.Controller.BuildingController.BuildingModeOff();
		
//			app.View.CanvasView.UIPanelView.Animator.SetBool("StartUIPanelHide", true);
		}
		
	}
}
