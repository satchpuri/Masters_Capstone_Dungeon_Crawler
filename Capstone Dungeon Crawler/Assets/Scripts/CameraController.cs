using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

<<<<<<< HEAD
    public GameObject player;       


    private Vector3 offset;         

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
=======
    public Transform player;            // The position that that camera will be following.
    public float smoothing = 5f;        // speed

    private Vector3 offset;

    void Start()
    {
        // Calculate the initial offset.
        offset = transform.position - player.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = player.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
>>>>>>> Merge
    }
}
