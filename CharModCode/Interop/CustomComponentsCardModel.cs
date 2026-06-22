using BaseLib.Abstracts;
using BaseLib.Cards.Variables;
using BaseLib.Patches.Content;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MinionLib.Component;

namespace CharMod.CharModCode.Interop;

public abstract class CustomComponentsCardModel : ComponentsCardModel, ICustomModel, ILocalizationProvider
{
    private bool _initializedFrameMaterial;
    private Material? _frameMaterial;
    private bool _initializedBannerMaterial;
    private Material? _bannerMaterial;

    protected CustomComponentsCardModel(
        int baseCost,
        CardType type,
        CardRarity rarity,
        TargetType target,
        bool showInCardLibrary = true,
        bool autoAdd = true)
        : base(baseCost, type, rarity, target, showInCardLibrary)
    {
        if (autoAdd) CustomContentDictionary.AddModel(GetType());
    }

    public override bool GainsBlock => DynamicVars.Any(dynVar => dynVar.Value is BlockVar or CalculatedBlockVar);

    public virtual Texture2D? CustomFrame => null;

    public Material? CustomFrameMaterial
    {
        get
        {
            if (!_initializedFrameMaterial)
            {
                _frameMaterial = CreateCustomFrameMaterial;
                _initializedFrameMaterial = true;
            }

            return _frameMaterial;
        }
    }

    public Material? CustomBannerMaterial
    {
        get
        {
            if (!_initializedBannerMaterial)
            {
                _bannerMaterial = CreateCustomBannerMaterial;
                _initializedBannerMaterial = true;
            }

            return _bannerMaterial;
        }
    }

    public virtual Material? CreateCustomFrameMaterial => null;
    public virtual Material? CreateCustomBannerMaterial => null;
    public virtual string? CustomBannerMaterialPath => null;
    public virtual string? CustomPortraitPath => null;
    public virtual Texture2D? CustomPortrait => null;
    public virtual List<(string, string)>? Localization => null;
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.Frame), MethodType.Getter)]
internal static class CustomComponentsCardFramePatch
{
    [HarmonyPrefix]
    private static bool UseAltTexture(CardModel __instance, ref Texture2D? __result)
    {
        if (__instance is not CustomComponentsCardModel customCard) return true;

        __result = customCard.CustomFrame;
        return __result == null;
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.FrameMaterial), MethodType.Getter)]
internal static class CustomComponentsCardFrameMaterialPatch
{
    [HarmonyPrefix]
    private static bool UseAltMaterial(CardModel __instance, ref Material? __result)
    {
        if (__instance is not CustomComponentsCardModel customCard) return true;

        __result = customCard.CustomFrameMaterial;
        return __result == null;
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.BannerMaterial), MethodType.Getter)]
internal static class CustomComponentsCardBannerMaterialPatch
{
    [HarmonyPrefix]
    private static bool UseAltMaterial(CardModel __instance, ref Material? __result)
    {
        if (__instance is not CustomComponentsCardModel customCard) return true;

        __result = customCard.CustomBannerMaterial;
        return __result == null;
    }
}

[HarmonyPatch(typeof(CardModel), "BannerMaterialPath", MethodType.Getter)]
internal static class CustomComponentsCardBannerMaterialPathPatch
{
    [HarmonyPrefix]
    private static bool UseAltMaterial(CardModel __instance, ref string? __result)
    {
        if (__instance is not CustomComponentsCardModel customCard) return true;

        __result = customCard.CustomBannerMaterialPath;
        return __result == null;
    }
}

[HarmonyPatch(typeof(CardModel), "PortraitPngPath", MethodType.Getter)]
internal static class CustomComponentsCardPortraitPngPathPatch
{
    [HarmonyPrefix]
    private static bool UseAltTexture(CardModel __instance, ref string? __result)
    {
        if (__instance is not CustomComponentsCardModel customCard) return true;
        if (customCard.CustomPortraitPath == null) return true;

        __result = customCard.CustomPortraitPath;
        return false;
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.Portrait), MethodType.Getter)]
internal static class CustomComponentsCardPortraitPatch
{
    [HarmonyPrefix]
    private static bool UseAltTexture(CardModel __instance, ref Texture2D? __result)
    {
        if (__instance is not CustomComponentsCardModel customCard) return true;

        if (customCard.CustomPortrait != null)
        {
            __result = customCard.CustomPortrait;
            return false;
        }

        if (customCard.CustomPortraitPath == null) return true;

        __result = ResourceLoader.Load<Texture2D>(customCard.CustomPortraitPath);
        return false;
    }
}

[HarmonyPatch(typeof(CardModel), nameof(CardModel.PortraitPath), MethodType.Getter)]
internal static class CustomComponentsCardPortraitPathPatch
{
    [HarmonyPrefix]
    private static bool UseAltTexture(CardModel __instance, ref string? __result)
    {
        if (__instance is not CustomComponentsCardModel customCard) return true;

        if (customCard.CustomPortrait != null)
        {
            __result = customCard.CustomPortrait.ResourcePath;
            return false;
        }

        if (customCard.CustomPortraitPath == null) return true;

        __result = ResourceLoader.Load<Texture2D>(customCard.CustomPortraitPath).ResourcePath;
        return false;
    }
}
