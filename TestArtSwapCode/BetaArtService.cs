using Godot;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Models;

public static class BetaArtService
{
    public static Texture2D? GetTexture(CardModel model)
    {
        if (model == null) return null;

        string cardId = model.Id.ToString();

        // 没开就直接用原图
        if (!BetaArtState.IsEnabled(cardId))
            return model.Portrait;

        // 反射 BetaPortraitPath
        try
        {
            var betaPath = HarmonyLib.Traverse
                .Create(model)
                .Property<string>("BetaPortraitPath")
                .Value;

            var tex = BetaArtState.GetOrLoad(betaPath);
            if (tex != null) return tex;
        }
        catch { }

        // fallback 构建路径
        try
        {
            string? pool = model.Pool?.Title?.ToLowerInvariant();
            string? id = model.Id.Entry?.ToLowerInvariant();

            if (!string.IsNullOrEmpty(pool) && !string.IsNullOrEmpty(id))
            {
                string path = $"atlases/card_atlas.sprites/{pool}/beta/{id}.tres";
                path = ImageHelper.GetImagePath(path);

                var tex = BetaArtState.GetOrLoad(path);
                if (tex != null) return tex;
            }
        }
        catch { }

        // fallback 默认
        return model.Portrait;
    }
}