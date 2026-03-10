using System.Collections.Generic;

public class EnemyInstance
{
    public EnemySO template;
    public int currentStrength;
    public List<PerkSO> currentPerks = new List<PerkSO>();

    public EnemyInstance(EnemySO so)
    {
        template = so;
        currentStrength = so.baseStrength;

        if (so.innatePerks != null)
        {
            currentPerks.AddRange(so.innatePerks);
        }
    }
}
