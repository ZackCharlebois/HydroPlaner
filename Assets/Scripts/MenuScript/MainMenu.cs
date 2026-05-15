using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject pane;
    public GameObject options;
    public Button play;
    public Button optionsButton;
    public Button quit;
    public Button exitOptionButton;
    public Button exitPlayButton;
    public TextMeshProUGUI titleText;

    public Button[] levelButtons = new Button[4];

    [SerializeField] private bool paneOpen = false;
    [SerializeField] private bool optionsOpen = false;


    private void Start()
    {
        pane.SetActive(false);
        options.SetActive(false);
        Cursor.visible = true;
        if (play != null)
        {
            
            play.onClick.AddListener(Play);
            
        }
        if (optionsButton != null)
        {
            optionsButton.onClick.AddListener(Options);
        }
        if (quit != null)
        {
            quit.onClick.AddListener(Quit);
        }
        if (exitOptionButton != null)
        {
            exitOptionButton.onClick.AddListener(ExitOptions);
        }
        if (exitPlayButton != null)
        {
            exitPlayButton.onClick.AddListener(ExitPlay);
        }

        if(levelButtons != null)
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                int index = i + 1;
                levelButtons[i].onClick.AddListener(() => SceneManager.LoadScene(index));
            }
        }


    }


    void Play()
    {
        if (optionsOpen || paneOpen)
        {
            return;
        }
        titleText.enabled = false;
        pane.SetActive(true);
        paneOpen = true;
    }

    void Options()
    {
        if (optionsOpen || paneOpen)
        {
            return;
        }
        titleText.enabled = false;
        options.SetActive(true);
        optionsOpen = true;
    }

    void Quit()
    {
        Application.Quit();
    }

    void ExitOptions()
    {
        options.SetActive(false);
       
        titleText.enabled = true;
        optionsOpen = false;
    }

    void ExitPlay()
    {
        pane.SetActive(false);
        titleText.enabled = true;
        paneOpen = false;
    }
}
