using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huhn_Behavoir : MonoBehaviour
{
    public int gockelSpeed = 4;
    private int timeForDirection;

    public float radius1 = 3;
    public float radius2 = 4;
    public float radius3 = 5;
    private Transform player;
    protected Vector3 GockelPos;
    protected Vector3 PlayerToGockelRange;

    private float y;
    private bool beingHandled;
    public float smooth = 1f;
    private Quaternion targetRotation;

    void Start()
    {
        player = Camera.main.transform;
        targetRotation = transform.rotation;
        timeForDirection = 10;
        gockelSpeed = UnityEngine.Random.Range(5, gockelSpeed);
    }

    void Update()
    {
        transform.RotateAround(player.position, Vector3.up, gockelSpeed*Time.deltaTime);

        Vector3 lookDir = Vector3.Cross(player.position - transform.position, transform.up);
        if (lookDir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(lookDir);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1);
    }

    void SetGockelToPosition()
    {
        GockelPos = transform.position;
        PlayerToGockelRange = GockelPos - player.position;

        //Absoluter Betrag 
        float betrag = Vector3.Distance(GockelPos, player.position);
        
        // r1
        if (betrag < radius1)
        {
            y += 180;
        }
        // r2
        if (betrag > radius1 && betrag < radius2)
        {
        }       
        // r3
        if (betrag > radius2 && betrag < radius3)
        {
        }
        if (betrag > radius3 )
        {
            y += 180;
        }       
    }

    void GetNewDirection()
    {
        // Vorläufiges hochsetzten der Gockel
        transform.position = new Vector3(transform.position.x, 2, transform.position.z);
        
        // Warten um sich einen neue Richtung zu suchen 
        targetRotation = Quaternion.Euler(0, y, 0);
        float betrag = Vector3.Distance(GockelPos, player.position);
        Debug.Log(gameObject.name + " y: " + y + " radius3: "  + radius3 + " betrag:" + betrag);
        if (!beingHandled)
            StartCoroutine(WaitForNewDirection());

        // neue Rotation richtung
        GetComponent<Rigidbody>().rotation = Quaternion.Lerp(transform.rotation, targetRotation, 10 * smooth * Time.deltaTime);      

        // In Neue Vectorrichtung schieben
        GetComponent<Rigidbody>().velocity = transform.forward * gockelSpeed;
    }

    IEnumerator WaitForNewDirection()
    {
        beingHandled = true;
        GetNewRotation();
        //Debug.Log();
        yield return new WaitForSeconds(timeForDirection);
        beingHandled = false;
        //Debug.Log(y);

    }

    void GetNewRotation()
    {  
        int r = UnityEngine.Random.Range(0, 100);
        if(r >= 0 && r < 60)
        {
            y = UnityEngine.Random.Range(-30, 30);
        }
        else if (r >= 60 && r < 80)
        {
            y = UnityEngine.Random.Range(-60, 60);
        }
        else if (r >= 80 && r <= 100)
        {
            y = UnityEngine.Random.Range(-120, 120);
        }       
    }

    public void GoDie()
    {
        SendMessageUpwards("OnGockelDied");
    }
}
