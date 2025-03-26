using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer sr;
    public GameObject explosePrefab;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private int HP;
    private Transform playerPos;

    private void Start()
    {
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
        GetPlayerPos();
        sr = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        GetPlayerPos();
        Rotate();
        AutoMove();
    }
    private void GetPlayerPos()
    {
        gameManager.player = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = gameManager.player;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            GameObject explose = Instantiate(explosePrefab, transform.position, Quaternion.identity);
            Destroy(bullet.gameObject);
            Destroy(explose,.5f);
            HP--;
            if (HP <= 0)
            {
                Destroy(gameObject);
            }
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
