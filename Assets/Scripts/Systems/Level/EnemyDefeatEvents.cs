using System;

public static class EnemyDefeatEvents
{
    public static event Action OnEnemyDefeated;

    public static void RaiseEnemyDefeated()
    {
        OnEnemyDefeated?.Invoke();
    }
}
