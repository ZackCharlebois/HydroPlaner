using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Handles events that do not require any information to be passed
// Mainly for player events, but can be used for other events too
// If the event requires projectile information to be transfered, then use ProjectileEventManager instead
public class PlayerEventDispatcher : MonoBehaviour
{
    public delegate void GameEventHandler();

    // Add events here
    // Make sure to include the OnEnable and OnDisable when using them in other classes

    // Gun Events
    public static event GameEventHandler GunShot;
    public static event GameEventHandler GunShootingStopped;
    public static event GameEventHandler GunReloaded;
    public static event GameEventHandler GunRefilled;
    public static event GameEventHandler GunRefillStopped;
    public static event GameEventHandler GunEmptied;

    // Player Events
    public static event GameEventHandler PlayerDied;
    public static event GameEventHandler PlayerJumped;
    public static event GameEventHandler PlayerDamaged;
    public static event GameEventHandler PlayerMovementStarted;
    public static event GameEventHandler PlayerMovementStopped;
    public static event GameEventHandler PlayerFellInHole;
    public static event GameEventHandler PlayerRespawned;
    
    // Other Events
    public static event GameEventHandler HoleApproached;
    public static event GameEventHandler EnemyApproached;
    public static event GameEventHandler ResevoirApproached;
    public static event GameEventHandler TripwireTriggered;
    public static event GameEventHandler LevelExited;
    public static event GameEventHandler CheckpointTriggered;

    //------------------Gun Events----------------------
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

    public static void TriggerGunRefillStopped()
    {
        GunRefillStopped?.Invoke();
    }
    public static void TriggerGunEmptied()
    {
        GunEmptied?.Invoke();
    }

    //--------------------Player Events-----------------
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
    
    public static void TriggerPlayerMovementStarted()
    {
        PlayerMovementStarted?.Invoke();
    }

    public static void TriggerPlayerMovementStopped()
    {
        PlayerMovementStopped?.Invoke();
    }

    public static void TriggerPlayerFellInHole()
    {
        PlayerFellInHole?.Invoke();
    }
    public static void TriggerPlayerRespawned()
    {
        PlayerRespawned?.Invoke();
    }

    //-----------------Other Events-----------------------
    public static void TriggerHoleApproached()
    {
        HoleApproached?.Invoke();
    }

    public static void TriggerEnemyApproached()
    {
        EnemyApproached?.Invoke();
    }

    public static void TriggerResevoirApproached()
    {
        ResevoirApproached?.Invoke();
    }

    public static void TriggerTripwireTriggered()
    {
        TripwireTriggered?.Invoke();
    }
    public static void TriggerLevelExited()
    {
        LevelExited?.Invoke();
    }
    public static void TriggerCheckpointTriggered()
    {
        CheckpointTriggered?.Invoke();
    }

    

}
