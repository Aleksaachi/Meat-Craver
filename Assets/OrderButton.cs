using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderButton : Interactable
{
    public GameObject buttonObject;
    public Vector3 buttonLowPoint;
    public Vector3 buttonHighPoint;
    public float pushTime;
    float pushTimer;
    bool pushing;
    bool rising;
    public override void Interact()
    {
        pushing = true;
        rising = false;
        pushTimer = pushTime;
    }

    void Update()
    {
        if (pushing)
        {
            if (pushTimer > 0 && !rising) pushTimer -= Time.deltaTime;
            else rising = true;

            if (pushTimer < pushTime && rising) pushTimer += Time.deltaTime;

            buttonObject.transform.localPosition = Vector3.Lerp(buttonLowPoint, buttonHighPoint, pushTimer / pushTime);

            if (pushTimer >= pushTime && rising)
            {
                rising = false;
                pushing = false;
            }
        }
    }
}
