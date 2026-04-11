using BaseLib.Config;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Modding;


[ModInitializer(nameof(Initialize))]
public partial class MainFile : Node
{
    public const string ModId = "TestArtSwap"; //At the moment, this is used only for the Logger and harmony names.

    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, LogType.Generic);

    public static void Initialize()
    {
        ModConfigRegistry.Register(ModId, new TestArtSwap.TestArtSwapCode.TestArtSwapConfig());

        Harmony harmony = new(ModId);
        harmony.PatchAll();
        Log.Debug("Mod initialized!");
    }
}
