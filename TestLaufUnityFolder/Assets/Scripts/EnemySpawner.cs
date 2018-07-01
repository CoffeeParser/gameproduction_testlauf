using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemy;
    public int enemyCount = 0;
    public float maxRange;
    public float minRange;
    private int radiusID;
    public Text score;
    private AudioSource audioSource;
    public AudioClip dyingSound;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        Generate();
    }
	
    public void Generate()
    {
        DestroyAll();
        for (int i = 0; i <= enemyCount - 1; i++)
        {
            Instantiate(enemy, GetRandomPosition(i * System.DateTime.Now.Millisecond), Quaternion.identity, transform);
        }
    }



    public void DestroyAll()
    {
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    public Vector3 GetRandomPosition(int seed = 0)
    {
        Random.InitState(seed);
        float randX = Random.Range(-2, 2);
        float randY = Random.Range(0, 2);
        float randZ = Random.Range(-2, 2);
        float randMagn = minRange + (Random.value * maxRange);

        Vector3 newPos = new Vector3(randX, randY, randZ) * randMagn + transform.position;

        //Debug.DrawRay(transform.position, newPos, Color.red, 0.5f);

        return newPos;
    }

    private void OnTransformChildrenChanged()
    {
        score.text = transform.childCount + " Gockels left!";
    }

    public void OnGockelDied()
    {
        audioSource.clip = dyingSound;
        audioSource.Play();

        //Destroy(gameObject);
    }
}

[CustomEditor(typeof(EnemySpawner))]
public class EnemySpawnerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemySpawner myScript = (EnemySpawner)target;
        if (GUILayout.Button("Generate"))
        {
            myScript.Generate();
        }
    }
}
