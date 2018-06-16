using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            collision.gameObject.GetComponent<Huhn_Behavoir>().GoDie();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Destroy(gameObject);
    }
}
