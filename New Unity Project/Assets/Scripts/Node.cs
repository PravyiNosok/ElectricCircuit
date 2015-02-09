using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour {
	private List<Wire> connectedElements;
	private bool connected = false;
	private float current = 0;
	private float voltage = 0;
	private Vector3 position;
	public Element owner; 
	private void OnDestroy(){

	}
	// Use this for initialization
	void Awake () {
		owner = transform.GetComponentInParent<Element> ();
		position = transform.position;
		connectedElements = new List<Wire> ();
	}
	// Update is called once per frame
	void Update () {
	
	}
    public void Move() {
        foreach(Wire wire in connectedElements) {
           if (wire!= null) wire.Move(Vector3.zero);
        }
    }
    public void AddWire(Wire wire) {
        connectedElements.Add(wire);
    }
	public void Remove(Wire wire){
		int amount = connectedElements.Count;
		for (int cur = 0; cur<amount; cur++) {
			if (connectedElements[cur] == wire){
				connectedElements.RemoveAt(cur);
				return;
			}		
		}
	}
}
