using UnityEngine;
using System.Collections;

public class WheelMesh : MonoBehaviour {

    public WheelCollider trackCollider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 position;
        Quaternion rotation;
        trackCollider.GetWorldPose(out position, out rotation);
        transform.position = position;
        transform.rotation = rotation;
	}
}
