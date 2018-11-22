using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuildableComponent : AppElement
{

    private List<Collider> colliderList;
    
    private SelectableComponent _selectableComponent;
    
    private bool isBuildable;
    public bool IsBuildable
    {
        get { return isBuildable; }
        set { isBuildable = value; }
    }
    
    public override void LastInAwake()
    {
        _selectableComponent = GetComponentInParent<SelectableComponent>();
        
        colliderList = new List<Collider>();
        SetBuildable();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            colliderList.Add(other);
        }

        SetBuildable();
        _selectableComponent.ChangeColor();
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            colliderList.Remove(other);
        }

        SetBuildable();
        _selectableComponent.ChangeColor();
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


}
