using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

    Transform LeftHand;
    Transform RightHand;
    Pickup nextItem;
    // Use this for initialization
    void Start () {
        LeftHand = transform.Find("LeftHand");
        RightHand = transform.Find("RightHand");
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (LeftHand.childCount != 0)
            {
               var drop = DropLeftItem();
                //remove from inventory
                Inventory.instance.RemoveItem(drop.GetComponent<Pickup>());
                Inventory.instance.currentIndex = 0;
            }
            else if (RightHand.childCount != 0)
            {
                var drop = DropRightItem();
                //remove from inventory
                Inventory.instance.RemoveItem(drop.GetComponent<Pickup>());
                Inventory.instance.currentIndex = 0;
            }
        }

        if(Input.GetMouseButtonUp(0)) // Toggle Left light
        {
            if(LeftHand.childCount != 0)
            {
                LeftHand.GetChild(0).GetComponent<Pickup>().UseItem();
                if(LeftHand.GetChild(0).GetComponent<PickableLight>())
                    LeftHand.GetChild(0).GetComponent<PickableLight>().switchedOn = !LeftHand.GetChild(0).GetComponent<PickableLight>().switchedOn;
            }
        }

        if (Input.GetMouseButtonUp(1)) // Toggle Right light
        {
            if (RightHand.childCount != 0)
            {
                RightHand.GetChild(0).GetComponent<Pickup>().UseItem();
                if (RightHand.GetChild(0).GetComponent<PickableLight>())
                    RightHand.GetChild(0).GetComponent<PickableLight>().switchedOn = !RightHand.GetChild(0).GetComponent<PickableLight>().switchedOn;
            }
        }

        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll < 0)
        {
            EquipNextInventoryItem();
        }
        if (scroll > 0)
        {
            EquipBackInventoryItem();
        }
    }


    //Equip next inventory Item
    void EquipNextInventoryItem()
    {
        GameObject droppedItem = DropLeftItem();
        if (droppedItem != null)
        {
            droppedItem.SetActive(false);
        }
        
        // Left Hand holds new item
        int index = Inventory.instance.NextIndex();
        nextItem = Inventory.instance.inventoryItems[index];
        EquipLeftHand(nextItem.transform);
        nextItem.prefab.SetActive(true);
        
        // Right hand holds next item
        droppedItem = DropRightItem();
        if (droppedItem != null)
        {
            droppedItem.SetActive(false);
        }

        nextItem = Inventory.instance.inventoryItems[Inventory.instance.NextIndex()];
        EquipRightHand(nextItem.transform);
        nextItem.prefab.SetActive(true);
    }

    //Equip previous inventory Item
    void EquipBackInventoryItem()
    {
        GameObject droppedItem = DropRightItem();
        if (droppedItem != null)
        {
            droppedItem.SetActive(false);
        }
        // Right hand holds next item
        nextItem = Inventory.instance.inventoryItems[Inventory.instance.NextIndex()];
        EquipRightHand(nextItem.transform);
        nextItem.prefab.SetActive(true);


        
        droppedItem = DropLeftItem();
        if (droppedItem != null)
        {
            droppedItem.SetActive(false);
        }
        
        // Left Hand holds new item
        int index = Inventory.instance.NextIndex();
        nextItem = Inventory.instance.inventoryItems[index];
        EquipLeftHand(nextItem.transform);
        nextItem.prefab.SetActive(true);
    }

    // Drop Item in the left hand
    GameObject DropLeftItem()
    {
        if (LeftHand.childCount != 0)
        {
            GameObject droppedItem;
            foreach (SphereCollider childCollider in LeftHand.GetComponentsInChildren<SphereCollider>())
            {
                childCollider.enabled = true;
            }
            droppedItem = LeftHand.GetChild(0).gameObject;
            droppedItem.GetComponent<Pickup>().isHolding = false;
            LeftHand.DetachChildren();
            return droppedItem;
        }
        return null;
    }

    //Drop Item in the right hand
    GameObject DropRightItem()
    {
        if (RightHand.childCount != 0)
        {
            GameObject droppedItem;
            foreach (SphereCollider childCollider in RightHand.GetComponentsInChildren<SphereCollider>())
            {
                childCollider.enabled = true;
            }
            droppedItem = RightHand.GetChild(0).gameObject;
            droppedItem.GetComponent<Pickup>().isHolding = false;
            RightHand.DetachChildren();
            return droppedItem;
        }
        return null;
    }

    // Pickup Items
    public void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyUp(KeyCode.E))     // Pickup objects
        {
            if(other.gameObject.GetComponent<Pickup>() != null)
            {
                if(LeftHand.childCount == 0)
                {
                    if (Inventory.instance.AddItem(other.gameObject.GetComponent<Pickup>()))
                    {
                        EquipLeftHand(other.transform);
                    }
                }
                else if(RightHand.childCount == 0)
                {
                    if (Inventory.instance.AddItem(other.gameObject.GetComponent<Pickup>()))
                    {
                        EquipRightHand(other.transform);
                    }
                }
                else
                {
                    if(Inventory.instance.AddItem(other.gameObject.GetComponent<Pickup>()))
                        other.gameObject.SetActive(false);                                      // disable if picked up
                }
               
            }

        }
    }

    void EquipLeftHand(Transform item)
    {
        item.SetParent(LeftHand);
        item.position = LeftHand.position;
        item.localRotation = Quaternion.identity;
        item.gameObject.GetComponent<Pickup>().isHolding = true;
        item.gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    void EquipRightHand(Transform item)
    {
        item.SetParent(RightHand);
        item.position = RightHand.position;
        item.localRotation = Quaternion.identity;
        item.gameObject.GetComponent<Pickup>().isHolding = true;
        item.gameObject.GetComponent<SphereCollider>().enabled = false;
    }

    void OnCollisionEnter(Collision other) {
		// Hits a Death Trigger with body
		if (other.gameObject.tag == "DeathTrigger") {
			Debug.Log (gameObject.name + " has hit a death trigger.");
		}
	}
}
