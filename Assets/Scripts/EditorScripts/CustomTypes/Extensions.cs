using UnityEngine;
using System.Collections.Generic;
using System;

namespace Assets.Scripts.EditorScripts.CustomTypes
{
    public static class Extensions
    {
        public static T GetRandomElement<T>(this IList<T> list)
        {
            return list[(UnityEngine.Random.Range(0, list.Count))];
        }
    }
}
