using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCarry : MonoBehaviour
{
    [SerializeField] private List<Carriable> carriables = new();
    [SerializeField] private Animator animator;
    private int maxCapacity = 3;
    private int currentLevel = 1;
    [SerializeField] private Material[] levelMaterials;
    [SerializeField] private Renderer renderer;

    public bool AddCarriable(Carriable c)
    {
        if (carriables.Count < maxCapacity)
        {
            animator.SetBool("IsCarrying", true);
            carriables.Add(c);
            return true;
        }
        return false;
    }

    public void SellCarriable()
    {
        GameManager.Instance.AddCash(carriables[carriables.Count - 1].GetCashAmount());
        Carriable carriable = carriables[carriables.Count - 1];
        carriables.Remove(carriable);
        carriable.GetDropped();
        //Destroy(npc.gameObject);
        if (carriables.Count == 0) animator.SetBool("IsCarrying", false);
    }

    public Transform GetTopCarriableTransform()
    {
        if (carriables.Count == 1) return transform;
        else return carriables[carriables.Count - 2].transform;
    }

    public int GetCarriablesCount()
    {
        return carriables.Count;
    }

    public void IncreaseMaxCapacity()
    {
        maxCapacity++;
        currentLevel++;
        UpdateColor(currentLevel);
    }

    private void UpdateColor(int currentLevel)
    {
        renderer.material = levelMaterials[currentLevel - 1];
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
