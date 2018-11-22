using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanelDragNDropComponent : AppElement, IBeginDragHandler, IDragHandler, IEndDragHandler {

		
	public void OnBeginDrag(PointerEventData eventData)
	{
//		Debug.Log("UIPanelDragNDropComponent OnBeginDrag");
		app.Controller.ScrollPanelController.StartDrag();
//		Debug.Log(eventData.delta);
//		Debug.Log(eventData.scrollDelta);
		
	}

	public void OnDrag(PointerEventData eventData)
	{

	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
		app.Controller.ScrollPanelController.StopDrag();
	}

}
