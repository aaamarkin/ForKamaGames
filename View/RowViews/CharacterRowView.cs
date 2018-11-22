using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRowView : AppElement {
	
	private CharacterView _characterView;
	public CharacterView CharacterView {get { return _characterView; }}
	private BasketRingView _basketRingView;
	public BasketRingView BasketRingView {get { return _basketRingView; }}
	private FlyingBlocksView _flyingBlocksView;
	public FlyingBlocksView FlyingBlocksView {get { return _flyingBlocksView; }}

	[HideInInspector]
	public RightBorderView RightBorderView;
	
	[HideInInspector]
	public LeftBorderView LeftBorderView;
	
	// Use this for initialization
	override public void LastInAwake() 
	{
	
		LeftBorderView = GetComponentInChildren<LeftBorderView>();
		RightBorderView = GetComponentInChildren<RightBorderView>();
		_characterView = GetComponentInChildren<CharacterView>();
		_basketRingView = GetComponentInChildren<BasketRingView>();
		_flyingBlocksView = GetComponentInChildren<FlyingBlocksView>();
	}
}
