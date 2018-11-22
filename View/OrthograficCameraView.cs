using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthograficCameraView : AppElement {

	private Camera _camera;
	public Camera Camera {get { return _camera; }}
	
	// Use this for initialization
	public override void LastInAwake() {

		_camera = GetComponent<Camera> ();
		
	}
}
