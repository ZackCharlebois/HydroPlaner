using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Geyser : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] float launchVelocity;
    private GameObject player;

    void Start()
    {
        ps.Stop();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Entered Geyser");
            //player.GetComponent<Rigidbody>().AddForce(Vector3.up * launchVelocity);
            player.GetComponent<PlayerMovement>().Launch(launchVelocity);
        }
    }

    private void OnEnable()
    {
        PlayerEventDispatcher.GeyserEnabled += OnGeyserEnable;
    }

    private void OnDisable()
    {
        PlayerEventDispatcher.GeyserEnabled -= OnGeyserEnable;
    }

    private void OnGeyserEnable()
    {
        ps.Play();

    }
}
