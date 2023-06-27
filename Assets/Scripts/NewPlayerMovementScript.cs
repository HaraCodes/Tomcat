using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NewPlayerMovementScript : NetworkBehaviour
{
    //Jumping animation
    int IsJumpingHash = Animator.StringToHash("IsJumping");

    [Header("Movement")]
    public float movespeed;
    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Jump Animation")]
    public GameObject armature;
    Animator animator;


    [Header("Ground Check")]
    public LayerMask whatIsGround;
    private bool grounded;
    public float PlayerHeight;

    [Header("Key Bindings")]
    public KeyCode jumpKey = KeyCode.Space;

    public Transform Orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        animator = armature.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, PlayerHeight * 0.5f + 0.2f, whatIsGround);
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        if (!IsOwner) return;
        MyInput();
        speedControl();
    }

    private void FixedUpdate()
    {
        if (!IsOwner) return;
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey) && grounded && readyToJump)
        {
            //Debug.Log("Jumped");
            readyToJump = false;
            jump();
            Invoke(nameof(resetJump), jumpCooldown);
        }
    }

    private void speedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);


        if (flatVel.magnitude > movespeed)
        {
            /*
         * rb.velocity = flatVel.normalized * movespeed;
         * doing this will affect the speed while jumping, so we use the below statement instead
         */
            flatVel = flatVel.normalized * movespeed;
            rb.velocity = new Vector3(flatVel.x, rb.velocity.y, flatVel.z);
        }
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = Orientation.forward * verticalInput + Orientation.right * horizontalInput;

        //if in ground
        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * movespeed * 10f, ForceMode.Force);
            if (animator.GetBool(IsJumpingHash))
                animator.SetBool(IsJumpingHash, false);
        }

        //else if (!grounded) ;
           
    }

    private void jump()
    {

        animator.SetBool(IsJumpingHash, true);
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        rb.AddForce(moveDirection.normalized * movespeed * 10f * airMultiplier, ForceMode.Force);
    }

    public void resetJump()
    {
        readyToJump = true;
    }

}
