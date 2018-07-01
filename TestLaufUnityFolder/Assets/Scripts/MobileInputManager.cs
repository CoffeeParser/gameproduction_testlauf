using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputManager : MonoBehaviour {

    public Rigidbody rocketPrefab;
    public float projetileSpeed;

    public float speedHorizontal = 2.0f;
    public float speedVertical = 2.0f;
    public float editorWalkSpeed = 2;

    public AudioClip shootSound;

    private Magazine magazine;
    private Gyroscope gyro;

    private GameObject cameraContainer;
    private Quaternion rot;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    private AudioSource audioSource;
    public float _miniMumDecibelToTriggerPeng = -20.0f;
    public float _pengDelaySeconds = 1.20f;
    private float _currentDelaySeconds;
    private bool _shotAllowed = true;

    private void Start()
    {
        MicInput.Instance = GetComponent<MicInput>();
        MicInput.Instance.InitMic(); // start mic input recording

        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        magazine = GetComponent<Magazine>();
        audioSource = GetComponent<AudioSource>();

        TryEnableGyro();
    }

    private bool TryEnableGyro()
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

    // TODO: peng cooldown implementieren
    private void Update()
    {

        if (gyro != null)
        {
            if (gyro.enabled && gyro != null)
            {
                transform.localRotation = gyro.attitude * rot;
            }
        }
        else
        {
            yaw += speedHorizontal * Input.GetAxis("Mouse X");
            pitch -= speedVertical * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }

        if (_currentDelaySeconds < _pengDelaySeconds && !_shotAllowed)
        {
            Debug.Log(_currentDelaySeconds);
            _currentDelaySeconds += Time.deltaTime;
        }
        else
        {
            
            _shotAllowed = true;
        }
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                shoot();
        } else if (Input.GetKeyDown(KeyCode.Mouse0) || MicInput.MicLoudnessinDecibels > _miniMumDecibelToTriggerPeng && _shotAllowed)
        {
            _shotAllowed = false;
            _currentDelaySeconds = 0.0f;
            
            shoot();
        }

        if (Input.GetKeyDown(KeyCode.R))
            magazine.ReloadWeapon();
    }

    private void shoot()
    {
        if (magazine.BulletCount() > 0)
        {
            SpawnProjectile();
            magazine.removeBullet();
        }
    }

    private void SpawnProjectile()
    {
        Rigidbody rocketInstance = Instantiate(rocketPrefab, transform.position, transform.rotation) as Rigidbody;
        rocketInstance.AddForce(transform.forward * projetileSpeed, ForceMode.Impulse);
        audioSource.clip = shootSound;
        audioSource.Play();
    }
}
