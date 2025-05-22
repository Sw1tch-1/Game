using UnityEngine;
using TMPro;

public class Diamond : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int value = 1;
    [SerializeField] private AudioClip collectSound; // Добавляем поле для звука сбора
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Воспроизводим звук сбора
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            diamondcollector.Instance.AddDiamond(value);
            Destroy(gameObject);
        }
    }
}