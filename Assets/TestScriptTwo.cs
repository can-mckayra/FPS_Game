using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScriptTwo : MonoBehaviour
{
    [SerializeField] private Quaternion myRotation;
    [SerializeField] private Quaternion forwardRotation;

    void Update()
    {
        myRotation = transform.rotation;
        forwardRotation = Quaternion.LookRotation(Vector3.forward);

        if (Input.GetKey(KeyCode.R))
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.02f);
        }
    }
}
