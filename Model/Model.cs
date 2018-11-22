using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR.iOS;

public class Model : AppElement
{

//	public bool useAR;
	
	public bool generatePlanes;

	private CameraModel _cameraModel;
	public CameraModel CameraModel {get { return _cameraModel; }}
	private CanvasModel _canvasModel;
	public CanvasModel CanvasModel {get { return _canvasModel; }}
	private BuildingModel _buildingModel;
	public BuildingModel BuildingModel {get { return _buildingModel; }}
	private DraggingModel _draggingModel;
	public DraggingModel DraggingModel {get { return _draggingModel; }}
	private AimModel _aimModel;
	public AimModel AimModel {get { return _aimModel; }}
	private InitializationModel _initializationModel;
	public InitializationModel InitializationModel {get { return _initializationModel; }}
	private RecordModel _recordModel;
	public RecordModel RecordModel {get { return _recordModel; }}
	private GeneratePlaneModel _generatePlaneModel;
	public GeneratePlaneModel GeneratePlaneModel {get { return _generatePlaneModel; }}
	private GameSceneModel _gameSceneModel;
	public GameSceneModel GameSceneModel {get { return _gameSceneModel; }}
	private AudioModel _audioModel;
	public AudioModel AudioModel {get { return _audioModel; }}
	private TimeModel _timeModel;
	public TimeModel TimeModel {get { return _timeModel; }}
	private SaveModel _saveModel;
	public SaveModel SaveModel {get { return _saveModel; }}
	
	void Awake() {

		_cameraModel = GetComponentInChildren<CameraModel>();
		_canvasModel = GetComponentInChildren<CanvasModel>();
		_buildingModel = GetComponentInChildren<BuildingModel>();
		_aimModel = GetComponentInChildren<AimModel>();
		_draggingModel = GetComponentInChildren<DraggingModel>();
		_initializationModel = GetComponentInChildren<InitializationModel>();
		_recordModel = GetComponentInChildren<RecordModel>();
		_generatePlaneModel = GetComponentInChildren<GeneratePlaneModel>();
		_gameSceneModel = GetComponentInChildren<GameSceneModel>();
		_audioModel = GetComponentInChildren<AudioModel>();
		_timeModel = GetComponentInChildren<TimeModel>();
		_saveModel = GetComponentInChildren<SaveModel>();
		
//		if (useAR)
//		{
//			UseAR();
//		}
//		else
//		{
//			UseOriginal();
//		}
		
		#if !UNITY_EDITOR
			UseAR();
		#else
			UseOriginal();
		#endif
		
		if (generatePlanes)
		{
			GeneratePlanes();
		}
		else
		{
			NotGeneratePlanes();
		}

	}

	private void UseAR()
	{
		_cameraModel.useARCamera = true;
		_cameraModel.updateCameraMatrix = true;
		_draggingModel.useNonARLongGesture = false;

	}

	private void UseOriginal()
	{
		_cameraModel.useARCamera = false;
		_cameraModel.updateCameraMatrix = false;
		_draggingModel.useNonARLongGesture = true;

	}
	
	private void GeneratePlanes()
	{
		_generatePlaneModel.generatePlanes = true;
		_cameraModel.PlaneDetection = UnityARPlaneDetection.Horizontal;

	}

	private void NotGeneratePlanes()
	{
		_generatePlaneModel.generatePlanes = false;
		_cameraModel.PlaneDetection = UnityARPlaneDetection.None;

	}

}
