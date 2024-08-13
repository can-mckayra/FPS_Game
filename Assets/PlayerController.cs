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

    [SerializeField] private bool jumpable = true;

    [SerializeField] private int jumpCredit = 2;

    [SerializeField] private float jumpForce = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        inputZ = Input.GetAxis("Vertical");
        inputX = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && jumpCredit > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCredit--;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            jumpCredit = 2;
        }
    }
}
