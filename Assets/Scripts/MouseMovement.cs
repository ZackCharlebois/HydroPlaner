using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    float xRotation = 0f;
    float yRotation = 0f;

    float topClamp = -90f; // -90f to look up
    float bottomClamp = 90f; // 90f to look down

    private bool playerDead = false;

    void Start()
    {
        //Locking the cursor and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

 
    void Update()
    {
        if (playerDead) return;
        //Get mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotate around the x axis (look up and down)
        xRotation -= mouseY;

        //Clamp the rotation
        xRotation = Mathf.Clamp(xRotation, topClamp, bottomClamp);

        //Rotate around the y axis (look left and right)
        yRotation += mouseX;

        //Apply rotations to trasform
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

    private void OnEnable()
    {
        PlayerEventDispatcher.PlayerDied += OnDeath;
    }

    private void OnDisable()
    {
        PlayerEventDispatcher.PlayerDied -= OnDeath;
    }

    private void OnDeath()
    {
        playerDead = true;
        //Camera.main.transform.position = new Vector3()
    }
}
