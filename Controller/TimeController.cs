using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : AppElement
{

	private float defaultFixedDeltaTime;

	private void Start()
	{
		defaultFixedDeltaTime = Time.fixedDeltaTime;
	}

	public void SlowTime()
	{
		Time.timeScale = app.Model.TimeModel.SlowDownFactor;
		Time.fixedDeltaTime = Time.timeScale * defaultFixedDeltaTime;
		
		
	}
	
	public void DoOriginalTime()
	{
		Time.timeScale = 1f;
		Time.fixedDeltaTime = Time.timeScale * defaultFixedDeltaTime;
		
		
	}
}

