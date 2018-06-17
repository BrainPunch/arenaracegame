using UnityEngine;
using System.Collections;

public class WheelSuspension : MonoBehaviour {

    public float range = 1f;
    public float radius = 0.5f;
    
    public float forceMax = 1f;

    public float frictionDynamic = 1f;

    Rigidbody body = null;
    GameObject collide = null;

    Transform wheel = null;

    float position = 0f;
    float positionLast = 0f;
    GameObject ground = null;
    Rigidbody groundBody = null;
    Vector3 groundPosition = Vector3.zero;
    Vector3 groundNormal = Vector3.up;
    GameObject groundLast = null;

    // Use this for initialization
    void Start () {
        wheel = transform.GetChild(0);
        body = GetComponentInParent<Rigidbody>();
        collide = body.GetComponentInChildren<Collider>().gameObject;
        position = range;
	}

    // FixedUpdate is run every physics simulation step
    void FixedUpdate () {
        scanGround();
        if (ground != null) {
            var force = Mathf.Lerp(forceMax, 0f, (position / range)) * transform.up;
            if (groundLast != null) {
                var groundVel = (groundBody != null) ? groundBody.GetPointVelocity(groundPosition) : Vector3.zero;
                var wheelVel = -50f * transform.forward + body.GetPointVelocity(groundPosition) + transform.TransformVector(Vector3.down * (position - positionLast) / Time.fixedDeltaTime);
                force += Vector3.ProjectOnPlane(groundVel - wheelVel, groundNormal).normalized * force.magnitude * frictionDynamic;
            }
            body.AddForceAtPosition(force, wheel.position);
            if (groundBody != null) {
                groundBody.AddForceAtPosition(-force, wheel.position);
            }
        }
        positionLast = position;
        groundLast = ground;
    }
	
	// Update is called once per frame
	void Update () {
        scanGround();
        wheel.localPosition = new Vector3(wheel.localPosition.x, -position, wheel.localPosition.z);
	}

    void scanGround () {
        collide.SetActive(false);

        RaycastHit hit;
        Collider[] col = Physics.OverlapSphere(transform.position, radius);
        if (col.Length > 0) {
            position = 0;
            ground = col[0].gameObject;
            groundBody = col[0].GetComponent<Rigidbody>();
            groundPosition = transform.position;
            groundNormal = transform.up;
        } else if (Physics.SphereCast(transform.position, radius, -transform.up, out hit, range)) {
            position = hit.distance;
            ground = hit.collider.gameObject;
            groundBody = hit.rigidbody;
            groundPosition = hit.point;
            groundNormal = hit.normal;
        } else {
            position = range;
            ground = null;
            groundBody = null;
        }

        collide.SetActive(true);

        position = Mathf.Clamp(position, 0f, range);
    }
}
