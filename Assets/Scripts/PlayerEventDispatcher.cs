using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles events that do not require any information to be passed
// Mainly for player events, but can be used for other events too
// If the event requires projectile information to be transfered, then use ProjectileEventManager instead
public class PlayerEventDispatcher : MonoBehaviour
{
    public delegate void GameEventHandler();

    // Add events here
    // Make sure to include the OnEnable and OnDisable when using them in other classes

    public static event GameEventHandler GunShot;
    public static event GameEventHandler GunShootingStopped;
    public static event GameEventHandler GunReloaded;
    public static event GameEventHandler GunRefilled;

    public static event GameEventHandler PlayerDied;
    public static event GameEventHandler PlayerJumped;
    public static event GameEventHandler PlayerDamaged;

    public static void TriggerGunShot()
    {
        GunShot?.Invoke();
    }

    public static void TriggerGunShootingStopped()
    {
        GunShootingStopped?.Invoke();
    }

    public static void TriggerGunReloaded()
    {
        GunReloaded?.Invoke();
    }

    public static void TriggerGunRefilled()
    {
        GunRefilled?.Invoke();
    }

    public static void TriggerPlayerDied()
    {
        PlayerDied?.Invoke();
    }

    public static void TriggerPlayerJumped()
    {
        PlayerJumped?.Invoke();
    }

    public static void TriggerPlayerDamaged()
    {
        PlayerDamaged?.Invoke();
    }


}
