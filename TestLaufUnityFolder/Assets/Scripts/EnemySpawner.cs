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
            Instantiate(enemy, GetRandomPosition(), transform.rotation);


        }
    }
	
	// Update is called once per frame
	void Update () {

    }
    public Vector3 GetRandomPosition()
    {
        int range = 10;
        Vector3 v = new Vector3(Random.Range(range, -range), 2, Random.Range(range, -range));
        return v;
    }
}
