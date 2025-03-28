using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Player")]
    public float playerHP;
    public float playerSpeed;

    [Header("Bullet")]
    public GameObject bulletPrefab;
    public GameObject muzzlePrefab;
    public Transform bulletFirePoint;
    public float bulletSpeed;

    [Header("Enemy")]
    public int enemyCount;
    public int enemyMax;
    public Transform playerIsChased;
    private Vector3 playerPos;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private float distanceToGenEnemy;

    [Header("Asteroid")]
    public int asteroidCount;
    public int asteroidMax;
    [SerializeField] private GameObject[] asteroidPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        
        GetPlayerPos();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Nhấn chuột trái
        {
            FireBullet();
        }
        CountEnemy();
        CountAsteroid();
        GetPlayerPos();
        GenerateEnemy();
        GenerateAsteroid();
    }
    public void FireBullet()
    {
        GameObject bullets = Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation);
        GameObject muzzle = Instantiate(muzzlePrefab, bulletFirePoint.position, bulletFirePoint.rotation);
        Destroy(muzzle, 0.2f);
        bullets.GetComponent<Bullet>();


    }
    private void GetPlayerPos()
    {
        playerIsChased = GameObject.FindGameObjectWithTag("Player").transform;
        playerPos = playerIsChased.position;
    }
    public void CountEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemyCount = enemies.Length;
    }
    public void CountAsteroid()
    {
        Asteroids[] asteroids = FindObjectsByType<Asteroids>(FindObjectsSortMode.None);
        asteroidCount = asteroids.Length;
    }
    public void GenerateEnemy()
    {
        if(enemyCount >= enemyMax)
        {
            return;
        }
        int randomIndex = Random.Range(0, enemyPrefab.Length);
        GameObject enemy = enemyPrefab[randomIndex];
        Instantiate(enemy, GetSpawnPosition(), Quaternion.identity);
    }
    public void GenerateAsteroid()
    {
        if (asteroidCount >= asteroidMax)
        {
            return;
        }
        int randomIndex = Random.Range(0, asteroidPrefab.Length);
        GameObject asteroid = asteroidPrefab[randomIndex];
        Instantiate(asteroid, GetSpawnPosition(), Quaternion.identity);
    }
    Vector3 GetSpawnPosition()
    {
        // Random góc trong khoảng 0 đến 360 độ
        float angle = Random.Range(0f, 2f * Mathf.PI);

        // Tính toán tọa độ tương đối
        float xOffset = Mathf.Cos(angle) * distanceToGenEnemy;
        float yOffset = Mathf.Sin(angle) * distanceToGenEnemy;

        // Tạo vị trí spawn dựa trên vị trí của Player
        Vector3 spawnPosition = playerPos + new Vector3(xOffset, yOffset, 0);

        return spawnPosition;
    }

}
