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
        // Находим игрока по тегу
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        // Создаем и настраиваем AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = proximitySound;
        audioSource.volume = soundVolume;
        audioSource.loop = loopSound;
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (playerTransform == null || proximitySound == null) return;

        // Вычисляем расстояние до игрока
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        
        // Проверяем расстояние и состояние звука
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

    // Визуализация радиуса в редакторе
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationDistance);
    }
}