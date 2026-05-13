using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
    Damage,
    Hole,
    Enemy,
    Resevoir,
    Tripwire,
    Death,
    Test,
    Fall,
    LevelExit,
    Checkpoint
}
// All purpose player trigger for various things
// Select what type of trigger zone in the inspector
public class TriggerZone : MonoBehaviour
{

    [SerializeField] TriggerType triggerType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            switch (triggerType)
            {
                case TriggerType.Damage:
                    {
                        PlayerEventDispatcher.TriggerPlayerDamaged();
                        break;
                    }
                case TriggerType.Hole:
                    {
                        PlayerEventDispatcher.TriggerHoleApproached();
                        break;
                    }
                case TriggerType.Enemy:
                    {
                        PlayerEventDispatcher.TriggerEnemyApproached();
                        break;
                    }
                case TriggerType.Resevoir:
                    {
                        PlayerEventDispatcher.TriggerResevoirApproached();
                        break;
                    }
                case TriggerType.Tripwire:
                    {
                        PlayerEventDispatcher.TriggerTripwireTriggered();
                        break;
                    }
                case TriggerType.Death:
                    {
                        PlayerEventDispatcher.TriggerPlayerDied();
                        break;
                    }
                case TriggerType.Test:
                    {
                        PlayerEventDispatcher.TriggerGunReloaded();
                        break;
                    }
                case TriggerType.Fall:
                    {
                        PlayerEventDispatcher.TriggerPlayerFellInHole();
                        break;
                    }
                case TriggerType.LevelExit:
                    {
                        PlayerEventDispatcher.TriggerLevelExited();
                        break;
                    }
                case TriggerType.Checkpoint:
                    {
                        Debug.Log("Checkpoint Triggered");
                        PlayerEventDispatcher.TriggerCheckpointTriggered();
                        break;
                    }
                default: break;
            }
        }
    }
}
