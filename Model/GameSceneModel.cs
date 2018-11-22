using System.Collections.Generic;
using UnityEngine;

public class GameSceneModel : AppElement {

	private GroundRowModel _groundRowModel;
	public GroundRowModel GroundRowModel {get { return _groundRowModel; }}
	private CharacterRowModel _characterRowModel;
	public CharacterRowModel CharacterRowModel {get { return _characterRowModel; }}
	private LineRendererRowModel _lineRendererRowModel;
	public LineRendererRowModel LineRendererRowModel {get { return _lineRendererRowModel; }}
	private VisualEffectsRowModel _visualEffectsRowModel;
	public VisualEffectsRowModel VisualEffectsRowModel {get { return _visualEffectsRowModel; }}
	


	public bool complexCharacterJump;
	
	public bool useForceForObjects;
	
	public bool copyGameScene;

	public Material GameSceneCopyMaterial;

	public Vector3 CharacterLeftDownJumpForce = new Vector3(-5, -200, 0);
	
	public Vector3 CharacterRightDownJumpForce = new Vector3(5, -200, 0);
	
	public Vector3 CharacterLeftUpJumpForce = new Vector3(-100, 200, 0);
	
	public Vector3 CharacterRightUpJumpForce = new Vector3(100, 200, 0);
	
	[HideInInspector]
	public float rotatableComponentVelocity;
	
	[HideInInspector]
	public bool isCharacterJumping;
	
	[HideInInspector]
	public bool runAllMovingComponents;
	
	[HideInInspector]
	public bool isOriginalGameScene;
	
	[HideInInspector]
	public Vector3 manipulatorDelta;
	
	[HideInInspector]
	public int StarsCollected;
	
	[HideInInspector]
	public int FlyingBlocksHit;
	
	public int CharacterNumOfJumpsAvailable;
	
	public int CharacterNumOfThrowsAvailable;
	
	public int CharacterPointNumber;
	
	[HideInInspector]
	public int MaxScore;
	
	[HideInInspector]
	public int TimePlayRemained;
	
	public int TimePlayTotal;
	
	[HideInInspector]
	public bool HitBasketBoard;
	
	[HideInInspector]
	public bool ShouldCountGoal;
	
	[HideInInspector]
	public Dictionary<int, MovingOnSceneComponent> MovingOnSceneComponents;
	
	override public void LastInAwake() {

		_groundRowModel = GetComponentInChildren<GroundRowModel>();
		_characterRowModel = GetComponentInChildren<CharacterRowModel>();
		_lineRendererRowModel = GetComponentInChildren<LineRendererRowModel>();
		_visualEffectsRowModel = GetComponentInChildren<VisualEffectsRowModel>();

		MovingOnSceneComponents = new Dictionary<int, MovingOnSceneComponent>();

	}

}
