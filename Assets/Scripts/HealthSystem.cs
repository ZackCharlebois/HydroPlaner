using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI; //don't miss that you need this for UnityEvent

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 3;
    [SerializeField] private int hp = 3;
    [SerializeField] private int damage = 1;
    [SerializeField] private float invincibilityDuration = 3f;
    [SerializeField] private bool isInvincible = false;
    public Image[] healthImages;

    private void OnEnable()
    {
        PlayerEventDispatcher.PlayerDamaged += OnDamaged;
    }
    private void OnDisable()
    {
        PlayerEventDispatcher.PlayerDamaged -= OnDamaged;
    }



    private void OnDamaged()
    {
        if(!isInvincible)
        {
            hp -= damage;
            isInvincible = true;
            healthImages[hp].enabled = false;
            if (hp <= 0)
            {
                PlayerEventDispatcher.TriggerPlayerDied();
                return;
            }
            StartCoroutine(FlashInvincibility());
            Invoke("resetInvincible", invincibilityDuration); //If they're invincible, why can I still see them?
        }

    }

    private void resetInvincible() //Resets the invincibility after a certain amount of time, allowing the player to be damaged again.
    {
        isInvincible = false;
    }

    private IEnumerator FlashInvincibility() //Flashes the player's sprite to indicate invincibility.
    {
        while (isInvincible)
        {
            healthImages[hp].enabled = false;
            yield return new WaitForSeconds(0.1f);
            healthImages[hp].enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        healthImages[hp].enabled = false;
    }

}
