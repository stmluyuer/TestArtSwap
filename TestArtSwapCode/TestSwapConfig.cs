using BaseLib;
using BaseLib.Config;

namespace TestArtSwap.TestArtSwapCode
{
    internal class TestArtSwapConfig : SimpleModConfig
    {
        // 全局开关：启用后，所有卡牌都使用测试画风（忽略单卡配置）
        public static bool EnableGlobalBetaArt { get; set; } = false;

    }
}
