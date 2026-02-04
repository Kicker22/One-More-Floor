using UnityEngine;

public class TestEnemyCombat : MonoBehaviour, IDamageable, IExperienceSource
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float attackInterval = 1.0f;
    [SerializeField] private float attackRange = 2f;

    [SerializeField] private int enemyHealth = 100;
    [SerializeField] private int xpReward = 25; // NEW
    
    private float nextAttackTime = 0f;
    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogError("No GameObject with 'Player' tag found!");
        }
    }

    public void TakeDamage(int amount)
    {
        enemyHealth -= amount;
        Debug.Log("Enemy took " + amount + " damage. Remaining health: " + enemyHealth);
        if (enemyHealth <= 0)
        {
            Die();
        }
    }

    void Update()
    {
        if (player == null || Time.time < nextAttackTime) return;
        
        float distance = Vector3.Distance(transform.position, player.position);
        
        if (distance <= attackRange)
        {
            IDamageable damageable = player.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
                nextAttackTime = Time.time + attackInterval;
                Debug.Log($"Enemy dealt {damageAmount} damage! Distance: {distance:F2}");
            }
            else
            {
                Debug.LogWarning("Player has no IDamageable component!");
            }
        }
    }

    // NEW - Interface implementation
    public int GetExperienceReward()
    {
        return xpReward;
    }

    private void Die()
    {
        Debug.Log("Enemy died.");
        
        // Award XP to player
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.AwardExperience(xpReward);
        }
        
        Destroy(gameObject);
    }
}