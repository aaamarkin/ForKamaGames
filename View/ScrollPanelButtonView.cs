using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BuildingContainerComponent))]
public class ScrollPanelButtonView : AppElement {

    private Button _button;
    public Button Button {get { return _button; }}
    
    private RectTransform _rectTransform;
    public RectTransform RectTransform {get { return _rectTransform; }}
    
    private BuildingContainerComponent _buildingContainerComponent;
    public BuildingContainerComponent BuildingContainerComponent {get { return _buildingContainerComponent; }}
    
//    private float _buttonDistanceToCenter;
//    public float Distance {get { return _buttonDistanceToCenter; }}
	
    override public void LastInAwake() {

        _button = GetComponentInChildren<Button> ();
        _rectTransform = GetComponent<RectTransform>();
        _buildingContainerComponent = GetComponent<BuildingContainerComponent>();

    }
    
}
