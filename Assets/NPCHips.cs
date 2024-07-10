using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHips : MonoBehaviour
{
    [SerializeField] private NPC npc;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCarry playerCarry))
        {
            npc.HipsTrigger(playerCarry);
        }
    }
}
