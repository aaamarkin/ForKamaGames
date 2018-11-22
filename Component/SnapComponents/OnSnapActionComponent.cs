using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OnSnapActionComponent : AppElement {

	public abstract void OnSnap(SnappableComponent snappableComponent);

	public abstract void OnUnSnap(SnappableComponent snappableComponent);
	
}
