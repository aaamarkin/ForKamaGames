using UnityEngine;

public class MotionCopierTransformComponent : MotionCopierComponent
{

    public override void SetSnapCenterPosition()
    {
        transform.position = _snappableComponent.SnapCenter;	
    }
    
    public override void SetRotation(Quaternion newRotation)
    {
        transform.rotation = newRotation;

    }
    
    public override void SetPosition(Vector3 newPosition)
    {
		transform.position = newPosition;
    }
    
    public override bool ShouldUnsnapSnappingComponent(float magnitude)
    {
        return transform.position.magnitude - magnitude > 0.015;
    }
    
    public override void SetPositionalFrozenWorldlOffsetAndLastSource()
    {
        if(positionalSpring.fixRelativePositionAtStart == PositionalSpringTweaksAndState.eFreezeType.IN_WORLD_FRAME)
        {
			positionalSpring.frozenWorldlOffset = transform.position - positionalSpring.sourceObject.transform.position;
			positionalSpring.lastSourcePosition = transform.position;

        }
        else if(positionalSpring.fixRelativePositionAtStart == PositionalSpringTweaksAndState.eFreezeType.IN_SOURCE_LOCAL_FRAME)
        {
			positionalSpring.frozenLocalOffset = positionalSpring.sourceObject.InverseTransformPoint(transform.position);
			positionalSpring.lastSourcePosition = transform.position;

        }	
    }
    
    public override void SetPRotationalFrozenWorldlOffsetAndLastSource()
    {
        if(rotationalSpring.fixRelativeOrientationAtStart)
        {
			rotationalSpring.frozenLocalOffset = Quaternion.Inverse(rotationalSpring.sourceObject.rotation) * transform.rotation;
			rotationalSpring.lastSourceRotation = transform.rotation;
        }
        else
        {
            rotationalSpring.frozenLocalOffset = Quaternion.identity;
            rotationalSpring.lastSourceRotation = rotationalSpring.sourceObject.rotation;

        }
    }
    
    public override void SetPositionSpringLastPosition()
    {
        positionalSpring.lastPosition = transform.position;
    }
    
    public override void SetPositionSpringLastRotation()
    {		
        rotationalSpring.lastRotation = transform.rotation;
    }
    
    public void Update()
    {
			
        if(IsAttached())
        {
            RepositionAndRotate(Time.deltaTime);
        } 
    }
    
}
