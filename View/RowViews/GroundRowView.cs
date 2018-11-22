using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRowView : AppElement
{
	[HideInInspector]
	public RightBorderView RightBorderView;
	
	[HideInInspector]
	public LeftBorderView LeftBorderView;
	
	// Use this for initialization
	override public void LastInAwake() 
	{

		LeftBorderView = GetComponentInChildren<LeftBorderView>();
		RightBorderView = GetComponentInChildren<RightBorderView>();
	}

}
