using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private Scene prologue;
    [SerializeField] private Scene level1;
    [SerializeField] private Scene level2;
    [SerializeField] private Scene level3;
    [SerializeField] private Scene level4;
    [SerializeField] private Scene level5;
    private Scene currentScene;

    [SerializeField] private int level;
    private GameObject player;
    private HashSet<GameObject> checkpoints;
    [SerializeField] private GameObject currentCheckpoint;

    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        player = GameObject.FindGameObjectWithTag("Player");
        checkpoints = new HashSet<GameObject>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Checkpoint");
        foreach (GameObject obj in objs)
        {
            checkpoints.Add(obj);
        }
    }

    private void OnEnable()
    {
        PlayerEventDispatcher.LevelExited += OnLevelExited;
        PlayerEventDispatcher.CheckpointTriggered += OnCheckpointTriggered;
        PlayerEventDispatcher.PlayerRespawned += OnPlayerRespawned;
    }
    private void OnDisable()
    {
        PlayerEventDispatcher.LevelExited -= OnLevelExited;
        PlayerEventDispatcher.CheckpointTriggered -= OnCheckpointTriggered;
        PlayerEventDispatcher.PlayerRespawned -= OnPlayerRespawned;
    }

    private void OnLevelExited()
    {
        if (currentScene == prologue)
        {
            SceneManager.SetActiveScene(level1);
        }
        else if (currentScene == level1)
        {
            SceneManager.SetActiveScene(level2);
        }
        else if (currentScene == level2)
        {
            SceneManager.SetActiveScene(level3);
        }
        else if (currentScene == level3)
        {
            SceneManager.SetActiveScene(level4);
        }
        else if (currentScene == level4)
        {
            SceneManager.SetActiveScene(level5);
        }
        else
        {
            Debug.Log("NO SCENE TO LOAD. ADD SCENES TO LEVELMANAGER TO LOAD THEM");
        }

    }

    private void OnCheckpointTriggered()
    {
        if (checkpoints == null) return;
        if (player == null) return;

        GameObject closestCheckpoint = null;

        float closestDistance = Mathf.Infinity;

        foreach (GameObject checkpoint in checkpoints) //Gets closest checkpoint to player
        {
            float distance = Vector3.SqrMagnitude(checkpoint.transform.position - player.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestCheckpoint = checkpoint;
            }
        }

        currentCheckpoint = closestCheckpoint;
    }

    private void OnPlayerRespawned()
    {
        player.transform.position = currentCheckpoint.transform.position;
    }
}
