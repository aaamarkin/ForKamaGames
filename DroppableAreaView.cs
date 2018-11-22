using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DroppableAreaView : AppElement, IDropHandler {

	public void OnDrop(PointerEventData eventData)
	{
	Debug.Log("OnDrop ");	
	}
}
