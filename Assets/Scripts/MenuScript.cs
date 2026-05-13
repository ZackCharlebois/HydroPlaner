using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuScript : MonoBehaviour
{
    public GameObject pane;
    public Button resume;
    public Button restart;
    public Button respawn;
    public Button quit;

   

    public bool deadScreen = false;
    [SerializeField] private bool isDead = false;

    private void OnEnable()
    {
        PlayerEventDispatcher.PlayerDied += OnDeath;
    }

    private void OnDisable()
    {
        PlayerEventDispatcher.PlayerDied -= OnDeath;
    }

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    private void Start()
    {
        pane.SetActive(false);
        Cursor.visible = false;


        if (resume != null)
        {
            resume.onClick.AddListener(Resume);
        }

        if (restart != null)
        {
            restart.onClick.AddListener(Restart);
        }

        if (respawn != null)
        {
            respawn.onClick.AddListener(Respawn);
        }

        if (quit != null)
        {
            quit.onClick.AddListener(Quit);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !deadScreen && !isDead)
        {
            pane.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Time.timeScale = 0f;
        }


    }

    void Resume()
    {
        pane.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    void Restart()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.ResetCheckpoint();
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void Respawn()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        PlayerEventDispatcher.TriggerPlayerRespawned();
    }
    void Quit()
    {
        Application.Quit();
    }


    private void OnDeath()
    {
        AudioManager.Instance.PlaySound(SoundType.Job);
        isDead = true;
        //Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        if (deadScreen)
        {
            pane.gameObject.SetActive(true);
        }
    }
}
