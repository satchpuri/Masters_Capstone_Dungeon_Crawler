using UnityEngine;


// Class for a Seeking Vehicle
public class Seeker : MonoBehaviour
{
    protected Vector3 acceleration;
    protected Vector3 velocity;
    protected Vector3 desired;
    public GameObject seekerTarget;

    [SerializeField]
	private bool isSeeking = false;

	// wander
	private float circ_distance = 20.0f;
	private float circ_radius = 10.0f;
	private float angle_change = 5.0f;

    //weighting
    public float seekWeight = 75.0f;

    //ultimate steering force that will be applied to acceleration
    private Vector3 ultimateForce;

    // Flocking/obstacle avoidance Weighting
    //public float safeDistance = 10.0f;
    //public float avoidWeight = 20.0f;
    //public float sepWeight = 50.0f;
    //public float alignWeight = 100.0f;
    //public float coWeight = 200.0f;

    public float maxSpeed = 3.0f;
    public float maxForce = 12.0f;
    public float mass = 1.0f;
    public float radius = 1.0f;

    public Vector3 Velocity
    {
        get { return velocity; }
    }
    

    void CalcSteeringForces() {
        ultimateForce = Vector3.zero;

		if (isSeeking) {
			// get a seeking force and add to the ultimate steering force
			ultimateForce += Seek (seekerTarget.transform.position) * seekWeight;
		}

		// otherwise, wander
		//ultimateForce += Wander();


        ultimateForce = Vector3.ClampMagnitude(ultimateForce, maxForce);
        
        ApplyForce(ultimateForce);
    }

    virtual public void Start()
    {
        acceleration = Vector3.zero;
        velocity = transform.forward;
        desired = Vector3.zero;

        ultimateForce = Vector3.zero;
    }


    // Update is called once per frame
    protected void Update()
    {
        CalcSteeringForces();
        
        //velocity += acceleration * Time.deltaTime;
        //velocity.y = 0;
        //velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        ////dude face torward target
        //transform.forward = velocity.normalized;
        //charControl.Move(velocity * Time.deltaTime);
        //acceleration = Vector3.zero;

    }

	// once colliding with the light, seek the player
	void OnCollisionEnter(Collision collision) {
        Debug.Log("collision");
        if (collision.gameObject.tag == "Player")
        {
            isSeeking = true;
        }
	}

	// once leaving the colliision with light, stop seeking
	void OnCollisionExit(Collision collision) {
        //if (collision.tag == "Player")
        //    isSeeking = false;
	}

    protected void ApplyForce(Vector3 steeringForce)
    {
        acceleration += steeringForce / mass;
    }

    protected Vector3 Seek(Vector3 targetPosition)
    {
        desired = targetPosition - transform.position;
        desired = desired.normalized * maxSpeed;
        Vector3 seekingForce = desired - velocity;
        seekingForce.y = 0;
        return seekingForce;
    }

	protected Vector3 Wander() {
		// Calculate the circle center
		Vector3 circ = velocity;
		circ.Normalize();
		circ = circ_radius * circ;

		// Calculate displacement force
		Vector3 displacement;
		displacement = new Vector3(0f, 0f, -1f);
		displacement = circ_radius * displacement;

        // Randomly change the vectors direction by making it its current angle
        float wanderAngle = 10.0f;
        SetAngle(displacement, wanderAngle);
        //wanderAngle += (Random.Range(0f, 1.0f) * angle_change - angle_change * .5f);
      

        Vector3 wanderForce;
		wanderForce = circ + displacement;
		return wanderForce;
	}

    private void SetAngle(Vector3 vect, float angle) {
        float mag = vect.magnitude;
        vect.x = Mathf.Cos(angle) * mag;
        vect.z = Mathf.Sin(angle) * mag;
    }

    /*protected Vector3 AvoidObstacle(GameObject obst, float sD)
    {
        desired = Vector3.zero;

        //distance from dude to obstacle
        Vector3 vecToCenter = obst.transform.position - transform.position;
        vecToCenter.y = 0;

        //radius of obstacle
        //float obstRad = obst.GetComponent<ObstacleScript>().Radius;

        //calculate safe distance
        if (vecToCenter.magnitude > sD)
            return Vector3.zero;

        //check obstacle behind
        if (Vector3.Dot(vecToCenter, transform.forward) < 0)
            return Vector3.zero;

        //will it not collide?
        //if (Mathf.Abs(Vector3.Dot(vecToCenter, transform.right)) > obstRad + radius) return Vector3.zero;


        //will it collide?
        //is it on your left or right?
        if (Vector3.Dot(vecToCenter, transform.right) < 0)
        { //on left move right
            desired = transform.right * maxSpeed;
            //Debug.DrawLine(transform.position, obst.transform.position, Color.red);
            Debug.DrawLine(obst.transform.position, transform.position, Color.red);
        }
        else //on right move left
        { 
            desired = transform.right * -maxSpeed;
            //Debug.DrawLine(transform.position, obst.transform.position, Color.green);
        }
        return desired;
    }*/

    public Vector3 StayInBounds(float radius, Vector3 center) { return Vector3.zero; }
}
