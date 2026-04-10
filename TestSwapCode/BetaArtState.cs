using Godot;

public static class BetaArtState
{
    public static readonly Dictionary<string, bool> CardSettings = new();
    private static readonly Dictionary<string, Texture2D> Cache = new();

    public static bool IsEnabled(string id)
    {
        return id != null && CardSettings.TryGetValue(id, out var v) && v;
    }

    public static void Set(string id, bool enabled)
    {
        if (id == null) return;
        CardSettings[id] = enabled;
    }

    public static Texture2D GetOrLoad(string path)
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