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
    [SerializeField] private AudioClip buySFX, noCashSFX;
    [SerializeField] private float regularTextSize, animatedTextSize;
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
        StartCoroutine(TextPulse(regularTextSize, animatedTextSize));
    }

    public void UseCash(int amount)
    {
        if (amount > cash)
        {
            Debug.LogError("Shouldn't ever reach this line!");
            return;
        }
        cash -= amount;
        cashText.text = cash.ToString();
        AudioSource.PlayClipAtPoint(buySFX, playerCarry.transform.position, 1f);
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

    public void BtnCloseLevelUpPanel()
    {
        CloseLevelUpPanel();
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
            AudioSource.PlayClipAtPoint(noCashSFX, playerCarry.transform.position, 1f);
        }
    }

    private void SetShopPrice()
    {
        currentShopPrice = shopPrices[playerCarry.GetCurrentLevel() - 1];
        shopPriceText.text = currentShopPrice.ToString();
    }

    private IEnumerator TextPulse(float originalSize, float goalSize)
    {
        float animationTime = 0f;
        while (cashText.fontSize < goalSize)
        {
            animationTime += Time.deltaTime * 5f;
            cashText.fontSize = Mathf.SmoothStep(originalSize, goalSize, animationTime);
            yield return null;
        }
        animationTime = 0f;
        while (cashText.fontSize > originalSize)
        {
            animationTime += Time.deltaTime * 5f;
            cashText.fontSize = Mathf.SmoothStep(goalSize, originalSize, animationTime);
            yield return null;
        }
    }

}
