using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CameraTestComponent : MonoBehaviour
{

	public bool test;

	[Range(1,6)]
	public float speed;
	
	private float timeCounterC;
	private Vector3 currentPos;
	
	// Use this for initialization
	void Start ()
	{
		speed = 1;
		timeCounterC = 0;
		currentPos = transform.position;
		
//		print("Current = " + currentPos);
		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (test)
		{
			
			float x = Mathf.Sin(timeCounterC) / 5f;
			float y = Mathf.Cos(timeCounterC) / 5f;
//			float z = Mathf.Sin(timeCounterC) / 5f;

//			Vector3 newPos  = new Vector3(transform.position.x, currentPos.y + y, currentPos.z + z);
			
			transform.position = new Vector3(currentPos.x + x, currentPos.y + y, transform.position.z);

			timeCounterC += Time.deltaTime * speed;
		}
				
	}
}

