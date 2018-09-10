using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public int size;
    public List<Pickup> inventoryItems;
    static public Inventory instance;
    public int currentIndex;
    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
        currentIndex = 0;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Add an item to the inventory
    public bool AddItem(Pickup newPickup)
    {
        if (inventoryItems.Count < size)
        {
            inventoryItems.Add(newPickup);
            //print("Added to inventory");
            return true;
        }
        return false;
    }

    // Remove an item from the inventory
    public void RemoveItem(Pickup rmItem)
    {
        foreach(Pickup item in inventoryItems)
        {
            if (item.name == rmItem.name)
            {
                inventoryItems.Remove(item);
                return;
            }
        }
        //print("Removed from inventory");
    }

    // Get the index of the next item to be equiped
    public int NextIndex()
    {
        ++currentIndex;
        if(inventoryItems.Count <= currentIndex )
        {
            currentIndex = 0;
        }
        return currentIndex;
    }

    // Get the index of the previous item equiped
    public int BackIndex()
    {
        if (--currentIndex < 0)
        {
            currentIndex = size;
        }
        return currentIndex;
    }
}
