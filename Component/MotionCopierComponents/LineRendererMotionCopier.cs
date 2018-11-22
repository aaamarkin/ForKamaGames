using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererMotionCopier : MotionCopierComponent {

	public Plane plane;

    private float length;
    
    private Vector3 pos;

    private Vector3 currentPos;
    
//    public TranformMovingComponent tranformMovingComponent;

    void Start()
    {
        _snappableComponent = TryGetComponentEverywhere<SnappableComponent>();
        _onSnapActionComponent = TryGetComponentEverywhere<OnSnapActionComponent>();

        
        plane = new Plane(transform.forward, transform.position);

//        pos = transform.position;

//        length = tranformMovingComponent.transform.position.x - tranformMovingComponent.transform.localScale.x;
            
//        tranformMovingComponent.SetPosition(new Vector3(_rigidbody.position.x + length, _rigidbody.position.y, _rigidbody.position.z + 0.035f));
        
//        tranformMovingComponent.transform.position = new Vector3(_rigidbody.position.x + length, _rigidbody.position.y, _rigidbody.position.z + 0.035f);
//        currentPos = tranformMovingComponent.transform.position;
    }


    public override void Attach(Transform sourceObject)
    {
        isAttached = true;
        
//        print("attaching");
        
        app.Controller.GameSceneController.LineRendererRowController.SetTrailRecodingOn();
        
        app.Controller.GameSceneController.LineRendererRowController.SetTrailPaintingOn();

    }
    
    public override void Dettach()
    {
        
//        print("dettaching");
        
        isAttached = false;

        app.Controller.RecordController.StopCharacterRecord();
        
        app.Controller.GameSceneController.LineRendererRowController.SetTrailRecordingOff();
        
        app.Controller.GameSceneController.LineRendererRowController.SetTrailPaintingOff();
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

            Ray ray = app.Model.CameraModel.MainCamera.ScreenPointToRay(new Vector3(app.Model.AimModel.ScreenPointOfAimContact.x,
                app.Model.AimModel.ScreenPointOfAimContact.y, 0f));

            float rayDistance;

            plane.Raycast(ray, out rayDistance);


            Vector3 point = ray.GetPoint(rayDistance);

            transform.position = point;
        } 
    }
}