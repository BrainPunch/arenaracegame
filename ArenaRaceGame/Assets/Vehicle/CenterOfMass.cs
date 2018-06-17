using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMass : MonoBehaviour {

    Rigidbody body;

    public Vector3 offset;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody>();
        body.centerOfMass = offset;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
