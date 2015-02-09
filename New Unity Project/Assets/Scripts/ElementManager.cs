using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ElementManager : MonoBehaviour {
	public List<Element> wholeSet;
    public Camera cam;
    private Element activeElement;
	public static ElementManager instance;
	public static ElementManager Instance {
				get {
						if (instance == null) {
							ElementManager[] instances = FindObjectsOfType<ElementManager>();
							foreach (ElementManager n in instances)
							Destroy(n);
						}
						if (instance == null) {
								instance = (ElementManager)new GameObject ("ElementManager").AddComponent<ElementManager> ();
								instance.Init ();
						}
						return instance;
				}
		}
	private void Init(){
		wholeSet = new List<Element>();
		transform.parent = GameObject.Find ("UI Root").transform;
		transform.localPosition = Vector3.zero;
		transform.localScale = Vector3.one;
        cam = Camera.main;
        if (cam == null)
            cam = Camera.current;
	}
	// Use this for initialization
	void Start () {
	//	newSet = wholeSet;
	//	wholeSet = new List<Element> ();
	}
	public void AddNewElement(ElementType type,float ElValue){
		GameObject GO = Resources.Load<GameObject> (string.Format ("Prefabs/Elements/{0}", type.ToString ()));
		if (GO == null)
						return;
		GO = (GameObject)Instantiate (GO);

		Element newOne = GO.GetComponent<Element> ();
		if (newOne == null) {
			Destroy (GO);
			Debug.Log ("Element missed");
			return;
		}
		GO.transform.parent = transform;
		GO.transform.localPosition = Vector3.zero;
		GO.transform.localScale = Vector3.one;
		wholeSet.Add (newOne);
	}
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (activeElement != null) {
                activeElement.Deselect();
            }
            RaycastHit2D info;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            info = Physics2D.Raycast(ray.origin,ray.direction);
            if (info.collider != null)
            {
                Node node = info.collider.GetComponent<Node>();
                if (node == null)
                {
                    activeElement = info.collider.GetComponent<Element>();
                    if (activeElement != null)
                    {
                        activeElement.Select();
                    }
                }
                else
                {
                    GameObject go2 = Resources.Load<GameObject>("Prefabs/Wire");
                    if (go2 != null)
                    {
                        Wire wire = ((GameObject)Instantiate(go2)).GetComponent<Wire>();
                        wire.Init(node);
                        activeElement = wire;
                    }
                }
            }
        }else if (Input.GetMouseButtonUp(0))
        {
            if (activeElement != null)
            {
                RaycastHit2D info;
                Node node = null;
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                info = Physics2D.Raycast(ray.origin, ray.direction);
                if (info.collider != null)
                {
                    node = info.collider.GetComponent<Node>();
                    activeElement.ConnectNode(node);
                    activeElement = null;
                }  else {
                    activeElement.ConnectNode(node);
                }
            } else { 
            }
        }else if (Input.GetMouseButton(0)){
            if (activeElement!=null)activeElement.Move(cam.ScreenToWorldPoint(Input.mousePosition));
        }
	}
}
