using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; //don't miss that you need this for UnityEvent

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHp = 3;
    [SerializeField] private int hp = 3;
    [SerializeField] private int damage = 1;
    [SerializeField] private float invincibilityDuration = 3f;
    [SerializeField] private bool isInvincible = false;

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
            Invoke("resetInvincible", invincibilityDuration); //If they're invincible, why can I still see them?
        }

        if (hp <= 0)
        {
            PlayerEventDispatcher.TriggerPlayerDied();
        }
    }

    private void resetInvincible() //Resets the invincibility after a certain amount of time, allowing the player to be damaged again.
    {
        isInvincible = false;
    }

}
