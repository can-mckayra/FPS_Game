using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class RecallAbility : MonoBehaviour
{
    private TestScript testScript;

    [SerializeField] private bool isRecalling = false;
    [SerializeField] private bool lockMovementInput = false;
    [SerializeField] private bool lockLookInput = false;
    [SerializeField] private bool enableRotationFinilize = false;

    [SerializeField] private float rewindBackSeconds = 3.0f;
    [SerializeField] private float rewindDuration = 1.25f;
    [SerializeField] private float cooldown = 5.0f;
    [SerializeField] private int maxPositions;

    [SerializeField] private float nextRecallTime;

    private float elapsedTime = 0.0f;
    private float interpolationRatio = 0.0f;

    private float t = 0.0f;

    [SerializeField] private Quaternion pastRotation;
    private List<Vector3> positions = new List<Vector3>();
    private List<Quaternion> rotations = new List<Quaternion>();

    void Start()
    {
        testScript = GetComponent<TestScript>();

        maxPositions = Mathf.CeilToInt(rewindBackSeconds / Time.fixedDeltaTime);

        /*if (cooldown < rewindTime)
        {
            cooldown = rewindTime;
        }*/
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isRecalling)
        {
            if (rotations.Count >= 2)
            {
                pastRotation = rotations[1];
            }

            if (Time.time >= nextRecallTime)
            {
                InitiateRecall();
                nextRecallTime = Time.time + cooldown;
            }
            else
            {
                Debug.Log($"On cooldown for: {nextRecallTime - Time.time:F2}");
            }
        }

        //Debug.Log(Time.fixedDeltaTime / rewindBackSeconds * rewindDuration);
        //Debug.Log($"X: {pastRotation.eulerAngles.x}, Y: {pastRotation.eulerAngles.y}");
    }

    private void FixedUpdate()
    {
        //interpolationRatio = Mathf.Clamp01(interpolationRatio);

        if (!isRecalling)
        {
            positions.Add(transform.position);
            rotations.Add(transform.rotation);

            if (positions.Count > maxPositions)
            {
                positions.RemoveAt(0);
                rotations.RemoveAt(0);
            }
        }
        if (isRecalling && interpolationRatio <= 1.0f)
        {
            //interpolationRatio += 1.0f / 150.0f;

            //transform.rotation = Quaternion.Lerp(transform.rotation, pastRotation, interpolationRatio);
        }
    }

    private void InitiateRecall()
    {
        if (positions.Count > 0)
        {
            StartCoroutine(RecallCoroutine());
            //StartCoroutine(RecallRotationCoroutine());
        }
        //rotations.Clear();

    }

    private IEnumerator RecallCoroutine()
    {
        isRecalling = true;

        lockMovementInput = true;
        lockLookInput = true;

        while (rotations.Count > 0)
        {
            transform.position = positions[positions.Count - 1];
            positions.RemoveAt(positions.Count - 1);

            transform.rotation = rotations[rotations.Count - 1];
            rotations.RemoveAt(rotations.Count - 1);

            yield return new WaitForSeconds(Time.fixedDeltaTime / rewindBackSeconds * rewindDuration);
        }

        isRecalling = false;

        testScript.GetMouseX = pastRotation.eulerAngles.y;

        lockMovementInput = false;
        lockLookInput = false;
        
        //enableRotationFinilize = true;
    }

    /*private IEnumerator RecallRotationCoroutine()
    {
        while (t < 1)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, pastRotation, t);

            t++;
        }
    }*/

    public bool GetIsRecalling
    {
        get => isRecalling;
        private set => isRecalling = value;
    }

    public bool GetLockMovementInput
    {
        get => lockMovementInput;
        private set => lockMovementInput = value;
    }

    public bool GetLockLookInput
    {
        get => lockLookInput;
        private set => lockLookInput = value;
    }

    public bool GetEnableFinalRotationFinilize
    {
        get => enableRotationFinilize;
        set => enableRotationFinilize = value;
    }

    public Quaternion GetPastRotation
    {
        get => pastRotation;
        private set => pastRotation = value;
    }
}
