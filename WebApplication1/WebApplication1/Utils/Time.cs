using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace WebApplication1.Utils;


public class DateRange
{
    public DateOnly Start { get; private set; }
    public DateOnly End { get; private set; }

    public DateRange(DateOnly start, DateOnly end)
    {
        Start = start;
        End = end;
    }

    public bool Overlaps(DateRange range)
    {
        return Start < range.End && End > range.Start;
    }
}


public static class Time
{
    public static DateTime ToDateTime(DateOnly d)
        => d.ToDateTime(TimeOnly.MinValue, DateTimeKind.Utc);

    public static bool Overlaps(DateOnly d1Start, DateOnly d1End, DateOnly d2Start, DateOnly d2End)
        => new DateRange(d1Start, d1End).Overlaps(new DateRange(d2Start, d2End));
}

public sealed class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateOnly.ParseExact(reader.GetString()!, Format, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format, CultureInfo.InvariantCulture));
    }
}
