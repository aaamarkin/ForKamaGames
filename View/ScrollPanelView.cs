using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollPanelView : AppElement {

	private RectTransform _scrollPanel;
	public RectTransform RectTransform {get { return _scrollPanel; }}
	
	private ScrollPanelButtonView[] _scrollPanelButtonViews;
	public ScrollPanelButtonView[] ScrollPanelButtonViews {get { return _scrollPanelButtonViews; }}

	
	override public void LastInAwake() {

		_scrollPanel = GetComponent<RectTransform> ();
		_scrollPanelButtonViews = GetComponentsInChildren<ScrollPanelButtonView>();

	}
}
