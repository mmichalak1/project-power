﻿using UnityEngine;
using System.Collections.Generic;

public static class Extensions
{
    public static T GetRandomElement<T>(this IList<T> list)
    {
        if (list.Count == 0)
            return default(T);
        int index = Random.Range(0, list.Count);
        return list[index];
    }


}

