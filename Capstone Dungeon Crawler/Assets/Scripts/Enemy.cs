using UnityEngine;

// Class for basic Enemy AI behavior
public class Enemy : MonoBehaviour
{
    [SerializeField]
    bool alerted = false;

    public Transform target;

    Transform originalPosition;

    private float rotSpeed = 3.0f;
    private float moveSpeed = 3.0f;

    // Alert trigger zone
    SphereCollider triggerZone;
    private float MAX_TRIGGER_SIZE = 15.0f;
    private float MIN_TRIGGER_SIZE = 5.0f;

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
        if (other.gameObject.tag == "Player") {
            //target = other.transform;     // only one player so don't need to change the target
            alerted = true;

            // enlarge trigger zone
            triggerZone.radius = MAX_TRIGGER_SIZE;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            alerted = false;
            //target = originalPosition;        // eventually have enemy return to their original position

            // shrink trigger zone back to normal
            triggerZone.radius = MIN_TRIGGER_SIZE;
        }
    }

    void Seek(Transform target_tr) {
        // face target
        transform.rotation = Quaternion.Slerp(transform.rotation
                    , Quaternion.LookRotation(target_tr.position - transform.position)
                    , rotSpeed * Time.deltaTime);

        // move towards target
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
