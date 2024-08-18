using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private Rigidbody rb;

    //[SerializeField] private LayerMask groundLayer;

    [SerializeField] private bool isGrounded;

    [SerializeField] private float playerHeight = 2.0f;

    [SerializeField] private float groundDrag = 5.0f;

    [SerializeField] private float verticalInput;
    [SerializeField] private float horizontalInput;

    [SerializeField] private float speedWalk = 5;
    [SerializeField] private float speedSprint = 7.5f;

    [SerializeField] private Vector3 movementDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //groundLayer = 7;
    }
    
    void Update()
    {
        HandleInput();
        GroundCheck();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleDrag();
    }

    private void HandleInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void HandleMovement()
    {
        movementDirection = transform.forward * verticalInput + transform.right * horizontalInput;

        rb.AddForce(movementDirection.normalized * speedWalk, ForceMode.Force);
    }

    private void HandleDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0.0f;
        }
    }

    private void GroundCheck()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f);
    }
}
