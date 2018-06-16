using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : MonoBehaviour {

    public Transform bulletHolder;
    public int maxBullets;
    public float reloadTime;
    public AudioClip reloadSound;
    private Queue<GameObject> bullets;
    private AudioSource audioSource;

    void Start () {
        bullets = new Queue<GameObject>();
        audioSource = GetComponent<AudioSource>();
        ReloadWeapon();
    }

    public int BulletCount()
    {
        return bullets.Count;
    }

    public GameObject addBullet()
    {
        Object bullet = Resources.Load("Bullet");

        Vector3 bulletPos = new Vector3(-5 - (maxBullets - bullets.Count - 1) * 90, 5, 0);
        GameObject bullet_go = Instantiate(bullet, bulletHolder) as GameObject;
        bullet_go.transform.localPosition = bulletPos;

        bullets.Enqueue(bullet_go);

        return bullet_go;
    }

    public void removeBullet()
    {
        GameObject shot_bullet = bullets.Dequeue();
        Destroy(shot_bullet);
    }

    public void ReloadWeapon()
    {
        StartCoroutine(IReloadWeapon());
    }

    private IEnumerator IReloadWeapon()
    {
        yield return new WaitForSeconds(reloadTime);
        for (int i = 0; i < maxBullets; i++)
        {
            addBullet();
        }
        audioSource.clip = reloadSound;
        audioSource.Play();
    }
}
