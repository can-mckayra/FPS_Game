using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        groundCheckRedundancy();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerController.IsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.IsGrounded = false;
    }

    private void groundCheckRedundancy()
    {
        if (playerController.Rb.velocity.y == 0 && playerController.IsGrounded != true)
        {
            playerController.IsGrounded = true;
        }
    }
}
