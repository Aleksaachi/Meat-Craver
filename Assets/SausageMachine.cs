using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SausageMachine : Interactable
{
    public ItemSpot sausageSpot;
    public Item sausage;
    public GameObject hiddenUI;
    public GameObject leverHitbox;
    public Transform leverModel;
    public Vector3 defaultLeverHitboxPosition;
    public float verticalThresholdTop = 0;
    public float verticalThresholdBottom = 0;
    public float timerHold;
    float timerHoldCurrent;
    bool holdingLever = false;
    public ParticleSystem particlesBurst;
    public ParticleSystem particlesDrip;
    public ParticleSystem particlesSmoke;
    public ParticleSystem particlesEnd;
    Shaking machineShake;

    void Awake()
    {
        machineShake = GetComponent<Shaking>();
        hiddenUI.SetActive(false);
    }
    public override void Interact()
    {
        PlayerStateManager.FocusOnObject(transform.position + transform.forward, -transform.rotation.eulerAngles);
        hiddenUI.SetActive(true);
    }
    public void HoldLever()
    {
        // Start machine
        holdingLever = true;
        machineShake.StartShaking();
        particlesSmoke.Play();
    }

    public void StopHoldingLever()
    {
        // Stop machine
        holdingLever = false;
        leverHitbox.transform.localPosition = defaultLeverHitboxPosition;
        timerHoldCurrent = 0;
        leverModel.localRotation = Quaternion.Euler(-45, 0, 0);

        particlesBurst.Stop();
        particlesDrip.Stop();
        particlesSmoke.Stop();
        machineShake.StopShaking();
    }
    void Update()
    {
        if (holdingLever)
        {
            // Calculate yThreshold
            float yThreshold = ((-(leverHitbox.transform.localPosition.y+verticalThresholdTop) / (verticalThresholdTop + verticalThresholdBottom)) + 1) / 2;
            yThreshold = Mathf.Clamp01(yThreshold);
            // Update lever rotation and hitbox
            leverModel.localRotation = Quaternion.Euler(Mathf.Lerp(-45, 45, yThreshold), 0, 0);
            leverHitbox.transform.localPosition = new Vector3(leverHitbox.transform.localPosition.x, Input.mousePosition.y - Camera.main.pixelHeight / 2, 0);

            // Update shake strenght
            machineShake.shakeStrenght = yThreshold;

            if (yThreshold >= 1)
            {
                // Activate sausage timer and start animations
                if (!particlesBurst.isEmitting)
                {
                    particlesBurst.Play();
                    particlesDrip.Play();
                }
                timerHoldCurrent += Time.deltaTime;

                if (timerHoldCurrent > timerHold)
                {
                    // Spawn sausage
                    SpawnSausage();
                    StopHoldingLever();
                    PlayerStateManager.Unfocus();
                    hiddenUI.SetActive(false);

                    particlesEnd.Play();
                }
            }
            else
            {
                // Stop sausage timer and animations
                particlesBurst.Stop();
                particlesDrip.Stop();
                timerHoldCurrent = 0;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopHoldingLever();
            }
        }
    }
    
    public void SpawnSausage()
    {
        sausageSpot.SpawnItem(sausage);
    }
}
