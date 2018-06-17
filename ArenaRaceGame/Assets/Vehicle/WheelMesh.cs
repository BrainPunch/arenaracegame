using UnityEngine;
using System.Collections;

public class WheelMesh : MonoBehaviour {

    public WheelCollider trackCollider;

    Vector3 positionLast, position, positionSpeed;
    Quaternion rotationLast, rotation;
    float timeIncrement = 0f;

	// Use this for initialization
	void Start () {
        trackCollider.GetWorldPose(out positionLast, out rotationLast);
        trackCollider.GetWorldPose(out position, out rotation);
        positionSpeed = Vector3.zero;
        timeIncrement = 0f;
    }

    // FixedUpdate is caled every physics step
    void FixedUpdate () {
        positionLast = position;
        rotationLast = rotation;
        trackCollider.GetWorldPose(out position, out rotation);
        position = transform.parent.InverseTransformPoint(position);
        rotation = Quaternion.Inverse(transform.parent.rotation) * rotation;
        timeIncrement = Time.fixedDeltaTime;
    }
	
	// Update is called once per frame
	void Update () {
        timeIncrement += Time.deltaTime;
        var t = (timeIncrement / Time.fixedDeltaTime);
        var targetPosition = Vector3.LerpUnclamped(positionLast, position, t);
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref positionSpeed, 0.025f);
        transform.localRotation = Quaternion.SlerpUnclamped(rotationLast, rotation, t);
    }
}
