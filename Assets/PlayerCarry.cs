using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [SerializeField] private List<NPC> carriables = new();


    public void AddCarriable(NPC c)
    {
        carriables.Add(c);
    }

    public void SellCarriables()
    {
        // Co-rotina de venda
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
