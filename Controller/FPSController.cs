using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : AppElement {


	private int frameCount = 0;
	private double dt = 0.0;
	private double fps = 0.0;
	private float updateRate = 1.0f;

	private GUIStyle style;

	void Start()
	{
		style = new GUIStyle();
		style.fontSize = 25;
	}

	void Update()
	{
		frameCount++;
		dt += Time.deltaTime;
		if (dt > 1.0/updateRate)
		{

			fps = frameCount / dt ;
			frameCount = 0;
			dt -= 1.0/updateRate;
		}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(300, 50, 1000, 1000),
			"FPS1 = " + Mathf.Round(1.0f / Time.smoothDeltaTime).ToString() + " " + "FPS2 = " + Mathf.Round((float) fps), style);        
 

	}
}
