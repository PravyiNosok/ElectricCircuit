using UnityEngine;
using System.Collections;

public class MainMenuController : MonoBehaviour {
	public ISection curentActiveSection;
	private bool menuOn = false;
    public static MainMenuController instance;
	public static MainMenuController Instance{
        get {
            if (instance == null)
                instance = FindObjectOfType<MainMenuController>();
            if (instance == null)
                    instance = new GameObject("mainmenu").AddComponent<MainMenuController>(); 
            return instance;}
    }
	[SerializeField] private GameObject menuSections;
	[SerializeField] private float openSpeed = 1;
	public void Init(){
		if (menuSections != null) {
			menuSections.transform.localScale = Vector3.one * 0.1f;
			menuOn = false;
			menuSections.gameObject.SetActive (false);
		}
	}
	// Use this for initialization
	public void SetState(ISection sect,bool state){
		if (curentActiveSection != null && sect != curentActiveSection) {
			StartCoroutine(curentActiveSection.Hide());
		}
		if (state)
			curentActiveSection = sect;
		else
			curentActiveSection = null;
	}
	// Update is called once per frame
	void Update () {
	
	}
	public void OnClick(){
		menuOn = !menuOn;
		StopAllCoroutines ();
		if (menuOn) {
						menuSections.SetActive (true);
						StartCoroutine (TurnOnMenu (1));
		}else
			StartCoroutine (TurnOnMenu(0.1f));
	}
	private IEnumerator TurnOnMenu(float addition){
		if (curentActiveSection != null){
			StartCoroutine(curentActiveSection.Hide());
		}
		curentActiveSection = null;
		if (menuSections == null)yield break;
		float scale = menuSections.transform.localScale.y;
		while (Mathf.Abs(scale-addition)>0.02) {
			scale = Mathf.Lerp(scale,addition,10*Time.deltaTime);
			menuSections.transform.localScale =new Vector3(1.0f,scale,1.0f);
			yield return null;
		}
		menuSections.transform.localScale =new Vector3(1.0f,addition,1.0f);
		menuSections.SetActive (menuOn);
	}
}
