using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraVIew : AppElement {

	private Rigidbody _rigidbody;
	public Rigidbody Rigidbody {get { return _rigidbody; }}
	private Camera _camera;
	public Camera Camera {get { return _camera; }}
	
	// Use this for initialization
	public override void LastInAwake() {
		
		_rigidbody = GetComponent<Rigidbody> ();
		_camera = GetComponent<Camera> ();
		
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		app.Controller.CameraController.RenderImage(sourceTexture, destTexture);
	}
}
