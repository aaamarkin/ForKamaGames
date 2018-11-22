using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : AppElement {

	private CameraController cameraController;
	public CameraController CameraController {get { return cameraController; }}
	private CanvasController _canvasController;
	public CanvasController CanvasController {get { return _canvasController; }}
	private BuildingController _buildingController;
	public BuildingController BuildingController {get { return _buildingController; }}
	private ScrollPanelController _scrollPanelController;
	public ScrollPanelController ScrollPanelController {get { return _scrollPanelController; }}
	private DraggingController _draggingController;
	public DraggingController DraggingController {get { return _draggingController; }}
	private AimController _aimController;
	public AimController AimController {get { return _aimController; }}
	private FPSController _fpsController;
	public FPSController FPSController {get { return _fpsController; }}
	private DebugController _debugController;
	public DebugController DebugController {get { return _debugController; }}
	private GeneratePlaneController _generatePlaneController;
	public GeneratePlaneController GeneratePlaneController {get { return _generatePlaneController; }}
	private InitializationController _initializationController;
	public InitializationController InitializationController {get { return _initializationController; }}
	private RecordController _recordController;
	public RecordController RecordController {get { return _recordController; }}
	private DisplaySystemOSController _displaySystemOsController;
	public DisplaySystemOSController DisplaySystemOSController {get { return _displaySystemOsController; }}
	private GameSceneController _gameSceneController;
	public GameSceneController GameSceneController {get { return _gameSceneController; }}
	private AudioController _audioController;
	public AudioController AudioController {get { return _audioController; }}
	private TimeController _timeController;
	public TimeController TimeController {get { return _timeController; }}
	private SaveController _saveController;
	public SaveController SaveController {get { return _saveController; }}
	private PlayfabController _playfabController;
	public PlayfabController PlayfabController {get { return _playfabController; }}
	
	override public void LastInAwake() {

		cameraController = GetComponentInChildren<CameraController> ();
		_canvasController = GetComponentInChildren<CanvasController>();
		_buildingController = GetComponentInChildren<BuildingController>();
		_scrollPanelController = GetComponentInChildren<ScrollPanelController>();
		_draggingController = GetComponentInChildren<DraggingController>();
		_aimController = GetComponentInChildren<AimController>();
		_fpsController = GetComponentInChildren<FPSController>();
		_debugController = GetComponentInChildren<DebugController>();
		_generatePlaneController = GetComponentInChildren<GeneratePlaneController>();
		_initializationController = GetComponentInChildren<InitializationController>();
		_recordController = GetComponentInChildren<RecordController>();
		_displaySystemOsController = GetComponentInChildren<DisplaySystemOSController>();
		_gameSceneController = GetComponentInChildren<GameSceneController>();
		_audioController = GetComponentInChildren<AudioController>();
		_timeController = GetComponentInChildren<TimeController>();
		_saveController = GetComponentInChildren<SaveController>();
		_playfabController = GetComponentInChildren<PlayfabController>();
	}
}
