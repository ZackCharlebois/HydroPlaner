using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum SoundType // Add new sounds here as they are implemented
{
    Shoot,
    Reload,
    Jump,
    Damage,
    Refill,
    Death,
    Hole,
    Enemy,
    Resevoir,
    Tripwire,
    Empty,
    FinishedShooting,
    Walk
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    // Music
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip musicClip;

    // Sound Effects
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip reloadClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip damageClip;
    [SerializeField] private AudioClip refillClip;
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private AudioClip holeClip;
    [SerializeField] private AudioClip enemyClip;
    [SerializeField] private AudioClip resevoirClip;
    [SerializeField] private AudioClip tripwireClip;
    [SerializeField] private AudioClip emptyClip;
    [SerializeField] private AudioClip finishedShootingClip;
    [SerializeField] private AudioClip walkClip;
    // Add new sounds here as they are implemented

    [Range(0f,1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.5f;

    // Tracks active sound effects so they can be stopped manually
    private Dictionary<SoundType, AudioSource> activeSounds;

    private void Awake()
    {
        // Makes AudioManager a Singleton and also persist between scenes
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Initialize music
        if (musicSource)
        {
            musicSource.volume = musicVolume;
        }
        Debug.Log("Music Initialized");

        // Initialize active sound list
        activeSounds = new Dictionary<SoundType, AudioSource>();
    }
    //----------------------------------------------
    // Music
    public void PlayMusic()
    {
        if (!musicSource || !musicClip) return;

        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource) musicSource.Stop();
    }
    //----------------------------------------------
    // Sound Effects

    public void PlaySound(SoundType type)
    {
        AudioClip clip = GetClip(type); // Gets audio clip of whatever sound is being played
        if (clip == null) return;

        //If already playing the same sound effect, do not play it again
        if (activeSounds.ContainsKey(type)) return;

        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = sfxVolume;
        source.spatialBlend = 0f; // Makes sound play at any distance from audio source
        source.loop = false;
        if(type == SoundType.Walk) { source.loop = true; }
        source.Play();

        activeSounds[type] = source; // Adds sound to active sound list

        StartCoroutine(RemoveAfterPlay(type, clip.length));
    }

    // If sound is in the list of active sounds, then it is stopped
    public void StopSound(SoundType type)
    {
        if (!activeSounds.ContainsKey(type)) return;

        AudioSource source = activeSounds[type];
        source.Stop();
        Destroy(source);
        activeSounds.Remove(type);
    }

    // Assigns the sound clips to a sound type for use in PlaySound and StopSound
    private AudioClip GetClip(SoundType type)
    {
        switch (type) // Add new sound effects here as they are implemented
        {
            case SoundType.Shoot: return shootClip;
            case SoundType.Reload: return reloadClip;
            case SoundType.Jump: return jumpClip;
            case SoundType.Damage: return damageClip;
            case SoundType.Refill: return refillClip;
            case SoundType.Death: return deathClip;
            case SoundType.Hole: return holeClip;
            case SoundType.Enemy: return enemyClip;
            case SoundType.Resevoir: return resevoirClip;
            case SoundType.Tripwire: return tripwireClip;
            case SoundType.Empty: return emptyClip;
            case SoundType.FinishedShooting: return finishedShootingClip;
            case SoundType.Walk: return walkClip;
            default: return null;
        }
    }

    // Removes sounds from list of active sounds when they finish playing
    private IEnumerator RemoveAfterPlay(SoundType type, float duration)
    {
        yield return new WaitForSeconds(duration);

        if (activeSounds.ContainsKey(type) && type != SoundType.Walk)
        {
            Destroy(activeSounds[type]);
            activeSounds.Remove(type);
        }
    }


    //--------------------------------------------------
    // Events to play sounds

    private void OnEnable()
    {
        PlayerEventDispatcher.GunReloaded += OnReload;
        PlayerEventDispatcher.GunShot += OnGunShot;
        PlayerEventDispatcher.GunShootingStopped += OnGunStoppedShooting;
        PlayerEventDispatcher.PlayerJumped += OnJump;
        PlayerEventDispatcher.PlayerDamaged += OnDamage;
        PlayerEventDispatcher.GunRefilled += OnRefill;
        PlayerEventDispatcher.GunRefillStopped += OnRefillStop;
        PlayerEventDispatcher.HoleApproached += OnHoleApproach;
        PlayerEventDispatcher.EnemyApproached += OnEnemyApproach;
        PlayerEventDispatcher.ResevoirApproached += OnResevoirApproach;
        PlayerEventDispatcher.TripwireTriggered += OnTripwire;
        PlayerEventDispatcher.GunEmptied += OnEmpty;
        PlayerEventDispatcher.PlayerMovementStarted += OnStartMove;
        PlayerEventDispatcher.PlayerMovementStopped += OnStopMove;

    }

    private void OnDisable()
    {
        PlayerEventDispatcher.GunReloaded -= OnReload;
        PlayerEventDispatcher.GunShot -= OnGunShot;
        PlayerEventDispatcher.GunShootingStopped -= OnGunStoppedShooting;
        PlayerEventDispatcher.PlayerJumped -= OnJump;
        PlayerEventDispatcher.PlayerDamaged -= OnDamage;
        PlayerEventDispatcher.GunRefilled -= OnRefill;
        PlayerEventDispatcher.GunRefillStopped -= OnRefillStop;
        PlayerEventDispatcher.HoleApproached -= OnHoleApproach;
        PlayerEventDispatcher.EnemyApproached -= OnEnemyApproach;
        PlayerEventDispatcher.ResevoirApproached -= OnResevoirApproach;
        PlayerEventDispatcher.TripwireTriggered -= OnTripwire;
        PlayerEventDispatcher.GunEmptied -= OnEmpty;
        PlayerEventDispatcher.PlayerMovementStarted -= OnStartMove;
        PlayerEventDispatcher.PlayerMovementStopped -= OnStopMove;
    }

    private void OnReload()
    {
        AudioManager.Instance.PlaySound(SoundType.Reload);
    }
    private void OnGunShot()
    {
        Debug.Log("Gun Shot Recieved By Audio Manager");
        AudioManager.Instance.PlaySound(SoundType.Shoot);
    }
    private void OnGunStoppedShooting()
    {
        Debug.Log("Gun Shooting Stopped Recieved By Audio Manager");
        AudioManager.Instance.PlaySound(SoundType.FinishedShooting);
        AudioManager.Instance.StopSound(SoundType.Shoot);
    }
    private void OnJump()
    {
        AudioManager.Instance.PlaySound(SoundType.Jump);
    }
    private void OnDamage()
    {
        AudioManager.Instance.PlaySound(SoundType.Damage);
    }
    private void OnRefill()
    {
        AudioManager.Instance.PlaySound(SoundType.Refill);
    }
    private void OnRefillStop()
    {
        AudioManager.Instance.StopSound(SoundType.Refill);
    }
    private void OnDeath()
    {
        AudioManager.Instance.PlaySound(SoundType.Death);
    }
    private void OnHoleApproach()
    {
        AudioManager.Instance.PlaySound(SoundType.Hole);
    }
    private void OnEnemyApproach()
    {
        AudioManager.Instance.PlaySound(SoundType.Enemy);
    }
    private void OnResevoirApproach()
    {
        AudioManager.Instance.PlaySound(SoundType.Resevoir);
    }
    private void OnTripwire()
    {
        AudioManager.Instance.PlaySound(SoundType.Tripwire);
    }
    private void OnEmpty()
    {
        AudioManager.Instance.PlaySound(SoundType.Empty);
    }
    private void OnStartMove()
    {
        AudioManager.Instance.PlaySound(SoundType.Walk);
    }
    private void OnStopMove()
    {
        AudioManager.Instance.StopSound(SoundType.Walk);
    }

}
