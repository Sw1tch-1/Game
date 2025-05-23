using UnityEngine;

public class ProximitySound : MonoBehaviour
{
    [Header("Sound Settings")]
    [SerializeField] private AudioClip proximitySound;
    [SerializeField] private float activationDistance = 3f;
    [SerializeField] private float soundVolume = 1f;
    [SerializeField] private bool loopSound = true;
    
    private AudioSource audioSource;
    private Transform playerTransform;
    private bool isPlaying;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = proximitySound;
        audioSource.volume = soundVolume;
        audioSource.loop = loopSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (playerTransform == null || proximitySound == null) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        
        if (distance <= activationDistance && !isPlaying)
        {
            audioSource.Play();
            isPlaying = true;
        }
        else if (distance > activationDistance && isPlaying)
        {
            audioSource.Stop();
            isPlaying = false;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}