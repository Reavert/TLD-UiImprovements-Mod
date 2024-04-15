using Harmony;
using Il2Cpp;

namespace UiImprovements;

internal static class Shared
{
    internal static string s_CachedToolLabelText;

    internal static void UpdateCachedToolLabelText(Panel_IceFishingHoleClear panel)
    {
        int currentItemIndex = panel.m_ScrollList.m_SelectedIndex;
        if (currentItemIndex < 0 || currentItemIndex >= panel.m_AvailableTools._size)
            return;
        
        GearItem selectedGearItem = panel.m_AvailableTools._items[currentItemIndex];
        IceFishingHoleClearItem selectedIceFishingHoleClearItem = selectedGearItem.m_IceFishingHoleClearItem;

        string gearInventoryName = selectedGearItem.GetDisplayNameWithoutConditionForInventoryInterfaces();
        float minutesToBreakIce = panel.m_IceFishingHole.NormalizedFrozen *
                                  selectedIceFishingHoleClearItem.m_NumGameMinutesToClear;

        s_CachedToolLabelText = minutesToBreakIce < 1.0f 
            ? $"{gearInventoryName} ({Localization.Get("GAMEPLAY_lessthanoneminute")})" 
            : $"{gearInventoryName} ({minutesToBreakIce:F0} {Localization.Get("GAMEPLAY_minutes")})";
    }
}

[HarmonyPatch(typeof(Panel_IceFishingHoleClear), "Update")]
internal class Panel_IceFishingHoleClear_Update
{
    private static int s_LastItemIndex = -1;

    public static void Postfix(Panel_IceFishingHoleClear __instance)
    {
        int currentItemIndex = __instance.m_ScrollList.m_SelectedIndex;
        if (currentItemIndex != s_LastItemIndex && currentItemIndex != -1)
        {
            Shared.UpdateCachedToolLabelText(__instance);
            s_LastItemIndex = currentItemIndex;
        }

        __instance.m_ToolNameLabel.text = Shared.s_CachedToolLabelText;
    }
}

[HarmonyPatch(typeof(Panel_IceFishingHoleClear), "Enable")]
internal class Panel_IceFishingHoleClear_Enable
{
    public static void Postfix(Panel_IceFishingHoleClear __instance, bool enable)
    {
        Shared.UpdateCachedToolLabelText(__instance);
    }
}
