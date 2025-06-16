using System;
using UnityEngine;

public class PlayerWalkControl : MonoBehaviour
{
    [SerializeField] public int MoveForce;
    [HideInInspector] public FloatingJoystick joystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform CrossBow;
    private bool isActive = true;
    private Transform target;

    private bool ExtraVelocityActive;

    //Get set
    public bool IsActive
    {
        get => isActive;
        set { isActive = value; }
    }

    void Awake()
    {
        GameObject.FindGameObjectWithTag("Joystick").TryGetComponent(out joystick);
    }

    private void Start()
    {
        PlayerManager.Instance._playerWalkControl = this;
    }

    private void LateUpdate()
    {
        // Rotation is handled in LateUpdate to ensure it's applied after movement (FixedUpdate) and animations, for more stable and accurate orientation.
       
        if (target)
        {
            Vector3 toTarget = target.position - CrossBow.position;

            if (toTarget.sqrMagnitude > 3f) // Çok yakınsa (yaklaşık 10 cm) hiç döndürme
            {
                Vector3 aimDir = CrossBow.forward;

                toTarget.y = 0f;
                aimDir.y = 0f;

                Vector3 toTargetDir = toTarget.normalized;

                Quaternion rotationDelta = Quaternion.FromToRotation(aimDir, toTargetDir);
                Quaternion targetRotation = rotationDelta * transform.rotation;

                float rotationSpeed = 360f;
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }


    void FixedUpdate()
    {
        if (!joystick || !isActive)
            return;

        if (joystick.Direction.sqrMagnitude > 0f)
        {
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 moveDirection = (cameraForward * joystick.Vertical + cameraRight * joystick.Horizontal).normalized;


            rb.velocity = MoveForce * moveDirection * joystick.Direction.sqrMagnitude;

            if (target == null)
            {
                float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0f, angle, 0f);
                transform.rotation =
                    Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
            }
        }
        else
        {
            if (ExtraVelocityActive)
                return;

            StopMovement();
        }
    }


    public void StopWalking()
    {
        isActive = false;
        StopMovement();
    }

    public void StopMovement()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void StartWalking()
    {
        isActive = true;
    }

    public void ApplyExtraVelocity(Vector3 direction, float multiplier)
    {
      
        Vector3 targetVelocity = rb.velocity + direction.normalized * multiplier * 10;
        float maxSpeed = 5f;
        if (targetVelocity.magnitude > maxSpeed)
        {
            targetVelocity = targetVelocity.normalized * maxSpeed;
        }

        rb.velocity = Vector3.MoveTowards(rb.velocity, targetVelocity, 600f * Time.deltaTime);
    }

    public void ExtraVelocityActiveState(bool state)
    {
        ExtraVelocityActive = state;
    }

    public void SetPlayerTarget(Transform targetTransform)
    {
        if (target == targetTransform)
            return;

        target = targetTransform;
    }
}