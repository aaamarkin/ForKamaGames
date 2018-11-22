using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSubstituesView : AppElement {

	private SubstituteCylinderView _substituteCylinderView;
	public SubstituteCylinderView SubstituteCylinderView {get { return _substituteCylinderView; }}
	private SubstituteCubeView _substituteCubeView;
	public SubstituteCubeView SubstituteCubeView {get { return _substituteCubeView; }}
	private SubstituteSphereView _substituteSphereView;
	public SubstituteSphereView SubstituteSphereView {get { return _substituteSphereView; }}
	
	override public void LastInAwake() {

		_substituteCubeView = GetComponentInChildren<SubstituteCubeView> ();
		_substituteCylinderView = GetComponentInChildren<SubstituteCylinderView> ();
		_substituteSphereView = GetComponentInChildren<SubstituteSphereView> ();
		
	}
}
