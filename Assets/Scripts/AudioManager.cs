// AudioManager.cs
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [Header("Main Sounds")]
    public AudioClip mainTheme;
    public AudioClip respawnSound;
    
    [Header("Sound Settings")]
    [Range(0f, 1f)] public float soundVolume = 0.7f;
    private AudioSource musicSource;
    private AudioSource effectSource;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            PlayMainTheme();
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAudioSources()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.volume = soundVolume;

        effectSource = gameObject.AddComponent<AudioSource>();
        effectSource.loop = false;
        effectSource.playOnAwake = false;
        effectSource.volume = soundVolume;
    }

    public void PlayProximitySound(AudioClip clip)
    {
        if (clip == null || effectSource == null) return;
        
        effectSource.clip = clip;
        effectSource.loop = true;
        effectSource.Play();
    }

    public void StopProximitySound()
    {
        if (effectSource != null && effectSource.isPlaying)
        {
            effectSource.loop = false;
            effectSource.Stop();
        }
    }



    public void PlayMainTheme()
    {
        if (mainTheme == null || musicSource == null) return;
        musicSource.clip = mainTheme;
        musicSource.Play();
    }

    public void PlayRespawnSound(Vector3 position)
    {
        if (respawnSound == null || effectSource == null) return;
        AudioSource.PlayClipAtPoint(respawnSound, position);
    }
}