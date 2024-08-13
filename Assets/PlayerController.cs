using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    private LayerMask groundLayer;

    [SerializeField] private float inputZ;
    [SerializeField] private float inputX;
    [SerializeField] Vector3 input;

    [SerializeField] private bool isGrounded = true;

    [SerializeField] private int jumpCredit;
    [SerializeField] private int jumpCreditMax = 2;
    //[SerializeField] private bool jump = false;

    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float speedSprint = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //inputZ = Input.GetAxis("Vertical");
        //inputX = Input.GetAxis("Horizontal");

        input = new Vector3(inputX = Input.GetAxis("Horizontal"), 0, inputZ = Input.GetAxis("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && jumpCredit > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCredit--;
            isGrounded = false;
        }

        if (isGrounded && jumpCredit < jumpCreditMax)
        {
            jumpCredit = jumpCreditMax;
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.MovePosition(transform.position + input * speedSprint * Time.deltaTime);
        }
        else
        {
            rb.MovePosition(transform.position + input * speed * Time.deltaTime);
        }
    }
    
    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public void SetIsGrounded(bool value)
    {
        isGrounded = value;
    }
}
