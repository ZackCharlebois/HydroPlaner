using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Unity.VisualScripting;
//An interaction system to allow players to interact with objects in the world. Currently only supports refilling the water gun at the reservoir, but can be expanded to include more interactions in the future.
public class InteractionSystem : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] private Boolean isTriggered = false;

    private void Awake()
    {
        text.text = ""; //At the start the text is empty
    }

    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f)) //If the player looks at an object, it checks the tag of the object and displays a certain text.
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

    public void resoivor() //Refills the water gun
    {
        float holdTimer = 0;
        if (!isTriggered) //Players must hold the E button for 3 seconds to refill the gun
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

