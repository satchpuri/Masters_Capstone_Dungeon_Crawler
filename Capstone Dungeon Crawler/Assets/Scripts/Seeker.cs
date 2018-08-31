using UnityEngine;

[RequireComponent(typeof(CharacterController))]

// Class for a Seeking Vehicle
public class Seeker : MonoBehaviour
{
    protected Vector3 acceleration;
    protected Vector3 velocity;
    protected Vector3 desired;
    public GameObject seekerTarget;

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

    public float maxSpeed = 6.0f;
    public float maxForce = 12.0f;
    public float mass = 1.0f;
    public float radius = 1.0f;

    public Vector3 Velocity
    {
        get { return velocity; }
    }

    CharacterController charControl;

    void CalcSteeringForces() {
        ultimateForce = Vector3.zero;

        // get a seeking force and add to the ultimate steering force
        ultimateForce += Seek(seekerTarget.transform.position) * seekWeight;
        
        ultimateForce = Vector3.ClampMagnitude(ultimateForce, maxForce);
        
        ApplyForce(ultimateForce);
    }

    virtual public void Start()
    {
        acceleration = Vector3.zero;
        velocity = transform.forward;
        desired = Vector3.zero;
        
        charControl = GetComponent<CharacterController>();

        ultimateForce = Vector3.zero;
    }


    // Update is called once per frame
    protected void Update()
    {
        CalcSteeringForces();
        
        velocity += acceleration * Time.deltaTime;
        velocity.y = 0;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        //dude face torward target
        transform.forward = velocity.normalized;
        charControl.Move(velocity * Time.deltaTime);
        acceleration = Vector3.zero;

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
