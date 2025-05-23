using UnityEngine;

public class DiamondBounceAnimation : MonoBehaviour
{
    private Vector3 initialPosition; 
    private float timer = 0f;
    public float animationDuration = 0.09f; 

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > animationDuration)
            timer = 0f;

        float progress = timer / animationDuration;

        float yOffset = 0f;

        if (progress < 1f / 3f) 
        {
            float t = progress * 3f; 
            yOffset = Mathf.Lerp(0f, -0.1f, t);
        }
        else if (progress < 2f / 3f) 
        {
            float t = (progress - 1f/3f) * 3f; 
            yOffset = Mathf.Lerp(-0.1f, 0.1f, t);
        }
        else 
        {
            float t = (progress - 2f/3f) * 3f; 
            yOffset = Mathf.Lerp(0.1f, 0f, t);
        }

        transform.position = initialPosition + new Vector3(0f, yOffset, 0f);
    }
}