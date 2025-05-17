using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] private float speed = 3f; // speed
    [SerializeField] private int lives = 5; // life
    [SerializeField] private float jumpForce = 15f; // jump
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
    // 🔁 === Появление еды ===
    [Header("Food Spawning")]
    [SerializeField] private GameObject[] foodPrefabs;  // массив префабов еды
    [SerializeField] private float spawnInterval = 2f;  // интервал спауна
    [SerializeField] private float spawnDistanceX = 2f; // расстояние по X от игрока
    [SerializeField] private float spawnHeight = 4f;    // высота спауна над игроком
    private float spawnTimer = 0f;
    // =========================
    private void Awake()
    {
        Instance = this; // add

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        // Debug.Log(sprite);
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded) State = States.idle;

        if (Input.GetButton("Horizontal"))
        {
            Run();
        }
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        // 🔁 Добавь спаун еды прямо сюда:
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

        // Vector3 dir = transform.right * Input.GetAxis("Horizontal");

        float horizontalInput = Input.GetAxis("Horizontal"); //add
                                                             //  Debug.Log("Horizontal Input: " + horizontalInput); //add
        Vector3 dir = transform.right * horizontalInput; // add

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        //sprite.flipX = dir.x < 0.0f;

        sprite.flipX = horizontalInput < 0.0f; // add

        // Debug.Log("FlipX: " + sprite.flipX); //add

    }

    private void Jump()
    {
         if (jumpSource != null)
         {
            jumpSource.Play();
         }
      
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

       
    }

    private void CheckGround() // new
    {
        Vector2 checkPosition = new Vector2(transform.position.x, transform.position.y - 0.5f); // ниже!
        float checkRadius = 0.2f;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(checkPosition, checkRadius);
        isGrounded = colliders.Length > 0;


        Debug.DrawLine(transform.position, checkPosition, Color.red); // отладка

        if (!isGrounded) State = States.jump;
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

public enum States
{
    idle,
    run,
    jump
}