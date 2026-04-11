using System.Collections.Generic;
using Godot;
using TestArtSwap.TestArtSwapCode;

public static class BetaArtState
{
    public static readonly Dictionary<string, bool> CardSettings = new();
    private static readonly Dictionary<string, Texture2D> Cache = new();

    public static bool IsEnabled(string id)
    {
        if (id == null)
            return TestArtSwapConfig.EnableGlobalBetaArt;

        return CardSettings.TryGetValue(id, out var enabled)
            ? enabled
            : TestArtSwapConfig.EnableGlobalBetaArt;
    }

    public static void Set(string id, bool enabled)
    {
        if (id == null) return;
        CardSettings[id] = enabled;
    }

    public static Texture2D? GetOrLoad(string path)
    {
        if (string.IsNullOrEmpty(path)) return null;

        if (Cache.TryGetValue(path, out var tex))
            return tex;

        if (!ResourceLoader.Exists(path)) return null;

        tex = ResourceLoader.Load<Texture2D>(path);
        Cache[path] = tex;
        return tex;
    }

}