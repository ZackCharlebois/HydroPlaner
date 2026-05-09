using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGunEmissiveAmmo : MonoBehaviour
{
    private WaterGun WaterGun;
    private Material MaterialInstance;
    private Color OgColor;
    private bool shooting;

    private void Awake()
    {
        WaterGun = transform.parent.GetComponent<WaterGun>();
        MaterialInstance = GetComponent<Renderer>().material;
        MaterialInstance.EnableKeyword("_EMISSION");
        OgColor = MaterialInstance.GetColor("_EmissionColor");
        shooting = false;

    }

    public void OnEnable()
    {
        PlayerEventDispatcher.GunRefillStopped += refill;
        PlayerEventDispatcher.GunShot += emptying;
        PlayerEventDispatcher.GunShootingStopped += stop;
    }

    private void OnDisable()
    {
        PlayerEventDispatcher.GunRefillStopped -= refill;
        PlayerEventDispatcher.GunShot -= emptying;
        PlayerEventDispatcher.GunShootingStopped -= stop;
    }

    public void Update()
    {
        //If you are shooting, slowly decrease the emissivness based on ammo
        if (!shooting) return;

        MaterialInstance.SetColor("_EmissionColor", OgColor * Mathf.LinearToGammaSpace((WaterGun.ammo / WaterGun.maxAmmo) * (WaterGun.ammo / WaterGun.maxAmmo)));
    }

    public void refill() 
    {
        MaterialInstance.SetColor("_EmissionColor", OgColor);
    }

    //change boolean to know if shooting or not
    public void emptying()
    {
        shooting = true;
    }

    public void stop()
    {
        shooting = false;
    }





}
