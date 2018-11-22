using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChangerSingleComponent : ModeChangerComponent {

	public bool useGravityInPlayMode;
   
	private BuildableComponent _buildableComponent;
	private Collider _collider;
	private Rigidbody _rigidbody;


	public override void LastInAwake()
	{
		_buildableComponent = GetComponent<BuildableComponent>();
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
           
	}

	public override void SetPlayMode()
	{
		if (useGravityInPlayMode)
		{
			_rigidbody.useGravity = true;
			_rigidbody.isKinematic = false;
		}
		else
		{
			_rigidbody.useGravity = false;
			_rigidbody.isKinematic = true;
		}

		_collider.isTrigger = false;

	}
    
	public override void SetBuildMode()
	{
		_rigidbody.useGravity = false;
		_rigidbody.velocity = Vector3.zero;
//		_rigidbody.freezeRotation = true;
		_rigidbody.freezeRotation = false;
		_rigidbody.isKinematic = true;
		_collider.isTrigger = true;

	}

	public override bool IsBuildabel()
	{
		return _buildableComponent.IsBuildable;
	}
}
