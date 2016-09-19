using UnityEngine;
using System.Collections;

[CreateAssetMenu (fileName = "Skill Tree", menuName ="Game/Skills/Skill Tree")]
public class SkillTree : ScriptableObject {

    //Tier 0 skills
    public Skill BasicAttack, ClassAttack;
    //Tier 1 skills
    public Skill Skill1, Skill2, Skill3;
    //Skill1 Upgrades
    public Skill Skill1Upgrade1, Skill1Upgrade2;
    //Skill2 Upgrades
    public Skill Skill2Upgrade1, Skill2Upgrade2;
    //Skill3 Upgrades
    public Skill Skill3Upgrade1, Skill3Upgrade2;


}
