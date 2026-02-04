using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance { get; private set; }

    [Header("XP Settings")]
    [Tooltip("Base XP required for level 2")]
    [SerializeField] private int baseXPRequirement = 100;
    
    [Tooltip("Multiplier for XP requirement per level (level 3 = base * multiplier, etc)")]
    [SerializeField] private float xpMultiplier = 1.5f;

    // Events for other systems to subscribe to
    public event System.Action<int> OnExperienceGained;
    public event System.Action<int> OnLevelUp;

    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AwardExperience(int amount)
    {
        if (amount <= 0) return;

        Debug.Log($"Awarded {amount} XP");
        OnExperienceGained?.Invoke(amount);
    }

    public int GetXPRequiredForLevel(int level)
    {
        if (level <= 1) return 0;
        
        // Formula: BaseXP * (Multiplier ^ (level - 2))
        // Level 2 = 100, Level 3 = 150, Level 4 = 225, etc.
        return Mathf.RoundToInt(baseXPRequirement * Mathf.Pow(xpMultiplier, level - 2));
    }

    public void NotifyLevelUp(int newLevel)
    {
        Debug.Log($"Player reached level {newLevel}!");
        OnLevelUp?.Invoke(newLevel);
    }
}