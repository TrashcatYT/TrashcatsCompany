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
        
        newScrap(20, "KittyCatMeow");
        newScrap(10, "MrWhale");
        newScrap(30, "Quaso");
        newScrap(30, "Hamburber");
        newScrap(10, "Stove");
        newScrap(10, "PotatoGlados");
        newScrap(5, "MonsterCan");
        
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

    private void newScrap(int iRarity, string path)
    {
        Item newItem = MyCustomAssets.LoadAsset<Item>("Assets/TrashcatsCompany/ScriptableObjects/Items/" + path + ".asset");
        if (newItem == null) {
            Logger.LogError("Failed to load " + path + ".");
            return;
        } else
        {
            Logger.LogError("Successfully loaded " + path + ".");
        }
        LethalLib.Modules.NetworkPrefabs.RegisterNetworkPrefab(newItem.spawnPrefab);
        LethalLib.Modules.Items.RegisterScrap(newItem, iRarity, LethalLib.Modules.Levels.LevelTypes.All);
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