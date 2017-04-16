using System;
public class GameDateTime
{

    public TimeSpan ToTimeSpan()
    {
        return new TimeSpan(Days, Hours, Minutes, Seconds);
    }

    public int hours;
    public int days;
    public int minutes;
    public int seconds;

    public int Days
    {
        get { return days; }
        set
        {
            if (value < 0)
                return;
            int additionalHours = value / 24;
            hours += additionalHours;
            days = value % 24;
        }
    }
    public int Hours
    {
        get { return hours; }
        set
        {
            if (value < 0)
                return;
            hours = value;
        }
    }
    public int Minutes
    {
        get { return minutes; }
        set
        {
            if (value < 0)
                return;

            int additionalHours = value / 60;
            Hours += additionalHours;
            minutes = value % 60;

        }
    }
    public int Seconds
    {
        get { return seconds; }
        set
        {
            if (value < 0)
                return;
            int additionalMinutes = value / 60;
            Minutes += additionalMinutes;
            seconds = value % 60;
        }
    }

}
