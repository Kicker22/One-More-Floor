using UnityEngine;
using System.Collections;

public class TestEnemyCombat : MonoBehaviour, IDamageable, IExperienceSource
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private float attackInterval = 1.0f;
    [SerializeField] private float attackRange = 2f;

    [SerializeField] private int enemyHealth = 100;
    [SerializeField] private int xpReward = 25;
    
    [Header("Damage Flash")]
    [SerializeField] private Color damageFlashColor = Color.red;
    [SerializeField] private float flashDuration = 0.1f;
    
    private float nextAttackTime = 0f;
    private Transform player;
    private Renderer[] enemyRenderers;
    private Color[] originalColors;
    private Coroutine flashCoroutine;

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
        
        // Get all renderers and store their original colors
        enemyRenderers = GetComponentsInChildren<Renderer>();
        if (enemyRenderers.Length > 0)
        {
            originalColors = new Color[enemyRenderers.Length];
            for (int i = 0; i < enemyRenderers.Length; i++)
            {
                originalColors[i] = enemyRenderers[i].material.color;
            }
        }
        else
        {
            Debug.LogWarning("No Renderer found on enemy or its children. Damage flash will not work.");
        }
    }

    public void TakeDamage(int amount)
    {
        enemyHealth -= amount;
        Debug.Log("Enemy took " + amount + " damage. Remaining health: " + enemyHealth);
        
        // Trigger damage flash effect
        if (enemyRenderers != null && enemyRenderers.Length > 0)
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
            }
            flashCoroutine = StartCoroutine(FlashDamage());
        }
        
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
    
    private IEnumerator FlashDamage()
    {
        // Change all renderers to damage color
        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            enemyRenderers[i].material.color = damageFlashColor;
        }
        
        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);
        
        // Return all renderers to original colors
        for (int i = 0; i < enemyRenderers.Length; i++)
        {
            enemyRenderers[i].material.color = originalColors[i];
        }
        flashCoroutine = null;
    }

    private void Die()
    {
        Debug.Log("Enemy died.");

        EnemyDefeatEvents.RaiseEnemyDefeated();
        
        // Award XP to player
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.AwardExperience(xpReward);
        }
        
        Destroy(gameObject);
    }
}