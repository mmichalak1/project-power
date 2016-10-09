using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Group Taunt", menuName = "Game/Skills/GroupTaunt")]
public class GroupTaunt : Skill {

    public Assets.Scripts.ScriptableObjects.TauntedBrain TauntedBrain;
    private Assets.Scripts.ScriptableObjects.TauntedBrain _myCopy;

    [Range(1, 5)]
    public int SkillDuration = 1;

    [Range(0, 100)]
    public int DamagePercentReduced = 20;

    public override void Initialize(GameObject parent)
    {
        _myCopy = Instantiate(TauntedBrain);
        _myCopy.Target = parent;
        _myCopy.Duration = SkillDuration;
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var enemyGroup = target.transform.parent.GetComponent<WolfGroupManager>();
        if (enemyGroup == null)
            return;

        var damageReductor = actor.AddComponent<DamageReductor>();
        damageReductor.Duration = SkillDuration;
        damageReductor.DamageReduced = DamagePercentReduced;

        foreach (var trans in enemyGroup.enemies)
        {
            trans.gameObject.GetComponent<AttackController>().AddBrain(_myCopy);
            
        }
       
        base.PerformAction(actor, target);
    }


}
