using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : AppElement {

    private CanvasView _canvasView;
    public CanvasView CanvasView {get { return _canvasView; }}
    private ARCameraView _arCameraView;
    public ARCameraView ARCameraView {get { return _arCameraView; }}
    private SimpleCameraVIew _simpleCameraVIew;
    public SimpleCameraVIew SimpleCameraVIew {get { return _simpleCameraVIew; }}
    private OrthograficCameraView _orthograficCameraView;
    public OrthograficCameraView OrthograficCameraView {get { return _orthograficCameraView; }}
    private MainRoomView _mainRoomView;
    public MainRoomView MainRoomView {get { return _mainRoomView; }}
    private TechLevelView _techLevelView;
    public TechLevelView TechLevelView {get { return _techLevelView; }}
    private GameSceneView _gameSceneView;
    public GameSceneView GameSceneView {get { return _gameSceneView; }
        set { _gameSceneView = value; }
    }
    private RotatableSceneView _rotatableSceneView;
    public RotatableSceneView RotatableSceneView {get { return _rotatableSceneView; }}
    private DisplaySystemView _displaySystemView;
    public DisplaySystemView DisplaySystemView {get { return _displaySystemView; }}
    private DisplaySystemOSView _displaySystemOsView;
    public DisplaySystemOSView DisplaySystemOSView {get { return _displaySystemOsView; }}
    private CameraSubstituesView _cameraSubstituesView;
    public CameraSubstituesView CameraSubstituesView {get { return _cameraSubstituesView; }}
	
    override public void LastInAwake() {

        _canvasView = GetComponentInChildren<CanvasView> ();
        _arCameraView = GetComponentInChildren<ARCameraView> ();
        _simpleCameraVIew = GetComponentInChildren<SimpleCameraVIew> ();
        _orthograficCameraView = GetComponentInChildren<OrthograficCameraView> ();
        _mainRoomView = GetComponentInChildren<MainRoomView> ();
        _techLevelView = GetComponentInChildren<TechLevelView>();
        _gameSceneView = GetComponentInChildren<GameSceneOriginalView>();
        _rotatableSceneView = GetComponentInChildren<RotatableSceneView>();
        		
        _displaySystemView = GetComponentInChildren<DisplaySystemView>();
        _displaySystemOsView = GetComponentInChildren<DisplaySystemOSView>();
        
        _cameraSubstituesView = GetComponentInChildren<CameraSubstituesView>();

    }

}
