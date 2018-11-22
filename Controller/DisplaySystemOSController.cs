using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySystemOSController : AppElement {

	// Use this for initialization
	void Start ()
	{
//		SetToLoginDisplay();
		SetToKSDisplay();
	}

	public void SetToLoginDisplay()
	{
		app.View.DisplaySystemOSView.OSPressKSComponent.gameObject.SetActive(false);
		app.View.DisplaySystemOSView.OSPressShopComponent.gameObject.SetActive(false);
		app.View.DisplaySystemOSView.OSPressLoginComponent.gameObject.SetActive(true);
		app.View.TechLevelView.RealCameraOSView.Drawing_BluePrint.enabled = false;

		SetCameraPosition(app.View.TechLevelView.LoginScreenView.transform.position.x, app.View.TechLevelView.LoginScreenView.transform.position.y);
	}
	
	public void SetToSelectDisplay()
	{
		app.View.DisplaySystemOSView.OSPressKSComponent.gameObject.SetActive(true);
		app.View.DisplaySystemOSView.OSPressShopComponent.gameObject.SetActive(true);
		app.View.DisplaySystemOSView.OSPressLoginComponent.gameObject.SetActive(false);
		app.View.TechLevelView.RealCameraOSView.Drawing_BluePrint.enabled = false;
		
		SetCameraPosition(app.View.TechLevelView.SelectScreenView.transform.position.x, app.View.TechLevelView.SelectScreenView.transform.position.y);
		
	}

	public void SetToKSDisplay()
	{
		app.View.DisplaySystemOSView.OSPressKSComponent.gameObject.SetActive(true);
		app.View.DisplaySystemOSView.OSPressShopComponent.gameObject.SetActive(true);
		app.View.DisplaySystemOSView.OSPressLoginComponent.gameObject.SetActive(false);
		app.View.TechLevelView.RealCameraOSView.Drawing_BluePrint.enabled = true;
		
		SetCameraPosition(app.View.TechLevelView.KSScreenView.transform.position.x, app.View.TechLevelView.KSScreenView.transform.position.y);
		
	}
	
	public void SetCameraPosition(float xPos, float yPos)
	{
		Vector3 currentCameraPosition = app.View.TechLevelView.RealCameraOSView.transform.position;
		Quaternion currentCameraRotation = app.View.TechLevelView.RealCameraOSView.transform.rotation;
		
		Vector3 newPosition = new Vector3(xPos, yPos, currentCameraPosition.z);
		
		app.View.TechLevelView.RealCameraOSView.transform.SetPositionAndRotation(newPosition, currentCameraRotation);

	}

	public void LoginButtonWasPressed()
	{
		SetToSelectDisplay();
	}
	
	public void KSButtonWasPressed()
	{
		SetToKSDisplay();
	}
}
