namespace WebApplication1.Utils;

public static class Timeslot
{
    [Flags]
    public enum Timeslots : byte
    {
        TenToTwelve = 0,
        TwelveToTwo = 1,
        TwoToFour = 2,
    }

    public static Timeslots AsEnum(int value)
        => Enum.Parse<Timeslots>($"{3 & value}");
    public static Timeslots AsEnum(this Timeslots t)
        => t;

    public static TimeSpan Duration(int timeslot)
        => Duration(AsEnum(timeslot));
    public static TimeSpan Duration(Timeslots timeslotFlag)
        => TimeSpan.FromHours(2);

    public static String AsString(int timeslot)
        => AsString(AsEnum(timeslot));
    public static String AsString(Timeslots timeslotFlag)
    {
        if (timeslotFlag == Timeslots.TenToTwelve)
            return "10:00 - 12:00";

        if (timeslotFlag == Timeslots.TwelveToTwo)
            return "12:00 - 14:00";

        if (timeslotFlag == Timeslots.TwoToFour)
            return "14:00 - 16:00";

        throw new NotImplementedException();
    }
}
