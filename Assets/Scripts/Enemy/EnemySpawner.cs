using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [Tooltip("The enemy prefab to spawn")]
    [SerializeField] private GameObject enemyPrefab;
    
    [Tooltip("Time between spawns in seconds")]
    [SerializeField] private float spawnInterval = 3f;
    
    [Tooltip("Maximum number of enemies that can be alive at once")]
    [SerializeField] private int maxEnemies = 5;
    
    [Tooltip("Radius around spawner where enemies can spawn")]
    [SerializeField] private float spawnRadius = 5f;
    
    [Header("Optional Settings")]
    [Tooltip("Start spawning automatically on start")]
    [SerializeField] private bool autoStart = true;
    
    [Tooltip("Spawn enemies at random Y rotation")]
    [SerializeField] private bool randomRotation = true;
    
    private float nextSpawnTime;
    private int currentEnemyCount;
    private bool isSpawning;

    void Start()
    {
        if (autoStart)
        {
            StartSpawning();
        }
    }

    void Update()
    {
        if (isSpawning && Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    public void StartSpawning()
    {
        isSpawning = true;
        nextSpawnTime = Time.time + spawnInterval;
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab not assigned to spawner!");
            return;
        }

        // Get random position within spawn radius
        Vector2 randomCircle = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);

        // Optional random rotation
        Quaternion spawnRotation = randomRotation ? 
            Quaternion.Euler(0, Random.Range(0f, 360f), 0) : 
            Quaternion.identity;

        // Spawn the enemy
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        
        // Track enemy count
        currentEnemyCount++;
        
        // Subscribe to enemy death to decrease count
        IDamageable damageable = enemy.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // We'll need to handle this differently - for now just track manually
            EnemyTracker tracker = enemy.AddComponent<EnemyTracker>();
            tracker.spawner = this;
        }

        Debug.Log($"Spawned enemy at {spawnPosition}. Total: {currentEnemyCount}/{maxEnemies}");
    }

    public void OnEnemyDestroyed()
    {
        currentEnemyCount--;
        if (currentEnemyCount < 0) currentEnemyCount = 0;
    }

    // Visualize spawn radius in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
        
        // Draw center point
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.3f);
    }
}

// Helper component to track when enemy is destroyed
public class EnemyTracker : MonoBehaviour
{
    public EnemySpawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.OnEnemyDestroyed();
        }
    }
}
