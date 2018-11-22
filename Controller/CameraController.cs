using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR.iOS;

public class CameraController : AppElement {

	private UnityARSessionNativeInterface m_session;
	private ARKitWorldTrackingSessionConfiguration config;
	private Material savedClearMaterial;
	
	
	// Noise Shader variables
	private Shader SCShader;
	private float TimeX = 1.0f;
	private Vector4 ScreenResolution;
	private Material SCMaterial;
	
	private float Fade = 1f;
	private float MaxFade = 1f;
	private float Value2 = 1f;
	private float Value3 = 1f;
	private float Value4 = 1f;
	private float time;
	private bool VelocityIsGreaterThanPossible;
	
	private Texture2D Texture2;
	
	Material material
	{
		get 
		{
			if(SCMaterial == null)
			{
				SCMaterial = new Material(SCShader);
				SCMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return SCMaterial;
		}
	}
	// Noise Shader variables

	public override void LastInAwake() {

		InitializationController.InitializationStateEvent += OnInitChangeEvent;
		time = 0;

	}

	void Start ()
	{

		Application.targetFrameRate = 60;
		
		SetToDefaultCameraSettings();
		
		Texture2 = Resources.Load ("CameraFilterPack_TV_Noise") as Texture2D;

		SCShader = Shader.Find("CameraFilterPack/Noise_TV");
		
		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
		
		
		
		m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface();

		#if !UNITY_EDITOR

		
        config = new ARKitWorldTrackingSessionConfiguration();
		config.planeDetection = app.Model.CameraModel.PlaneDetection;
		config.alignment = app.Model.CameraModel.StartAlignment;
		config.getPointCloudData = app.Model.CameraModel.GetPointCloud;
		config.enableLightEstimation = app.Model.CameraModel.EnableLightEstimation;
        m_session.RunWithConfig(config);
		
		#else
		//put some defaults so that it doesnt complain
		UnityARCamera scamera = new UnityARCamera ();
		scamera.worldTransform = new UnityARMatrix4x4 (new Vector4 (1, 0, 0, 0), new Vector4 (0, 1, 0, 0), new Vector4 (0, 0, 1, 0), new Vector4 (0, 0, 0, 1));
		Matrix4x4 projMat = Matrix4x4.Perspective (60.0f, 1.33f, 0.1f, 30.0f);
		scamera.projectionMatrix = new UnityARMatrix4x4 (projMat.GetColumn(0),projMat.GetColumn(1),projMat.GetColumn(2),projMat.GetColumn(3));

		UnityARSessionNativeInterface.SetStaticCamera (scamera);

		#endif
	}

	public void SetToDefaultCameraSettings()
	{
		
//		Application.targetFrameRate = 60;
		
		if (app.Model.CameraModel.useARCamera)
		{
			
			app.View.ARCameraView.gameObject.SetActive(true);
			app.View.SimpleCameraVIew.gameObject.SetActive(false);
			app.View.OrthograficCameraView.gameObject.SetActive(false);
			app.View.TechLevelView.RealCameraOSView.gameObject.SetActive(true);
			
			app.Model.CameraModel.MainCamera = app.View.ARCameraView.Camera;
			app.Model.CameraModel.MainCameraRigidbody = app.View.ARCameraView.Rigidbody;
			
			app.Model.CameraModel.updateCameraMatrix = true;
		}
		else
		{
			
			app.View.ARCameraView.gameObject.SetActive(false);
			app.View.SimpleCameraVIew.gameObject.SetActive(true);
			app.View.OrthograficCameraView.gameObject.SetActive(false);
			app.View.TechLevelView.RealCameraOSView.gameObject.SetActive(true);
			
			app.Model.CameraModel.MainCamera = app.View.SimpleCameraVIew.Camera;
			app.Model.CameraModel.MainCameraRigidbody = app.View.ARCameraView.Rigidbody;
			
		}
	}

	public void SetToDefaultCameraSettingsAndContinueSession()
	{
		SetToDefaultCameraSettings();

		if (app.Model.CameraModel.useARCamera)
		{
			m_session.RunWithConfig(config);
		}
	}

	public void SetToOrthographicCameraSettings()
	{
		
//		Application.targetFrameRate = 30;
		
		app.View.ARCameraView.gameObject.SetActive(false);
		app.View.SimpleCameraVIew.gameObject.SetActive(false);
		app.View.OrthograficCameraView.gameObject.SetActive(true);
		app.View.TechLevelView.RealCameraOSView.gameObject.SetActive(false);
		
		app.Model.CameraModel.MainCamera = app.View.OrthograficCameraView.Camera;
		app.Model.CameraModel.MainCameraRigidbody = null;

		app.Model.CameraModel.updateCameraMatrix = false;
		
		m_session.Pause();
	}

	public void RenderImage (RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if(SCShader != null)
		{
			TimeX+=Time.deltaTime;
			if (TimeX > 100)  TimeX = 0;
			material.SetFloat("_TimeX", TimeX);
			material.SetFloat("_Value", Fade);
			material.SetFloat("_Value2", Value2);
			material.SetFloat("_Value3", Value3);
			material.SetFloat("_Value4", Value4);
			material.SetVector("_ScreenResolution", new Vector4(sourceTexture.width,sourceTexture.height, 0.0f, 0.0f));
			material.SetTexture("Texture2", Texture2);

			Graphics.Blit(sourceTexture, destTexture, material);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}
	
	void Update()
	{
		float deltaTime = Time.deltaTime;
		app.Model.CameraModel.cameraCurrentLinearVelocity = ((app.Model.CameraModel.MainCamera.transform.position - app.Model.CameraModel.previousCameraPosition).magnitude) / deltaTime;
		app.Model.CameraModel.cameraCurrentRotationVelocity = Quaternion.Angle(app.Model.CameraModel.MainCamera.transform.rotation, app.Model.CameraModel.previousCameraRotation) / deltaTime;
		app.Model.CameraModel.previousCameraPosition = app.Model.CameraModel.MainCamera.transform.position;
		app.Model.CameraModel.previousCameraRotation = app.Model.CameraModel.MainCamera.transform.rotation;

		if (app.Model.CameraModel.cameraCurrentLinearVelocity < app.Model.CameraModel.MinCameraVelocityRequired)
		{
			time += Time.deltaTime;
			
		}
		else
		{
			time = 0;
		}
		
		
		if (app != null && app.Model != null)
		{
			if (PerlinNoiseFadeCondition())
			{
				if (Fade + app.Model.InitializationModel.FadeOutStep <= MaxFade)
				{
					Fade = Mathf.Lerp(Fade, MaxFade, app.Model.InitializationModel.FadeOutStep);
				}
            
			}
			else
			{
				if (Fade * 10 >= app.Model.InitializationModel.FadeInStep)
				{
					Fade = Mathf.Lerp(Fade, 0f, app.Model.InitializationModel.FadeInStep);
				}
				else
				{
					Fade = 0;
				}
			}
		}
		
		if (app.Model.CameraModel.useARCamera && app.Model.CameraModel.updateCameraMatrix)
		{
			// JUST WORKS!
			Matrix4x4 matrix = m_session.GetCameraPose();
			app.Model.CameraModel.MainCamera.transform.localPosition = UnityARMatrixOps.GetPosition(matrix);
			app.Model.CameraModel.MainCamera.transform.localRotation = UnityARMatrixOps.GetRotation (matrix);

			app.Model.CameraModel.MainCamera.projectionMatrix = m_session.GetCameraProjection ();
		}
		
	}
	
	private bool PerlinNoiseFadeCondition()
	{
		#if !UNITY_EDITOR
			return ( time >= app.Model.CameraModel.TimeBeforeNoiseStart || app.Model.InitializationModel.State < 2) ; 
		#else
			return ( time >= app.Model.CameraModel.TimeBeforeNoiseStart && app.Model.CameraModel.ShouldStartNoise) ; 
		#endif
		    
	}
	
	public void OnInitChangeEvent(int state)
	{
//		switch (state)
//		{
//			case 0:
//				MaxFade = 1f;
//				break;
//			case 1:
//				MaxFade = 0.7f;
//				break;
//			case 2:
//				MaxFade = 0.7f;
//				break;
//			case 3:
//				MaxFade = 0.2f;
//				break;
//			case 4:
//				MaxFade = 0f;
//				break;
//			default:
//				MaxFade = 1f;
//				break;
//
//		}
	}
}
