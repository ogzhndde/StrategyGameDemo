using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;


public class CinemachineShake : SingletonManager<CinemachineShake>
{
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;

    public float shakeTimer;
    // Start is called before the first frame update
    void Awake()
    {

    }
    public void ShakeCamera(float intensity, float time)
    {

        cinemachineBasicMultiChannelPerlin =
                     cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        shakeTimer = time;
    }
    // Update is called once per frame
    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {

                cinemachineBasicMultiChannelPerlin =
                     cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;

            }
        }
    }
}
