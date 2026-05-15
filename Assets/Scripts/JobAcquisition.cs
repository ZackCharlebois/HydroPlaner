using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class JobAcquisition : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI text;
    private void Awake()
    {
        //text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        text.text = "";
    }

    private void OnEnable()
    {
        PlayerEventDispatcher.HoleApproached += OnThisHappen;
    }

    private void OnDisable()
    {
        PlayerEventDispatcher.HoleApproached -= OnThisHappen;
    }

    private void OnThisHappen()
    {
        if (text == null) return;

        Debug.Log("It happened");
        text.text = "HydroPlaner";
    }
}
