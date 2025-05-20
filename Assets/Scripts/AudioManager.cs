// AudioManager.cs
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    public AudioClip mainTheme;
    public AudioClip respawnSound;
    private AudioSource musicSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            PlayMainTheme();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMainTheme()
    {
        if (mainTheme == null) return;
        
        musicSource.clip = mainTheme;
        musicSource.Play();
    }

    public void PlayRespawnSound(Vector3 position)
    {
        AudioSource.PlayClipAtPoint(respawnSound, position);
    }
}