using UnityEngine;
using System.Collections.Generic;

public class LightTriggerer : MonoBehaviour
{
    [SerializeField] private List<Light> lights;


    private void OnEnable()
    {
        PlayerEventDispatcher.LightTriggered += OnLightTriggered;
    }
    private void OnDisable()
    {
        PlayerEventDispatcher.LightTriggered -= OnLightTriggered;
    }

    void Start()
    {
        lights = new List<Light>(transform.GetComponentsInChildren<Light>());

        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].enabled = false;
        }
    }


    private void OnLightTriggered()
    {
        for (int i = 0; i < lights.Count; i++)
        {
            lights[i].enabled = true;
        }
    }


}
