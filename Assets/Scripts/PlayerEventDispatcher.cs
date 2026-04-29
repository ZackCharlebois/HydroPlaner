using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventDispatcher : MonoBehaviour
{
    public delegate void GameEventHandler();

    public static event GameEventHandler GunShot;
    public static event GameEventHandler GunShootingStopped;
    public static event GameEventHandler GunReloaded;
    public static event GameEventHandler GunRefilled;

    public static event GameEventHandler PlayerDied;
    public static event GameEventHandler PlayerJumped;

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
        GunShootingStopped?.Invoke();
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



}
