﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    public interface IProvideStatistics
    {
        int GetDamage();
        int GetDefence();
        int GetMaxHealth();
        float GetDamageMultiplicator();


    }
}
