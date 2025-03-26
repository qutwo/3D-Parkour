using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerStateMachine : MonoBehaviour
{
    [SerializeReferenceDropdown]
    [SerializeReference] public StateFactory stateFactory;
    [SerializeField] float walkSpeed;
    [SerializeField] float sprintSpeed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float slideSpeed;
    [SerializeField] float jumpTime;
    [SerializeField] float slideTime;
    [SerializeField] float forceAppliedInAir;
    [SerializeField] LayerMask ground;
    [SerializeField] Animator animator;
    [SerializeField]Camera cam;
    [SerializeField] float gravity; 
    [SerializeField] float slopeLimit;
    [SerializeField] float groundGravity;
    [SerializeField] float distanceToGround;
    Vector2 movedir;
    Vector2 lookdir;
    bool isGrounded;
    bool isJumping = false;
    bool isCrouching = false;
    bool isSliding = false;
    bool isSlope = false;
    PlayerCharacterController controller;
    Rigidbody rb;
    CapsuleCollider capsule;
    Inputs inputs;
    InputAction moveinput;
    InputAction sprintinput;
    InputAction jumpinput;
    InputAction crouchinput;
    Vector3 moveDirection = Vector3.zero;

    
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        controller = new(rb,capsule);
        inputs = new();
        moveinput = inputs.Player.move;
        sprintinput = inputs.Player.sprint;
        jumpinput = inputs.Player.jump;
        crouchinput = inputs.Player.crouch;
        stateFactory.setPrerequisites();
        controller._setGroundLayer(ground);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
    }
    private void OnEnable()
    {
        inputs.Player.Enable();
    }
    private void OnDisable()
    {
        inputs.Player.Disable();
    }

  

    void Update()
    {
        
        isGrounded = controller.isGrounded();


        stateFactory.update();
       




    }

    void FixedUpdate()
    {
        _controller._TGTvelocityDirection = moveDirection;
        Artificialgravity();
        stateFactory.fixedUpdate();
    }

    Vector2 move()
    {
        return moveinput.ReadValue<Vector2>();
    }
    public void look()
    {
        transform.rotation = Quaternion.Euler(0, Mathf.Atan2(move().x, move().y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y, 0);
    }
    public Vector3 MovementVector()
    {

    
       
     

        
        Vector3 outp = Quaternion.Euler(0, Mathf.Atan2(move().x, move().y) * Mathf.Rad2Deg + cam.transform.eulerAngles.y, 0)*Vector3.forward;
        if (isGrounded) 
        {
            float angle = Vector3.Angle(Vector3.up, controller._hit.normal);
            if(angle < slopeLimit && angle != 0)
            {
                outp = Vector3.ProjectOnPlane(outp, controller._hit.normal);
                isSlope = true;
                return outp;
                
            }
           
        }
        isSlope = false;
        return outp;
    }

    public void Artificialgravity()
    {
        if (!isGrounded)
            controller.ApplyGravity(gravity,Vector3.down);
        else if (isSlope)
        {
            controller.ApplyGravity(groundGravity,-controller._hit.normal);
            
        }
        

    }

    private void OnAnimatorIK(int layerIndex)
    {
        if(isGrounded && !isSliding && !moveinput.IsInProgress())
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
            animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
            animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);
            RaycastHit hit;
            if (Physics.Raycast(animator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up*0.01f, Vector3.down, out hit, 1f, ground))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += distanceToGround;
                animator.SetIKPosition(AvatarIKGoal.LeftFoot, footPosition);
                animator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
               
            }
            if (Physics.Raycast(animator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up*0.01f, Vector3.down, out hit, 1f, ground))
            {
                Vector3 footPosition = hit.point;
                footPosition.y += distanceToGround;
                animator.SetIKPosition(AvatarIKGoal.RightFoot, footPosition);
                animator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }

        }
       

    }

    public InputAction _move { get { return moveinput; } }
    public InputAction _sprint { get { return sprintinput; } }
    public InputAction _jump { get { return jumpinput; } }
    public InputAction _crouch { get { return crouchinput; } }
    public Animator _animator { get { return animator; } }
    public bool _grounded { get { return isGrounded; } }
    public bool _isJumping { get { return isJumping; } set { isJumping = value; } }
    public bool _isCrouching { get { return isCrouching; } set { isCrouching = value; } }
    public bool _isSliding { get { return isSliding; } set { isSliding = value; } }
    public PlayerCharacterController _controller { get { return controller; } }
    public float _walkSpeed { get { return walkSpeed; } }
    public float _sprintSpeed { get { return sprintSpeed; } }
    public float _jumpSpeed { get { return jumpSpeed; } }
    public float _crouchSpeed { get { return crouchSpeed; } }
    public float _slideSpeed { get { return slideSpeed; } }
    public float _jumpTime { get { return jumpTime; } }
    public float _slideTime { get { return slideTime; } }
    public float _forceAppliedInAir { get { return forceAppliedInAir; } }
    public float _moveDirectionx { set { moveDirection.x = value; } }
    public float _moveDirectiony { set { moveDirection.y = value; } }
    public float _moveDirectionz { set { moveDirection.z = value; } }
}
