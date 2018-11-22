using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSPressKSComponent : MotionCopierComponent {

	private bool isPressed;


	void Start()
	{
		isPressed = false;

	}
    
	public override void Attach(Transform sourceObject)
	{
		isAttached = true;

	}
    
	public override void SetSnapCenterPosition()
	{   

	}
    
	public override void SetRotation(Quaternion newRotation)
	{

	}
    
	public override void SetPosition(Vector3 newPosition)
	{

	}
    
	public override bool ShouldUnsnapSnappingComponent(float magnitude)
	{
		return _rigidbody.position.magnitude - magnitude > 0.015;
	}
    
	public override void SetPositionalFrozenWorldlOffsetAndLastSource()
	{

	}
    
	public override void SetPRotationalFrozenWorldlOffsetAndLastSource()
	{

	}
    
	public override void SetPositionSpringLastPosition()
	{

	}
    
	public override void SetPositionSpringLastRotation()
	{       

	}
    
	public void Update()
	{
            
		if(IsAttached())
		{
            
			if (!isPressed)
			{

				isPressed = true;

				app.Controller.DisplaySystemOSController.KSButtonWasPressed();

			}
            
		}
		else
		{
			if (isPressed)
			{
				isPressed = false;
			}
		}
        
	}
}
