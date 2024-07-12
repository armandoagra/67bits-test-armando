using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int cash;
    [SerializeField] private GameObject levelUpPanel;
    [SerializeField] private PlayerCarry playerCarry;
    [SerializeField] private int[] shopPrices;
    [SerializeField] private int currentShopPrice;
    [SerializeField] private TMPro.TextMeshProUGUI shopPriceText;
    [SerializeField] private TMPro.TextMeshProUGUI cashText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetShopPrice();
    }

    public void AddCash(int amount)
    {
        cash += amount;
        cashText.text = cash.ToString();
    }

    public void UseCash(int amount)
    {
        if (amount > cash)
        {
            Debug.LogError("Shouldn't ever reach this line!");
        }
        cash -= amount;
        cashText.text = cash.ToString();
    }

    public void OpenLevelUpPanel()
    {
        levelUpPanel.SetActive(true);
    }

    public void CloseLevelUpPanel()
    {
        levelUpPanel.SetActive(false);
    }

    public void BtnLevelUpCapacity()
    {
        LevelUpCapacity();
    }

    private void LevelUpCapacity()
    {
        if (cash >= currentShopPrice)
        {
            playerCarry.IncreaseMaxCapacity();
            UseCash(currentShopPrice);
            SetShopPrice();
        }
        else
        {
            Debug.Log("not enough cash");
        }
    }

    private void SetShopPrice()
    {
        currentShopPrice = shopPrices[playerCarry.GetCurrentLevel() - 1];
        shopPriceText.text = currentShopPrice.ToString();
    }
}
