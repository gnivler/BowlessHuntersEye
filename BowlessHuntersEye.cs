using System.Reflection;
using BepInEx;
using Harmony;

namespace BowlessHuntersEye
{
    [BepInPlugin("com.gnivler.BowlessHuntersEye", "BowlessHuntersEye", "1.0")]
    public class BowlessHuntersEye : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = HarmonyInstance.Create("com.gnivler.BowlessHuntersEye");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(TargetingSystem), "TrueRange", MethodType.Getter)]
    public class TrueRangePatch
    {
        public static void Postfix(TargetingSystem __instance, Character ___m_character, ref float __result)
        {
            if (___m_character != null)
            {
                __result = ___m_character.Inventory.SkillKnowledge.IsItemLearned(8205160)
                    ? __instance.HunterEyeRange
                    : __instance.LongRange;
            }
        }
    }

    [HarmonyPatch(typeof(ItemDetailsDisplay), "GetTemporaryDesc", MethodType.Normal)]
    public class GetTemporaryDescPatch
    {
        public static void Postfix(Item _item, ref string __result)
        {
            if (_item.Description == "You can lock on enemies at longer distances when using a bow.")
            {
                __result = "You can lock on enemies at longer distances.";
            }
        }
    }
    
}