using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController character = null;
    [SerializeField] private Camera cam = null;
    [SerializeField] private float moveSpeed = 20.0f;
    public float mouseSensitivity = 100f;

    Vector3 velocity;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpSpeed = 20f;
    [SerializeField] private float dashSpeed = 20.0f;
    [SerializeField] private float dashTimeout = 5f;
    [SerializeField] private float dashPlayTime = 0.4f;

    bool isDashing = false;
    float dashLastTimer = 0;
    Vector3 dashDirection = Vector3.zero;

    bool isGrounded;
    bool doubleJumpAllowed = true;

    float xRotation = 0f;
    float verticalVelocity = 0;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Look();
        Movement();
    }

    void FixedUpdate()
    {
        //
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 direction = Vector3.ClampMagnitude(((transform.right * x) + (transform.forward * z)), 1);

        if (isGrounded)
        {
            doubleJumpAllowed = true;
        }

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -groundDistance;
        }

        if (isDashing && Time.time > (dashLastTimer + dashPlayTime))
        {
            isDashing = false;
            dashDirection = Vector3.zero;
        }        

        verticalVelocity += gravity * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            verticalVelocity += Mathf.Sqrt(jumpSpeed * gravity * -2);
        }

        if (Input.GetButtonDown("Jump") && !isGrounded && doubleJumpAllowed)
        {
            verticalVelocity += Mathf.Sqrt(jumpSpeed * gravity * -2);
            doubleJumpAllowed = false;
        }

        if (Input.GetButtonDown("Dash") && Time.time >= (dashLastTimer + dashTimeout))
        {
            Debug.Log("Dash");
            dashLastTimer = Time.time;
            isDashing = true;
            dashDirection = direction == Vector3.zero ? transform.forward : direction;
        }

        Vector3 move = ((direction * moveSpeed) + (transform.up * verticalVelocity) + (isDashing ? dashDirection * dashSpeed : Vector3.zero)) * Time.deltaTime;
        character.Move(move);
    }

    private void Look()
    {
        var mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        var mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        this.transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
