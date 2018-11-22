using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;


public class ARAnchorManager 
{

	private Dictionary<string, ARPlaneAnchorGameObject> planeAnchorMap;
	private Dictionary<float, string> planeSizeMap;

//	private bool initRoom;

	public ARAnchorManager (bool initializqRoom)
	{
		
		planeAnchorMap = new Dictionary<string, ARPlaneAnchorGameObject> ();
		planeSizeMap = new Dictionary<float, string> ();
		UnityARSessionNativeInterface.ARAnchorAddedEvent += AddAnchor;
		UnityARSessionNativeInterface.ARAnchorUpdatedEvent += UpdateAnchor;
		UnityARSessionNativeInterface.ARAnchorRemovedEvent += RemoveAnchor;

//		initRoom = initializqRoom;

	}


	public void AddAnchor(ARPlaneAnchor arPlaneAnchor)
	{
		planeSizeMap.Add(arPlaneAnchor.extent.x * arPlaneAnchor.extent.z, arPlaneAnchor.identifier);
		
		
		GameObject go = UnityARUtility.CreatePlaneInScene (arPlaneAnchor, IsBiggestPlane(arPlaneAnchor));
		

		go.AddComponent<DontDestroyOnLoad> ();  //this is so these GOs persist across scene loads
		ARPlaneAnchorGameObject arpag = new ARPlaneAnchorGameObject ();
		arpag.planeAnchor = arPlaneAnchor;
		arpag.gameObject = go;
		planeAnchorMap.Add (arPlaneAnchor.identifier, arpag);


//		if (IsBiggestPlane(arPlaneAnchor) && initRoom)
//		{
//			GameObject room = UnityARRoomUtility.CreateRoomInScene(arPlaneAnchor);
//		}

	}

	public void RemoveAnchor(ARPlaneAnchor arPlaneAnchor)
	{
		if (planeAnchorMap.ContainsKey (arPlaneAnchor.identifier)) {
			ARPlaneAnchorGameObject arpag = planeAnchorMap [arPlaneAnchor.identifier];
			GameObject.Destroy (arpag.gameObject);
			planeAnchorMap.Remove (arPlaneAnchor.identifier);

			float sizeToRemove = -1f;
			foreach (float size in planeSizeMap.Keys)
			{
				if (planeSizeMap[size].Equals(arPlaneAnchor.identifier))
				{
					sizeToRemove = size;
				}
			}
			if (planeSizeMap.ContainsKey(sizeToRemove))
			{
				planeSizeMap.Remove(sizeToRemove);
			}
		}
	}

	public void UpdateAnchor(ARPlaneAnchor arPlaneAnchor)
	{
		if (planeAnchorMap.ContainsKey (arPlaneAnchor.identifier)) {
			
			float sizeToUpdate = -1f;
			foreach (float size in planeSizeMap.Keys)
			{
				if (planeSizeMap[size].Equals(arPlaneAnchor.identifier))
				{
					sizeToUpdate = size;
				}
			}
			if (planeSizeMap.ContainsKey(sizeToUpdate))
			{
				planeSizeMap.Remove(sizeToUpdate);
				planeSizeMap.Add(arPlaneAnchor.extent.x * arPlaneAnchor.extent.z, arPlaneAnchor.identifier);
			}
			
			ARPlaneAnchorGameObject arpag = planeAnchorMap [arPlaneAnchor.identifier];
	
			UnityARUtility.UpdatePlaneWithAnchorTransform (arpag.gameObject, arPlaneAnchor, IsBiggestPlane(arPlaneAnchor));
			
			planeAnchorMap [arPlaneAnchor.identifier] = arpag;
		}
	}

	public void Destroy()
	{
		foreach (ARPlaneAnchorGameObject arpag in GetCurrentPlaneAnchors()) {
			GameObject.Destroy (arpag.gameObject);
		}

		planeAnchorMap.Clear ();
		planeSizeMap.Clear();
	}

	public List<ARPlaneAnchorGameObject> GetCurrentPlaneAnchors()
	{
		return planeAnchorMap.Values.ToList ();
	}

	private bool IsBiggestPlane(ARPlaneAnchor arPlaneAnchor)
	{
		var list = planeSizeMap.Keys.ToList();

		float maxVal = float.MinValue;
		
		foreach (float size in list)
		{
			if (size > maxVal)
			{
				maxVal = size;
			}
		}

		if (planeSizeMap.ContainsKey(maxVal) && planeSizeMap[maxVal].Equals(arPlaneAnchor.identifier))
		{
			return true;
		}
		else
		{
			return false;
		}
	}
	
}

public class GeneratePlaneController : AppElement {

	public GameObject planePrefab;
	public GameObject roomPrefab;
	
	private ARAnchorManager unityARAnchorManager;
	
	

	
	private GUIStyle style;

	// Use this for initialization
	void Start () {

		if (app.Model.CameraModel.useARCamera && app.Model.GeneratePlaneModel.generatePlanes)
		{
			unityARAnchorManager = new ARAnchorManager(app.Model.GeneratePlaneModel.initializeRoomPrefab);
			UnityARUtility.InitializePlanePrefab (planePrefab);
			UnityARRoomUtility.InitializeRoomPrefab(roomPrefab);

		}

	
	}

	void OnDestroy()
	{
		if (app.Model.CameraModel.useARCamera && app.Model.GeneratePlaneModel.generatePlanes)
		{
			unityARAnchorManager.Destroy ();
		}
		
	}

	void Update()
	{
		if (app.Model.CameraModel.useARCamera && app.Model.GeneratePlaneModel.generatePlanes)
		{
			List<ARPlaneAnchorGameObject> arpags = unityARAnchorManager.GetCurrentPlaneAnchors ();
			app.Model.GeneratePlaneModel.PlanesNumber = arpags.Count;
		}

	}
	
	


}
