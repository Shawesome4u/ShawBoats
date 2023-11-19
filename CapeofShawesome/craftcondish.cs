using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using static Jotunn.Utils.GameConstants;
using Jotunn.Managers;
using Jotunn.Configs;
using Jotunn.Entities;
using HarmonyLib;


public class CraftingConditions : MonoBehaviour
{
    /*
    [HarmonyPatch(typeof(InventoryGui))]
    [HarmonyPrefix]
    [HarmonyPatch(nameof(InventoryGui.UpdateRecipeList))]
    static void UpdateRecipeList(InventoryGui __instance, ref List<Recipe> recipes)
    {
        if (__instance.InCraftTab())
        {
            var currentEnvironment = EnvMan.instance.GetCurrentEnvironment();
            var thunderstormActive = currentEnvironment.m_name == Weather.ThunderStorm;
            recipes.RemoveAll(recipe =>
            {
                return (
                !thunderstormActive && (recipe.m_item.name == "BeltMeld")
                || (thunderstormActive && recipe.m_item.name == "ClearWeatherItem")
                );
            });

        }
    }
    */
    [HarmonyPostfix]
    [HarmonyPatch(typeof(Inventory), "CountItems")]
    private static void PostfixCountItems(Inventory __instance, ref int __result, string name, int quality, bool matchWorldLevel)
    {
        //DBG.blogDebug("inPostfix");
        if (ConditionItems.Exists(item => { return (item.m_shared.m_name == name); }))
        {
            __result += hasCondition(name, __instance);
        }
    }

    private static int hasCondition(string name, Inventory inv)
    {
        if (name == "Active Thunderstorm")
        {
            int returnval = (EnvMan.instance.GetCurrentEnvironment().m_name == Weather.ThunderStorm) ? 33 : 0;
            returnval += (EnvMan.instance.GetCurrentEnvironment().m_name == Weather.ClearThunderStorm) ? 33 : 0;
            //AllTameable.DBG.blogDebug("returnval=" + returnval);
            return returnval;
        }
        else if (name == "Tamed Creatures Nearby")
        {
            Vector3 plr_pos = Player.m_localPlayer.transform.position;
            return getTamedCloseto(plr_pos, 10);
        }
        else if (name == "Refineries Nearby")
        {
            Vector3 plr_pos = Player.m_localPlayer.transform.position;
            return getSmelters(plr_pos, 20);
        }
        else if (name == onfire_prefNname[1])
        {
            return getOnFire(Player.m_localPlayer);
        }
        else if (name == freezing_prefNname[1])
        {
            return getFreezing(Player.m_localPlayer);
        }
        return 0;
    }
    public static int getTamedCloseto(Vector3 center, float maxdist)
    {
        List<Character> characters = Character.GetAllCharacters();
        int count = 0;
        foreach (Character character in characters)
        {
            if ((Vector3.Distance(character.transform.position, center) < maxdist) && (character.m_tamed) && character.GetComponent<ZNetView>().IsValid())
            {
                //AllTameable.DBG.blogDebug("Added "+character.name +" to close tames");
                count++;
            }
        }
        return count;
    }
    public static int getSmelters(Vector3 center, float maxdist)
    {
        //AllTameable.DBG.blogDebug("in get smelt");
        int count = 0;
        int m_triggerMask = LayerMask.GetMask("piece_nonsolid");
        Collider[] array = Physics.OverlapSphere(center, maxdist, m_triggerMask, QueryTriggerInteraction.Collide);
        List<GameObject> loadedsmelts = new List<GameObject>();
        foreach (Collider collider in array)
        {
            if (!(bool)collider.transform.parent)
            {
                continue;
            }
            //AllTameable.DBG.blogDebug("coll=" + collider.name);
            GameObject smelter = collider.transform.parent.gameObject;
            if (!smelter.name.StartsWith("eitrrefinery"))
            {
                continue;
            }
            if ((bool)smelter.GetComponent<Smelter>())
            {
                //AllTameable.DBG.blogDebug("smelt=" + smelter.name);
                if (loadedsmelts.Contains(smelter))
                {
                    //AllTameable.DBG.blogDebug("already added smelt=" + smelter.name);
                    continue;
                }
                loadedsmelts.Add(smelter);
                if (smelter.transform.Find("_enabled").gameObject.activeSelf)
                {
                    //AllTameable.DBG.blogDebug("is active=" + smelter.name);

                    count++;
                }

            }
        }
        //AllTameable.DBG.blogDebug("smelt num=" + count);
        return count;
    }
    public static int getOnFire(Player plr)
    {
        List<StatusEffect> effects = plr.m_seman.m_statusEffects;
        float count = 0;
        float resistant_mod = 1;
        if (EnvMan.instance.m_currentBiome != Heightmap.Biome.AshLands)
        {
            return 0;
        }
        foreach (StatusEffect effect in effects)
        {
            Type effect_type = effect.GetType();
            if (effect_type == typeof(SE_Burning))
            {
                SE_Burning effect_Burn = effect as SE_Burning;
                count += effect_Burn.m_fireDamagePerHit;
            }
            else if (effect_type == typeof(SE_Stats))
            {
                SE_Stats effect_Stat = effect as SE_Stats;
                foreach (HitData.DamageModPair hit in effect_Stat.m_mods)
                {
                    //AllTameable.DBG.blogDebug("SE_Stat=" + hit.m_modifier + ", " + hit.m_type);
                    if (hit.m_type != HitData.DamageType.Fire)
                    {
                        continue;
                    }
                    if (hit.m_modifier == HitData.DamageModifier.Resistant)
                    {
                        resistant_mod *= 1.8f;
                    }
                    else if (hit.m_modifier == HitData.DamageModifier.VeryResistant)
                    {
                        resistant_mod *= 3.6f;
                    }
                    else if (hit.m_modifier == HitData.DamageModifier.Immune)
                    {
                        count += 10;
                    }
                }
            }

            //AllTameable.DBG.blogDebug("Burning is at " + count*resistant_mod);
        }
        //AllTameable.DBG.blogDebug("Burning is at " + count * resistant_mod);
        return (int)(count * resistant_mod);
    }

