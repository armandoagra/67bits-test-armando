using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriable : MonoBehaviour
{
    private bool isFirst = false;
    [SerializeField] private Vector3 playerOffset, carriableOffset;
    private Vector3 offset;
    private Transform parent;
    private float moveSpeed = 5f;
    private float rotateSpeed = 10f;
    [SerializeField] private int cashAmount = 50;

    void Update()
    {
        if (parent == null) return;
        Move();
        Rotate();
    }

    private void Move()
    {
        
        transform.position = Vector3.Slerp(transform.position, parent.position + offset, moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, parent.rotation, rotateSpeed * Time.deltaTime);
    }

    public void SetStackParent(Transform newParent) {
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
            transform.Translate(Vector3.down * Time.deltaTime * 3f, Space.World);
            yield return null;
        }
        Destroy(gameObject);
    }
}
