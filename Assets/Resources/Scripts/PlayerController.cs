using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;
    Rigidbody thisRigidbody;
    public float speed = 5;
    public float lockTime;
    float lockTimer;
    Vector3 lockPosition;
    Vector3 startPosition;
    void Awake()
    {
        instance = this;   
    }
    void Start()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (PlayerStateManager.instance.focused)
        {
            if (lockTimer > 0) lockTimer -= Time.deltaTime;
            transform.position = Vector3.Lerp(lockPosition, startPosition, lockTimer / lockTime);
        }
        else
        {
            float forwardInput = Input.GetAxisRaw("Vertical");
            float sideInput = Input.GetAxisRaw("Horizontal");
            float rotation = CameraController.instance.currentRotationX * Mathf.Deg2Rad;
            if (forwardInput != 0)
            {
                Vector3 direction = new Vector3(Mathf.Sin(rotation), 0, Mathf.Cos(rotation));
                thisRigidbody.AddForce(direction * speed * forwardInput * Time.deltaTime);
            }

            if (sideInput != 0)
            {
                float directionOffset = 90 * Mathf.Deg2Rad * sideInput;
                Vector3 direction = new Vector3(Mathf.Sin(rotation + directionOffset), 0, Mathf.Cos(rotation + directionOffset));
                thisRigidbody.AddForce(direction * speed * Time.deltaTime);
            }
        }
    }

    public void LockPositionAt(Vector3 position){
        position.y = transform.position.y;
        lockPosition = position;
        startPosition = transform.position;
        thisRigidbody.velocity = Vector3.zero;
        thisRigidbody.angularVelocity = Vector3.zero;
        lockTimer = lockTime;
    }
}
