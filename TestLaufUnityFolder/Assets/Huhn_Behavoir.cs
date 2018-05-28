using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huhn_Behavoir : MonoBehaviour
{


    int i = 0;
    int timeForDirection = 100;
    float directionRange = 5;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetNewDirection();
    }


    void GetNewDirection()
    {
        transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        i++;
        GetNewVector3XZ();
        if (i < timeForDirection){
            
        } else{
            i = 0;

            GetComponent<Rigidbody>().velocity = GetNewVector3XZ();

        }
    }


    public Vector3 GetNewVector3XZ()
    {

        Vector3 RandomDirection = new Vector3(Random.Range(directionRange, -directionRange), 3, Random.Range(directionRange, -directionRange));
        return RandomDirection;
    }

    public GameObject projectile;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject == projectile)
        {
            Debug.Log("Collision");
        }
    }


}
