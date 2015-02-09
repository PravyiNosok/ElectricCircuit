using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ToolBoxView : MonoBehaviour {
	[SerializeField] private UISprite mainImage;
	[SerializeField] private List<UIButton> buttons;
	[SerializeField] private Transform center;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void AddElement(string prefabName){
		GameObject additional = Resources.Load<GameObject> (string.Format ("Prefabs/{0}", prefabName));
		if (additional == null)
						return;
		additional = (GameObject)Instantiate(additional);
		if (center!=null)additional.transform.parent = center;
		additional.transform.localScale = Vector3.one;
	}
}
