using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class CameraModel : AppElement {

	public UnityARAlignment StartAlignment = UnityARAlignment.UnityARAlignmentGravity;
	
	public bool GetPointCloud = true;
	public bool EnableLightEstimation = true;
	
	

	public float instantiationDistanceFromCamera;
	public float TimeBeforeNoiseStart = 5f;
	public bool ShouldStartNoise = true;
	public float MinCameraVelocityRequired = 0.05f;
	
	[HideInInspector]
	public UnityARPlaneDetection PlaneDetection = UnityARPlaneDetection.Horizontal;
	[HideInInspector]
	public Vector3 previousCameraPosition;
	[HideInInspector]
	public Quaternion previousCameraRotation;
	[HideInInspector]
	public float cameraCurrentLinearVelocity; 
	[HideInInspector]
	public float cameraCurrentRotationVelocity; 
	[HideInInspector]
	public Shader SCShader;
	[HideInInspector]
	public Texture2D Texture2;
	[HideInInspector]
	public bool useARCamera;
	[HideInInspector]
	public bool updateCameraMatrix;
	
	private Camera _mainCamera;
	
	public Camera MainCamera
	{
		get { return _mainCamera; }
		set { _mainCamera = value; }
	}
	
	private Rigidbody _caRigidbody;
	
	public Rigidbody MainCameraRigidbody
	{
		get { return _caRigidbody; }
		set { _caRigidbody = value; }
	}
	
	
	private float _screenCenterX;
	private float _screenCenterY;
	
	public Vector2 ScreenCenter
	{
		get { return new Vector2(_screenCenterX, _screenCenterY); }
	}
	
	public override void LastInAwake()
	{
		_screenCenterX = Screen.width / 2;
		_screenCenterY = Screen.height / 2;
		
		cameraCurrentLinearVelocity = 0;
		cameraCurrentRotationVelocity = 0;
		previousCameraPosition = Vector3.one;
		previousCameraRotation = Quaternion.identity;

	}
}
