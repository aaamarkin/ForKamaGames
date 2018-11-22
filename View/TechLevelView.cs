using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechLevelView : AppElement {
	
	private RealCameraOSView _realCameraOsView;
	public RealCameraOSView RealCameraOSView {get { return _realCameraOsView; }}
	private SelectScreenView _selectScreenView;
	public SelectScreenView SelectScreenView {get { return _selectScreenView; }}
	private LoginScreenView _loginScreenView;
	public LoginScreenView LoginScreenView {get { return _loginScreenView; }}
	private KSScreenView _kSScreenView;
	public KSScreenView KSScreenView {get { return _kSScreenView; }}

	override public void LastInAwake() {

		_realCameraOsView = GetComponentInChildren<RealCameraOSView>();
		_selectScreenView = GetComponentInChildren<SelectScreenView>();
		_loginScreenView = GetComponentInChildren<LoginScreenView>();
		_kSScreenView = GetComponentInChildren<KSScreenView>();
	}
}
