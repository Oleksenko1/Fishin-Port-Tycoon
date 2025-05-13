using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private float moveSpeed;
    [Space(5)]
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationSmoothness;
    [Header("Components")]
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private Transform modelTransform;

    private CharacterController characterController;

    private Quaternion rotationOffset = Quaternion.Euler(0, -45, 0);
    private Vector2 moveDirection = Vector2.zero;
    private float currentAngle = 0f;
    private float angleVelocity;
    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        modelTransform.rotation = rotationOffset;
    }

    public void HandleInput()
    {
        moveDirection = joystick.Direction.normalized;
    }
    public void MovePlayer()
    {
        if (moveDirection != Vector2.zero)
        {
            HandleMovement();

            HandleRotation();
        }
    }
    private void HandleMovement()
    {
        // Calculating movement vector and multiplying by rotation offset
        Vector3 movementVector = rotationOffset * new Vector3(moveDirection.x, 0, moveDirection.y);

        characterController.Move(movementVector * Time.deltaTime * moveSpeed);
    }
    private void HandleRotation()
    {
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.y) * Mathf.Rad2Deg;

        currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref angleVelocity, rotationSmoothness);

        modelTransform.rotation = Quaternion.Euler(0, currentAngle, 0) * rotationOffset;
    }
    public Vector2 GetInputVector() => moveDirection;
}
