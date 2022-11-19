using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public Vector3 originalCamPos;
    public CinemachineVirtualCamera cam;
    CinemachineBasicMultiChannelPerlin p;

    public float shakeVariation;
    public bool isShaking;
    public float shakeTimer;
    public bool isUsingTimer = true;

    public void ShakeCamera(float duration)
    {
        shakeTimer = duration;
    }

    public void Start()
    {
        originalCamPos = transform.position;
        p = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isUsingTimer)
        {
            ShakeCameraWithTimer();
        }
    }

    public void ShakeCameraWithTimer()
    {
        if (shakeTimer > 0)
        {
            if (!isShaking)
            {
                isShaking = true;
            }

            p.m_AmplitudeGain = shakeVariation;
            shakeTimer -= Time.deltaTime;
        }

        if (shakeTimer <= 0)
        {
            cameraTransform.position = originalCamPos;
            p.m_AmplitudeGain = 0;
        }
    }

    public void StartShakingCamera(float cameraShakeVariation)
    {
        isUsingTimer = false;
        p.m_AmplitudeGain = cameraShakeVariation;
    }

    public void StopShakingCamera()
    {
        isUsingTimer = true;
        p.m_AmplitudeGain = 0;
    }
}
