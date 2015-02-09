using UnityEngine;
using System.Collections;
public class Element : MonoBehaviour {
	#region Private variables
		[SerializeField]protected Node[] contacts;
        [SerializeField]protected float ElementValue;
		[SerializeField]private UISprite[] Images = new UISprite[1];
        [SerializeField]protected float current = 0;
        protected ViewManager view;
        protected GameObject model;
	#endregion
	public Node[] Contacts{	get{return contacts;}}
	// Use this for initialization
	void Awake () {
		ViewManager.Instance.chStyle += ChangeIcon;
        ChangeIcon(ViewManager.Instance.CurrentStyle.ToString());
	}
	void OnDestroy(){
        ViewManager.Instance.chStyle -= ChangeIcon;
	}
	public virtual void ChangeIcon(string atlasName){
		if (Images == null || Images.Length <1)
			return;
		UIAtlas newAtlas = Resources.Load<UIAtlas> (string.Format ("Atlases/{0}", atlasName));
		if (newAtlas == null) return;
		foreach (UISprite img in Images) {
			if  (img != null){
				img.atlas = newAtlas;
                img.MakePixelPerfect();
			}
		}
	}
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime ==5){

		}else if(Time.deltaTime == 3){

		}else{

		}
	}
	private void procces(){

	}
	public virtual void  Show(){

	}
    public void Select() { 
    
    }
    public void Deselect() { 
    
    }
    public virtual void ConnectNode(Node node) { 
        
    }
    public virtual void Move(Vector3 position) {
        if (contacts != null) {
            foreach (Node node in contacts) {
                if (node != null)
                {
                    node.Move();
                }
            }
        }
    }

	public void ChangeElementView(){
		if (model != null)
			Destroy (model);
	//	SetModel
	}
}
public enum ElementType{
	battery = 0,
	lamp = 1,
	resistor = 2,
	capacitor = 3,
	inductor =4
}
