using UnityEngine.UI;

public class NumOfThrowsCounterView : AppElement
{
	public Text Text;
	
	// Use this for initialization
	void Start ()
	{
		Text = GetComponentInChildren<Text>();
		Text.text = app.Model.GameSceneModel.CharacterNumOfThrowsAvailable.ToString();
	}
}
