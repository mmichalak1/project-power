using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Skill Tree", menuName = "Game/Skills/Skill Tree")]
public class SkillTree : ScriptableObject
{

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

    public List<Skill> AllSkills
    {
        get
        {
            var res = new List<Skill>();
            res.Add(BasicAttack);
            res.Add(ClassAttack);
            res.Add(Skill1);
            res.Add(Skill2);
            res.Add(Skill3);
            //res.Add(Skill1Upgrade1);
            //res.Add(Skill1Upgrade2);
            //res.Add(Skill2Upgrade1);
            //res.Add(Skill2Upgrade2);
            //res.Add(Skill3Upgrade1);
            //res.Add(Skill3Upgrade2);
            return res;
        }
    }

}
