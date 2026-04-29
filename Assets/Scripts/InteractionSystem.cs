using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractionSystem : MonoBehaviour
{
    public TextMeshProUGUI text;

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
                case "Reservoir": text.text = "Press E to refill water";

                    break;
                default:
                    text.text = "";
                    break;
            }
            if (Input.GetKeyDown(KeyCode.E))
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
        WaterGun waterGun = GetComponentInChildren<WaterGun>();
        waterGun.magLeft = 3;
    }
}

