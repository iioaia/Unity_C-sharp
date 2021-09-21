using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HardlyBrief.Movement {

    [RequireComponent(typeof(Rigidbody))]
    public class MovementController : MonoBehaviour {

        public bool IsGounded { get { return _isGrounded; } }
        public bool IsSprinting { get { return _isSprinting; } }
        public float CurrentSpeed { get { return _rigidBody.velocity.magnitude; } }

        [Header("Movement Controls")]
        [SerializeField]        
        protected MovementControls _movementControls;

        [Header("Base Movement")]
        [SerializeField]
        protected float _baseMaxSpeed = 15f;
        [SerializeField]
        protected float _baseMoveForce = 100f;
        [SerializeField]
        protected float _stopSlideSpeed = 1f;

        [Header("Ground Check")]
        [SerializeField]
        protected bool _shouldCheckGround = true;
        [SerializeField]
        protected Transform _groundCheckTransform;
        [SerializeField]
        protected float _groundCheckRadius = 0.25f;
        [SerializeField]
        protected LayerMask _groundLayerMask;

        [Header("Sprinting")]
        [SerializeField]
        protected bool _allowSprinting = true;
        [SerializeField]
        protected float _sprintMaxSpeed = 20f;
        [SerializeField]
        protected float _sprintMoveForce = 150f;

        [Header("Jumping")]
        [SerializeField]
        protected bool _allowJumping = true;
        [SerializeField]
        protected float _jumpForce = 25f;

        
        protected Rigidbody _rigidBody;
        protected bool _isGrounded = true;
        protected bool _isSprinting = false;
        protected float _currentMaxSpeed;
        protected float _currentMoveForce;


        private void Awake() {
            _rigidBody = this.GetComponent<Rigidbody>();
            
            FirstInitialize();
        }

        protected virtual void FirstInitialize(){
            ResetBaseSpeedAndForce();

            if(_movementControls == null){
                throw new System.Exception($"Movement Controller on {this.gameObject.name} requires a MovementControls class.");
            } else {

                _movementControls.Initialize();
                _movementControls.StartSprint += Sprinting;
                _movementControls.StopSprint += () => {
                    ResetBaseSpeedAndForce();
                    _isSprinting = false;
                };
                _movementControls.Jump += Jump;
            }

            if(_shouldCheckGround && _groundCheckTransform == null) {
                throw new System.Exception($"Movement Controller on {this.gameObject.name} requires a groundCheckTransform object.");
            }
        }

        protected virtual void FixedUpdate() {
            Move();
            PhysicsChecks();
        }

        protected virtual void Move(){
            // check to see if we are sliding too much and stop movement
            if(_rigidBody.velocity.magnitude <= _stopSlideSpeed && _movementControls.MoveDirection == Vector2.zero)
                _rigidBody.velocity = Vector3.zero;

            // adjusts for max speeds
            float speedFactor = (_currentMaxSpeed - _rigidBody.velocity.magnitude ) / _currentMaxSpeed;
            Vector3 moveDirection = new Vector3(_movementControls.MoveDirection.x, 0f, _movementControls.MoveDirection.y);
            _rigidBody.AddForce(speedFactor * _currentMoveForce * moveDirection, ForceMode.Force);
        }

        protected virtual void PhysicsChecks(){
            // Ground Check
            if(_shouldCheckGround)
               GroundCheck();
        }

        protected virtual void GroundCheck(){
             _isGrounded = Physics.CheckSphere(_groundCheckTransform.position, _groundCheckRadius, _groundLayerMask);
        }

        protected virtual void Sprinting(){
            if(_allowSprinting && _isGrounded){
                _isSprinting = true;
                _currentMaxSpeed = _sprintMaxSpeed;
                _currentMoveForce = _sprintMoveForce;
            }
        }

        protected void ResetBaseSpeedAndForce(){
            _currentMaxSpeed = _baseMaxSpeed;
            _currentMoveForce = _baseMoveForce;
        }

        protected virtual void Jump(){
            if(_allowJumping && _isGrounded){
                
                _rigidBody.AddForce(new Vector3(0f, _jumpForce, 0f), ForceMode.Impulse);
            }
        }
    }

    public abstract class MovementControls : MonoBehaviour {

        public event System.Action Jump;
        public event System.Action StartSprint;
        public event System.Action StopSprint;
        public Vector2 MoveDirection;


        public abstract void Initialize();

        protected virtual void OnJump(){
            Jump?.Invoke();
        }

        protected virtual void OnStartSprint(){
            StartSprint?.Invoke();
        }

        protected virtual void OnStopSprint(){
            StopSprint?.Invoke();
        }
    }

    

}