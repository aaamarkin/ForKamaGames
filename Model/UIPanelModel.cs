using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelModel : AppElement
{

	public int buttonBuildDragPower; 
		
		
	private ScrollPanelModel _scrollPanelModel;
	public ScrollPanelModel ScrollPanelModel {get { return _scrollPanelModel; }}
	
	override public void LastInAwake() {

		_scrollPanelModel = GetComponentInChildren<ScrollPanelModel> ();

	}
}
