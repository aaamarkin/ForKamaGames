using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DraggingModel : AppElement
{
	[HideInInspector]
	public bool useNonARLongGesture;

	public bool forceARGestures;

	public float dragCoef = 1f;
	
	public float movingCopierForceCoef = 100f;

	public float playTimeAfterPress;

	public bool isDownJumpPressed;

	private bool _isDraggingSmth;
	public bool IsDraggingSmth
	{
		get { return _isDraggingSmth; }
		set { _isDraggingSmth = value; }
	}

}
