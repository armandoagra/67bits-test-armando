using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriable : MonoBehaviour
{
    private bool isFirst = false;
    [SerializeField] private Vector3 playerOffset, carriableOffset;
    private Vector3 offset;
    private Transform parent;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private int cashAmount = 50;

    [SerializeField] private float moveDelay = 0.1f;
    [SerializeField] private float rotateDelay = 0.1f;
    [SerializeField] private float maxTiltAngle = 15f;
    [SerializeField] private float maxBendDistance = 0.2f;
    private Vector3 playerVelocity;
    [SerializeField] private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        if (parent == null) return;
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 targetPosition = parent.position + CalculateBend();
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition + offset, ref playerVelocity, moveDelay);
    }

    private void Rotate()
    {
        Quaternion targetRotation = parent.rotation * Quaternion.Euler(CalculateTilt());
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateDelay);
    }

    private Vector3 CalculateBend()
    {
        Vector3 playerVelocity = playerMovement.GetMovementVector();
        Vector3 bend = playerVelocity.normalized * maxBendDistance;
        return new Vector3(bend.x, 0, bend.z);
    }

    private Vector3 CalculateTilt()
    {
        Vector3 playerVelocity = playerMovement.GetMovementVector();
        float tiltAngle = playerVelocity.magnitude / maxTiltAngle;
        return new Vector3(tiltAngle, 0, -tiltAngle);
    }

    public void SetStackParent(Transform newParent)
    {
        parent = newParent;
    }

    public int GetCashAmount()
    {
        return cashAmount;
    }

    public void SetIsFirst(bool newIsFirst)
    {
        isFirst = newIsFirst;
        SetOffset();
    }

    private void SetOffset()
    {
        offset = isFirst ? playerOffset : carriableOffset;
    }

    public void GetDropped()
    {
        StartCoroutine(SellSequence());
    }

    private IEnumerator SellSequence()
    {
        parent = null;
        yield return new WaitForSeconds(.5f);
        while (transform.position.y > 0f)
        {
            transform.Translate(Time.deltaTime * 3f * Vector3.down, Space.World);
            yield return null;
        }
        Destroy(gameObject);
    }
}
