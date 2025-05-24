using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Respawn Settings")]
    public Transform spawnPoint;
    public float fallThreshold = -10f;
    public float respawnDelay = 1f;

    [Header("Effects")]
    public ParticleSystem respawnParticles;

    private bool isRespawning;
    private SpriteRenderer spriteRenderer;
    private Collider2D playerCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (transform.position.y < fallThreshold && !isRespawning)
        {
            StartCoroutine(RespawnWithEffects());
        }
    }

    private IEnumerator RespawnWithEffects()
    {
        isRespawning = true;

        spriteRenderer.enabled = false;
        playerCollider.enabled = false;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayRespawnSound(transform.position);
        }

        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        if (respawnParticles != null)
        {
            Instantiate(respawnParticles, transform.position, Quaternion.identity);
        }

        yield return new WaitForSeconds(respawnDelay);

        spriteRenderer.enabled = true;
        playerCollider.enabled = true;

        isRespawning = false;
    }

    public void ForceRespawn()
    {
        if (!isRespawning)
        {
            StartCoroutine(RespawnWithEffects());
        }
    }
}