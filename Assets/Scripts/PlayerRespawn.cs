using UnityEngine;
using System.Collections; // Для IEnumerator

public class PlayerRespawn : MonoBehaviour
{
    public Transform spawnPoint; // Точка спавна
    public float fallThreshold = -10f; // Граница падения
    public float respawnDelay = 1f; // Задержка перед респавном (настраивается в Unity)

    private bool isRespawning; // Флаг, чтобы избежать множественных респавнов

    void Update()
    {
        // Если игрок упал ниже границы и ещё не начал респавн
        if (transform.position.y < fallThreshold && !isRespawning)
        {
            StartCoroutine(RespawnWithDelay());
        }
    }

    // Корутина для респавна с задержкой
    private IEnumerator RespawnWithDelay()
{
    isRespawning = true;
    
    // 1. Отключаем визуал и физику
    GetComponent<SpriteRenderer>().enabled = false;
    GetComponent<Collider2D>().enabled = false;

    // 2. Проигрываем звук респавна
    if (AudioManager.Instance != null)
    {
        AudioManager.Instance.PlayRespawnSound(transform.position);
    }

    // 3. Ждём
    yield return new WaitForSeconds(respawnDelay);

    // 4. Возвращаем
    if (spawnPoint != null)
    {
        transform.position = spawnPoint.position;
    }

    GetComponent<SpriteRenderer>().enabled = true;
    GetComponent<Collider2D>().enabled = true;
    isRespawning = false;
}

}