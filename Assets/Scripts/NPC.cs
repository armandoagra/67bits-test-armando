using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    private bool isCarriable = false;
    private bool gettingCarried = false;
    private Transform parent;
    private float movementSpeed = 5f;
    private bool isFirst = false;
    [SerializeField] private int cashAmount = 50;
    [SerializeField] private Rigidbody[] rigidbodies;
    [SerializeField] private Collider[] ragdollColliders;
    [SerializeField] private Collider regularCollider;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody hipsRigidbody;
    [SerializeField] private Vector3 playerOffset;
    [SerializeField] private Vector3 objectOffset;
    [SerializeField] private float timeBeforeSlowdown, slowDownSeconds, slowDownAmount = 0.1f;
    [SerializeField] private bool isPunched;
    [SerializeField] private GameObject punchVFX;
    [SerializeField] private AudioClip punchSFX;

    // Start is called before the first frame update
    private void Awake()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();

        animator = GetComponent<Animator>();
        ToggleRagdoll(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gettingCarried) return;
        Move();
        Rotate();
    }

    private void Move()
    {
        Vector3 offset = isFirst ? playerOffset : objectOffset;
        transform.position = Vector3.Lerp(transform.position, parent.position + offset, movementSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, parent.rotation, movementSpeed * Time.deltaTime);
    }

    public void ToggleRagdoll(bool isRegular)
    {
        //regularCollider.enabled = isRegular;
        animator.enabled = isRegular;
        foreach (Rigidbody r in rigidbodies)
        {
            r.isKinematic = isRegular;
        }

        if (!isRegular)
        {
            StartCoroutine(EnableIsCarriable());
        }
    }

    private IEnumerator EnableIsCarriable()
    {
        yield return new WaitForSeconds(3f);
        isCarriable = true;
    }

    private IEnumerator PunchSequence()
    {
        ToggleRagdoll(false);
        Instantiate(punchVFX, transform);
        AudioSource.PlayClipAtPoint(punchSFX, transform.position, 1f);
        yield return new WaitForSecondsRealtime(0.2f);
        Time.timeScale = slowDownAmount;
        yield return new WaitForSecondsRealtime(slowDownSeconds);
        Time.timeScale = 1f;
    }

    public void HipsTrigger(PlayerCarry playerCarry)
    {
        if (!isPunched)
        {
            playerCarry.GetComponent<PlayerActions>().Punch(); // improve this
            StartCoroutine(PunchSequence());

            isPunched = true;
        }

        if (isCarriable && !gettingCarried)
        {
            foreach (Collider c in ragdollColliders)
            {
                c.enabled = false;
            }
            if (playerCarry.AddCarriable(this))
            {
                parent = playerCarry.GetTopCarriableTransform();
                isFirst = playerCarry.GetCarriablesCount() == 1;
                Vector3 offset = isFirst ? playerOffset : objectOffset;
                hipsRigidbody.transform.localPosition = Vector3.zero;
                transform.position = parent.position + offset;
                gettingCarried = true;
                hipsRigidbody.isKinematic = true;

            }
        }
    }

    public int GetCashAmount()
    {
        return cashAmount;
    }
}
