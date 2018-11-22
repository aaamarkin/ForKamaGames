using UnityEngine;
using UnityEngine.EventSystems;

public class UIPanelView : AppElement {

	private ScrollPanelView _scrollPanelView;
	public ScrollPanelView ScrollPanelView {get { return _scrollPanelView; }}
	
		
	private CenterToCompareView _centerToCompareView;
	public CenterToCompareView CenterToCompareView {get { return _centerToCompareView; }}
	
	private Animator _animator;
	public Animator Animator {get { return _animator; }}
	
	override public void LastInAwake() {

		_scrollPanelView = GetComponentInChildren<ScrollPanelView> ();
		_centerToCompareView = GetComponentInChildren<CenterToCompareView>();
		_animator = GetComponent<Animator>();
		
	}
}
