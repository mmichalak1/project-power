using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface IProvideEntityInfo
    {
        int GetBasicHealth();
        int GetHealthWithItems();

        int GetBasicAttack();
        int GetAttackWithItems();

        int GetBasicDefence();
        int GetDefenceWithItems();
        float GetDefenceFromWool();

        int GetMaxWool();
        int GetCurrentWool();

        int GetCurrentLevel();
        int GetCurrentExp();
        int GetExpForNextLvl();
           
    }
}
