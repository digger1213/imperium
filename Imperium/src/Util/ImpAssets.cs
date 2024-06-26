#region

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

#endregion

namespace Imperium.Util;

public abstract class ImpAssets
{
    /*
     * UI Prefabs
     */
    internal static GameObject ImperiumUIObject;
    internal static GameObject TeleportUIObject;
    internal static GameObject WeatherUIObject;
    internal static GameObject SpawningUIObject;
    internal static GameObject MoonUIObject;
    internal static GameObject SaveUIObject;
    internal static GameObject ObjectsUIObject;
    internal static GameObject SettingsUIObject;
    internal static GameObject ConfirmationUIObject;
    internal static GameObject RenderingUIObject;
    internal static GameObject OracleUIObject;
    internal static GameObject NavigatorUIObject;
    internal static GameObject VisualizerUIObject;
    internal static GameObject MapUIObject;
    internal static GameObject MinimapSettingsObject;
    internal static GameObject LayerSelectorObject;
    internal static GameObject MinimapOverlayObject;

    /*
     * Other Prefabs
     */
    internal static GameObject IndicatorObject;
    internal static GameObject NoiseOverlay;
    internal static GameObject NetworkHandler;
    internal static GameObject SpawnTimerObject;
    internal static GameObject SpikeTrapTimerObject;
    internal static GameObject SpawnIndicator;
    internal static GameObject ObjectInsightPanel;

    /*
     * Audio Clips
     */
    internal static AudioClip GrassClick;
    internal static AudioClip ButtonClick;

    /*
     * Materials
     */
    public static Material XrayMaterial;
    public static Material FresnelWhiteMaterial;
    public static Material FresnelBlueMaterial;
    public static Material FresnelYellowMaterial;
    public static Material FresnelGreenMaterial;
    public static Material FresnelRedMaterial;
    public static Material WireframeNavMeshMaterial;
    public static Material WireframePurpleMaterial;
    public static Material WireframeCyanMaterial;
    public static Material WireframeAmaranthMaterial;
    public static Material WireframeYellowMaterial;
    public static Material WireframeGreenMaterial;
    public static Material WireframeRedMaterial;
    public static Material ShiggyMaterial;

    internal static AssetBundle ImperiumAssets;

    internal static bool Load()
    {
        var assetFile = Path.Combine(
            Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!,
            "imperium_assets"
        );
        ImperiumAssets = AssetBundle.LoadFromFile(assetFile);
        if (ImperiumAssets == null)
        {
            Imperium.Log.LogInfo($"[PRELOAD] Failed to load assets from {assetFile}, aborting!");
            return false;
        }

        logBuffer = [];
        List<bool> loadResults =
        [
            LoadAsset(ImperiumAssets, "Assets/Prefabs/imperium_ui.prefab", out ImperiumUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/teleport_ui.prefab", out TeleportUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/weather_ui.prefab", out WeatherUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/spawning_ui.prefab", out SpawningUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/moon_ui.prefab", out MoonUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/save_ui.prefab", out SaveUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/objects_ui.prefab", out ObjectsUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/settings_ui.prefab", out SettingsUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/rendering_ui.prefab", out RenderingUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/oracle_ui.prefab", out OracleUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/navigator_ui.prefab", out NavigatorUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/visualizer_ui.prefab", out VisualizerUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/confirmation_ui.prefab", out ConfirmationUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/indicator.prefab", out IndicatorObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/map_ui.prefab", out MapUIObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/minimap.prefab", out MinimapOverlayObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/minimap_settings.prefab", out MinimapSettingsObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/layer_selector.prefab", out LayerSelectorObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/spawn_timer.prefab", out SpawnTimerObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/spiketrap_timer.prefab", out SpikeTrapTimerObject),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/insight_panel.prefab", out ObjectInsightPanel),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/spawn_indicator.prefab", out SpawnIndicator),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/noise_overlay.prefab", out NoiseOverlay),
            LoadAsset(ImperiumAssets, "Assets/Prefabs/network_handler.prefab", out NetworkHandler),
            LoadAsset(ImperiumAssets, "Assets/Materials/xray.mat", out XrayMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/fresnel_white.mat", out FresnelWhiteMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/fresnel_blue.mat", out FresnelBlueMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/fresnel_red.mat", out FresnelRedMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/fresnel_green.mat", out FresnelGreenMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/fresnel_yellow.mat", out FresnelYellowMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/wireframe_navmesh.mat", out WireframeNavMeshMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/wireframe_purple.mat", out WireframePurpleMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/wireframe_cyan.mat", out WireframeCyanMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/wireframe_amaranth.mat", out WireframeAmaranthMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/wireframe_yellow.mat", out WireframeYellowMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/wireframe_green.mat", out WireframeGreenMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/wireframe_red.mat", out WireframeRedMaterial),
            LoadAsset(ImperiumAssets, "Assets/Materials/shig.mat", out ShiggyMaterial),
            LoadAsset(ImperiumAssets, "Assets/Audio/GrassClick.wav", out GrassClick),
            LoadAsset(ImperiumAssets, "Assets/Audio/ButtonClick.ogg", out ButtonClick)
        ];


        if (loadResults.Any(result => result == false))
        {
            Imperium.Log.LogInfo($"[PRELOAD] Failed to load one or more assets from {assetFile}, aborting!");
            return false;
        }

        ImpOutput.LogBlock(logBuffer, "Imperium Resource Loader");

        return true;
    }

    private static List<string> logBuffer = [];

    private static bool LoadAsset<T>(AssetBundle assets, string path, out T loadedObject) where T : Object
    {
        loadedObject = assets.LoadAsset<T>(path);
        if (!loadedObject)
        {
            Imperium.Log.LogError($"[PRELOAD] Failed to load '{path}' from ./imperium_assets");
            return false;
        }

        logBuffer.Add($"> Successfully loaded {path.Split("/").Last()} from asset bundle.");

        return true;
    }
}