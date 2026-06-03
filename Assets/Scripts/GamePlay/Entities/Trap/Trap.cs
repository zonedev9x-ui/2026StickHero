using TMPro;
using UnityEngine;

public class Trap : Entity
{   
    public TrapType trapType = TrapType.None;
   
    public void InitTrap(StrengthScoreType type, int score)
    {
        strengthScore.InitStrengthScore(type, score);
    }

    public virtual void Attack()
    {
        EnableStrengthScore(false);
    }
}
