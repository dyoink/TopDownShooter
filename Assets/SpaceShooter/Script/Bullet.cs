using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager gameManager;
    public GameObject explosePrefab;
    private Vector3 bulletpos;
    private void Awake()
    {
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
        bulletpos = gameManager.bulletFirePoint.up;
    }
    private void FixedUpdate()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        rb.linearVelocity = bulletpos * gameManager.bulletSpeed;
        DestroyBullet();
    }

    private void DestroyBullet()
    {
        Destroy(gameObject, 1f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra va chạm với Enemy
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            GameObject explose = Instantiate(explosePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explose, 0.2f);
        }

    }

}
