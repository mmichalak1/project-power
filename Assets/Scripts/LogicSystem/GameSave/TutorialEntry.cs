using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TutorialEntry
{
    public static TutorialEntry FromRuntime(Tutorial tutorial)
    {
        return new TutorialEntry() { TutorialName = tutorial.name, WasSeen = tutorial.WasSeen };
    }


    public static void FromData(ref Tutorial tutorial, TutorialEntry entry)
    {
        tutorial.WasSeen = entry.WasSeen;
    }

    public string TutorialName { get; set; }
    public bool WasSeen { get; set; }

}

