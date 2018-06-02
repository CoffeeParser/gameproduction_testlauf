using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour {

    public float horizontalSpeed;
    public float verticalSpeed;
    public float amplitude;

    private Vector3 tempPosition;

	// Use this for initialization
	void Start ()
    {
        tempPosition = transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        tempPosition.x = Mathf.Sin(Time.realtimeSinceStartup * horizontalSpeed) * amplitude;
        tempPosition.y = Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude;

        transform.position = tempPosition;

	}
}
