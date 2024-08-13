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

    private void OnTriggerEnter(Collider other)
    {
        playerController.SetIsGrounded(true);
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.SetIsGrounded(false);
    }
}
