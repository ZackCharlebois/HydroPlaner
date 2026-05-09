using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEventDispatcher : MonoBehaviour
{
    public delegate void GameEventHandler(GameObject hitObject);

    // Add events here involving the projectile colliding with an object that needs to be passed as a parameter

    public static event GameEventHandler ParticleCollided; 


    public static void TriggerParticleCollided(GameObject hitObject)
    {
        ParticleCollided?.Invoke(hitObject);
    }



}
