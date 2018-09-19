using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public string name;
    public bool isHolding = false;
    public  GameObject prefab;

	// Use this for initialization
	void Start () {
        prefab = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void UseItem()
    {
        print("Item in use");
    }
}
