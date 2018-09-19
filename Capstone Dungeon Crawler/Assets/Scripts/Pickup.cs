using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    public string name;
    public bool isHolding = false;
<<<<<<< HEAD
	// Use this for initialization
	void Start () {
		
=======
    public  GameObject prefab;

	// Use this for initialization
	void Start () {
        prefab = this.gameObject;
>>>>>>> Merge
	}
	
	// Update is called once per frame
	void Update () {
		
	}
<<<<<<< HEAD
=======

    public virtual void UseItem()
    {
        print("Item in use");
    }
>>>>>>> Merge
}
