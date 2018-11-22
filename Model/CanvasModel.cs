using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasModel : AppElement {

    private UIPanelModel _uiPanelModel;
    public UIPanelModel UIPanelModel {get { return _uiPanelModel; }}
    private BuildButtonModel _buildButtonModel;
    public BuildButtonModel BuildButtonModel {get { return _buildButtonModel; }}
    private AimModel _aimModel;
    public AimModel AimModel {get { return _aimModel; }}
    
    override public void LastInAwake() {

        _uiPanelModel = GetComponentInChildren<UIPanelModel> ();
        _buildButtonModel = GetComponentInChildren<BuildButtonModel> ();
        _aimModel = GetComponentInChildren<AimModel> ();
        
    }
}
