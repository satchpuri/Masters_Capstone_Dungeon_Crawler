using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableLight : MonoBehaviour {

    public bool switchedOn = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(switchedOn)
        {
            for(int i=0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).gameObject.tag == "LightSource")
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.tag == "LightSource")
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
}
