using UnityEngine;
using System.Collections;
using System;

public class ViewManager : MonoBehaviour {

	# region private
	[SerializeField]private VisualStyle currentStyle = 0;
	# endregion

	# region public
	public VisualStyle CurrentStyle{get{ return currentStyle;}}
	public event Action<string> chStyle;
	public static ViewManager instance;
	public static ViewManager Instance {
		get {
			if (instance == null) {
				ViewManager[] instances = FindObjectsOfType<ViewManager>();
				foreach (ViewManager n in instances)
					Destroy(n);
			}
			if (instance == null) {
				instance = ElementManager.Instance.gameObject.AddComponent<ViewManager> ();
			}
			return instance;
		}
	}
	# endregion
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void ChangeStyle(VisualStyle style){
		if (currentStyle != style) {
			currentStyle = style;
			if (chStyle!= null)
				chStyle(style.ToString());
		}
	}
}
public enum VisualStyle{
	real = 0,
	child,
	schematic
}
