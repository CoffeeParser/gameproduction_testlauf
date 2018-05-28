using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    private int enemyCount = 9;
    public int counter = 10;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < enemyCount; i++)
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
        Vector3 v = new Vector3(Random.Range(range, -range), 0, Random.Range(range, -range));
        return v;
    }
}
