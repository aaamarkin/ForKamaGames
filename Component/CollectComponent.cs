using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectComponent : AppElement {
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Collectible")
		{
//			Destroy(other.gameObject);
			
			other.gameObject.GetComponent<MovingOnSceneComponent>().RemoveFromModelAndDestroy();

			app.Model.GameSceneModel.StarsCollected += 1;
		
//			print(app.Model.GameSceneModel.starsCollected);
		}
		
	}
	
}
