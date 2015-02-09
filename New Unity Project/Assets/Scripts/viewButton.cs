using UnityEngine;
using System.Collections;

public class viewButton : MonoBehaviour {
    public VisualStyle style = 0;
	// Use this for initialization
    public void OnClick() {
        ViewManager.Instance.ChangeStyle(style);
    }
}
