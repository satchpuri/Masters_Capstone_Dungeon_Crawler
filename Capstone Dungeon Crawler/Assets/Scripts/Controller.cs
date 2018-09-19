using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

	public float moveSpeed = 6;

	Rigidbody rigidbody;
	Camera viewCamera;
	Vector3 velocity;
<<<<<<< HEAD

	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		viewCamera = Camera.main;
	}
=======
    Animator animator;
    void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		viewCamera = Camera.main;
        animator = GetComponent<Animator>();
    }
>>>>>>> Merge

	void Update () {
		Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		transform.LookAt (mousePos + Vector3.up * transform.position.y);
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;

		// reset scene
		if (Input.GetKeyDown (KeyCode.LeftShift))
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
<<<<<<< HEAD
=======
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) )
        {
            animator.SetBool("Run", true);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            animator.SetBool("Run", false);
        }
>>>>>>> Merge
    }

	void FixedUpdate() {
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
<<<<<<< HEAD
	}
=======
    }
>>>>>>> Merge
}