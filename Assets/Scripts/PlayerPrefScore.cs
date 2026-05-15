using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefScore : MonoBehaviour
{
    public static PlayerPrefScore Instance { get; private set; }

    [SerializeField] private int[] time = new int[4];
    [SerializeField] public float sensitivity = 500f;

    public bool liveMode = true;
    public bool lightMode = false;



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public float getSensitivity()
    {
        return sensitivity;
    }

    public void setTime(int newTime)
    {
        int levelIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex - 1;

        time[levelIndex] = newTime;
    }

    public void setSensitivity(float newSensitivity)
    {
        sensitivity = newSensitivity;
        MouseMovement mouseMovement = FindObjectOfType<MouseMovement>();
        if (mouseMovement != null)
        {
            mouseMovement.mouseSensitivity = sensitivity;
        }
    }

    public float GetSensitivity()
    {
        return sensitivity;
    }


}
