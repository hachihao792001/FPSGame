using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{

    GameObject PlayerBody;
    Rigidbody rb;
    Vector3 input;

    [Header("Mouse")]
    public float sentivity;
    public float yaw, pitch;
    public float yawUpLimit, yawDownLimit;

    [Space]
    [Header("Movement")]
    public float walkSpeed;
    public float runSpeed;
    public float currentSpeed;
    public float jumpForce;

    [Space]
    public bool running;
    public bool grounded;
    public float groundDistance;

    public GameObject shootDirection;

    // Use this for initialization
    void Start()
    {
        PlayerBody = transform.parent.gameObject;
        rb = PlayerBody.GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.playing) return;

        grounded = Physics.Raycast(PlayerBody.transform.position, Vector3.down, groundDistance);

        //input
        input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        yaw -= Input.GetAxis("Mouse Y") * sentivity;
        yaw = Mathf.Clamp(yaw, yawDownLimit, yawUpLimit);
        pitch += Input.GetAxis("Mouse X") * sentivity;

        transform.parent.eulerAngles = new Vector3(0, pitch, 0);

        if (GameManager.shooting)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(yaw, pitch, 0), 0.1f);
        else
            transform.eulerAngles = new Vector3(yaw, pitch, 0);

        //movement
        running = Input.GetKey(KeyCode.LeftControl);
        currentSpeed = (running) ? runSpeed : walkSpeed;

        Vector3 localVelocity = transform.parent.InverseTransformDirection(rb.velocity);
        localVelocity = new Vector3(input.x * currentSpeed, rb.velocity.y, input.z * currentSpeed);
        rb.velocity = transform.parent.TransformDirection(localVelocity);


        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}