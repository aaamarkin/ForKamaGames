using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterToCompareView : AppElement {
	
	private RectTransform _rectTransform;
	public RectTransform RectTransform {get { return _rectTransform; }}

	override public void LastInAwake() {

		_rectTransform = GetComponent<RectTransform>();

	}
}
