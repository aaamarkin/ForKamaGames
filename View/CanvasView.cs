using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasView : AppElement {

    private UIPanelView _uiPanelView;
    public UIPanelView UIPanelView {get { return _uiPanelView; }}
    private BuildButtonView _buildButtonView;
    public BuildButtonView BuildButtonView {get { return _buildButtonView; }}
    private AimView _aimView;
    public AimView AimView {get { return _aimView; }}
    
	
    override public void LastInAwake() {

        _uiPanelView = GetComponentInChildren<UIPanelView> ();
        _buildButtonView = GetComponentInChildren<BuildButtonView> ();
        _aimView = GetComponentInChildren<AimView> ();
        
    }

}
