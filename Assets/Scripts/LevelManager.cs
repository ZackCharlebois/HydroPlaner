using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private string prologueName;
    [SerializeField] private string level1Name;
    [SerializeField] private string level2Name;
    [SerializeField] private string level3Name;
    [SerializeField] private string level4Name;
    [SerializeField] private string level5Name;
    private Scene currentScene;

    private GameObject player;
    private HashSet<GameObject> checkpoints;
    
    private GameObject currentCheckpoint;
    [SerializeField] private Vector3 currentCheckpointPosition;
    private bool hasCheckpoint = false;
    private bool isRespawning = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        GetSceneInfo();
    }

    private void GetSceneInfo()
    {
        currentScene = SceneManager.GetActiveScene();
        checkpoints = new HashSet<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        PlayerEventDispatcher.LevelExited -= OnLevelExited;
        PlayerEventDispatcher.CheckpointTriggered -= OnCheckpointTriggered;
        PlayerEventDispatcher.PlayerRespawned -= OnPlayerRespawned;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnLevelExited()
    {
        if (currentScene.name == prologueName)
        {
            SceneManager.LoadScene(level1Name);
        }
        else if (currentScene.name == level1Name)
        {
            SceneManager.LoadScene(level2Name);
        }
        else if (currentScene.name == level2Name)
        {
            SceneManager.LoadScene(level3Name);
        }
        else if (currentScene.name == level3Name)
        {
            SceneManager.LoadScene(level4Name);
        }
        else if (currentScene.name == level4Name)
        {
            SceneManager.LoadScene(level5Name);
        }
        else
        {
            Debug.Log("NO SCENE TO LOAD. ADD SCENE NAMES TO LEVELMANAGER TO LOAD THEM.");
        }

        GetSceneInfo();
    }

    private void OnCheckpointTriggered()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        
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

        if (closestCheckpoint != null)
        {
            currentCheckpointPosition = closestCheckpoint.transform.position;
            hasCheckpoint = true;
        }
    }

    private void OnPlayerRespawned()
    {
        isRespawning = true;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetSceneInfo();

        if (hasCheckpoint && isRespawning)
        {
            StartCoroutine(RespawnAfterLoad());
        }
    }
    private IEnumerator RespawnAfterLoad()
    {
        yield return null; //Waits so player is loaded before teleporting to checkpoint

        player.transform.position = currentCheckpointPosition;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        isRespawning = false;
    }

    public void ResetCheckpoint()
    {
        isRespawning = false;
        hasCheckpoint = false;
    }
}
