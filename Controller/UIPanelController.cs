using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanelController : AppElement {

    private ScrollPanelController _scrollPanelController;
    public ScrollPanelController ScrollPanelController {get { return _scrollPanelController; }}
	
    override public void LastInAwake() {

        _scrollPanelController = GetComponentInChildren<ScrollPanelController>();

    }
}
