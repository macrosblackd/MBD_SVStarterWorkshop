using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;

namespace MBD_SVStarterWorkshop
{
    [BepInPlugin(pluginGuid, pluginName, pluginVersion)]
    public class GuaranteeStarterWorkshop : BaseUnityPlugin
    {
        public const string pluginGuid = "macrosblackd.starvalormods.guaranteestarterworkshop";
        public const string pluginName = "Guarantee Starter Workshop";
        public const string pluginVersion = "1.0.0";

        private static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource(pluginName);

        private void Awake()
        {
            Harmony.CreateAndPatchAll(typeof(GuaranteeStarterWorkshop));
            logger.LogInfo($"Plugin {pluginName} version {pluginVersion} is loaded!");
        }
        
        [HarmonyPatch(typeof(TSector), MethodType.Constructor)]
        [HarmonyPatch(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int), typeof(int) })]
        [HarmonyPostfix]
        public static void PostFix_TSector_Ctor(TSector __instance, int mode)
        {
            logger.LogDebug($"PostFix_TSector_Ctor called with mode: {mode}");
            logger.LogDebug($"Sector has workshop? {__instance.HasStationOfFaction(6)}");
            if (!__instance.HasStationOfFaction(6) && mode == 1)
            {
                Workshop workshop = new Workshop(__instance.level, __instance.GetMainCoords(true), 6, __instance.Index, 0, -1);
                logger.LogInfo($"Creating workshop at {__instance.GetMainCoords(true)} in sector {__instance.Index}");
            }
        }
    }
}
