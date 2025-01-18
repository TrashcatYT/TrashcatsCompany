using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LobbyCompatibility.Attributes;
using LobbyCompatibility.Enums;
using UnityEngine;

namespace TrashcatsCompany;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency("BMX.LobbyCompatibility", BepInDependency.DependencyFlags.HardDependency)]
[BepInDependency(LethalLib.Plugin.ModGUID)] 
[LobbyCompatibility(CompatibilityLevel.ClientOnly, VersionStrictness.None)]
public class TrashcatsCompany : BaseUnityPlugin
{
    public static TrashcatsCompany Instance { get; private set; } = null!;
    internal new static ManualLogSource Logger { get; private set; } = null!;
    
    public static AssetBundle MyCustomAssets;
    internal static Harmony? Harmony { get; set; }

    private void Awake()
    {
        Logger = base.Logger;
        Instance = this;

        Patch();
        
        string sAssemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        MyCustomAssets = AssetBundle.LoadFromFile(Path.Combine(sAssemblyLocation, "trashcatscompany"));
        if (MyCustomAssets == null) {
            Logger.LogError("Failed to load custom assets."); // ManualLogSource for your plugin
            return;
        }

        #region Items
        int iRarity = 30;
        int iPrice  = 10;
        #region kittyCatMeow
            iRarity = 30;
            Item kittyCatMeow = MyCustomAssets.LoadAsset<Item>("Assets/TrashcatsCompany/ScriptableObjects/Items/KittyCatMeow.asset");
            if (kittyCatMeow == null) {
                Logger.LogError("Failed to load kittyCatMeow.");
                return;
            } else
            {
                Logger.LogError("Successfully loaded kittyCatMeow.");
            }
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(kittyCatMeow.spawnPrefab);
            LethalLib.Modules.Items.RegisterScrap(kittyCatMeow, iRarity, LethalLib.Modules.Levels.LevelTypes.All);
        #endregion
        
        #region mrWhale
            iRarity = 30;
            Item mrWhale = MyCustomAssets.LoadAsset<Item>("Assets/TrashcatsCompany/ScriptableObjects/Items/MrWhale.asset");
            if (mrWhale == null) {
                Logger.LogError("Failed to load mrWhale.");
                return;
            } else
            {
                Logger.LogError("Successfully loaded mrWhale.");
            }
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(mrWhale.spawnPrefab);
            LethalLib.Modules.Items.RegisterScrap(mrWhale, iRarity, LethalLib.Modules.Levels.LevelTypes.All);
        #endregion
        
        #region quaso
            iRarity = 10;
            Item quaso = MyCustomAssets.LoadAsset<Item>("Assets/TrashcatsCompany/ScriptableObjects/Items/Quaso.asset");
            if (quaso == null) {
                Logger.LogError("Failed to load quaso.");
                return;
            } else
            {
                Logger.LogError("Successfully loaded quaso.");
            }
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(quaso.spawnPrefab);
            LethalLib.Modules.Items.RegisterScrap(quaso, iRarity, LethalLib.Modules.Levels.LevelTypes.All);
        #endregion
        
        #region hamburber
            iRarity = 10;
            Item hamburber = MyCustomAssets.LoadAsset<Item>("Assets/TrashcatsCompany/ScriptableObjects/Items/Hamburber.asset");
            if (hamburber == null) {
                Logger.LogError("Failed to load hamburber.");
                return;
            } else
            {
                Logger.LogError("Successfully loaded hamburber.");
            }
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(hamburber.spawnPrefab);
            LethalLib.Modules.Items.RegisterScrap(hamburber, iRarity, LethalLib.Modules.Levels.LevelTypes.All);
        #endregion
        
        #region stove
            iRarity = 10;
            Item stove = MyCustomAssets.LoadAsset<Item>("Assets/TrashcatsCompany/ScriptableObjects/Items/Stove.asset");
            if (stove == null) {
                Logger.LogError("Failed to load stove.");
                return;
            } else
            {
                Logger.LogError("Successfully loaded stove.");
            }
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(stove.spawnPrefab);
            LethalLib.Modules.Items.RegisterScrap(stove, iRarity, LethalLib.Modules.Levels.LevelTypes.All);
        #endregion
        
        #region diamondSword
            iPrice  = 150;
            Item diamondSword = MyCustomAssets.LoadAsset<Item>("Assets/TrashcatsCompany/ScriptableObjects/Items/DiamondSword.asset");
            if (diamondSword == null) {
                Logger.LogError("Failed to load diamondSword.");
                return;
            } else
            {
                Logger.LogError("Successfully loaded diamondSword.");
            }
            LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(diamondSword.spawnPrefab);
            
            TerminalNode iTerminalNode = MyCustomAssets.LoadAsset<TerminalNode>("Assets/TrashcatsCompany/ScriptableObjects/Terminal/TerminalNodes/iTerminalNode.asset");
            if (iTerminalNode == null) {
                Logger.LogError("Failed to load iTerminalNode.");
                return;
            } else
            {
                Logger.LogError("Successfully loaded iTerminalNode.");
            }
            LethalLib.Modules.Items.RegisterShopItem(diamondSword, null, null, iTerminalNode, iPrice);
            
        #endregion
        #endregion
        
        Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
    }

    internal static void Patch()
    {
        Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

        Logger.LogDebug("Patching...");

        Harmony.PatchAll();

        Logger.LogDebug("Finished patching!");
    }

    internal static void Unpatch()
    {
        Logger.LogDebug("Unpatching...");

        Harmony?.UnpatchSelf();

        Logger.LogDebug("Finished unpatching!");
    }
}