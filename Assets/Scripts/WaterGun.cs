using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The water gun script, which handles shooting and reloading the gun. 
public class WaterGun : MonoBehaviour
{

    [SerializeField] private float ammo = 50f;
    [SerializeField] private float maxAmmo = 50f;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float fireRate = 0.1f;
    public float magLeft = 3;
    
    private ParticleSystem ps;

    private bool isReloading = false;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }
    private void FixedUpdate()
    {
        if (isReloading) 
        {
            return;
        }

        if (Input.GetMouseButton(0) && ammo > 0)
        {  
            Shoot();
        }

        if (magLeft != 0)
        {
            if ((Input.GetKeyDown(KeyCode.R) && ammo < maxAmmo) || ammo <= 0)
            {
                isReloading = true;
                StartCoroutine(Reload());
            }
        }
    }

    public void Shoot() //Shooting the gun
    {   
        ps.Play();
        ammo -= 1f + (Time.deltaTime / fireRate);

    }

    private IEnumerator Reload() //reloading the gun
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        magLeft -= 1;
        isReloading = false;
    }
}
