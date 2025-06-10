using System;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeFrames;

    public float shakeStrength = 0;
    private void Awake()
    {
        EnemyBehaviour.AttackCameraShake += AttackConnectCameraShake;
    }

    private void Update()
    {
        if (shakeFrames > 0)
        { CameraShakeMethod(shakeStrength); }
    }

    private void FixedUpdate()
    {
        if (shakeFrames > 0)
        { shakeFrames--; }
    }

    public void AttackConnectCameraShake(float chargeDealt)
    {
        shakeStrength = chargeDealt / 40.0f;
        shakeFrames = chargeDealt / 4.0f;
    }

    private void CameraShakeMethod(float strength)
    {
        float randomX = UnityEngine.Random.value - 0.5f;
        float randomY = UnityEngine.Random.value - 0.5f;
        float randomZ = UnityEngine.Random.value - 0.5f;

        transform.localEulerAngles = new Vector3(randomX, randomY, randomZ) * strength;
    }
}
