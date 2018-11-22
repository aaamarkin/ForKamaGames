using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChangerChildComponent : AppElement {

   
	private BuildableComponent _buildableComponent;
	private Collider _collider;


	public override void LastInAwake()
	{
		_buildableComponent = GetComponent<BuildableComponent>();
		_collider = GetComponent<Collider>();
           
	}

	public void SetPlayMode()
	{
		_collider.isTrigger = false;

	}
    
	public void SetBuildMode()
	{
		_collider.isTrigger = true;

	}

	public bool IsBuildabel()
	{
		return _buildableComponent.IsBuildable;
	}
}
