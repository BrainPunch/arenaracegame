using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleControl : MonoBehaviour {
    
    public float torque;
    public float brake;
    public float turn;

    private Rigidbody body;
    private WheelCollider[] wheels;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        wheels = GetComponentsInChildren<WheelCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        var motor = Input.GetAxis("Vertical") * torque;
        var speed = transform.InverseTransformVector(body.velocity).z;
        var shouldBrake = Mathf.Abs(motor) > 0 && Mathf.Abs(speed) > 0 && Mathf.Sign(motor) != Mathf.Sign(speed);
        var stop = shouldBrake ? brake : 0;
        var angle = Input.GetAxis("Horizontal") * turn;

        foreach (var wheel in wheels) {
            wheel.motorTorque = motor;
            wheel.brakeTorque = stop;
            wheel.steerAngle = angle * Mathf.Sign(wheel.gameObject.transform.localPosition.z);
        }
    }
}
