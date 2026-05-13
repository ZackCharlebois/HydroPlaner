using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinnin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 37;
    [SerializeField] private BoxCollider collectionBox;

    void Start()
    {
        collectionBox = transform.GetComponentInChildren<BoxCollider>();
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerEventDispatcher.TriggerGunReloaded();
            Destroy(gameObject);
        }
    }
}
