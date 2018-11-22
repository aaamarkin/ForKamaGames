using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnappableComponent : AppElement
{
	private bool _shouldBeSnapped;
	public bool ShouldBeSnapped
	{
		get { return _shouldBeSnapped; }
		set { _shouldBeSnapped = value; }
	}
	
	private Vector3 _snapCenter;
	public Vector3 SnapCenter
	{
		get { return _snapCenter; }
	}
	
	private SnapPointComponent _snapPointComponent;
	public SnapPointComponent SnapPointComponent
	{
		get { return _snapPointComponent; }
	}
	
	void Start()
	{
		_shouldBeSnapped = false;
		// Faking snap center
		_snapCenter = transform.position * 2;
	}

	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "SnapPoint")
		{
			_shouldBeSnapped = true;

			_snapCenter = other.transform.position;

			_snapPointComponent = other.transform.gameObject.GetComponent<SnapPointComponent>();
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if (other.tag == "SnapPoint")
		{
			_shouldBeSnapped = false;
		}

	}

	public bool IsSnapped()
	{
		return (transform.position - _snapCenter).magnitude < 0.001f;

	}


}
