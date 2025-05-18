using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private AudioSource jumpSource;

    private bool isGrounded = false;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    // === Появление еды ===
    [Header("Food Spawning")]
    [SerializeField] private GameObject[] foodPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnDistanceX = 2f;
    [SerializeField] private float spawnHeight = 4f;
    private float spawnTimer = 0f;

    private void Awake()
    {
        Instance = this;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded)
        {
            if (Input.GetButton("Horizontal"))
                Run();
            else
                State = States.idle;

            if (Input.GetButtonDown("Jump"))
                Jump();
        }
        else
        {
            State = States.jump;
        }

        // Спавн еды
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnFood();
            spawnTimer = 0f;
        }
    }

    private void Run()
    {
        if (isGrounded) State = States.run;

        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 dir = transform.right * horizontalInput;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = horizontalInput < 0.0f;
    }

    private void Jump()
    {
        if (jumpSource != null)
        {
            jumpSource.Play();
        }

        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        State = States.jump;
    }

    private void CheckGround()
    {
        Vector2 checkPosition = new Vector2(transform.position.x, transform.position.y - 0.5f);
        float checkRadius = 0.2f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkPosition, checkRadius);
        isGrounded = colliders.Length > 0;

        Debug.DrawLine(transform.position, checkPosition, Color.red);
    }

    public void GetDamage()
    {
        lives -= 1;
        Debug.Log(lives);
    }

    private void SpawnFood()
    {
        if (foodPrefabs.Length == 0) return;

        int index = Random.Range(0, foodPrefabs.Length);
        float offsetX = Random.Range(-spawnDistanceX, spawnDistanceX);
        Vector2 spawnPos = new Vector2(transform.position.x + offsetX, transform.position.y + spawnHeight);

        Instantiate(foodPrefabs[index], spawnPos, Quaternion.identity);
    }
}

// === Перечисление состояний анимации ===
public enum States
{
    idle,
    run,
    jump
}
