using UnityEngine.UI;

public class MaxScoreView : AppElement
{
	public Text Text;
	
	// Use this for initialization
	void Start ()
	{
		Text = GetComponentInChildren<Text>();
		Text.text = app.Model.GameSceneModel.MaxScore.ToString();
	}
	
}