    public static int getFreezing(Player plr)
    {
        List<StatusEffect> effects = plr.m_seman.m_statusEffects;
        float count = 0;
        float resistant_mod = 1;
        if (EnvMan.instance.m_currentBiome != Heightmap.Biome.DeepNorth)
        {
            //AllTameable.DBG.blogDebug("Not in DeepNorth");
            return 0;
        }

        //AllTameable.DBG.blogDebug("In DeepNorth");
        foreach (StatusEffect effect in effects)
        {
            Type effect_type = effect.GetType();
            if (effect_type == typeof(SE_Frost))
            {
                SE_Frost effect_Frost = effect as SE_Frost;
                count += effect_Frost.m_freezeTimePlayer / 3;
            }
            else if (effect_type == typeof(SE_Stats))
            {

                SE_Stats effect_Stat = effect as SE_Stats;
                if (effect_Stat.m_hitType == HitData.HitType.Freezing)
                {
                    count -= effect_Stat.m_healthPerTick;

                }
                foreach (HitData.DamageModPair hit in effect_Stat.m_mods)
                {
                    //AllTameable.DBG.blogDebug("SE_Stat=" + hit.m_modifier + ", " + hit.m_type);
                    if (hit.m_type != HitData.DamageType.Frost)
                    {
                        continue;
                    }

                    if (hit.m_modifier == HitData.DamageModifier.Resistant)
                    {
                        resistant_mod *= 1.8f;
                    }
                    else if (hit.m_modifier == HitData.DamageModifier.VeryResistant)
                    {
                        resistant_mod *= 3.6f;
                    }
                }
            }

            //AllTameable.DBG.blogDebug("Burning is at " + count*resistant_mod);
        }
        //AllTameable.DBG.blogDebug("Freezing is at " + count * resistant_mod);
        return (int)(count * resistant_mod);
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Player), "HaveRequirements", new Type[] { typeof(Recipe), typeof(bool), typeof(int) })]
    private static void PostfixHaveRequirementItems(Player __instance, Recipe recipe, bool discover, int qualityLevel)
    {

        bool addedKnown = false;
        if (EnvMan.instance.GetCurrentEnvironment().m_name == Weather.ThunderStorm)
        {
            if (!__instance.m_knownMaterial.Contains(thunderstorm_prefNname[1]))
            {
                //AllTameable.DBG.blogDebug("Added Thunderstorm item");
                __instance.m_knownMaterial.Add(thunderstorm_prefNname[1]);
                addedKnown = true;
            }
        }
        if (!__instance.m_knownMaterial.Contains(tamednearby_prefNname[1]))
        {
            //AllTameable.DBG.blogDebug("Added Tamed Creatures Nearby item");
            __instance.m_knownMaterial.Add(tamednearby_prefNname[1]);
            addedKnown = true;
        }
        if (!__instance.m_knownMaterial.Contains(refinernearby_prefNname[1]))
        {
            //AllTameable.DBG.blogDebug("Added Refineries Nearby");
            __instance.m_knownMaterial.Add(refinernearby_prefNname[1]);
            addedKnown = true;
        }
        if (!__instance.m_knownMaterial.Contains(onfire_prefNname[1]))
        {
            //AllTameable.DBG.blogDebug("Added onFire");
            __instance.m_knownMaterial.Add(onfire_prefNname[1]);
            addedKnown = true;
        }
        if (!__instance.m_knownMaterial.Contains(freezing_prefNname[1]))
        {
            __instance.m_knownMaterial.Add(freezing_prefNname[1]);
            addedKnown = true;
        }
        if (addedKnown) { __instance.UpdateKnownRecipesList(); }
    }


    public GameObject Root;
    public static List<ItemDrop.ItemData> ConditionItems = new List<ItemDrop.ItemData>();
    public static string[] thunderstorm_prefNname = new string[] { "Thunderstorm_Item", "Active Thunderstorm" };
    public static string[] tamednearby_prefNname = new string[] { "HasTamed_Item", "Tamed Creatures Nearby" };
    public static string[] refinernearby_prefNname = new string[] { "HasRefiner_Item", "Refineries Nearby" };
    public static string[] onfire_prefNname = new string[] { "OnFire_Item", "Is on Fire... in Ashlands" };
    public static string[] freezing_prefNname = new string[] { "AT_Freezing_Item", "Is Freezing in Deep North" };
    private void Awake()
    {
        Root = new GameObject("CraftingConditions");
        Root.transform.SetParent(shawcape.Shawesomes_Divine_Armaments.Root.transform);
        Root.SetActive(value: false);
        UnityEngine.Object.DontDestroyOnLoad(Root);
        shawcape.Shawesomes_Divine_Armaments.logger.LogWarning("in crafting condish");
        //setConditionItems();
    }

    public static void setConditionItems()
    {
        addThunderstormCondition();
        addHasTamedCondition();
        addHasSmeltersCondition();
        addOnFireCondition();
        addFreezingCondition();
    }
    private static void addThunderstormCondition()
    {
        CustomItem item_cust = ItemManager.Instance.GetItem(thunderstorm_prefNname[0]);
        if (item_cust == null)
        {
            item_cust = new CustomItem(thunderstorm_prefNname[0], "Thunderstone");
            ItemManager.Instance.AddItem(item_cust);
            var idata = item_cust.ItemDrop.m_itemData;
            idata.m_shared.m_name = thunderstorm_prefNname[1];
            idata.m_shared.m_description = "A thunderstorm is occurring";
            ConditionItems.Add(idata);
        }
    }
    private static void addHasTamedCondition()
    {
        CustomItem item_cust = ItemManager.Instance.GetItem(tamednearby_prefNname[0]);
        if (item_cust == null)
        {
            item_cust = new CustomItem(tamednearby_prefNname[0], "TrophyBoar");
            ItemManager.Instance.AddItem(item_cust);
            var idata = item_cust.ItemDrop.m_itemData;
            idata.m_shared.m_name = tamednearby_prefNname[1];
            idata.m_shared.m_description = "You have tamed creatures nearby";
            ConditionItems.Add(idata);
        }
    }

    private static void addHasSmeltersCondition()
    {
        CustomItem item_cust = ItemManager.Instance.GetItem(refinernearby_prefNname[0]);
        if (item_cust == null)
        {
            item_cust = new CustomItem(refinernearby_prefNname[0], "Eitr");
            ItemManager.Instance.AddItem(item_cust);
            var idata = item_cust.ItemDrop.m_itemData;
            idata.m_shared.m_name = refinernearby_prefNname[1];
            idata.m_shared.m_description = "You have refineries nearby";
            GameObject eitrrefine = PrefabManager.Instance.GetPrefab("eitrrefinery");
            idata.m_shared.m_icons[0] = eitrrefine.GetComponent<Piece>().m_icon;
            ConditionItems.Add(idata);
        }
    }

    private static void addOnFireCondition()
    {
        CustomItem item_cust = ItemManager.Instance.GetItem(onfire_prefNname[0]);
        if (item_cust == null)
        {
            item_cust = new CustomItem(onfire_prefNname[0], "TrophySurtling");
            ItemManager.Instance.AddItem(item_cust);
            var idata = item_cust.ItemDrop.m_itemData;
            idata.m_shared.m_name = onfire_prefNname[1];
            idata.m_shared.m_description = "You are on fire while in Ahslands";
            GameObject eitrrefine = PrefabManager.Instance.GetPrefab("bonfire");
            idata.m_shared.m_icons[0] = eitrrefine.GetComponent<Piece>().m_icon;
            ConditionItems.Add(idata);
        }
    }

    private static void addFreezingCondition()
    {
        CustomItem item_cust = ItemManager.Instance.GetItem(freezing_prefNname[0]);
        if (item_cust == null)
        {
            item_cust = new CustomItem(freezing_prefNname[0], "FreezeGland");
            ItemManager.Instance.AddItem(item_cust);
            var idata = item_cust.ItemDrop.m_itemData;
            idata.m_shared.m_name = freezing_prefNname[1];
            idata.m_shared.m_description = "You are Freezing while in the Deep North";
            idata.m_shared.m_icons[0] = shawcape.Shawesomes_Divine_Armaments.FreezingIcon;
            ConditionItems.Add(idata);
        }
    }


}
