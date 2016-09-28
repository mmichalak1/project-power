using UnityEngine;
using System.Collections;
[CreateAssetMenu(fileName = "Group Taunt", menuName = "Game/Skills/GroupTaunt")]
public class GroupTaunt : Skill {

    public Assets.Scripts.ScriptableObjects.TauntedBrain TauntedBrain;
    private Assets.Scripts.ScriptableObjects.TauntedBrain _myCopy;

    public override void Initialize(GameObject parent)
    {
        _myCopy = Instantiate(TauntedBrain);
        _myCopy.Target = parent;
        base.Initialize(parent);
    }

    protected override void PerformAction(GameObject actor, GameObject target)
    {
        var enemyGroup = target.transform.parent.GetComponent<WolfGroupManager>();
        if (enemyGroup == null)
            return; 

        foreach (var trans in enemyGroup.enemies)
        {
            trans.gameObject.GetComponent<AttackController>().AddBrain(_myCopy);
        }
       
        base.PerformAction(actor, target);
    }


}
