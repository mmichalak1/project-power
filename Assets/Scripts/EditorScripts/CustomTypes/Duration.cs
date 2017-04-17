using UnityEngine;
using System;

[Serializable]
public class Duration
{
    public int days = 1;
    public int hours = 0;
    public int minutes = 0;
    public int seconds = 0;

    public TimeSpan ToTimeSpan()
    {
        if (!ValidateInput())
            throw new Exception("Current Duration is not covertable to Timepan - check if every value is grater than 0");
        else
            return new TimeSpan(days, hours, minutes, seconds);
    }

    private bool ValidateInput()
    {
        if (days < 0 || hours < 0 || minutes < 0 || seconds < 0)
            return false;

        return true;
    }
}
