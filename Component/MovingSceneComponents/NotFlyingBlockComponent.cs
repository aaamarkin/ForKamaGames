using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotFlyingBlockComponent : FlyingBlockComponent {

	public override void Move()
	{

	}
	
	public override void IsTouchedByPlayer()
	{
		
		
			ChangeColorToPlayerHit();
		
			
	}
	
	public override void IsNotTouchedByPlayer()
	{
		ChangeColorToDefault();	
	}

	private void ChangeColorToPlayerHit()
	{
		Material[] mat = Renderer.materials;
		mat[0].color = Color.green;
		Renderer.materials = mat;
	}
	
	private void ChangeColorToDefault()
	{
		Material[] mat = Renderer.materials;
		mat[0].color = DefaultColor;
		Renderer.materials = mat;
	}
}
