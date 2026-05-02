
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The water gun script, which handles shooting and reloading the gun. 
public class WaterGun : MonoBehaviour
{

    public float ammo = 50f;
    public float maxAmmo = 50f;
    [SerializeField] private float fireRate = 10f;
    
    private ParticleSystem ps;

    private bool isShooting = false;
    private float fireTimer = 0f;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        HandleInput();
        HandleShooting();
    }

    private void HandleInput() // Handles user inputs for shooting and stopping shooting
    {
        if (Input.GetMouseButtonDown(0) && ammo > 0)
        {
            StartShooting();
        }
        if (Input.GetMouseButtonUp(0))
        {
            StopShooting();
        }
        if (Input.GetMouseButtonDown(0) && ammo <= 0)
        {
            PlayerEventDispatcher.TriggerGunEmptied();
        }
        if (Input.GetMouseButton(0) && ammo <= 0) 
        {
            StopShooting();
        }
    }

    private void StartShooting() //Starts shooting state
    {
        if (isShooting) return;
        isShooting = true;
        ps.Play();
        fireTimer = 0f;
        Shoot();
        PlayerEventDispatcher.TriggerGunShot();
    }

    private void StopShooting() //Stops shooting state
    {
        if (!isShooting) return;
        isShooting = false;
        ps.Stop();
        PlayerEventDispatcher.TriggerGunShootingStopped();
    }

    private void HandleShooting() //Controls firerate of gun
    {
        if (!isShooting) return;

        ammo -= fireRate;

        fireTimer += Time.deltaTime;

        if (fireTimer >= 1f / fireRate)
        {
            fireTimer = 0f;
            Shoot();
        }
    }

    public void Shoot() //Counts remaining ammo when shooting
    {
        Debug.Log(ammo);
        if (ammo <= 0)
        {
            StopShooting();
            return;
        }
        ammo -= fireRate;
    }

}
