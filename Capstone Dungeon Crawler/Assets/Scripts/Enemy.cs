using UnityEngine;

// Class for basic Enemy AI behavior
public class Enemy : MonoBehaviour
{
    [SerializeField]
    bool alerted = false;
	bool pursuingPlayer = false;

    public Transform target;

    Transform originalPosition;

    private float rotSpeed = 3.0f;
    private float moveSpeed = 3.0f;

    // Alert trigger zone
    SphereCollider triggerZone;
    private float MAX_TRIGGER_SIZE = 8.0f;
    private float MIN_TRIGGER_SIZE = 3.0f;

    void Start() {
        triggerZone = GetComponent<SphereCollider>();
        originalPosition = transform;
    }
    
    void Update()
    {
        // Seek the target if alerted
        if (alerted)
        {
            Seek(target);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "LightSource") {
            target = other.transform;	// target light source
            alerted = true;

			// LATER: If lightsource is player held, target and try to attack

            // enlarge trigger zone
            triggerZone.radius = MAX_TRIGGER_SIZE;
        }
<<<<<<< HEAD

=======
>>>>>>> Merge
    }

    void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "LightSource") {
            alerted = false;
            //target = originalPosition;        // eventually have enemy return to their original position

            // shrink trigger zone back to normal
            triggerZone.radius = MIN_TRIGGER_SIZE;
        }
    }

	void OnCollisionEnter(Collision other) {
		// Hits a Death Trigger with body
		if (other.gameObject.tag == "DeathTrigger") {
			Debug.Log (gameObject.name + " has died.");
			Destroy (gameObject);
		}
<<<<<<< HEAD
	}
=======
        if (other.gameObject.tag == "Fire")
        {
            Debug.Log(gameObject.name + " has died by burning.");
            Destroy(gameObject);
        }
    }
>>>>>>> Merge

    void Seek(Transform target_tr) {
        // face target
        transform.rotation = Quaternion.Slerp(transform.rotation
                    , Quaternion.LookRotation(target_tr.position - transform.position)
                    , rotSpeed * Time.deltaTime);

        // move towards target
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
