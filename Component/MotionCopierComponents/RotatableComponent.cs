using System;
using UnityEngine;
using UnityEngine.XR.iOS;

public class RotatableComponent : MotionCopierComponent
{
    public Plane plane;

    private float length;
    
    private Vector3 pos;

    private Vector3 currentPos;
    
    public TranformMovingComponent tranformMovingComponent;

    void Start()
    {
        _snappableComponent = TryGetComponentEverywhere<SnappableComponent>();
        _onSnapActionComponent = TryGetComponentEverywhere<OnSnapActionComponent>();

        _rigidbody = GetComponentInParent<Rigidbody>();
        
        plane = new Plane(_rigidbody.transform.up, _rigidbody.position);

        pos = transform.position;

        length = tranformMovingComponent.transform.position.x - tranformMovingComponent.transform.localScale.x;
            
//        tranformMovingComponent.SetPosition(new Vector3(_rigidbody.position.x + length, _rigidbody.position.y, _rigidbody.position.z + 0.035f));
        
        tranformMovingComponent.transform.position = new Vector3(_rigidbody.position.x + length, _rigidbody.position.y, _rigidbody.position.z + 0.035f);
        currentPos = tranformMovingComponent.transform.position;
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
			
        if(IsAttached())
        {

            Ray ray = app.Model.CameraModel.MainCamera.ScreenPointToRay(new Vector3(app.Model.AimModel.AimCenter.x,
                app.Model.AimModel.AimCenter.y, 0f));

            float rayDistance = 0;

            plane.Raycast(ray, out rayDistance);

//            if (plane.Raycast(ray, out rayDistance)) ;
               
//            Vector3 p1 = app.Model.CameraModel.MainCamera.ScreenToWorldPoint(new Vector3(app.Model.AimModel.AimCenter.x,
//                app.Model.AimModel.AimCenter.y, 0f));

            Vector3 point = ray.GetPoint(rayDistance);
  
            Vector3 helpAngleVector = Vector3.ProjectOnPlane(transform.position - tranformMovingComponent.transform.position, _rigidbody.transform.up) * -1;

            Vector3 helpAngleVectorHorizontal = Vector3.ProjectOnPlane(_rigidbody.position - tranformMovingComponent.transform.position, _rigidbody.transform.up) * -1;

            Vector3 forceVector = point - new Vector3(transform.position.x, transform.position.y, _rigidbody.position.z);
            
//            Vector3 v2b = Vector3.ProjectOnPlane(point - transform.position, _rigidbody.transform.up);
        


            Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, _rigidbody.position.z), helpAngleVector, Color.red);
            
            Debug.DrawRay(_rigidbody.position, helpAngleVectorHorizontal, Color.green );
            
            float angle = Vector3.SignedAngle(helpAngleVectorHorizontal, helpAngleVector, transform.up);
            
//            Debug.DrawLine(p1, point, Color.green);

//            _rigidbody.MoveRotation(Quaternion.AngleAxis(angle, transform.up) * _rigidbody.rotation);
            
            _rigidbody.AddForceAtPosition(forceVector, new Vector3(transform.position.x, transform.position.y, _rigidbody.position.z));

//            if (_rigidbody.angularVelocity.magnitude > 0)
//            {
//                print(_rigidbody.angularVelocity);
//            }
            
            app.Model.GameSceneModel.rotatableComponentVelocity = _rigidbody.angularVelocity.z;

            app.Model.GameSceneModel.manipulatorDelta = pos - transform.position;
            
            
//            tranformMovingComponent.SetPosition(new Vector3(_rigidbody.position.x + length, _rigidbody.position.y, _rigidbody.position.z + 0.035f));

            tranformMovingComponent.transform.SetPositionAndRotation(new Vector3(currentPos.x - app.Model.GameSceneModel.manipulatorDelta.x, currentPos.y, currentPos.z), Quaternion.AngleAxis(angle, transform.up));
            
//            print(app.Model.GameModel.manipulatorDelta);
        } 
    }
}

