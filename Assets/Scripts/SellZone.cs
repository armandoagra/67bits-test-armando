using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellZone : MonoBehaviour
{
    private bool playerInZone = false;
    private PlayerCarry playerCarry;
    private float timer;
    [SerializeField] private float timeToSell;
    [SerializeField] private AudioClip sellSFX;

    // Update is called once per frame
    void Update()
    {
        if (!playerInZone || !playerCarry) return;
        if (playerCarry.GetCarriablesCount() == 0) return;

        timer += Time.deltaTime;
        if (timer >= timeToSell)
        {
            AudioSource.PlayClipAtPoint(sellSFX, transform.position, 1f);
            playerCarry.SellCarriable();
            timer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCarry _playerCarry))
        {
            playerInZone = true;
            playerCarry = _playerCarry;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerCarry>()){
            playerInZone = false;
            timer = 0f;
        }
    }
}
