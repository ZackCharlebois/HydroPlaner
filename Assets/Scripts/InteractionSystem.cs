using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
//An interaction system to allow players to interact with objects in the world. Currently only supports refilling the water gun at the reservoir, but can be expanded to include more interactions in the future.
public class InteractionSystem : MonoBehaviour
{
    public TextMeshProUGUI text;
    public GameObject sliderObject;
    private Slider slider;
    [SerializeField] private Boolean isTriggered = false;
    public float holdTimer = 0;

    private void Awake()
    {
        text.text = ""; //At the start the text is empty
        slider = sliderObject.GetComponent<Slider>();
        slider.value = 0; 
        sliderObject.SetActive(false); 
    }

    void Update()
    {
        WaterGun waterGun = GetComponentInChildren<WaterGun>();
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f)) //If the player looks at an object, it checks the tag of the object and displays a certain text.
        {
            string tag = hit.collider.gameObject.tag;
            switch (tag)
            {
                case "Reservoir": text.text = "Hold E to refill water";
                    if (waterGun.ammo < waterGun.maxAmmo)
                    {
                        if (Input.GetKey(KeyCode.E))
                        {
                            resoivor(waterGun);
                        }
                        else
                        {
                            sliderObject.SetActive(false);
                            holdTimer = 0;
                            slider.value = 0;
                            isTriggered = false;
                        }
                    }
                    else 
                    {
                        sliderObject.SetActive(false);
                        text.text = "Water gun is full";
                    }
                        break;

                default:
                    text.text = "";
                    sliderObject.SetActive(false);
                    holdTimer = 0;
                    slider.value = 0;

                    break;
            }

        }
    }

    public void resoivor(WaterGun waterGun) //Refills the water gun
    {
        slider.value = 0;
        if (!isTriggered && waterGun.ammo < waterGun.maxAmmo) //Players must hold the E button for 3 seconds to refill the gun
        {
            PlayerEventDispatcher.TriggerGunRefilled();
            sliderObject.SetActive(true);
            holdTimer += Time.deltaTime;
            slider.value = holdTimer / 3f;
            if (holdTimer >= 3f)
            {
                PlayerEventDispatcher.TriggerGunRefillStopped();
                waterGun.ammo = waterGun.maxAmmo;
                isTriggered = true;
            }
        }
        else
        {
            PlayerEventDispatcher.TriggerGunRefillStopped();
            sliderObject.SetActive(false);
            holdTimer = 0;
            slider.value = 0;
            isTriggered = false;
        }


    }
}

