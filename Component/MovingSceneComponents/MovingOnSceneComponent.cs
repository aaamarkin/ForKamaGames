using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using UnityEngine;


public abstract class MovingOnSceneComponent : AppElement {
    
    [HideInInspector]
    public Rigidbody Rigidbody;
    [HideInInspector]
    public Renderer Renderer;
    [HideInInspector]
    public Color DefaultColor;
    
    [HideInInspector]
    public float previousXPosition;

    private float singleFrameDistance;
    
    override public void LastInAwake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponentInChildren<Renderer>(); 
        
        Material[] mat = Renderer.materials;
        DefaultColor = mat[0].color;
    }
    
    void OnEnable()
    {
        if (app.Model)
        {
            if (app.Model.GameSceneModel.useForceForObjects)
            {
                AddForce();
            }

            previousXPosition = Rigidbody.position.x;
//            app.Model.GameSceneModel.MovingOnSceneComponents.Add(gameObject.GetInstanceID(), this);
        } 
    }

    void OnDisable()
    {
//        app.Model.GameSceneModel.MovingOnSceneComponents.Remove(gameObject.GetInstanceID());
    }

    void FixedUpdate()
    {

        if (ShouldBeDisabled())
        {
//            RemoveFromModelAndDestroy();
            MoveToStartPoint();
        }
        else if (ShouldBeMoved())
        {
            Move();

            singleFrameDistance = Mathf.Abs(Rigidbody.position.x - previousXPosition);

            previousXPosition = Rigidbody.position.x;
        } 
        else if (ShouldBeStopped())
        {
            Rigidbody.velocity = Vector3.zero;
        }

    }

    public float GetCurrentDistanceFromStart()
    {
        return Math.Abs(Rigidbody.position.x - GetInstantiationPoint().x);
    }
    
    public float GetSingleFrameDistance()
    {
        return singleFrameDistance;
    }

    public virtual bool ShouldBeMoved()
    {
        return !app.Model.GameSceneModel.useForceForObjects && app.Model.GameSceneModel.runAllMovingComponents;
    }

    public bool ShouldBeStopped()
    {
        return !app.Model.GameSceneModel.useForceForObjects && !app.Model.GameSceneModel.runAllMovingComponents;
    }

    public virtual void RemoveFromModelAndDestroy()
    {
//        app.Model.GameSceneModel.MovingOnSceneComponents.Remove(gameObject.GetInstanceID());
               
        gameObject.SetActive(false);
            
//        Destroy(gameObject);
    }

    public void MoveToStartPoint()
    {
        Rigidbody.MovePosition(GetInstantiationPoint());
    }

    public abstract void AddForce();

    public abstract bool ShouldBeDisabled();

    public abstract void Move();

    public abstract Vector3 GetInstantiationPoint();
    
    public abstract int GetRecordId();
    
    
}