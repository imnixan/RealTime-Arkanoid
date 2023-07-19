public static class StaticConstants
{
    private static string _turnedOnSettingsVaule = "ON";
    private static string _turnedOffSettingsVaule = "OFF";
    private static string _vibroSettingsPrefs = "VIBRO";
    private static string _soundSettingsPrefs = "SOUND";

    public static string TurnedOnSettingsValue
    {
        get { return _turnedOnSettingsVaule; }
    }

    public static string TurnedOffSettingsVaule
    {
        get { return _turnedOffSettingsVaule; }
    }

    public static string VibroSettingsPrefs
    {
        get { return _vibroSettingsPrefs; }
    }

    public static string SoundSettingsPrefs
    {
        get { return _soundSettingsPrefs; }
    }
}
