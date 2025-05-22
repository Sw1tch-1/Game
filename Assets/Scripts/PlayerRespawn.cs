// PlayerRespawn.cs
using UnityEngine;
using System.Collections; 

public class PlayerRespawn : MonoBehaviour
{
    public Transform spawnPoint;
    public float fallThreshold = -10f;
    public float respawnDelay = 1f; 

    private bool isRespawning; 

    void Update()
    {
        if (transform.position.y < fallThreshold && !isRespawning)
        {
            StartCoroutine(RespawnWithDelay());
        }
    }

    private IEnumerator RespawnWithDelay()
    {
        isRespawning = true;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayRespawnSound(transform.position);
        }

        yield return new WaitForSeconds(respawnDelay);

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
        isRespawning = false;
    }
     public void ForceRespawn()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnWithDelay());
        }
    }
}