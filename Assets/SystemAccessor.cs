using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using System.Linq;

public static class SystemAccessor {


    private static List<ISystem> systems = new List<ISystem>();

    public static T GetSystem<T> () where T : class
    {
        return systems.FirstOrDefault(x => x is T) as T;
    }

    public static void AddSystem(ISystem system)
    {
        if (systems.FirstOrDefault(x => x.GetType() == system.GetType()) != null)
            Debug.LogError("System " + system.GetType().ToString() + " already registered");
        else
            systems.Add(system);

    }

    public static void RemoveSystem(ISystem system)
    {
        systems.Remove(system);
    }
}
