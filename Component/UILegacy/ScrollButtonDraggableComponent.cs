using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ScrollButtonDraggableComponent : AppElement, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	private ScrollPanelButtonView _scrollPanelButtonView;

	void Start()
	{
		_scrollPanelButtonView = GetComponent<ScrollPanelButtonView>();
	}
	
	public void OnBeginDrag(PointerEventData eventData)
	{
//		Debug.Log("ScrollButtonDraggableComponent OnBeginDrag");
//		Debug.Log(eventData.delta);
//		Debug.Log(eventData.scrollDelta);
//		Debug.Log(eventData.IsScrolling());
//		var newButon = Instantiate(this.gameObject, this.transform.position, this.transform.rotation);
//		newButon.transform.parent = this.gameObject.transform.parent;
	}

	public void OnDrag(PointerEventData eventData)
	{
//		this.transform.position = eventData.position;
	}
	
	public void OnEndDrag(PointerEventData eventData)
	{
//		Destroy(this.gameObject);
//		print();
		if (_scrollPanelButtonView.Button.GetComponent<RectTransform>().position.y >= 150)
		{
			app.Controller.BuildingController.PlacePreview();
		}
	}
}
