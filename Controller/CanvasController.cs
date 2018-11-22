using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : AppElement {

	private UIPanelController _uiPanelController;
	public UIPanelController UIPanelController {get { return _uiPanelController; }}
	private BuildButtonController _buildButtonController;
	public BuildButtonController BuildButtonController {get { return _buildButtonController; }}
	private AimController _aimController;
	public AimController AimController {get { return _aimController; }}
	
	override public void LastInAwake() {

		_uiPanelController = GetComponentInChildren<UIPanelController>();
		_buildButtonController = GetComponentInChildren<BuildButtonController>();
		_aimController = GetComponentInChildren<AimController>();
	}
}
