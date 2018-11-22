using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumOfJumpsCounterView : AppElement
{
	public Text Text;
	
	// Use this for initialization
	void Start ()
	{
		Text = GetComponentInChildren<Text>();
		
//		Text.text = app.Model.GameSceneModel.CharacterNumOfJumpsAvailable.ToString();
		Text.text = app.Model.GameSceneModel.TimePlayRemained.ToString();
	}
}