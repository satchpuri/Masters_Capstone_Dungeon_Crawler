using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_AI_script : MonoBehaviour
{
    NavMeshAgent enemyAgent;
    public Transform lightSource;
    public Transform Player;

    bool pursuePlayer = false;
    bool pursueLight = false;
    bool attackPlayer = false;
    bool lightIsOn = true;

    public float enemyVision = 10f;

    // Use this for initialization
    void Start()
    {
        enemyAgent = GetComponentInChildren<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {

        float distanceToLight = Vector3.Distance(lightSource.position, transform.position);

        if (lightIsOn)
        {
            if (distanceToLight <= enemyVision)
            {
                PursueLight();
                Debug.Log("I see the light!");
            }
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pursuePlayer = true;
            Debug.Log("I will eat the child!");
        }
    }

    void PursueLight()
    {
        //seek lights position
        enemyAgent.SetDestination(lightSource.position);
    }

    void PursuePlayer()
    {

    }

    void AttackPlayer()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemyVision);
    }

}
