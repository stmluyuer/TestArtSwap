using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Cards;

[HarmonyPatch(typeof(NCard), "UpdateVisuals")]
public static class Patch_NCard_UpdateVisuals
{
    static void Postfix(NCard __instance)
    {
        try
        {
            var model = __instance.Model;
            if (model == null) return;

            var tex = BetaArtService.GetTexture(model);
            if (tex == null) return;

            __instance._portrait.Texture = tex;

            // ancient
            var ancient = Traverse.Create(__instance)
                .Field<TextureRect>("_ancientPortrait").Value;

            if (ancient != null)
                ancient.Texture = tex;
        }
        catch (Exception e)
        {
            Log.Error($"[MyMod] 替换卡图失败: {e}");
        }
    }
}