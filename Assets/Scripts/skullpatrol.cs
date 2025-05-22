using UnityEngine;

public class SkullPatrol : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float patrolRange = 5f; 

    [Header("Sound Settings")]
    [SerializeField] private AudioClip patrolSound;
    [SerializeField] private float soundActivationDistance = 4f;
    
    [Header("Debug")]
    [SerializeField] private bool drawGizmos = true;
    [SerializeField] private Color gizmoColor = Color.cyan;

    private bool movingRight;
    private SpriteRenderer spriteRenderer;
    private float leftBound;
    private float rightBound;
    private Vector3 spawnPosition;
    private Transform playerTransform;
    private bool soundActive;


    void Start()
    {
        spawnPosition = transform.position;
        leftBound = spawnPosition.x - patrolRange;
        rightBound = spawnPosition.x + patrolRange;
        movingRight = true;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (spriteRenderer == null)
        {
            Debug.LogError("Missing SpriteRenderer on SkullPatrol", this);
        }

    }

    void Update()
    {
        MoveCharacter();
        FlipSprite();
        UpdateSoundState();
    }

    private void UpdateSoundState()
    {
        if (playerTransform == null || patrolSound == null || AudioManager.Instance == null)
        {
            if (soundActive) AudioManager.Instance.StopProximitySound();
            return;
        }

        float distance = Vector3.Distance(transform.position, playerTransform.position);
        bool shouldPlay = distance <= soundActivationDistance;

        if (shouldPlay != soundActive)
        {
            soundActive = shouldPlay;
            if (shouldPlay)
            {
                AudioManager.Instance.PlayProximitySound(patrolSound);
            }
            else
            {
                AudioManager.Instance.StopProximitySound();
            }
        }
    }


    private void MoveCharacter()
    {
        float direction = movingRight ? 1 : -1;
        float newX = transform.position.x + direction * speed * Time.deltaTime;
        if (newX > rightBound)
        {
            newX = rightBound;
            movingRight = false;
        }
        else if (newX < leftBound)
        {
            newX = leftBound;
            movingRight = true;
        }
        
        transform.position = new Vector2(newX, transform.position.y);
    }

    private void FlipSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !movingRight;
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawGizmos) return;

        Gizmos.color = gizmoColor;
        float height = 2f;
        Vector3 center = Application.isPlaying ? spawnPosition : transform.position;
        
        Vector3 leftPos = new Vector3(center.x - patrolRange, center.y, 0);
        Vector3 rightPos = new Vector3(center.x + patrolRange, center.y, 0);
        
        Gizmos.DrawLine(leftPos + Vector3.up * height/2, leftPos - Vector3.up * height/2);
        Gizmos.DrawLine(rightPos + Vector3.up * height/2, rightPos - Vector3.up * height/2);
        Gizmos.DrawLine(leftPos, rightPos);
        Gizmos.DrawWireSphere(leftPos, 0.15f);
        Gizmos.DrawWireSphere(rightPos, 0.15f);

        Gizmos.color = new Color(1, 0.5f, 0, 0.3f);
        Gizmos.DrawWireSphere(transform.position, soundActivationDistance);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var respawn = collision.gameObject.GetComponent<PlayerRespawn>();
            respawn?.ForceRespawn();
        }
    }
}