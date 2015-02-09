using UnityEngine;
using System.Collections;

public class ISection : MonoBehaviour {
	private MainMenuController MC;
	private bool ready = false;
	[SerializeField] private Transform anchor;
	// Use this for initialization
	void Start () {
		MC = MainMenuController.Instance;
		if (anchor != null) {
			anchor.localScale = Vector3.one*0.1f;
			anchor.gameObject.SetActive(false);
		}
	}
	public void OnEnable(){
		if (anchor == null)
			return;
	}
	public void OnClick(){
		MC.SetState (this, !ready);
		if (ready) {
			StartCoroutine (Hide ());
		} else {
			StartCoroutine (Show ());
		}
	}
	public IEnumerator Hide(){
		ready = false;
		if (anchor == null)
			yield break;
		while (anchor.localScale.x >0.2f){
			anchor.localScale =Vector3.one*(Mathf.Lerp(anchor.localScale.x,0.05f,10*Time.deltaTime));
			yield return null;
		}
		anchor.gameObject.SetActive(false);
	}
	public IEnumerator Show(){
		ready = true;
		if (anchor == null)
			yield break;
		anchor.gameObject.SetActive(true);
		while (anchor.localScale.x < 0.99){
			anchor.localScale = Vector3.one*(Mathf.Lerp(anchor.localScale.x,1,10*Time.deltaTime));
			yield return null;
		}
	}
}
