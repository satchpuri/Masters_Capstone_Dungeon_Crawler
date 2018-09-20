using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour {

    public float speed = 6f;            // player speed

    Vector3 movement;                   // player's movement vector
    Animator anim;                      
    Rigidbody playerRigidbody;          
    int floorMask;                      
    float camRayLength = 100f;          

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");

        // Set up references.
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
      
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();

        // Animate the player.
        Animating(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        // Create a boolean that is true if either of the input axes is non-zero.
        bool walking = h != 0f || v != 0f;

        // Tell the animator whether or not the player is walking.
        anim.SetBool("Run", walking);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene(1);
        }
    }
}
