using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollPanelButtonLongPressComponents : AppElement
//    , IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
//	[SerializeField]
//	[Tooltip("How long must pointer be down on this object to trigger a long press")]
//	private float holdTime = 1f;
//	
//	private bool growing = false;
//	private float growingTime = 0;
//
//	private Image _image;
//	private Button _button;
// 
//	private MeshRenderer _meshRenderer;
//	private BuildingComponent _buildingComponent;
// 
//	public UnityEvent onLongPress = new UnityEvent();
//
//	private void Start()
//	{
//		_image = GetComponent<Image>();
//		_button = GetComponent<Button>();
//	}
//
//	public void OnPointerDown(PointerEventData eventData)
//	{
//		if (_button.interactable)
//		{
//			Invoke("OnLongPress", holdTime);
//			startGrowing();
//		}
//	}
// 
//	public void OnPointerUp(PointerEventData eventData)
//	{
//		CancelInvoke("OnLongPress");
//
//		stopGrowing();
//		setBuildingComponentOriginalColor();
//
//	}
// 
//	public void OnPointerExit(PointerEventData eventData)
//	{
//		CancelInvoke("OnLongPress");
//
//		stopGrowing();
//		setBuildingComponentOriginalColor();
//	}
// 
//	void OnLongPress()
//	{
//		stopGrowing();
//		onLongPress.Invoke();
//	}
//	
//	private void Update()
//	{
//		if (growing && app.Model.BuildingModel.HasCurrentBuildingObject())
//		{
//			_meshRenderer = app.Model.BuildingModel.CurrentBuildingObjectMeshRenderer;
//			_buildingComponent = app.Model.BuildingModel.CurrentBuildingObjectBuildingComponent;
//				
//			growingTime += Time.unscaledDeltaTime;
//			_meshRenderer.material.color = Color.Lerp(_buildingComponent.CorrectColor(), Color.white, growingTime / holdTime);
//
//		}
//	}
//	
//	private void startGrowing()
//	{
//		growing = true;
//			
//	}
//
//	private void stopGrowing()
//	{
//		growing = false;
//		growingTime = 0;
//
//	}
//
//	private void setBuildingComponentOriginalColor()
//	{
//		if (app.Model.BuildingModel.HasCurrentBuildingObject())
//		{
//			_meshRenderer = app.Model.BuildingModel.CurrentBuildingObjectMeshRenderer;
//			_buildingComponent = app.Model.BuildingModel.CurrentBuildingObjectBuildingComponent;
//			_meshRenderer.material.color = _buildingComponent.CorrectColor();
//		}
//	}
}