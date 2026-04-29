using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    private Renderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        GameEventDispatcher.ParticleCollided += HandleHit;
    }

    private void OnDisable()
    {
        GameEventDispatcher.ParticleCollided -= HandleHit;
    }

    void HandleHit(GameObject hitObject)
    {
        if (hitObject != gameObject) return;

        if (_renderer != null)
        {
            _renderer.material.color = Color.red;
        }
    }

}
