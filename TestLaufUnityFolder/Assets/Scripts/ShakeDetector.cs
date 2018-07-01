using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeDetector : MonoBehaviour
{

    public float shakeDetectionThreshold;
    public float shakePeriod;
    private float timeAfterShake;

    private float lowPassFilterFactor;
    private float accelerometerUpdateInterval = 1.0f / 60.0f;
    private Vector3 lowPassValue;
    private float lowPassKernelWidthInSeconds = 1.0f;

    // Use this for initialization
    void Start()
    {

        lowPassFilterFactor = accelerometerUpdateInterval / lowPassKernelWidthInSeconds;
        shakeDetectionThreshold *= shakeDetectionThreshold;
        lowPassValue = Input.acceleration;
        timeAfterShake = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 acceleration = Input.acceleration;
        lowPassValue = Vector3.Lerp(lowPassValue, acceleration, lowPassFilterFactor);
        Vector3 deltaAcceleration = acceleration - lowPassValue;

        timeAfterShake += Time.deltaTime;

        //Debug.Log(deltaAcceleration.sqrMagnitude);

        if ((deltaAcceleration.sqrMagnitude >= shakeDetectionThreshold) && timeAfterShake > shakePeriod)
        {
            timeAfterShake = 0;
            Debug.Log("SHAKE");
            // Perform your "shaking actions" here.
            SendMessageUpwards("ReloadWeapon");
        }
    }
}
