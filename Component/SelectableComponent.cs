using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableComponent : AppElement
{
    
    private MeshRenderer _meshRenderer;
    private BuildableComponent _buildableComponent;

    private Color _greenColor = Color.green;
    private Color _redColor = Color.red;
    private Color _defaultColor;
      
    private bool _isSelected;
    public bool IsSelected
    {
        get { return _isSelected; }
    }

    public override void LastInAwake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _buildableComponent = GetComponentInChildren<BuildableComponent>();
        
        _defaultColor = _meshRenderer.sharedMaterial.color;
        
        _isSelected = false;
           
    }

    void Start()
    {
        ChangeColor();
    }

    public void Select()
    {
        _isSelected = true;
        ChangeColor();
    }
    
    public void Deselect()
    {
        _isSelected = false;
        ChangeColor();
    }


    public Color CorrectColor()
    {
        if (_isSelected)
        {
            if (_buildableComponent.IsBuildable)
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
     
    public void ChangeColor()
    {
        if (!_buildableComponent.IsBuildable)
        {
            ChangeColor(_redColor);
        }
        else if (_isSelected)
        {
            if (_buildableComponent.IsBuildable)
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

    
}
