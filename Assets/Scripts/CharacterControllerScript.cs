using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    [Header("Speed Settings")]
    [Tooltip("Базовая скорость движения")]
    public float maxSpeed = 5f;          // Уменьшена максимальная скорость
    [Tooltip("Скорость в режиме спринта")]
    public float sprintSpeed = 7f;       // Уменьшена скорость спринта
    [Tooltip("Время разгона до максимальной скорости (секунды)")]
    public float accelerationTime = 0.8f; // Увеличено время разгона
    
    [Header("Advanced Settings")]
    [Tooltip("Множитель ускорения при спринте")]
    [Range(1f, 2f)] 
    public float sprintAccelMultiplier = 3f; // Небольшой множитель вместо резкого скачка
    
    private bool isFacingRight = true;
    private bool isGrounded = false;
    private bool isSprinting = false;
    private float groundRadius = 0.1f;
    private float currentVelocityX;
    private float accelerationProgress;
    private float currentTargetSpeed;

    [Header("References")]
    public Transform groundCheck;
    public LayerMask whatIsGround;
    public Animator anim;

    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Проверка земли
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isGrounded);
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().linearVelocity.y);

        if (!isGrounded)
            return;

        // Проверка спринта
        isSprinting = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float move = Input.GetAxis("Horizontal");

        // Плавное изменение целевой скорости
        currentTargetSpeed = isSprinting ? sprintSpeed : maxSpeed;
        
        // Плавный разгон с разным временем для разных режимов
        if (Mathf.Abs(move) > 0.1f)
        {
            float accelerationRate = isSprinting ? 
                (1f/accelerationTime) * sprintAccelMultiplier : 
                (1f/accelerationTime);
                
            accelerationProgress = Mathf.Min(accelerationProgress + Time.fixedDeltaTime * accelerationRate, 1f);
        }
        else
        {
            accelerationProgress = 0f;
        }

        // Расчет текущей скорости с плавным разгоном
        currentVelocityX = Mathf.Lerp(
            0f,
            move * currentTargetSpeed,
            accelerationProgress
        );

        // Применение скорости
        GetComponent<Rigidbody2D>().linearVelocity = new Vector2(
            currentVelocityX,
            GetComponent<Rigidbody2D>().linearVelocity.y
        );

        // Анимации
        anim.SetFloat("Speed", Mathf.Abs(currentVelocityX));
        anim.SetBool("IsSprinting", isSprinting && Mathf.Abs(currentVelocityX) > 0.1f);

        // Поворот персонажа
        if (move > 0 && !isFacingRight)
            Flip();
        else if (move < 0 && isFacingRight)
            Flip();
    }

    private void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 650f));
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}