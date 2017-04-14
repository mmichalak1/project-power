using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.LogicSystem
{
    public class PlanComparator : Comparer<Plan>, IEqualityComparer<Plan>
    {
        public override int Compare(Plan x, Plan y)
        {
            if (x.Actor == y.Actor && x.Target == y.Target && x.Skill == y.Skill)
                return 0;
            else
                return -1;
        }

        public bool Equals(Plan x, Plan y)
        {
            if (x.Actor == y.Actor && x.Target == y.Target && x.Skill == y.Skill)
                return true;
            else
                return false;
        }

        public int GetHashCode(Plan obj)
        {
            return obj.GetHashCode();
        }
    }
}
