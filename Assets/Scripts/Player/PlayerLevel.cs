using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private int startingLevel = 1;

    [Header("Current Level")]
    [SerializeField] private int currentLevel = 1;
    
    [Header("Stat Bonuses Per Level")]
    [Tooltip("Max health gained per level")]
    [SerializeField] private int healthPerLevel = 10;
    
    [Tooltip("Damage bonus per level")]
    [SerializeField] private int damagePerLevel = 2;

    // Current progression
    [SerializeField] private int currentXP;
    [SerializeField] private int xpToNextLevel;

    // References to player components
    private PlayerHealth playerHealth;
    private PlayerCombat playerCombat;

    // Events for UI
    public event System.Action<int, int, int> OnXPChanged; // currentXP, xpToNext, level
    public event System.Action<int> OnLevelChanged;

    // Public properties
    public int CurrentLevel => currentLevel;
    public int CurrentXP => currentXP;
    public int XPToNextLevel => xpToNextLevel;

    void Start()
    {
        // Get component references
        playerHealth = GetComponent<PlayerHealth>();
        playerCombat = GetComponent<PlayerCombat>();

        // Initialize
        currentLevel = startingLevel;
        currentXP = 0;
        UpdateXPRequirement();

        // Subscribe to ExperienceManager
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.OnExperienceGained += AddExperience;
        }
        else
        {
            Debug.LogError("ExperienceManager not found in scene!");
        }

        // Notify UI of initial state
        OnXPChanged?.Invoke(currentXP, xpToNextLevel, currentLevel);
    }

    void OnDestroy()
    {
        // Unsubscribe from events
        if (ExperienceManager.Instance != null)
        {
            ExperienceManager.Instance.OnExperienceGained -= AddExperience;
        }
    }

    void AddExperience(int amount)
    {
        currentXP += amount;
        
        // Check for level up
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        // Notify UI
        OnXPChanged?.Invoke(currentXP, xpToNextLevel, currentLevel);
    }

    void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;

        Debug.Log($"LEVEL UP! Now level {currentLevel}");

        // Apply stat bonuses
        ApplyLevelBonuses();

        // Update XP requirement for next level
        UpdateXPRequirement();

        // Notify systems
        ExperienceManager.Instance?.NotifyLevelUp(currentLevel);
        OnLevelChanged?.Invoke(currentLevel);
        
        // Notify UI with updated values
        OnXPChanged?.Invoke(currentXP, xpToNextLevel, currentLevel);
    }

    void ApplyLevelBonuses()
    {
        // Increase max health
        if (playerHealth != null)
        {
            // TODO: Add method to PlayerHealth to increase max health
            Debug.Log($"+{healthPerLevel} max health");
        }

        // Increase damage
        if (playerCombat != null)
        {
            // TODO: Add method to PlayerCombat to increase damage
            Debug.Log($"+{damagePerLevel} damage");
        }
    }

    void UpdateXPRequirement()
    {
        if (ExperienceManager.Instance != null)
        {
            xpToNextLevel = ExperienceManager.Instance.GetXPRequiredForLevel(currentLevel + 1);
        }
    }

    // Debug method - remove in production
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddExperience(50); // Test XP gain
        }
    }
}