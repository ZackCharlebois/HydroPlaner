using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class WaterCollision : MonoBehaviour
{
    private ParticleSystem ps;
    [SerializeField] private int timeLife = 5;
    public GameObject[] splash = new GameObject[5];
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other) //When the water particles collide with another objects
    {
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);


        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 pos = collisionEvents[i].intersection;
            Vector3 normal = collisionEvents[i].normal;

            Debug.Log(other.name);
            ProjectileEventDispatcher.TriggerParticleCollided(other); //Goes to the ProjectileEventDispatcher
            GameObject splashAtObject = Instantiate(splash[0], pos, Quaternion.LookRotation(-normal));//Creates a splash at the position of the collision
            splashAtObject.transform.SetParent(other.transform);

            Destroy(splashAtObject, timeLife);

        }
    }
}