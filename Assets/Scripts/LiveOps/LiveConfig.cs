using System;
using System.Globalization;

[Serializable]
public class LiveConfig
{
    public int version = 0;
    public string eventId = "";
    public string eventStartUtc = "";
    public string eventEndUtc = "";
    public float eventTrustMultiplier = 1f;
    public bool eventDecorations = false;
    public int quietDayThreshold = 2;

    public bool IsEventActive(DateTime nowUtc)
    {
        if (string.IsNullOrEmpty(eventId)) return false;
        if (!TryParseUtc(eventStartUtc, out DateTime start)) return false;
        if (!TryParseUtc(eventEndUtc, out DateTime end)) return false;
        return nowUtc >= start && nowUtc <= end;
    }

    static bool TryParseUtc(string text, out DateTime result)
    {
        return DateTime.TryParse(text, CultureInfo.InvariantCulture,
            DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal, out result);
    }
}