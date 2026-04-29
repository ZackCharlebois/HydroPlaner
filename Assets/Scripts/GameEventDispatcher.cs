using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventDispatcher : MonoBehaviour
{
    public delegate void GameEventHandler(GameObject hitObject);

    public static event GameEventHandler ParticleCollided; //Naming convention: end in -ed, usually start with noun

    public static void TriggerParticleCollided(GameObject hitObject)
    {
        ParticleCollided?.Invoke(hitObject);
    }


}
