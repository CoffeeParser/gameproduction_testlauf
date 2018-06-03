using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huhn_Behavoir : MonoBehaviour
{
    //int i = 0;

    //float directionRange = 5;

    //public int radiusID;

    public int gockelSpeed = 4;
    private int timeForDirection;

    public float radius1 = 3;
    public float radius2 = 4;
    public float radius3 = 5;
    public Transform playerPosition;
    protected Vector3 GockelPos;
    protected Vector3 PlayerToGockelRange;

    private float y;
    private bool beingHandled;
    public float smooth = 1f;
    private Quaternion targetRotation;

    //public Rigidbody rb;
    //public GameObject Gockel;

    // Use this for initialization
    void Start()
    {

        targetRotation = transform.rotation;

        timeForDirection = gockelSpeed / 4;
        /*
        radiusID = Random.Range(1,3);

        if(radiusID == 1)
        {
            transform.position = new Vector3(Random.Range(radius1,radius2),0, Random.Range(radius1, radius2));
        }
        if (radiusID == 2)
        {
            transform.position = new Vector3(Random.Range(radius2, radius3), 0, Random.Range(radius3, radius3+10));
        }
        */
    }

    // Update is called once per frame
    void Update()
    {

        GetNewDirection();
        SetGockelToPosition();
    }

    void SetGockelToPosition()
    {
        GockelPos = transform.position;
        PlayerToGockelRange = playerPosition.position - GockelPos;

        float betrag = Mathf.Sqrt(PlayerToGockelRange.x * PlayerToGockelRange.x + PlayerToGockelRange.z * PlayerToGockelRange.z);

        // r1
        if (betrag < radius1)
        {
            
            //Debug.Log("radius1: " + betrag);
            y = 180;
        }
        // r2
        if (betrag > radius1 && betrag < radius2)
        {
            
            //Debug.Log("radius2: " + betrag);
        }       
        // r3
        if (betrag > radius2 && betrag < radius3)
        {

            //Debug.Log("radius3: " + betrag);
        }
        //r4 (out of range)
        if (betrag > radius3 )
        {
            y = 180;
            //Debug.Log("radius4: " + betrag);
        }       
    }

    void GetNewDirection()
    {
        // Vorläufiges hochsetzten der Gockel
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        
        // Warten um sich einen neue Richtung zu suchen 
        targetRotation = Quaternion.Euler(0, y, 0);
        if (!beingHandled)
        {
            StartCoroutine(WaitForNewDirection());
        }

        // neue Rotation richtung
        GetComponent<Rigidbody>().rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);      

        // In Neue Vectorrichtung schieben
        GetComponent<Rigidbody>().velocity = transform.forward * gockelSpeed;

    }

    IEnumerator WaitForNewDirection()
    {

        beingHandled = true;
        GetNewRotation();
        yield return new WaitForSeconds(timeForDirection);
        beingHandled = false;
        //Debug.Log(y);

    }

    void GetNewRotation()
    {  
        int r = Random.Range(0, 100);
        if(r >= 0 && r < 60)
        {
            y = Random.Range(-30, 30);
        }
        else if (r >= 60 && r < 80)
        {
            y = Random.Range(-60, 60);
        }
        else if (r >= 80 && r <= 100)
        {
            y = Random.Range(-120, 120);
        }       
    }


    //public GameObject projectile;
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log("Collision");

        /*
        if (collision.gameObject == projectile)
        {
            Debug.Log("Collision");
        }
        */
    }


}
