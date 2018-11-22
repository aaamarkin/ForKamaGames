using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffectsRowView : AppElement {

	[HideInInspector]
	public RightBorderView RightBorderView;
	
	[HideInInspector]
	public LeftBorderView LeftBorderView;
	
	// Use this for initialization
	override public void LastInAwake() 
	{
	
//		BlockViews = GetComponentsInChildren<BlockView>();
		LeftBorderView = GetComponentInChildren<LeftBorderView>();
		RightBorderView = GetComponentInChildren<RightBorderView>();
	}
}
