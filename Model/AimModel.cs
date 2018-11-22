using System.Collections;
using System.ComponentModel.Design;
using TouchScript.Gestures.TransformGestures;
using UnityEngine;

public class AimModel : AppElement
{

	public Sprite aimSelected;
	public Sprite aimNotSelected;

	private float _screenCenterX;
	private float _screenCenterY;

	private Vector2 _screenPointOfAimaContact;
	
	public Vector2 AimCenter
	{
		get { return new Vector2(app.View.CanvasView.AimView.GetComponent<RectTransform>().position.x, app.View.CanvasView.AimView.GetComponent<RectTransform>().position.y); }
	}
	
	public Vector3 ScreenPointOfAimContact
	{
		get { return _screenPointOfAimaContact; }
		
		set { _screenPointOfAimaContact = value; }
	}
	
	private string _lastAimInstanceID;
	public string LastAimInstanceID
	{
		get { return _lastAimInstanceID; }
		set { _lastAimInstanceID = value; }
	}
		
	private bool _aimSelected;
	public bool AimSelected
	{
		get { return _aimSelected; }
		set { _aimSelected = value; }
	}
	
	private Collider _aimHitCollider;
	public Collider AimHitCollider
	{
		get { return _aimHitCollider; }
		set
		{
			_aimHitCollider = value; 
			_motionCopier = value.GetComponentInParent<MotionCopierComponent>();
			_selectableComponent = value.GetComponentInParent<SelectableComponent>();
			_aimableComponent = value.transform.parent.GetComponentInChildren<AimableComponent>();
	
		}
	}
    
	private MotionCopierComponent _motionCopier;
	public MotionCopierComponent MotionCopierComponent
	{
		get { return _motionCopier; }
	}
    
	private SelectableComponent _selectableComponent;
	public SelectableComponent SelectableComponent
	{
		get { return _selectableComponent; }
	}
	
	private AimableComponent _aimableComponent;
	public AimableComponent AimableComponent
	{
		get { return _aimableComponent; }
	}

	public bool IsSelectableComponentSelected()
	{
		if (_selectableComponent != null)
		{
			return _selectableComponent.IsSelected;
		}
		else
		{
			return false;
		}

	}

	public void SetRotQuaternsToDampedSpringMotionCopier(Quaternion quartanion)
	{
		if (_motionCopier != null)
		{
			_motionCopier.SetRotatableQuartanions(quartanion);
		}

	}

	public void ChangeColorSelectableComponent()
	{
		if (_selectableComponent != null)
		{
			_selectableComponent.ChangeColor();
		}
		
	}

	public void DettachDampedSpringMotionCopier()
	{
		if (_motionCopier != null)
		{
			_motionCopier.Dettach();
		}
		
	}

	public void DeselectSelectableComponent()
	{
		if (_selectableComponent != null)
		{
			_selectableComponent.Deselect();
		}
	}
	
	public void HighlightAimableComponent()
	{
		if (_aimableComponent != null)
		{
			_aimableComponent.HighlightMaterial();
		}
	}


	public void UnhighlightAimableComponent()
	{
		if (_aimableComponent != null)
		{
			_aimableComponent.UnhighlightMaterial();
		}
	}


	public void RemoveAimHitCollider()
	{
		DettachDampedSpringMotionCopier();
		UnhighlightAimableComponent();
		DeselectSelectableComponent();

		_aimHitCollider = null;
		_motionCopier = null;
		_selectableComponent = null;
		_aimableComponent = null;

	}

	public void ResetLastAimInstanceID()
	{
		_lastAimInstanceID = "fakeInstanceId";
	}

	void Start()
	{
		ResetLastAimInstanceID();
	}
}
