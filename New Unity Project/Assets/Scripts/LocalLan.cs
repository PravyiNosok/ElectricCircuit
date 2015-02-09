using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
[ExecuteInEditMode]
public class LocalLan : MonoBehaviour {
	private Dictionary<string,UILabel> _74234826;
    private Dictionary<string, TextAsset> _72348829;
    private string currentLanguage = "En";
	private void GetAllElements (){
        _74234826 = new Dictionary<string, UILabel>();
        UILabel[] _2367 = FindObjectsOfType<UILabel>();
        foreach (UILabel _98 in _2367)
        {
			if (!string.IsNullOrEmpty(_98.name))
				_74234826[_98.name] = _98;
            Debug.Log(_98.name);
		}
        string pref = PlayerPrefs.GetString("lan");
        if (!string.IsNullOrEmpty(pref)) {
            currentLanguage = pref;
        }
        _72348829 = new Dictionary<string, TextAsset>();
        SetLan(currentLanguage);
	}
	public void Init(){
		GetAllElements ();
	}
    public void SetLan(string _8942) { 
        TextAsset _tx;
        if (!_72348829.ContainsKey(_8942)) {
            _tx = Resources.Load<TextAsset>(string.Format("Localization/{0}", _8942));
            if (_tx != null)
                _72348829[_8942] = _tx;
        }else{
            _tx = _72348829[_8942];
        }

        if (_tx == null) return;
        string[] texts = _tx.text.Split("\n"[0]);
        foreach (string newOne in texts) {
            if (string.IsNullOrEmpty(newOne))
                continue;
            string label = newOne.Split("|"[0])[0];
            if (!_74234826.ContainsKey(label))
                continue;
            if (newOne.Length <= label.Length + 1)
                continue;
            string sd = newOne.Remove(0, label.Length + 1);
            if (string.IsNullOrEmpty(sd))
                continue;
            _74234826[label].text = sd;

        }
    }
    // Update is called once per frame
	void Update () {
	
	}
}
