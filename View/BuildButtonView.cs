using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildButtonView : AppElement {

	private BuildButtonImageView _buildButtonImageView;
	public BuildButtonImageView BuildButtonImageView {get { return _buildButtonImageView; }}
	
	override public void LastInAwake() {

		_buildButtonImageView = GetComponentInChildren<BuildButtonImageView> ();

	}
}
