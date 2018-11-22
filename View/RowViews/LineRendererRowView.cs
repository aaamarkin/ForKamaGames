using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererRowView : AppElement {

	[HideInInspector]
	public RightBorderView RightBorderView;
	[HideInInspector]
	public LeftBorderView LeftBorderView;
	[HideInInspector]
	public LineRendererView LineRendererView;
	
	[HideInInspector]
	public NumOfCharacterPointsView NumOfCharacterPointsView;
	[HideInInspector]
	public NumOfJumpsCounterView NumOfJumpsCounterView;
	[HideInInspector]
	public NumOfThrowsCounterView NumOfThrowsCounterView;
	[HideInInspector]
	public MaxScoreView MaxScoreView;
	
	// Use this for initialization
	override public void LastInAwake() 
	{
	
		LeftBorderView = GetComponentInChildren<LeftBorderView>();
		RightBorderView = GetComponentInChildren<RightBorderView>();
		LineRendererView = GetComponentInChildren<LineRendererView>();
		
		NumOfCharacterPointsView = GetComponentInChildren<NumOfCharacterPointsView>();
		NumOfJumpsCounterView = GetComponentInChildren<NumOfJumpsCounterView>();
		NumOfThrowsCounterView = GetComponentInChildren<NumOfThrowsCounterView>();
		MaxScoreView = GetComponentInChildren<MaxScoreView>();
	}
}
