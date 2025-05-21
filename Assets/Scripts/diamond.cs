using UnityEngine;
using TMPro;
public class diamond : MonoBehaviour
{
    [SerializeField] private int value = 1; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            diamondcollector.Instance.AddDiamond(value);
            Destroy(gameObject);
        }
    }
}