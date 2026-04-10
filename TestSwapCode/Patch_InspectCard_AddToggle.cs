using System;
using System.Collections.Generic;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Nodes.Screens.InspectScreens;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.addons.mega_text;
using MegaCrit.Sts2.Core.Nodes.Screens;

namespace TestSwap.TestSwapCode
{
    [HarmonyPatch(typeof(NInspectCardScreen), "_Ready")]
    public static class Patch_InspectCard_AddToggle
    {
        private const string MOD_NAME = "[MyMod]";
        private const string TOGGLE_LABEL_TEXT = "测试卡图";
        private static readonly Vector2 TOGGLE_OFFSET = new Vector2(40, 45);

        static void Postfix(NInspectCardScreen __instance)
        {
            try
            {
                if (__instance.HasNode("TestArtToggleContainer"))
                    return;

                AddCustomToggle(__instance);
            }
            catch (Exception e)
            {
                Log.Error($"{MOD_NAME} UI创建失败: {e}");
            }
        }

        private static void AddCustomToggle(NInspectCardScreen screen)
        {
            var original = screen.GetNode<NTickbox>("Upgrade");
            if (original == null) return;

            var container = new HBoxContainer
            {
                Name = "TestArtToggleContainer",
                Position = original.Position + TOGGLE_OFFSET,
                MouseFilter = Control.MouseFilterEnum.Stop
            };

            // ===== Tick视觉 =====
            var visuals = original.GetNode<Control>("TickboxVisuals").Duplicate() as Control;

            if (visuals.Material != null)
                visuals.Material = (Material)visuals.Material.Duplicate();

            // ===== Label =====
            var label = original.GetNode<MegaLabel>("ShowUpgradeLabel").Duplicate() as MegaLabel;
            label.SetTextAutoSize(TOGGLE_LABEL_TEXT);

            container.AddChild(visuals);
            container.AddChild(label);

            var ticked = visuals.GetNode<CanvasItem>("Ticked");
            var notTicked = visuals.GetNode<CanvasItem>("NotTicked");

            string cardId = GetCardId(screen);
            bool isOn = BetaArtState.IsEnabled(cardId);

            UpdateTick(ticked, notTicked, isOn);

            container.GuiInput += (InputEvent e) =>
            {
                if (e is InputEventMouseButton mb &&
                    mb.Pressed &&
                    mb.ButtonIndex == MouseButton.Left)
                {
                    string id = GetCardId(screen);
                    if (string.IsNullOrEmpty(id)) return;

                    isOn = !isOn;

                    BetaArtState.Set(id, isOn);

                    UpdateTick(ticked, notTicked, isOn);

                    // 🔥 强制刷新卡图
                    AccessTools.Method(typeof(NInspectCardScreen), "UpdateCardDisplay")
                        .Invoke(screen, null);
                }
            };

            screen.AddChild(container);
        }

        private static void UpdateTick(CanvasItem t, CanvasItem n, bool on)
        {
            if (t != null) t.Visible = on;
            if (n != null) n.Visible = !on;
        }

        private static string GetCardId(NInspectCardScreen screen)
        {
            var card = Traverse.Create(screen)
                .Field<NCard>("_card").Value;

            return card?.Model?.Id.ToString();
        }
    }
}