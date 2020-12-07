#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [Header("GameObjects")]
        [SerializeField] private CharacterController? character;
        [SerializeField] private Camera? lookCam;
        [SerializeField] private WeaponManager? weaponManager;

        [Header("Settings")]
        [SerializeField] private float mouseSensitivity = 200f;
        [SerializeField] private float moveSpeed = 20.0f;
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] private Transform? groundCheck;
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
        private bool fireFlag = false;
        private bool meleeFlag = false;

        void Start()
        {
            //weaponManager = GameManager.Instance.wea
        }

        // Update is called once per frame
        public void UpdateBehaviour()
        {
            if (GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                Fire();
                Look();
                Movement();
            }
        }

        public void LateUpdateBehaviour()
        {
            // if (isGrounded && character.velocity.sqrMagnitude > GameManager.Instance.GameSettings.footstepSpeedSqr)
            // {
            //     if (isDashing)
            //     {
            //         AudioManager.Instance.PlayDash();
            //     }
            //     else
            //     {
            //         AudioManager.Instance.PlayFootsteps();
            //     }
            // }



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
            if (context.started)
            {
                this.fireFlag = true;
            }
            else if (context.canceled)
            {
                this.fireFlag = false;
            }
        }

        public void MeleeEvent(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                this.meleeFlag = true;
            }
        }

        public void DashEvent(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                this.dashFlag = true;
            }
        }

        public void Reset()
        {
            this.transform.position = Vector3.zero; 
            this.transform.rotation = Quaternion.identity;
        }

        private void Fire()
        {
            if (fireFlag && GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                var weapon = GameManager.Instance.WeaponManager?.GetCurrentWeapon();

                if (weapon != null && weapon.CanFire)
                {
                    weapon.Fire();
                    
                    if(!weapon.IsAutomatic)
                    {
                        fireFlag = false;
                    }
                }
            }
            else
            {
                fireFlag = false;
            }
        }

        private void Melee()
        {
            if (meleeFlag && GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                var melee = GameManager.Instance.WeaponManager?.GetMeleeWeapon();

                if (melee != null && melee.CanFire)
                {
                    melee.Fire();
                }
            }
            else
            {
                meleeFlag = false;
            }
        }

        private void Movement()
        {
            Vector3 direction = CalcMovementDirection();

            isGrounded = groundCheck != null ? Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) : true;

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

            if (dashFlag && CanDash())
            {
                dashLastTimer = Time.time;
                isDashing = true;
                dashDirection = direction == Vector3.zero ? transform.forward : direction;
                GameManager.Instance.ConsumeEnergy(GameManager.Instance.GameSettings != null ? GameManager.Instance.GameSettings.dashEnergy : 0);
            }

            Vector3 move = ((direction * moveSpeed) + (transform.up * verticalVelocity) + (isDashing ? dashDirection * dashSpeed : Vector3.zero)) * Time.deltaTime;
            
            character?.Move(move);


            jumpFlag = false;
            dashFlag = false;
        }

        private Vector3 CalcMovementDirection()
        {
            return Vector3.ClampMagnitude(((transform.right * this.moveDirection.x) + (transform.forward * this.moveDirection.y)), 1);
        }

        private void Look()
        {
            var mouse = this.lookDirection * Time.deltaTime * mouseSensitivity;

            this.transform.Rotate(Vector3.up * mouse.x);

            xRotation -= mouse.y;
            xRotation = Mathf.Clamp(xRotation, -90, 90);

            if (lookCam != null)
            {
                lookCam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            }
        }

        private bool CanDash()
        {
            if(GameManager.Instance.GameSettings == null)
            {
                Logger.LogError(this, "No GameSettings found");
                return false;
            }
            return Time.time >= (dashLastTimer + dashTimeout) && (GameManager.Instance.Data.energy >= GameManager.Instance.GameSettings.dashEnergy);
        }

    }
}