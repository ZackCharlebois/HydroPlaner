using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Handles projectile collisions with Terrain elements
public class TerrainManager : MonoBehaviour
{
    private HashSet<GameObject> terrainObjects;

    [SerializeField] private Color waterColor = Color.blue;
    [SerializeField] private float glowIntensity = 5.0f;

    void Awake() //Gets list of all game objects with the "Terrain" tag
    {
        terrainObjects = new HashSet<GameObject>();

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Terrain");
        foreach (GameObject obj in objs)
        {
            terrainObjects.Add(obj);
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
        if (!terrainObjects.Contains(hitObject)) return;

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
