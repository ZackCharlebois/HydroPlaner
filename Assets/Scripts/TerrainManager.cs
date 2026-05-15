using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Handles projectile collisions with Terrain elements
public class TerrainManager : MonoBehaviour
{
    public static PlayerPrefScore playerPrefScore;

    private HashSet<GameObject> terrainObjects;
    private HashSet<GameObject> shootableTerrainObjects;

    [SerializeField] private Color waterColor = Color.blue;
    [SerializeField] private float glowIntensity = 5.0f;

    [SerializeField] private bool darkMode = true;
    [SerializeField] private bool lightMode = false;
    [SerializeField] private bool liveMode = false;

    [SerializeField] private Material default_material;
    [SerializeField] private Material dark_material;
    [SerializeField] private Material light_material;


    void Awake() //Gets list of all game objects with the "Terrain" tag
    {


        terrainObjects = new HashSet<GameObject>();
        shootableTerrainObjects = new HashSet<GameObject>(); // Separate list for "ShootableTerrain"

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Terrain");
        foreach (GameObject obj in objs)
        {
            terrainObjects.Add(obj);
        }

        GameObject[] shoot_objs = GameObject.FindGameObjectsWithTag("ShootableTerrain");
        foreach (GameObject obj in shoot_objs)
        {
            shootableTerrainObjects.Add(obj);
        }

        foreach (GameObject t in terrainObjects)
        {
            if (darkMode)
            {
                t.GetComponent<Renderer>().material = dark_material;
            }
            else if (lightMode && !darkMode)
            {
                t.GetComponent<Renderer>().material = light_material;
            }
            else
            {
                t.GetComponent<Renderer>().material = default_material;
            }
            
        }

        foreach (GameObject t in shootableTerrainObjects)
        {
            if (darkMode)
            {
                t.GetComponent<Renderer>().material = dark_material;
            }
            else if (lightMode && !darkMode)
            {
                t.GetComponent<Renderer>().material = light_material;
            }
            else
            {
                t.GetComponent<Renderer>().material = default_material;
            }

        }


        if(liveMode)
        {
            RenderSettings.ambientIntensity = 0;
            RenderSettings.reflectionIntensity = 0;

            RenderSettings.fog = true;
            RenderSettings.fogColor = Color.black;
            RenderSettings.fogDensity = 0.02f;
        }

    }



    private void OnEnable() //Enables event listening
    {
        ProjectileEventDispatcher.ParticleCollided += HandleHit;
    }

    private void OnDisable() //Disables event listening
    {
        ProjectileEventDispatcher.ParticleCollided -= HandleHit;
    }

    void HandleHit(GameObject hitObject) //Runs when a terrain object is hit by a particle
    {
        if (!shootableTerrainObjects.Contains(hitObject)) return;

        // Replace with whatever we want to happen when terrain is hit
        Renderer _renderer = hitObject.GetComponent<Renderer>();
        Light _light = hitObject.GetComponent<Light>();

        if (_renderer != null)
        {
            _renderer.material.color = waterColor;
        }
        if (_light != null)
        {
            _light.color = waterColor;
            _light.intensity = glowIntensity;
        }
    }

}
