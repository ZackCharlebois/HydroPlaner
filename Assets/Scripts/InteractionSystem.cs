using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;

public class InteractionSystem : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float holdTimer = 0;
    [SerializeField] private Boolean isTriggered = false;

    private void Awake()
    {
        text.text = "";
    }

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f))
        {
            string tag = hit.collider.gameObject.tag;
            switch (tag)
            {
                case "Reservoir": text.text = "Hold E to refill water";

                    break;
                default:
                    text.text = "";
                    break;
            }
            if (Input.GetKey(KeyCode.E))
            {
                switch (tag)
                {
                    case "Reservoir":
                        resoivor();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void resoivor()
    {
        if (!isTriggered)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer >= 3f)
            {
                WaterGun waterGun = GetComponentInChildren<WaterGun>();
                waterGun.magLeft = 3;
                isTriggered = true;
            }
        }
        else
        {
            holdTimer = 0;
            isTriggered = false;
        }

    }
}

