using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameManager gameManager;
    
    private Vector3 bulletpos;
    private void Start()
    {
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
        bulletpos = gameManager.bulletFirePoint.up;
    }
    private void Update()
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
    
}
