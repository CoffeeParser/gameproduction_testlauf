using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapFollowPlayerRotation : MonoBehaviour
{

    public Transform PlayerCameraTransform; // this is the transform where the minimap will follow along the euler-y

    private float xRotLookDown = 90f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(xRotLookDown, PlayerCameraTransform.eulerAngles.y, 0);
	}
}
