using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockWheelsComponent : AppElement {

	public override void LastInAwake()
	{
		foreach (WheelCollider w in GetComponentsInChildren<WheelCollider>()) 
			w.motorTorque = 0.000001f;
           
	}
}
