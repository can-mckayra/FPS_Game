using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private Camera playerCamera;

    private LayerMask groundLayer;

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private float mouseSensitivity = 5.0f;

    [SerializeField] private Vector3 eulerAngles;

    [SerializeField] private float inputZ;
    [SerializeField] private float inputX;

    [SerializeField] private bool isGrounded = true;

    [SerializeField] private int jumpCredit;
    [SerializeField] private int jumpCreditMax = 2;

    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float speedSprint = 10.0f;

    private Vector3 move;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerCamera = GetComponentInChildren<Camera>();

        jumpCredit = jumpCreditMax;
    }

    void Update()
    {
        HandleInputMovement();
        HandleJumping();
        //HandleCamera();

        //Debug.Log(transform.rotation.eulerAngles);
    }


    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        //HandleCamera();
    }

    private void HandleInputMovement()
    {
        inputZ = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");
    }

    private void HandleMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift) && inputZ > 0)
        {
            move = ((inputX * speedSprint * transform.right) + (new Vector3(0.0f, rb.velocity.y, 0.0f)) + (inputZ * speedSprint * transform.forward));
            rb.velocity = move;
        }
        else
        {
            move = ((inputX * speed * transform.right) + (new Vector3(0.0f, rb.velocity.y, 0.0f)) + (inputZ * speed * transform.forward));
            rb.velocity = move;
        }
    }

    private void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCredit > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z); // this is better
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCredit--;
            isGrounded = false;
        }

        if (isGrounded && jumpCredit < jumpCreditMax)
        {
            jumpCredit = jumpCreditMax;
        }
    }

    private void HandleCamera()
    {

        mouseX = (mouseX + Input.GetAxis("Mouse X") * mouseSensitivity) % 360;
        mouseY = (mouseY + Input.GetAxis("Mouse Y") * mouseSensitivity) % 360;

        mouseY = Mathf.Clamp(mouseY, -90.0f, 90.0f);

        playerCamera.transform.localRotation = Quaternion.Euler(-mouseY, 0.0f, 0.0f);
        transform.rotation = Quaternion.Euler(0.0f, mouseX, 0.0f);
    }
    
    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }

    public Rigidbody Rb
    {
        get => rb;
        private set => rb = value;
    }
}
