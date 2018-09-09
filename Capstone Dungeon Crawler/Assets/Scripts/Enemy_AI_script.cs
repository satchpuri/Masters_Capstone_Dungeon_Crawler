using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_script : MonoBehaviour
{
    NavMeshAgent enemyAgent;
    Ray enemyVision;
    RaycastHit rayHit;
    bool pursuePlayer = false;
    bool pursueLight = false;

	// Use this for initialization
	void Start ()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
		
	}

    // Update is called once per frame
    void Update()
    {
        enemyVision = new Ray(transform.position, transform.forward * 10);
        Debug.DrawRay(transform.position, transform.forward * 10, Color.blue);

        if (Physics.Raycast(transform.position, transform.forward, out rayHit, 30))
        {
            Debug.DrawRay(transform.position, Vector3.up, Color.red);

            if (rayHit.transform.tag == "LightSource")
            {

                Debug.Log("I see the light!");
                pursueLight = true;
                PursueLight();
            }

        }
    }

    void PursueLight()
    {
        //seek lights position
    }
}
