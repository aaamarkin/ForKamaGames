using System;
using UnityEngine;
using System.Collections.Generic;
using TouchScript.Gestures;

public class BuildingComponents : AppElement
{
    public bool useGravityInPlayMode;
    
    private List<Collider> colliderList;
    
    private Collider _collider;
    private Rigidbody _rigidbody;
    private MeshRenderer _meshRenderer;
//    private FixedJoint _fixedJoint;
    private Color _greenColor = Color.green;
    private Color _redColor = Color.red;
    private Color _defaultColor;
    
    private TapGesture _tapGesture;
    
    private bool isBuildable;
    public bool IsBuildable
    {
        get { return isBuildable; }
        set { isBuildable = value; }
    }
    
    private bool _isTapped;
    public bool IsTapped
    {
        get { return _isTapped; }
        set { _isTapped = value; }
    }

    public override void LastInAwake()
    {
        _tapGesture = GetComponent<TapGesture>();
//        _fixedJoint = GetComponent<FixedJoint>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _defaultColor = _meshRenderer.sharedMaterial.color;
        
        colliderList = new List<Collider>();

        _isTapped = true;
           
    }

    void Start()
    {
        SetBuildable();
        ChangeColor();
        AddCameraFixJoint();
    }

    public void SetPlayMode()
    {
        if (useGravityInPlayMode)
        {
            _rigidbody.useGravity = true;
        }
        else
        {
            _rigidbody.isKinematic = true;
        }

        _collider.isTrigger = false;
        RemoveCameraFixJoint();
        
        _tapGesture.Tapped -= TappedHandler;
    }
    
    public void SetBuildMode()
    {
        _rigidbody.useGravity = false;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.freezeRotation = true;
        _rigidbody.freezeRotation = false;
        _rigidbody.isKinematic = false;
        _collider.isTrigger = true;
        
        _tapGesture.Tapped += TappedHandler;
    }

    public void PlacePreview()
    {
        if (isBuildable)
        {
            _isTapped = false;
            RemoveCameraFixJoint();
            ChangeColor();
//            app.Model.BuildingModel.RemoveCurrentBuildingObject();
//            app.Model.BuildingModel.AddBuildingComponent(this);
        }
    }
    
    public Color CorrectColor()
    {
        if (_isTapped)
        {
            if (isBuildable)
            {
                return _greenColor;
            }
            else
            {
                return _redColor;
            }
        }
        else
        {
            return _defaultColor;
        }
    }
    
    private void OnEnable()
    {
        _tapGesture.Tapped += TappedHandler;
    }
    
    private void OnDisable()
    {
        _tapGesture.Tapped -= TappedHandler;
    }
    
    private void TappedHandler(object sender, EventArgs e)
    {
        if (_isTapped)
        {
            PlacePreview();
        }
        else
        {
            _isTapped = true;
            AddCameraFixJoint();
            ChangeColor();
        }
        
    }
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && _isTapped)
        {
            colliderList.Add(other);
        }

        SetBuildable();
        ChangeColor();

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            colliderList.Remove(other);
        }

        SetBuildable();
        ChangeColor();
    }

    private void SetBuildable()
    {
        if (colliderList.Count == 0)
        {
            isBuildable = true;
        }
        else
        {
            isBuildable = false;
        }
    }

    private void ChangeColor()
    {
        if (_isTapped)
        {
            if (isBuildable)
            {
                ChangeColor(_greenColor);
            }
            else
            {
                ChangeColor(_redColor);
            }
        }
        else
        {
            ChangeColor(_defaultColor);
        }
    }
    
    private void ChangeColor(Color color)
    {

        _meshRenderer.material.color = color;
    }

    private void AddCameraFixJoint()
    {
        FixedJoint _fixedJoint = transform.gameObject.AddComponent<FixedJoint>();
        _fixedJoint.connectedBody = app.Model.CameraModel.MainCameraRigidbody;
    }
    
    private void RemoveCameraFixJoint()
    {
        FixedJoint _fixedJoint = GetComponent<FixedJoint>();
        if (_fixedJoint != null)
        {
            _fixedJoint.connectedBody = null;
            Component.Destroy(_fixedJoint); 
        }
    }
}
