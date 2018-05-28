using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputManager : MonoBehaviour {

    private bool gyroEnabled;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    public Rigidbody rocketPrefab;
    public Transform barrelEnd;
    public float projetileSpeed;

    private void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnabledGyro();

    }

    private bool EnabledGyro()
    {
        if (SystemInfo.supportsGyroscope)
        {

            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f,90f,0);
            rot = new Quaternion(0,0,1,0);

            return true;
        }
        return false;
    }

    private void Update()
    {

        if (gyro.enabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
        



        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                Rigidbody rocketInstance;
                rocketInstance = Instantiate(rocketPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
                rocketInstance.AddForce(barrelEnd.forward * projetileSpeed);
                AudioSource s = GetComponent<AudioSource>();
                s.Play();
            }
        }

    }


}
