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

            Quaternion randomAngle = Quaternion.AngleAxis(UnityEngine.Random.Range(0f, 360f), Vector3.forward);


            ProjectileEventDispatcher.TriggerParticleCollided(other); //Goes to the ProjectileEventDispatcher
            GameObject splashAtObject = Instantiate(splash[0], pos, Quaternion.LookRotation(-normal) * randomAngle);//Creates a splash at the position of the collision
            splashAtObject.transform.SetParent(other.transform);



            Destroy(splashAtObject, timeLife);

        }
    }
}