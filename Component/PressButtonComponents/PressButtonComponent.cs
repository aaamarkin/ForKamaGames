using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PressButtonComponent : MotionCopierComponent {

    [HideInInspector]
    public bool isPressed;
    [HideInInspector]
    public Vector3 defaulPos;

    void Start()
    {
        isPressed = false;

        defaulPos = transform.position;

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
	
    public void FixedUpdate()
    {

        if (IsAttached())
        {

            if (!isPressed)
            {
                PressButton();
            }

        }
        else
        {
            if (isPressed)
            {              
                ReleaseButton();
            }
        }
		
    }

    public abstract void PressButton();

    public abstract void ReleaseButton();
}
