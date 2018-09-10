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
    public float enemyVisionWhenLightIsOff = 3f;

    // Use this for initialization
    void Start()
    {
        enemyAgent = GetComponentInChildren<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        float distanceToLight = Vector3.Distance(lightSource.position, transform.position);
        float distanceToPlayer = Vector3.Distance(Player.position, transform.position);

        if (lightIsOn)
        {
            if (distanceToLight <= enemyVision)
            {
                PursueLight();
                Debug.Log("I see the light!");
            }
            else
            {
                StopPursuing();
            }
        }
        else
        {
            if (distanceToPlayer <= enemyVisionWhenLightIsOff)
            {
                PursuePlayer();
                Debug.Log("I see the player!");
            }
            else
            {
                StopPursuing();
            }
        }
       
    }

    void StopPursuing()
    {
        enemyAgent.destination = transform.position;
    }

    public void SetLightStatus(bool status)
    {
        lightIsOn = status;
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
        enemyAgent.SetDestination(Player.position);
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
