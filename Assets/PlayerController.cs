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
    //[SerializeField] private bool jump = false;

    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float speedSprint = 10.0f;

    private Vector3 move;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        HandleLooking();

        inputZ = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && jumpCredit > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCredit--;
            isGrounded = false;
        }

        if (isGrounded && jumpCredit < jumpCreditMax)
        {
            jumpCredit = jumpCreditMax;
        }

        Debug.Log(eulerAngles);
    }

    private void FixedUpdate()
    {
        /*if (Input.GetKey(KeyCode.LeftShift))
        {
            move = (transform.right * inputX) + (transform.forward * inputZ);
            rb.velocity = move * speedSprint;
        }
        else
        {
            move = (transform.right * inputX) + (transform.forward * inputZ);
            rb.velocity = move * speed;
        }*/

        move = ((transform.right * inputX * speed) + (new Vector3(0.0f, rb.velocity.y, 0.0f)) + (transform.forward * inputZ * speed));
        rb.velocity = move;
    }

    private void LateUpdate()
    {
        playerCamera.transform.eulerAngles = new Vector3(playerCamera.transform.eulerAngles.x, transform.eulerAngles.y, 0.0f);

        if (rb.velocity.y == 0 && isGrounded != true)
        {
            isGrounded = true;
        }
    }

    private void HandleLooking()
    {
        eulerAngles = playerCamera.transform.eulerAngles;
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, -90.0f, 90.0f);
        
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        playerCamera.transform.Rotate(mouseSensitivity * -mouseY * Vector3.right);
        transform.Rotate(mouseSensitivity * mouseX * Vector3.up);
        //playerCamera.transform.eulerAngles = eulerAngles;
    }

    /*private void HandleMovement()
    {

    }
    */

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void SetIsGrounded(bool value)
    {
        isGrounded = value;
    }
}
