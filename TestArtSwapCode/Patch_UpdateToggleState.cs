using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Screens;

[HarmonyPatch(typeof(NInspectCardScreen), "UpdateCardDisplay")]
public static class Patch_UpdateToggleState
{
    static void Postfix(NInspectCardScreen __instance)
    {
        try
        {
            var toggle = __instance.GetNodeOrNull<Control>("TestArtToggleContainer");
            if (toggle == null) return;

            var visuals = toggle.GetNode<Control>("TickboxVisuals");

            var ticked = visuals.GetNodeOrNull<CanvasItem>("Ticked");
            var notTicked = visuals.GetNodeOrNull<CanvasItem>("NotTicked");

            var card = Traverse.Create(__instance)
                .Field<NCard>("_card").Value;

            string? id = card?.Model?.Id.ToString();
            if (string.IsNullOrEmpty(id)) return;

            bool isOn = BetaArtState.IsEnabled(id);

            if (ticked != null) ticked.Visible = isOn;
            if (notTicked != null) notTicked.Visible = !isOn;

        }
        catch (Exception e)
        {
            Log.Error($"Toggle刷新失败: {e}");
        }
    }
}