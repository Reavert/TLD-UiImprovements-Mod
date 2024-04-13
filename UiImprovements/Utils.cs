namespace UiImprovements;

public class Utils
{
    public const string ColdColorCode = "5b828f";
    public const string HotColorCode = "be7817";
    
    public static string ColorCodeFromHeatPercents(float heatPercents) => 
        heatPercents <= 0.0f ? ColdColorCode : HotColorCode;
}