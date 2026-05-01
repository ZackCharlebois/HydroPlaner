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
            other.transform.GetComponent<HealthSystem>()?.Damage(1);

            //Recoil maybe add later?
            //Vector3 awayDirection = (Vector3)(other.transform.position - transform.position);
            //other.transform.GetComponent<PlayerMovement>()?.Recoil(awayDirection * 3f);
        }
    }
}
