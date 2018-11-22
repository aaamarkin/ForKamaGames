using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BuildingModel : AppElement
{

    private Dictionary<int, ModeChangerComponent> buildComponents;
    private Dictionary<int, Vector3> buildComponentPositions;
    private Dictionary<int, Quaternion> buildComponentRotations;    

    public override void LastInAwake()
    {
        buildComponents = new Dictionary<int, ModeChangerComponent>();
        buildComponentPositions = new Dictionary<int, Vector3>();
        buildComponentRotations = new Dictionary<int, Quaternion>();
    }

    private Vector3 _buildingPosition;
    public Vector3 BuildingPosition
    {
        get { return _buildingPosition; }
        set { _buildingPosition = value; }
    }
    
    private bool _buildModeOn;
    public bool BuildModeOn
    {
        get { return _buildModeOn; }
        set { _buildModeOn = value; }
    }
    
    private bool _showPreview;
    public bool ShowPreview
    {
        get { return _showPreview; }
        set { _showPreview = value; }
    }

    public void AddModeChangerComponent(ModeChangerComponent component)
    {
        buildComponents.Add(component.GetInstanceID(), component);
        buildComponentPositions.Add(component.GetInstanceID(), component.transform.position);
        buildComponentRotations.Add(component.GetInstanceID(), component.transform.rotation);
    }
    
    public void SaveModeChangerComponentRotPos()
    {
        foreach (var buildComponentsKey in buildComponents.Keys)
        {
            ModeChangerComponent mChangComponent;
            buildComponents.TryGetValue(buildComponentsKey, out mChangComponent);
            buildComponentPositions[buildComponentsKey] = mChangComponent.transform.position;
            buildComponentRotations[buildComponentsKey] = mChangComponent.transform.rotation;

        }
        
    }
    
    public void UpdateModeChangerComponentRotPos()
    {
        foreach (var buildComponentsKey in buildComponents.Keys)
        {
            ModeChangerComponent mChangComponent;
            Vector3 savedPosition;
            Quaternion savedRotation;
            
            buildComponents.TryGetValue(buildComponentsKey, out mChangComponent);
            buildComponentPositions.TryGetValue(buildComponentsKey, out savedPosition);
            buildComponentRotations.TryGetValue(buildComponentsKey, out savedRotation);

            mChangComponent.transform.position = savedPosition;
            mChangComponent.transform.rotation = savedRotation;
        }
        
    }

    public List<ModeChangerComponent> AllModeChangerComponents()
    {
        return buildComponents.Values.ToList();
    }
}
