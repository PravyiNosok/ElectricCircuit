using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
[ExecuteInEditMode]
public class LocalLan : MonoBehaviour {
	private Dictionary<string,Text> _74234826;
	private void GetAllElements (){
		_74234826 = new Dictionary<string,Text> ();
		Text[] _2367 = FindObjectsOfType<Text> ();
		foreach (Text _98 in _2367){
			if (!string.IsNullOrEmpty(_98.text))
				_74234826[_98.text] = _98;
			print (_98.text);
		}
		print (_2367.Length);
	}
#if UNITY_EDITOR 
	void Awake(){
		GetAllElements ();
	}
#endif
	// Use this for initialization
	void Start () {
		GetAllElements ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
