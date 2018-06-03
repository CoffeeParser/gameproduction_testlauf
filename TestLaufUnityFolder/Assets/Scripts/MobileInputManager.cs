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

    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    public float editorWalkSpeed = 2;

    public Rigidbody rocketInstance;

    private void Start()
    {
        MicInput.Instance = GetComponent<MicInput>();
        MicInput.Instance.InitMic(); // start mic input recording

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

    Vector3 velocity = new Vector3();
    Vector3 movement = new Vector3();

    float inAirMultiplier = 0.25f;
    float speed = 17f;

    private float _miniMumDecibelToTriggerPeng = -20.0f;
    // TODO: peng cooldown implementieren
    private void Update()
    {

#if UNITY_EDITOR
        //Debug.Log("Mic db:" + MicInput.MicLoudnessinDecibels);
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch -= speedV * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKeyDown(KeyCode.Mouse0) || MicInput.MicLoudnessinDecibels > _miniMumDecibelToTriggerPeng)
        {
            SpawnProjectile();
        }
#else
        if (gyro.enabled)
        {
            transform.localRotation = gyro.attitude * rot;
        }
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.GetTouch(i).phase == TouchPhase.Began)
            {
                SpawnProjectile();
            }
        }
#endif

    }

    void SpawnProjectile()
    {
        
        rocketInstance = Instantiate(rocketPrefab, barrelEnd.position, barrelEnd.rotation) as Rigidbody;
        rocketInstance.AddForce(barrelEnd.forward * projetileSpeed);
        AudioSource s = GetComponent<AudioSource>();
        s.Play();
    }


}
