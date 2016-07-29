using UnityEngine;
using System.Collections;

public class WheelSuspension : MonoBehaviour {

    public float range = 1f;
    public float radius = 0.5f;
    
    Rigidbody body = null;
    GameObject collide = null;

    Transform wheel = null;

    float position = 0f;

    // Use this for initialization
    void Start () {
        wheel = transform.GetChild(0);
        body = GetComponentInParent<Rigidbody>();
        collide = body.GetComponentInChildren<Collider>().gameObject;
	}
	
	// Update is called once per frame
	void Update () {
        collide.SetActive(false);

        RaycastHit hit;
        if (Physics.CheckSphere(transform.position, radius)) {
            position = 0;
        } else if (Physics.SphereCast(transform.position, radius, -transform.up, out hit, range)) {
            position = hit.distance;
        } else {
            position = range;
        }

        position = Mathf.Clamp(position, 0f, range);

        wheel.localPosition = new Vector3(wheel.localPosition.x, -position, wheel.localPosition.z);

        collide.SetActive(true);
	}
}
