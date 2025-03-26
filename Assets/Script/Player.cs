using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    private float moveX;
    private float moveY;
    private Vector2 movement;
    private Vector3 mousePos;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        rb.linearVelocity = movement * speed;
    }

    void GetInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        movement = new Vector2(moveX, moveY);
    }

    void Rotate()
    {
        if (movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle-90);
        }
    }

    
}
