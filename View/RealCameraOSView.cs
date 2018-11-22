using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealCameraOSView : AppElement {

	private CameraFilterPack_Drawing_BluePrint _drawingBluePrint;
	public CameraFilterPack_Drawing_BluePrint Drawing_BluePrint {get { return _drawingBluePrint; }}
	
	override public void LastInAwake() {

		_drawingBluePrint = GetComponent<CameraFilterPack_Drawing_BluePrint>();
		
	}
}
