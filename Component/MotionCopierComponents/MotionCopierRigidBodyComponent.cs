using UnityEngine;

   
public class MotionCopierRigidBodyComponent : MotionCopierComponent
{
    public override void SetSnapCenterPosition()
    {	
        _rigidbody.position = _snappableComponent.SnapCenter;
    }
    
    public override void SetRotation(Quaternion newRotation)
    {
        _rigidbody.rotation = newRotation;

    }
    
    public override void SetPosition(Vector3 newPosition)
    {
        _rigidbody.position = newPosition;
    }
    
    public override bool ShouldUnsnapSnappingComponent(float magnitude)
    {
        return _rigidbody.position.magnitude - magnitude > 0.015;
    }
    
    public override void SetPositionalFrozenWorldlOffsetAndLastSource()
    {
        if(positionalSpring.fixRelativePositionAtStart == PositionalSpringTweaksAndState.eFreezeType.IN_WORLD_FRAME)
        {		
            positionalSpring.frozenWorldlOffset = _rigidbody.position - positionalSpring.sourceObject.transform.position;
            positionalSpring.lastSourcePosition = _rigidbody.position;

        }
        else if(positionalSpring.fixRelativePositionAtStart == PositionalSpringTweaksAndState.eFreezeType.IN_SOURCE_LOCAL_FRAME)
        {
            positionalSpring.frozenLocalOffset = positionalSpring.sourceObject.InverseTransformPoint(_rigidbody.position);
            positionalSpring.lastSourcePosition = _rigidbody.position;

        }	
    }
    
    public override void SetPRotationalFrozenWorldlOffsetAndLastSource()
    {
        if(rotationalSpring.fixRelativeOrientationAtStart)
        {				
            rotationalSpring.frozenLocalOffset = Quaternion.Inverse(rotationalSpring.sourceObject.rotation) * _rigidbody.rotation;
            rotationalSpring.lastSourceRotation = _rigidbody.rotation;
        }
        else
        {
            rotationalSpring.frozenLocalOffset = Quaternion.identity;
            rotationalSpring.lastSourceRotation = rotationalSpring.sourceObject.rotation;

        }
    }
    
    public override void SetPositionSpringLastPosition()
    {
        positionalSpring.lastPosition = _rigidbody.position;
    }
    
    public override void SetPositionSpringLastRotation()
    {		
        rotationalSpring.lastRotation = _rigidbody.rotation;
    }
    
    public void FixedUpdate()
    {
			
        if(IsAttached())
        {
            RepositionAndRotate(Time.fixedDeltaTime);
        } 
    }
    

}
