using UnityEngine;
using TMPro;

public class Diamond : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int value = 1;
    [SerializeField] private AudioClip collectSound; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position);
            }
            
            diamondcollector.Instance.AddDiamond(value);
            Destroy(gameObject);
        }
    }
}