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
        hp -= damage;
        if (hp <= 0)
        {
            PlayerEventDispatcher.TriggerPlayerDied();
        }
    }

}
