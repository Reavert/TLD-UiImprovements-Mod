using HarmonyLib;
using Il2Cpp;
using Il2CppTLD.Gear;

namespace UiImprovements;

[Harmony.HarmonyPatch(typeof(Panel_InsulatedFlask), "RefreshTables")]
internal class Panel_InsulatedFlask_RefreshTables
{
    public static void Postfix(Panel_InsulatedFlask __instance)
    {
        string heatPercents = $"{__instance.m_InsulatedFlask.m_HeatPercent:F0}%";

        string containerTitle = __instance.m_InsulatedFlask.GearItem.GetDisplayNameWithoutConditionForInventoryInterfaces();
        __instance.m_ContainerUI.m_ContainerTitle.text = $"{containerTitle} [{Utils.ColorCodeFromHeatPercents(__instance.m_InsulatedFlask.m_HeatPercent)}]({heatPercents})[-]";
    }
}

[HarmonyPatch(typeof(ItemDescriptionPage), "UpdateInsulatedFlaskIndicators")]
internal class ItemDescriptionPage_UpdateInsulatedFlaskIndicators
{
    public static void Postfix(ItemDescriptionPage __instance, InsulatedFlask insulatedFlask)
    {
        if (insulatedFlask == null)
            return;

        var flaskHotProgressBar = __instance.m_FlaskHotFillSprite.transform.parent;
        flaskHotProgressBar.gameObject.SetActive(false);
        __instance.m_FlaskHot.GetComponentInChildren<UILabel>().text = $"{Localization.Get("GAMEPLAY_Hot")} ({insulatedFlask.m_HeatPercent:F0}%)";
    }
}