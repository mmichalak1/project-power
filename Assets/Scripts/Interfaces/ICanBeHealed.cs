﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Interfaces
{
    interface ICanBeHealed
    {
        void Heal(int value);

        void HealToFull();
    }
}
