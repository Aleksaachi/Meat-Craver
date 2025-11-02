using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    public float shakeStrenght;
    Vector3 defaultRotation;
    public bool shakeOnAwake;
    bool shaking = false;
    void Start()
    {
        if (shakeOnAwake) StartShaking();
    }
    void Update()
    {
        if (shaking)
        {
            float xShake = Random.Range(-shakeStrenght, shakeStrenght);
            float yShake = Random.Range(-shakeStrenght, shakeStrenght);
            float zShake = Random.Range(-shakeStrenght, shakeStrenght);
            transform.rotation = Quaternion.Euler(defaultRotation.x + xShake, defaultRotation.y + yShake, defaultRotation.z + zShake);
        }
    }

    public void StartShaking()
    {
        defaultRotation = transform.rotation.eulerAngles;
        shaking = true;
    }

    public void StopShaking()
    {
        shaking = false;
        transform.rotation = Quaternion.Euler(defaultRotation.x, defaultRotation.y, defaultRotation.z);
    }
}
