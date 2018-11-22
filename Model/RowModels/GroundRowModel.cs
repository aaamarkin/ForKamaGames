using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRowModel : AppElement {

	public float GroundBlockVelocity = 0.3f;
	
	public float GroundBlockInstantiationPeriod = 0.41f;
	
	public bool IsGroundBlockRunning = false;
	
	public Transform GroundBlock;
	
	[HideInInspector]
	public Dictionary<int, Transform> GroundBlockDictionaryDisabled;
	
	[HideInInspector]
	public Dictionary<int, Transform> GroundBlockDictionaryEnabled;
	
}
