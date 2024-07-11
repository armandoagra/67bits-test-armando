using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [SerializeField] private List<NPC> carriables = new();
    [SerializeField] private Animator animator;

    public void AddCarriable(NPC c)
    {
        animator.SetBool("IsCarrying", true);
        carriables.Add(c);
    }

    public void SellCarriable()
    {
        GameManager.Instance.AddCash(carriables[carriables.Count - 1].GetCashAmount());
        NPC npc = carriables[carriables.Count - 1];
        carriables.Remove(npc);
        Destroy(npc.gameObject);
        if (carriables.Count == 0) animator.SetBool("IsCarrying", false);
    }

    public Transform GetTopCarriableTransform()
    {
        if (carriables.Count == 0) return transform;
        else return carriables[carriables.Count - 1].transform;
    }

    public int GetCarriablesCount()
    {
        return carriables.Count;
    }
}
