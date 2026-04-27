using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivity = 500f;

    float xRotation = 0f;
    float yRotation = 0f;

    float topClamp = 0f; // -90f to look up
    float bottomClamp = 0f; // 90f to look down

    void Start()
    {
        //Locking the cursor and making it invisible
        Cursor.lockState = CursorLockMode.Locked;
    }

 
    void Update()
    {
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
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
