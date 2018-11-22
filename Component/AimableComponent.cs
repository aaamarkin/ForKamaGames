using UnityEngine;

public class AimableComponent : AppElement
{
	public Material highlitedMaterial;
	public Renderer _renderer;
	
	private Material defaultMaterial;
	
//	private bool isAimed;

	
	// Use this for initialization
	public override void LastInAwake()
	{
		if (_renderer == null)
		{
			_renderer = GetComponent<Renderer>();
		}
		
		defaultMaterial = _renderer.materials[0];
//		isAimed = false;


	}

	public void HighlightMaterial()
	{
		Material[] mats = _renderer.materials;
		mats[0] = highlitedMaterial; 
		_renderer.materials = mats;
	}

	public void UnhighlightMaterial()
	{
		Material[] mats = _renderer.materials;
		mats[0] = defaultMaterial; 
		_renderer.materials = mats;
	}
}
