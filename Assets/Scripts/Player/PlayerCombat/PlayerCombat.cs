using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    [Header("Melee Attack Settings")]
    [SerializeField] private int meleeDamage = 25;
    [SerializeField] private float meleeRange = 2f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private LayerMask enemyLayer;
    
    private InputAction attackAction;
    private float nextAttackTime = 0f;

    void Start()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
    }

    void Update()
    {
        if (attackAction.WasPressedThisFrame() && Time.time >= nextAttackTime)
        {
            PerformMeleeAttack();
            nextAttackTime = Time.time + attackCooldown;
        }
    }

    void PerformMeleeAttack()
    {        
        // Find all enemies in attack range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, meleeRange, enemyLayer);
        
        foreach (Collider enemy in hitEnemies)
        {
            IDamageable damageable = enemy.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(meleeDamage);
            }
        }
        
        if (hitEnemies.Length == 0)
        {
            Debug.Log("Attack missed - no enemies in range");
        }
    }

    // Visualize attack range in Scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }
}