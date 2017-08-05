using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface ITurnManager
    {
        void SelectSheep(GameObject sheep);
        bool SelectSkill(Skill skill);
        void SelectTarget(GameObject target);
        void BeginFight(EnemyGroup group);
        void EndTurn(bool forced);
        void CancelSkill();
    }
}
