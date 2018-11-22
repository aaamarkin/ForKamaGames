using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class LineRendererView : AppElement
{

	[HideInInspector]
	public TrailRenderer TrailRenderer;


	public override void LastInAwake()
	{
		TrailRenderer = GetComponentInChildren<TrailRenderer>();
		
	}
	
	
}
