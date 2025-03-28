using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer sr;
    public GameObject enemyExplosePrefab;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private int HP;
    private Transform playerPos;

    private void Start()
    {
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
        
        sr = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        GetPlayerPos();
        Rotate();
        
    }
    private void FixedUpdate()
    {
        AutoMove();
    }
    private void GetPlayerPos()
    {
        gameManager.playerIsChased = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = gameManager.playerIsChased;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            GetDMG();
        }
    }
    private void GetDMG()
    {
        HP--;
        if (HP <= 0)
        {
            Destroy(gameObject);
            GameObject enemyExplose = Instantiate(enemyExplosePrefab, transform.position, Quaternion.identity);
            
        }
    }
    private void AutoMove()
    {
        if (playerPos != null)
            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);
        else
            Destroy(gameObject);
    }
    private void Rotate()
    {
        Vector3 direction = (playerPos.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, -direction);
    }


}
