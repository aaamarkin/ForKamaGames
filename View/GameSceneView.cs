using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneView : AppElement {

	private GroundRowView _groundRowView;
	public GroundRowView GroundRowView {get { return _groundRowView; }}
	private CharacterRowView _characterRowView;
	public CharacterRowView CharacterRowView {get { return _characterRowView; }}
	private LineRendererRowView _lineRendererRowView;
	public LineRendererRowView LineRendererRowView {get { return _lineRendererRowView; }}
	private VisualEffectsRowView _visualEffectsRowView;
	public VisualEffectsRowView VisualEffectsRowView {get { return _visualEffectsRowView; }}
	

	
	override public void LastInAwake() {

		_groundRowView = GetComponentInChildren<GroundRowView>();
		_characterRowView = GetComponentInChildren<CharacterRowView>();
		_lineRendererRowView = GetComponentInChildren<LineRendererRowView>();
		_visualEffectsRowView = GetComponentInChildren<VisualEffectsRowView>();


	}
	
}
