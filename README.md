# TestArtSwap

TestArtSwap 是一个针对 Slay the Spire 2 的 Godot/.NET Mod。它在卡牌检视界面增加“测试卡图”开关，支持按卡牌单独切换 Beta 艺术图，并提供全局开关控制测试画风显示。

## 功能

- 在 `Inspect Card` 界面添加自定义切换按钮
- 支持单卡开关状态保存
- 支持全局启用 Beta 艺术图
- 自动加载 `BetaPortraitPath` 或预设 Beta 卡图路径
- 使用 `HarmonyLib` 注入、`BaseLib` 配置，以及 Godot/.NET 技术栈

## 目录说明

- `TestArtSwap.csproj` - 项目文件，包含编译与复制到 `mods/` 的配置
- `TestArtSwapCode/` - Mod 的核心 C# 源代码
- `TestArtSwap.json` - Mod 描述文件
- `LICENSE` - MIT 许可证

## 依赖与要求

- Slay the Spire 2
- 已安装 `BaseLib`（作为 Mod 依赖）
- `0Harmony`
- Godot.NET 4.5.1 / .NET 9.0
- Windows 环境（当前 `csproj` 默认路径为 Windows）

## 构建与安装

1. 打开 `TestArtSwap.csproj`，根据你的本机环境调整以下路径：
   - `Sts2Dir`
   - `Sts2DataDir`
   - `ModsPath`
   - `GodotPath`（可选，用于导出 Godot `.pck`）
2. 在 Visual Studio 或 Godot 编辑器中构建项目
3. 构建完成后，会自动复制 `TestArtSwap.dll` 和 `TestArtSwap.json` 到 `$(ModsPath)/TestArtSwap/`
4. 启动游戏，进入 `mods/` 文件夹即可加载该 Mod

## 使用说明

- 进入卡牌检视界面后，会在原有升级切换附近显示“测试卡图”按钮
- 点击按钮可启用/禁用当前卡牌的 Beta 艺术图
- 当启用全局开关时，所有卡牌默认显示 Beta 艺术图

## 许可证

本项目使用 MIT 许可证。详见 `LICENSE`。