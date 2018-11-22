using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterRowController : AppElement {

	private float time;

	private float totalFrequency;
	
	void Start()
	{
		totalFrequency = app.Model.GameSceneModel.CharacterRowModel.blockFrequency +
		                 app.Model.GameSceneModel.CharacterRowModel.failBlockFrequency;
		
		app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryDisabled = new Dictionary<int, Transform>();
		
		app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryDisabled = new Dictionary<int, Transform>();
		
		app.Model.GameSceneModel.CharacterRowModel.BasketRingDictionaryDisabled = new Dictionary<int, Transform>();
		
		app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryEnabled = new Dictionary<int, Transform>();
		
		app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryEnabled = new Dictionary<int, Transform>();
		
		app.Model.GameSceneModel.CharacterRowModel.BasketRingDictionaryEnabled = new Dictionary<int, Transform>();

		for (int i = 0; i < 20; i++)
		{
			Transform tr = Instantiate(app.Model.GameSceneModel.CharacterRowModel.FlyingBlock, app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
			
			app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryDisabled.Add(tr.gameObject.GetInstanceID(), tr);
			
			tr.gameObject.SetActive(false);
			
			tr = Instantiate(app.Model.GameSceneModel.CharacterRowModel.FailBlock, app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
			
			app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryDisabled.Add(tr.gameObject.GetInstanceID(), tr);
			
			tr.gameObject.SetActive(false);
			
		}
	}

	void FixedUpdate()
	{
		if (app.Model.GameSceneModel.runAllMovingComponents || app.Model.GameSceneModel.CharacterRowModel.InstantiateFlyingBlocksPeriodically)
		{
			float randomVal = Random.value;
		
			time += Time.fixedDeltaTime;
		
			if (time >= app.Model.GameSceneModel.CharacterRowModel.instantiationPeriod)
			{
				if (randomVal <= app.Model.GameSceneModel.CharacterRowModel.failBlockFrequency / totalFrequency )
				{
					if (app.Model.GameSceneModel.isOriginalGameScene)
					{
						EnableFailBlock(app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
					}
					else
					{
						EnableFailBlock(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
					}
					
				} else if (randomVal <= app.Model.GameSceneModel.CharacterRowModel.blockFrequency / totalFrequency) {
					if (app.Model.GameSceneModel.isOriginalGameScene)
					{
						EnableFlyingBlock(app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);

					}
					else
					{
						EnableFlyingBlock(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
					}
					
				}  
			
			time = 0;
			}
		}
		
		if (app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity.sqrMagnitude > 0)
		{
			if (app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity.y > 0)
			{
				if (!app.Model.GameSceneModel.CharacterRowModel.IsCharacterGoingUp)
				{
					if (app.Model.GameSceneModel.CharacterRowModel.UseZAxeCharacterMoving)
					{
						SetCharacterUpJumpZPos();
					}

					app.Model.GameSceneModel.CharacterRowModel.IsCharacterGoingUp = true;
				}
				
			}
			else if (app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.velocity.y < 0  &&
			         !app.Model.GameSceneModel.CharacterRowModel.IsCharacterTriggeringFlyingBlock)
			{
				if (app.Model.GameSceneModel.CharacterRowModel.IsCharacterGoingUp)
				{
					if (app.Model.GameSceneModel.CharacterRowModel.UseZAxeCharacterMoving)
					{
						SetCharacterDefaultZPos();
					}
					
					app.Model.GameSceneModel.CharacterRowModel.IsCharacterGoingUp = false;
				}
				
			}
		}
	}

	public Transform InstantiateFlyingBlock()
	{
		Transform tr;
		
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			tr = EnableFlyingBlock(app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
		}
		else
		{
			tr = EnableFlyingBlock(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
		}
		if (!app.Model.GameSceneModel.isOriginalGameScene)
		{
			foreach (var renderer in tr.GetComponentsInChildren<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
			
			foreach (var renderer in tr.GetComponents<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
		}
		return tr;
	}
	
	public Transform InstantiateFailBlock()
	{
		Transform tr;
		
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			tr = EnableFailBlock(app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
		}
		else
		{
			tr = EnableFailBlock(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
		}
		if (!app.Model.GameSceneModel.isOriginalGameScene)
		{
			foreach (var renderer in tr.GetComponentsInChildren<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
			
			foreach (var renderer in tr.GetComponents<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
		}
		return tr;
	}
	
	public Transform InstantiateBasketRing()
	{
		Transform tr;
		
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			tr = EnableBasketRing(app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
		}
		else
		{
			tr = EnableBasketRing(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position, Quaternion.identity);
		}
		if (!app.Model.GameSceneModel.isOriginalGameScene)
		{
			foreach (var renderer in tr.GetComponentsInChildren<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
			
			foreach (var renderer in tr.GetComponents<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
		}
		return tr;
	}
	
	public Transform InstantiateFlyingBlock(Quaternion rotation, float height)
	{
		Vector3 position;
		
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			position = new Vector3(app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position.x, height,
				app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position.z);
		}
		else
		{
			Debug.LogError("Not supported yet");
			position = new Vector3(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position.x, height,
				app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position.z);
		}
		
		Transform tr = EnableFlyingBlock(position, rotation);
		
		if (!app.Model.GameSceneModel.isOriginalGameScene)
		{
			foreach (var renderer in tr.GetComponentsInChildren<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
			foreach (var renderer in tr.GetComponents<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			} 
		}
		return tr;
	}
	
	public Transform InstantiateFailBlock(Quaternion rotation, float height)
	{
		Vector3 position;
		
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			position = new Vector3(app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position.x, height,
				app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position.z);
		}
		else
		{
			Debug.LogError("Not supported yet");
			position = new Vector3(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position.x, height,
				app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position.z);
		}
		
		Transform tr = EnableFailBlock(position, rotation);
		
		if (!app.Model.GameSceneModel.isOriginalGameScene)
		{
			foreach (var renderer in tr.GetComponentsInChildren<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
			foreach (var renderer in tr.GetComponents<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			} 
		}
		return tr;
	}
	
	public Transform InstantiateBasketRing(Quaternion rotation, float height)
	{
		Vector3 position;
		
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			position = new Vector3(app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position.x, height,
				app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position.z);
		}
		else
		{
			Debug.LogError("Not supported yet");
			position = new Vector3(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position.x, height,
				app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position.z);
		}
		
		Transform tr = EnableBasketRing(position, rotation);
		
		if (!app.Model.GameSceneModel.isOriginalGameScene)
		{
			foreach (var renderer in tr.GetComponentsInChildren<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
			foreach (var renderer in tr.GetComponents<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			} 
		}
		return tr;
	}
	
	public Transform InstantiateFlyingBlock(float width, float height)
	{
		Vector3 position;
		
		if (app.Model.GameSceneModel.isOriginalGameScene)
		{
			position = new Vector3(width, height,
				app.View.GameSceneView.CharacterRowView.RightBorderView.transform.position.z);
		}
		else
		{
			Debug.LogError("Not supported yet");
			position = new Vector3(app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position.x, height,
				app.View.TechLevelView.KSScreenView.GameSceneCopyView.CharacterRowView.RightBorderView.transform.position.z);
		}
		
		Transform tr = EnableFlyingBlock(position, Quaternion.identity);
		
		if (!app.Model.GameSceneModel.isOriginalGameScene)
		{
			foreach (var renderer in tr.GetComponentsInChildren<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			}
			foreach (var renderer in tr.GetComponents<Renderer>())
			{
				Material[] mats = renderer.materials;
				mats[0] = app.Model.GameSceneModel.GameSceneCopyMaterial; 
				renderer.materials = mats;
			} 
		}
		return tr;
	}

	private Transform EnableFlyingBlock(Vector3 position, Quaternion rotation)
	{
		Transform tr = app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryDisabled.First().Value;
		
		app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryEnabled.Add(tr.gameObject.GetInstanceID(), tr);
		
		app.Model.GameSceneModel.MovingOnSceneComponents.Add(tr.gameObject.GetInstanceID(), tr.GetComponent<MovingOnSceneComponent>());

		app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryDisabled.Remove(tr.gameObject.GetInstanceID());

		tr.position = position;

		tr.rotation = rotation;
		
		tr.gameObject.SetActive(true);
		
		return tr;
	}
	
	private Transform EnableFailBlock(Vector3 position, Quaternion rotation)
	{
		Transform tr = app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryDisabled.First().Value;
		
		app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryEnabled.Add(tr.gameObject.GetInstanceID(), tr);
		
		app.Model.GameSceneModel.MovingOnSceneComponents.Add(tr.gameObject.GetInstanceID(), tr.GetComponent<MovingOnSceneComponent>());

		app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryDisabled.Remove(tr.gameObject.GetInstanceID());

		tr.position = position;

		tr.rotation = rotation;
		
		tr.gameObject.SetActive(true);
		
		return tr;
	}
	
	private Transform EnableBasketRing(Vector3 position, Quaternion rotation)
	{
		Transform tr = app.Model.GameSceneModel.CharacterRowModel.BasketRingDictionaryDisabled.First().Value;
		
		app.Model.GameSceneModel.CharacterRowModel.BasketRingDictionaryEnabled.Add(tr.gameObject.GetInstanceID(), tr);
		
		app.Model.GameSceneModel.MovingOnSceneComponents.Add(tr.gameObject.GetInstanceID(), tr.GetComponent<MovingOnSceneComponent>());

		app.Model.GameSceneModel.CharacterRowModel.BasketRingDictionaryDisabled.Remove(tr.gameObject.GetInstanceID());

		tr.position = position;

		tr.rotation = rotation;
		
		tr.gameObject.SetActive(true);
		
		return tr;
	}
	
	private void SetCharacterUpJumpZPos()
	{
//		print("SetCharacterUpJumpZPos");
		Vector3 pos = new Vector3(app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position.x,
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position.y,
			app.View.GameSceneView.CharacterRowView.CharacterView.OriginalZPosition + 0.2f);
		app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.MovePosition(pos);
	}
	
	private void SetCharacterDefaultZPos()
	{
//		print("SetCharacterDefaultZPos");
		Vector3 pos = new Vector3(app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position.x,
			app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.position.y,
			app.View.GameSceneView.CharacterRowView.CharacterView.OriginalZPosition);
		app.View.GameSceneView.CharacterRowView.CharacterView.Rigidbody.MovePosition(pos);
	}

	public void RemoveAllMovingComponents()
	{
		foreach (var value in app.Model.GameSceneModel.MovingOnSceneComponents.Values)
		{
			if (app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryEnabled.ContainsKey(value.gameObject
				.GetInstanceID()))
			{
				app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryDisabled.Add(value.gameObject.GetInstanceID(), value.transform);

				app.Model.GameSceneModel.CharacterRowModel.FlyingBlockDictionaryEnabled.Remove(value.gameObject.GetInstanceID());
			}
			
			if (app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryEnabled.ContainsKey(value.gameObject
				.GetInstanceID()))
			{
				app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryDisabled.Add(value.gameObject.GetInstanceID(), value.transform);

				app.Model.GameSceneModel.CharacterRowModel.FailBlockDictionaryEnabled.Remove(value.gameObject.GetInstanceID());
			}
			
			if (app.Model.GameSceneModel.CharacterRowModel.BasketRingDictionaryEnabled.ContainsKey(value.gameObject
				.GetInstanceID()))
			{
				app.Model.GameSceneModel.CharacterRowModel.BasketRingDictionaryDisabled.Add(value.gameObject.GetInstanceID(), value.transform);

				app.Model.GameSceneModel.CharacterRowModel.BasketRingDictionaryEnabled.Remove(value.gameObject.GetInstanceID());
			}
			
			value.gameObject.SetActive(false);
		}
		
		app.Model.GameSceneModel.MovingOnSceneComponents = new Dictionary<int, MovingOnSceneComponent>();
	}

}
