using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpZone : MonoBehaviour
{
    private float timer;
    private bool playerIsIn = false;
    private bool panelIsOpen = false;
    [SerializeField] private float timeToOpenPanel = 1.5f;

    // Update is called once per frame
    void Update()
    {
        if (playerIsIn && !panelIsOpen)
        {
            timer += Time.deltaTime;
            if (timer > timeToOpenPanel)
            {
                GameManager.Instance.OpenLevelUpPanel();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsIn = true;  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CloseLevelUpPanel();
            timer = 0f;
            playerIsIn = false;
            panelIsOpen = false;
        }
    }
}
