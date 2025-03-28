using UnityEngine;

public class Asteroids : MonoBehaviour
{
    private Rigidbody2D rb;
    private float xVelocity; // Tốc độ theo chiều x
    private float yVelocity; // Tốc độ theo chiều y
    [SerializeField] private int HP;
    public GameObject ExplosePrefab;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        RandomSpeed();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    }
    private void RandomSpeed()
    {
        xVelocity = Random.Range(-4, 4);
        yVelocity = Random.Range(-4, 4);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bullet bullet = collision.GetComponent<Bullet>();
        if (bullet != null)
        {
            GameObject explose = Instantiate(ExplosePrefab, transform.position, Quaternion.identity);
            Destroy(bullet.gameObject);
            Destroy(explose, 0.2f);
            GetDMG();
        }
    }
    private void GetDMG()
    {
        HP--;
        if (HP <= 0)
        {
            Destroy(gameObject);
            GameObject enemyExplose = Instantiate(ExplosePrefab, transform.position, Quaternion.identity);

        }
    }
}
