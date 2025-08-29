using System;

public class ProgressionComponent
{
    public int CurrentLevel { get; private set; }
    public int CurrentXP { get; private set; }
    private ProgressionType progressionType; // Guarda el tipo de curva que usa.

    public event Action<int> OnLevelUp;
    public event Action<int, int> OnXPChanged;

    // El constructor ahora nos obliga a especificar qué curva de progresión usar.
    public ProgressionComponent(ProgressionType type, int startLevel = 1)
    {
        this.progressionType = type;
        CurrentLevel = startLevel;

        LevelData levelData = ProgressionManager.Instance.GetLevelData(progressionType, CurrentLevel);
        CurrentXP = levelData != null ? levelData.XP_Required : 0;
    }

    public void GainXP(int amount)
    {
        CurrentXP += amount;

        LevelData nextLevelData = ProgressionManager.Instance.GetLevelData(progressionType, CurrentLevel + 1);
        int xpForNextLevel = nextLevelData != null ? nextLevelData.XP_Required : CurrentXP;
        
        OnXPChanged?.Invoke(CurrentXP, xpForNextLevel);
        CheckForLevelUp();
    }

    private void CheckForLevelUp()
    {
        // Consultamos los datos para el siguiente nivel USANDO NUESTRO TIPO.
        LevelData nextLevelData = ProgressionManager.Instance.GetLevelData(this.progressionType, CurrentLevel + 1);

        if (nextLevelData != null && CurrentXP >= nextLevelData.XP_Required)
        {
            CurrentLevel++;
            OnLevelUp?.Invoke(CurrentLevel);
            CheckForLevelUp();
        }
    }
}