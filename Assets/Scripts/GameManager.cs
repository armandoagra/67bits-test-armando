using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int cash;


    private void Awake()
    {
        Instance = this;
    }

    public void AddCash(int amount)
    {
        cash += amount;
    }
    
}
