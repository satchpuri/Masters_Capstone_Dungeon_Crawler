﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

    Transform LeftHand;
    Transform RightHand;
	// Use this for initialization
	void Start () {
        LeftHand = transform.Find("LeftHand");
        RightHand = transform.Find("RightHand");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(LeftHand.childCount != 0)
            {
                LeftHand.DetachChildren();
                //print("left drop");
            }
            else if (RightHand.childCount != 0)
            {
                RightHand.DetachChildren();
                //print("right drop");
            }
        }

        if(Input.GetMouseButtonUp(0)) // Toggle Left light
        {

        }

        if (Input.GetMouseButtonUp(0)) // Toggle Right light
        {

        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E))
        {
            if(other.gameObject.GetComponent<Pickup>() != null)
            {
                if(LeftHand.childCount == 0)
                {
                    other.transform.SetParent(LeftHand);
                    other.transform.position = LeftHand.position;
                    other.transform.localRotation = Quaternion.identity;
                }
                else if(RightHand.childCount == 0)
                {
                    other.transform.SetParent(RightHand);
                    other.transform.position = RightHand.position;
                    other.transform.localRotation = Quaternion.identity;
                }
                other.gameObject.GetComponent<Pickup>().isHolding = true;
                print("picked up\n");
            }

        }
    }
}
