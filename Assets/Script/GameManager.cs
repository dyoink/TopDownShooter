using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject bulletPrefab;
    public int enemyCount;
    public int enemyMax;
    public Transform player;
    public Transform bulletFirePoint;
    public  float bulletSpeed;
    [SerializeField] private GameObject[] enemyPrefab;
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Nhấn chuột trái
        {
            FireBullet();
        }
        CountEnemy();
        GenerateEnemy();
    }
    public void FireBullet()
    {
        GameObject bullets = Instantiate(bulletPrefab, bulletFirePoint.position, bulletFirePoint.rotation);
        bullets.GetComponent<Bullet>();


    }
    public void CountEnemy()
    {
        Enemy[] enemies = FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        enemyCount = enemies.Length;
    }

    public void GenerateEnemy()
    {
        if(enemyCount >= enemyMax)
        {
            return;
        }
        int randomIndex = Random.Range(0, enemyPrefab.Length);
        int randomX = Random.Range(-50, 50);
        int randomY = Random.Range(-50, 50);
        GameObject enemy = enemyPrefab[randomIndex];
        Instantiate(enemy, new Vector3(randomX, randomY, 0), Quaternion.identity);
    }
    
}
