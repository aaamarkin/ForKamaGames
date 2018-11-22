using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRowModel : AppElement {

	public float FlyingBlockVelocity = 0.3f;
	
	public float BasketRingVelocity = 0.3f;
	
	public float instantiationPeriod = 5f;
	
	public bool IsFlyingBlockRunning = false;
	
	public bool IsBasketRingRunning = false;
	
	public bool InstantiateFlyingBlocksPeriodically = false;
	
	public Transform FlyingBlock;
	
	public Transform FailBlock;

	public int blockFrequency;

	public int failBlockFrequency;
	
	public bool IsCharacterGoingUp = false;
	
	public bool IsCharacterTriggeringFlyingBlock = false;
	
	public bool UseZAxeCharacterMoving = false;
	
	[HideInInspector]
	public Dictionary<int, Transform> FlyingBlockDictionaryDisabled;
	[HideInInspector]
	public Dictionary<int, Transform> FailBlockDictionaryDisabled;
	[HideInInspector]
	public Dictionary<int, Transform> BasketRingDictionaryDisabled;
	
	[HideInInspector]
	public Dictionary<int, Transform> FlyingBlockDictionaryEnabled;
	[HideInInspector]
	public Dictionary<int, Transform> FailBlockDictionaryEnabled;
	[HideInInspector]
	public Dictionary<int, Transform> BasketRingDictionaryEnabled;

}