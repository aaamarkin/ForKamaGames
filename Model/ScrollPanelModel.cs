using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollPanelModel : AppElement
{

	public float ButtonLerpingSpeed;

	public int MaxRepositionDistance;

	private bool _isDragging;

	public bool IsDragging
	{
		get { return _isDragging; }
		set { _isDragging = value; }
	}
	
	private bool _isAutoDragging;

	public bool IsAutoDragging
	{
		get { return _isAutoDragging; }
		set { _isAutoDragging = value; }
	}
	
	private int _distanceBetweenButtons;

	public int DistanceBetweenButtons
	{
		get { return _distanceBetweenButtons; }
		set { _distanceBetweenButtons = value; }
	}
	
	private float[] _buttonDistances;
	public float[] ButtonDistances
	{
		get { return _buttonDistances; }
		set { _buttonDistances = value; }
	}
	
	private float[] _buttonDistanceRepositions;
	public float[] ButtonDistanceRepositions
	{
		get { return _buttonDistanceRepositions; }
		set { _buttonDistanceRepositions = value; }
	}
	
	private ScrollPanelButtonView _nearestButtonView;

	public ScrollPanelButtonView NearestButtonView
	{
		get { return _nearestButtonView; }
		set { _nearestButtonView = value; }
	}
	

}
