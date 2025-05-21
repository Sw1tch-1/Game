using UnityEngine;

public class DiamondBounceAnimation : MonoBehaviour
{
    private Vector3 initialPosition; // Начальная позиция объекта
    private float timer = 0f;
    public float animationDuration = 0.09f; // 90 мс (последний ключевой кадр)

    void Start()
    {
        // Запоминаем начальную позицию
        initialPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Зацикливаем анимацию
        if (timer > animationDuration)
            timer = 0f;

        // Вычисляем текущий прогресс анимации (0 → 1)
        float progress = timer / animationDuration;

        // Определяем смещение по Y в зависимости от времени
        float yOffset = 0f;

        if (progress < 1f / 3f) // 0-30 мс: вниз на 0.1
        {
            float t = progress * 3f; // Нормализуем в [0;1]
            yOffset = Mathf.Lerp(0f, -0.1f, t);
        }
        else if (progress < 2f / 3f) // 30-60 мс: вверх на 0.1
        {
            float t = (progress - 1f/3f) * 3f; // Нормализуем в [0;1]
            yOffset = Mathf.Lerp(-0.1f, 0.1f, t);
        }
        else // 60-90 мс: возврат в 0
        {
            float t = (progress - 2f/3f) * 3f; // Нормализуем в [0;1]
            yOffset = Mathf.Lerp(0.1f, 0f, t);
        }

        // Применяем новую позицию (смещение 0.1 вместо 100)
        transform.position = initialPosition + new Vector3(0f, yOffset, 0f);
    }
}