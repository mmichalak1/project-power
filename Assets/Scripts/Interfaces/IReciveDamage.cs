using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    interface IReciveDamage
    {
        void DealDamage(int value, UnityEngine.GameObject source);
        void DealDamage(int value);
    }
}
