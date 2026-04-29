using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterCollision : MonoBehaviour
{
    private ParticleSystem ps;
    [SerializeField] private int timeLife = 5;
    public GameObject[] splash = new GameObject[5];
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);


        for (int i = 0; i < numCollisionEvents; i++)
        {
            Vector3 pos = collisionEvents[i].intersection;
            Vector3 normal = collisionEvents[i].normal;

            Debug.Log(other.name);
            GameEventDispatcher.TriggerParticleCollided(other);
            GameObject splashAtObject = Instantiate(splash[0], pos, Quaternion.LookRotation(-normal));
            splashAtObject.transform.SetParent(other.transform);
            Destroy(splashAtObject, timeLife);
        }
    }
}
