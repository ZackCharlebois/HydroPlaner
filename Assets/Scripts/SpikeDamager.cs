using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamager : MonoBehaviour
{



    //When colliding with player, this will do one damage
    public void OnCollisionEnter(Collision other)
    {
        Debug.Log("Touch");
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Hit");
            PlayerEventDispatcher.TriggerPlayerDamaged();

        }
    }
}
