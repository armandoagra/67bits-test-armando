using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private CharacterController characterController;
    private Vector2 startInputPosition;
    private bool isMoving = false;
    [SerializeField] private Animator animator;
    private int isMovingHash;
    private Vector3 movementVector;
    [SerializeField] private UIJoystick uiJoystick;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        isMovingHash = Animator.StringToHash("IsMoving");
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            isMoving = false;
            animator.SetBool(isMovingHash, false);
        }
        if (Input.touchCount == 1 && isMoving == false)
        {
            isMoving = true;
            //startInputPosition = Input.GetTouch(0).position;
        }
        if (isMoving)
        {
            Vector2 currentInputPosition = Input.GetTouch(0).position;
            Vector3 movementVector = new Vector3(uiJoystick.GetInputVector().x, 0, uiJoystick.GetInputVector().y).normalized;
            //movementVector = new Vector3(currentInputPosition.x - startInputPosition.x, 0f, currentInputPosition.y - startInputPosition.y).normalized;
            if (movementVector.magnitude > 0.1f)
            {
                transform.forward = movementVector;
                animator.SetBool(isMovingHash, true);
            }
            Vector3 groundedMovementVector = movementVector;
            groundedMovementVector.y = -1f;
            characterController.Move(speed * Time.deltaTime * groundedMovementVector);
        }
        else
        {
            Vector3 groundMovementVector = new Vector3(0f, -1f, 0f);
            characterController.Move(speed * Time.deltaTime * groundMovementVector);
        }
    }

    public Vector3 GetMovementVector()
    {
        return movementVector;
    }
}
