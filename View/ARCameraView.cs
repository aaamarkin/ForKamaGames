using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ARCameraView : AppElement {

	private Rigidbody _rigidbody;
	public Rigidbody Rigidbody {get { return _rigidbody; }}
	private Camera _camera;
	public Camera Camera {get { return _camera; }}
	
//	public Vector3 previous;
//	public float velocity;
	
//	private GUIStyle style;
	
	// Use this for initialization
	public override void LastInAwake() {
		
		_rigidbody = GetComponent<Rigidbody> ();
		_camera = GetComponent<Camera> ();

//		velocity = 0;
//		previous = transform.position;
		
//		style = new GUIStyle();
//		style.fontSize = 25;
	}
	
	void OnRenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		app.Controller.CameraController.RenderImage(sourceTexture, destTexture);
	}

	private void OnCollisionEnter(Collision other)
	{
//		throw new System.NotImplementedException();
	}
}
