using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerOrientation;

    [SerializeField] private float mouseX;
    [SerializeField] private float mouseY;
    [SerializeField] private float mouseSensitivity = 5.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseX = (mouseX + Input.GetAxis("Mouse X") * mouseSensitivity) % 360;
        mouseY = (mouseY + Input.GetAxis("Mouse Y") * mouseSensitivity) % 360;

        mouseY = Mathf.Clamp(mouseY, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(-mouseY, mouseX, 0.0f);
        playerOrientation.rotation = Quaternion.Euler(0.0f, mouseX, 0.0f);
    }

}
