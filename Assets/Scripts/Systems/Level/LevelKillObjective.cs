using System;
using UnityEngine;

public class LevelKillObjective : MonoBehaviour
{
    [Header("Level Objective")]
    [SerializeField] private int targetKills = 5;

    [Header("Progress")]
    [SerializeField] private int currentKills;
    [SerializeField] private bool isComplete;

    public event Action OnLevelComplete;

    public int TargetKills => targetKills;
    public int CurrentKills => currentKills;
    public bool IsComplete => isComplete;

    void OnEnable()
    {
        EnemyDefeatEvents.OnEnemyDefeated += HandleEnemyDefeated;
    }

    void OnDisable()
    {
        EnemyDefeatEvents.OnEnemyDefeated -= HandleEnemyDefeated;
    }

    void HandleEnemyDefeated()
    {
        if (isComplete)
        {
            return;
        }

        currentKills++;

        if (currentKills >= targetKills)
        {
            isComplete = true;
            Debug.Log($"Level complete. Defeated {currentKills}/{targetKills} enemies.");
            OnLevelComplete?.Invoke();
        }
    }
}
