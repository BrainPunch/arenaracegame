using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleVehicleController : MonoBehaviour {

    public float acceleration;
    public float turning;
    public float skidForce;

    Rigidbody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Only one wheel has to be grounded
    bool IsGrounded () {
        var size = transform.localScale * 0.45f;
        var wheelPositions = new[] {
            transform.TransformPoint(0, -size.y, 0),
            transform.TransformPoint(-size.x, -size.y, -size.z),
            transform.TransformPoint(-size.x, -size.y, size.z),
            transform.TransformPoint(size.x, -size.y, -size.z),
            transform.TransformPoint(size.x, -size.y, size.z),
        };
        var down = transform.TransformDirection(Vector3.down);
        foreach (var wheelPosition in wheelPositions) {
            var ray = new Ray(wheelPosition, down);
            if (Physics.Raycast(ray, 0.25f)) {
                return true;
            }
        }
        return false;
    }

    void FixedUpdate () {
        if (!IsGrounded()) return;

        var vel = transform.InverseTransformVector(body.velocity);
        var rot = transform.InverseTransformDirection(body.angularVelocity);

        var accel = Input.GetAxis("Vertical") * acceleration;
        body.AddRelativeForce(Vector3.forward * accel, ForceMode.Acceleration);

        var turn = Mathf.Min(vel.z * turning * Input.GetAxis("Horizontal"), 3);
        body.AddRelativeTorque(Vector3.up * 5 * (turn - rot.y), ForceMode.Acceleration);

        var skid = vel.x * skidForce;
        body.AddRelativeForce(Vector3.left * skid, ForceMode.Acceleration);
    }
}
