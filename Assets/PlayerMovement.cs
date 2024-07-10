using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private CharacterController characterController;
    private Vector2 startInputPosition;
    private bool isMoving = false;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.touchCount == 0) isMoving = false;
        if (Input.touchCount == 1 && isMoving == false)
        {
            isMoving = true;
            startInputPosition = Input.GetTouch(0).position;
        }
        if (isMoving)
        {
            Vector2 currentInputPosition = Input.GetTouch(0).position;
            Vector3 movementVector = new(currentInputPosition.x - startInputPosition.x, 0f, currentInputPosition.y - startInputPosition.y);
            characterController.Move(speed * Time.deltaTime * movementVector.normalized);
        }
    }
}
