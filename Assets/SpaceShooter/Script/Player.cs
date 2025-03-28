using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] private float rotateDuration;

    private Rigidbody2D rb;
    private bool isRotate;
    private float moveX;
    private float moveY;
    private Vector2 movement;
    private Vector3 mousePos;
    private bool isTakingDmg = false;
    [SerializeField] private float dmgInterval;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameManager.instance;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        RotateTowardsMouse();
        Rotate();
    }

    private void RotateTowardsMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0; // Ensure the character does not rotate on the Z axit
            Vector3 direction = (mousePos - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
    }

    private void FixedUpdate()
    {
        move();
    }
    private void move()
    {

        rb.linearVelocity = movement.normalized * gameManager.playerSpeed;

    }
    void GetInput()
    {
        if (isRotate)
        {
            movement = Vector2.zero;
            return;
        }
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY);
    }

    void Rotate()
    {
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            rb.constraints = RigidbodyConstraints2D.None;
            if (!isRotate)
            {
                StartCoroutine(StopRotateAfterTime(rotateDuration));
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && !isTakingDmg)
        {
            StartCoroutine(DamageOverTime(1));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && !isTakingDmg)
        {
            StartCoroutine(DamageOverTime(1));
        }
    }

    private IEnumerator DamageOverTime(int damage)
    {
        isTakingDmg = true;
        GetDMG(damage); // Gọi hàm trừ HP
        yield return new WaitForSeconds(dmgInterval); // Chờ một khoảng thời gian trước khi trừ tiếp
        isTakingDmg = false;
    }
    private void GetDMG(float dmg)
    {
        gameManager.playerHP -= dmg;
        if (gameManager.playerHP <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void Reset()
    {

    }
    private IEnumerator StopRotateAfterTime(float time)
    {
        isRotate = true;
        yield return new WaitForSeconds(time);
        rb.angularVelocity = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        isRotate = false;
    }


}
