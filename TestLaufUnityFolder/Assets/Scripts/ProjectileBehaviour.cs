using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(gameObject);
    }
}
