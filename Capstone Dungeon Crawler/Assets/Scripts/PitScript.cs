using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PitScript : MonoBehaviour
{


	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
       
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("collided");
            other.gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;
        }
    }
}
