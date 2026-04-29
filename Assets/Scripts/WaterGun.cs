using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGun : MonoBehaviour
{
    [SerializeField] private float ammo = 50f;
    [SerializeField] private float maxAmmo = 50f;
    [SerializeField] private float reloadTime = 2f;
    [SerializeField] private float fireRate = 0.1f;
    private ParticleSystem ps;

    private bool isReloading = false;

    [SerializeField] private AudioClip reloadSFX;
    [SerializeField] private AudioSource shootSFXSource;

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

        if ((Input.GetKeyDown(KeyCode.R) && ammo < maxAmmo) || ammo <= 0)
        {
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    public void Shoot()
    {
        {
            ps.Play();
            Debug.Log("Boom");
            ammo -= 1f + (Time.deltaTime / fireRate);
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        isReloading = false;
    }
}
