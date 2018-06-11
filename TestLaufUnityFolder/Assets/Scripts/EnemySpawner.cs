using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public int enemyCount = 0;
    private int radiusID;

	// Use this for initialization
	void Start () {


        for (int i = 0; i < enemyCount -1; i++)
        {
            var huhn = Instantiate(enemy, GetRandomPosition(), transform.rotation);
            huhn.name += i;

        }
        
    }
	
	// Update is called once per frame
	void Update () {

    }
    public Vector3 GetRandomPosition()
    {
        int range = 10;
        var a = Random.Range(5, 10);
        var b = Random.Range(-5, -10);     


        Vector3 v = new Vector3(Random.Range(a, b), 2, Random.Range(a, b));
        

        return v;
    }
}
