using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Base class for all elements in this application.
public class AppElement : MonoBehaviour
{
	[HideInInspector]
	public GUIStyle guiStyle;
	
	private App application;

	void Awake(){
		
		application = GameObject.FindObjectOfType<App>();
		
		guiStyle = new GUIStyle();
		guiStyle.fontSize = 25;
		
		LastInAwake();
	}

	public virtual void LastInAwake ()
	{
	}

	// Gives access to the application and all instances.
	public App app { get { return application; }}


	public T TryGetComponentEverywhere<T>()
	{
		var component = GetComponent<T>();
		if (component == null)
		{
			component = GetComponentInChildren<T>();
		}
		if (component == null)
		{
			component = GetComponentInParent<T>();
		}
		return component;
	}
	
	public T TryGetComponentFromNeigboors<T>()
	{
		var component = transform.parent.gameObject.GetComponentInChildren<T>();
		return component;
	}
	
}

// Audio base class for AudioController and all AudioComponents
public class AudioElement : AppElement
{

	
}

// Root class for easy access like app.Controller.SomeController or app.Model.SpecificModel
public class App : MonoBehaviour {

	// static reference to access GameMaster instance
	public static App app;
	
	private Controller controller;
	private Model model;
	private View view;

	public Controller Controller {get { return controller; }}
	public Model Model {get { return model; }}
	public View View {get { return view; }}

	void Awake() {
		// Checking if GameMaster is instantiated.
		
		if (app == null) {
			app = GameObject.FindObjectOfType<App>();
		}
		controller = GetComponentInChildren<Controller> ();
		model = GetComponentInChildren<Model> ();
		view = GetComponentInChildren<View> ();
		
	}
	
}

[System.Serializable]
public class Tuple<T1, T2>
{
	public T1 first;
	public T2 second;

	private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
	private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;

	public Tuple(T1 first, T2 second)
	{
		this.first = first;
		this.second = second;
	}

	public override string ToString()
	{
		return string.Format("<{0}, {1}>", first, second);
	}

	public static bool operator ==(Tuple<T1, T2> a, Tuple<T1, T2> b)
	{
		if (Tuple<T1, T2>.IsNull(a) && !Tuple<T1, T2>.IsNull(b))
			return false;

		if (!Tuple<T1, T2>.IsNull(a) && Tuple<T1, T2>.IsNull(b))
			return false;

		if (Tuple<T1, T2>.IsNull(a) && Tuple<T1, T2>.IsNull(b))
			return true;

		return
			a.first.Equals(b.first) &&
			a.second.Equals(b.second);
	}

	public static bool operator !=(Tuple<T1, T2> a, Tuple<T1, T2> b)
	{
		return !(a == b);
	}

	public override int GetHashCode()
	{
		int hash = 17;
		hash = hash * 23 + first.GetHashCode();
		hash = hash * 23 + second.GetHashCode();
		return hash;
	}

	public override bool Equals(object obj)
	{
		var other = obj as Tuple<T1, T2>;
		if (object.ReferenceEquals(other, null))
			return false;
		else
			return Item1Comparer.Equals(first, other.first) &&
			       Item2Comparer.Equals(second, other.second);
	}

	private static bool IsNull(object obj)
	{
		return object.ReferenceEquals(obj, null);
	}
}

[System.Serializable]
public class Tuple<T1, T2, T3>
{
	public T1 first;
	public T2 second;
	public T3 third;

	private static readonly IEqualityComparer<T1> Item1Comparer = EqualityComparer<T1>.Default;
	private static readonly IEqualityComparer<T2> Item2Comparer = EqualityComparer<T2>.Default;
	private static readonly IEqualityComparer<T3> Item3Comparer = EqualityComparer<T3>.Default;

	public Tuple(T1 first, T2 second, T3 third)
	{
		this.first = first;
		this.second = second;
		this.third = third;
	}

	public override string ToString()
	{
		return string.Format("<{0}, {1}, {2}>", first, second, third);
	}

	public static bool operator ==(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
	{
		if (Tuple<T1, T2, T3>.IsNull(a) && !Tuple<T1, T2, T3>.IsNull(b))
			return false;

		if (!Tuple<T1, T2, T3>.IsNull(a) && Tuple<T1, T2, T3>.IsNull(b))
			return false;

		if (Tuple<T1, T2, T3>.IsNull(a) && Tuple<T1, T2, T3>.IsNull(b))
			return true;

		return
			a.first.Equals(b.first) &&
			a.second.Equals(b.second);
	}

	public static bool operator !=(Tuple<T1, T2, T3> a, Tuple<T1, T2, T3> b)
	{
		return !(a == b);
	}

	public override int GetHashCode()
	{
		int hash = 17;
		hash = hash * 23 + first.GetHashCode();
		hash = hash * 23 + second.GetHashCode();
		hash = hash * 23 + third.GetHashCode();
		return hash;
	}

	public override bool Equals(object obj)
	{
		var other = obj as Tuple<T1, T2, T3>;
		if (object.ReferenceEquals(other, null))
			return false;
		else
			return Item1Comparer.Equals(first, other.first) &&
			       Item2Comparer.Equals(second, other.second) &&
			       Item3Comparer.Equals(third, other.third);
	}

	private static bool IsNull(object obj)
	{
		return object.ReferenceEquals(obj, null);
	}
}