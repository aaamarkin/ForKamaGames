using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplaySystemOSView : AppElement {

	private OSPressLoginComponent _osPressLoginComponent;
	public OSPressLoginComponent OSPressLoginComponent {get { return _osPressLoginComponent; }}
	private OSPressShopComponent _osPressShopComponent;
	public OSPressShopComponent OSPressShopComponent {get { return _osPressShopComponent; }}
	private OSPressKSComponent _osPressKsComponent;
	public OSPressKSComponent OSPressKSComponent {get { return _osPressKsComponent; }}
	
	override public void LastInAwake() {

		_osPressLoginComponent = GetComponentInChildren<OSPressLoginComponent>();
		_osPressShopComponent = GetComponentInChildren<OSPressShopComponent>();
		_osPressKsComponent = GetComponentInChildren<OSPressKSComponent>();
		
	}

}
