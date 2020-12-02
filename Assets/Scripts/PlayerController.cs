using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private CharacterController character = null;
        [SerializeField] private Camera lookCam = null;

        [Header("Settings")]
        [SerializeField] private float mouseSensitivity = 200f;
        [SerializeField] private float moveSpeed = 20.0f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.4f;
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private float jumpSpeed = 20f;
        [SerializeField] private float dashSpeed = 20.0f;
        [SerializeField] private float dashTimeout = 5f;
        [SerializeField] private float dashPlayTime = 0.4f;


        Vector3 velocity;


        bool isDashing = false;
        float dashLastTimer = 0;
        Vector3 dashDirection = Vector3.zero;

        bool isGrounded;
        bool doubleJumpAllowed = true;

        float xRotation = 0f;
        float verticalVelocity = 0;

        private Vector2 lookDirection = Vector2.zero;
        private Vector2 moveDirection = Vector2.zero;
        private bool jumpFlag = false;
        private bool dashFlag = false;

        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        public void UpdateBehaviour()
        {
            Look();
            Movement();
        }

        public void MoveEvent(InputAction.CallbackContext context)
        {
            this.moveDirection = context.ReadValue<Vector2>();
        }


        public void LookEvent(InputAction.CallbackContext context)
        {
            this.lookDirection = context.ReadValue<Vector2>();
        }

        public void JumpEvent(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                this.jumpFlag = true;
            }
        }

        public void FireEvent(InputAction.CallbackContext context)
        {
            Logger.Log(this, "Fire!");
        }

        public void DashEvent(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                this.dashFlag = true;
            }
        }

        private void Movement()
        {
            Vector3 direction = Vector3.ClampMagnitude(((transform.right * this.moveDirection.x) + (transform.forward * this.moveDirection.y)), 1);
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

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

            if (jumpFlag && isGrounded)
            {
                verticalVelocity += Mathf.Sqrt(jumpSpeed * gravity * -2);
            }

            if (jumpFlag && !isGrounded && doubleJumpAllowed)
            {
                verticalVelocity += Mathf.Sqrt(jumpSpeed * gravity * -2);
                doubleJumpAllowed = false;
            }

            if (dashFlag && Time.time >= (dashLastTimer + dashTimeout))
            {
                dashLastTimer = Time.time;
                isDashing = true;
                dashDirection = direction == Vector3.zero ? transform.forward : direction;
            }

            Vector3 move = ((direction * moveSpeed) + (transform.up * verticalVelocity) + (isDashing ? dashDirection * dashSpeed : Vector3.zero)) * Time.deltaTime;
            character.Move(move);

            jumpFlag = false;
            dashFlag = false;
        }

        private void Look()
        {
            var mouse = this.lookDirection * Time.deltaTime * mouseSensitivity;

            this.transform.Rotate(Vector3.up * mouse.x);

            xRotation -= mouse.y;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            lookCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        }
    }
}