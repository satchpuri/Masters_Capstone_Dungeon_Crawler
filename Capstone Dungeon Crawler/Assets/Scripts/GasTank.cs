﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasTank : Pickup {
    [SerializeField]
    private List<Vector3> points = new List<Vector3>();
    RaycastHit hit;

    LineRenderer line;
    public GameObject firePrefab;
	// Use this for initialization
	void Start () {
        line = GetComponent<LineRenderer>();
        prefab = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        UseItem();
	}

    float DistanceToLastPoint(Vector3 point)
    {
        if (points.Count == 0)
            return Mathf.Infinity;
        return Vector3.Distance(points[points.Count - 1], point);
    }

    public override void UseItem()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.GetComponent<Pickup>().isHolding)
            points.Clear();
        if (Input.GetMouseButton(0) && gameObject.GetComponent<Pickup>().isHolding)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 100))
            {
                if (DistanceToLastPoint(hit.point) > 1f)
                {
                    points.Add(hit.point);

                    line.positionCount = points.Count;
                    line.SetPositions(points.ToArray());
                }
            }
        }
        else if (Input.GetMouseButtonUp(0) && gameObject.GetComponent<Pickup>().isHolding)
        {
            foreach (Vector3 point in points)
            {
                Instantiate(firePrefab, point, Quaternion.identity);
            }
            line.positionCount = 0;
            Inventory.instance.RemoveItem(gameObject.GetComponent<Pickup>());
            Destroy(gameObject);
        }
    }
}
