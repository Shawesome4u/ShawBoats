using BepInEx;
using HarmonyLib;
using Jotunn; 
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Reflection;
using System.Linq;
using UnityEngine;


namespace shawcape
{
    [BepInPlugin("IDshawesome4u", "Shawesomes_Divine_Armaments", "2.0.2")]
    [BepInDependency("com.jotunn.jotunn", BepInDependency.DependencyFlags.HardDependency)]
    public class Shawesomes_Divine_Armaments : BaseUnityPlugin
    {

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ItemStyle), "Setup")] //checks if there are more than one material
        private static void PostfixIDropStart(ItemStyle __instance, int style)
        {
            //DBG.blogDebug("in ItemStyle Setup go=" + __instance.gameObject.name + ",style=" + style);
            IEquipmentVisual[] vis = __instance.transform.parent.GetComponentsInChildren<IEquipmentVisual>();
            for (int i = 0; i < vis.Length; i++)
            {
                //harmonyLog.LogWarning("vis" + i + " name=" + vis[i].ToString());
                if (vis[i].GetType() != typeof(ItemStyle))
                {
                    continue;
                }
                /*
                if ((ItemStyle)vis[i] == __instance)
                {
                    continue;
                }
                */
                MonoBehaviour mb = vis[i] as MonoBehaviour;
                //DBG.blogDebug("vis Setup go=" + mb.gameObject.name);
                if ((bool)mb)
                {
                    Material mat = mb.GetComponent<Renderer>().material;
                    mat.SetFloat("_Style", style);
                    Color[] styles = mat.GetColorArray("_StyleColors");
                    Color[] styletints = mat.GetColorArray("_StyleTints");
                    if (styles != null)
                    {
                        //harmonyLog.LogWarning("styles not null");
                        mat.SetColor("_Color", styles[Math.Min(styles.Length - 1, style)]);
                    }
                    if (styletints != null)
                    {
                        //harmonyLog.LogWarning("styletints not null");
                        mat.SetColor("_TintColor", styletints[Math.Min(styletints.Length - 1, style)]);
                    }
                    Color[] styles2 = mat.GetColorArray("_StyleGradient0");
                    Color[] styles3 = mat.GetColorArray("_StyleGradient1");
                    if (styles2 != null || styles3 != null)
                    {
                        //harmonyLog.LogWarning("styles gradients not null");
                        ParticleSystem part = mb.GetComponent<ParticleSystem>();
                        if ((bool)part)
                        {
                            //harmonyLog.LogWarning("part is not null");
                            ParticleSystem.ColorOverLifetimeModule cols = part.colorOverLifetime;
                            GradientColorKey[] gradkeys = (GradientColorKey[])cols.color.gradient.colorKeys.Clone();
                            if (styles2 != null) { gradkeys[0].color = styles2[style]; }
                            if (styles3 != null) { gradkeys[1].color = styles3[style]; }
                            Gradient grad = cols.color.gradient;
                            grad.colorKeys = gradkeys;
                            cols.color = new ParticleSystem.MinMaxGradient(grad);
                            //DBG.blogDebug("color=" + cols.color.gradient.colorKeys[0].color.ToString());
                            //DBG.blogDebug("color3=" + grad.colorKeys[0].color.ToString());
                        }
                    }
                }
            }
        }

        public Harmony harmony;
        public static GameObject Root;
        public static CraftingConditions craftcond;
        public string version = "1.0.0";
        public static BepInEx.Logging.ManualLogSource logger;
        public static BepInEx.Logging.ManualLogSource harmonyLog;
        private static AssetBundle Shawcassets;
        public static Sprite FreezingIcon;
        

        public void Awake()
        {
            logger = base.Logger;
            harmony = new Harmony("IDshawesome4u");
            Root = new GameObject("Shaw Root");
            craftcond = Root.AddComponent<CraftingConditions>();
            UnityEngine.Object.DontDestroyOnLoad(Root);

            harmony.PatchAll(typeof(global::shawcape.Shawesomes_Divine_Armaments));
            harmony.PatchAll(typeof(global::CraftingConditions));
            harmonyLog = Logger;
            Jotunn.Managers.ItemManager.Instance.ToString();
            harmonyLog.LogWarning("Divine Armaments of Unrivaled Awesomeness Descend from the Heavens... Will you be Worthy?");

            Shawcassets = AssetUtils.LoadAssetBundleFromResources("Shawcassets", Assembly.GetExecutingAssembly());
            FreezingIcon = Shawcassets.LoadAsset<Sprite>("Assets/ShawesomeCapeimage/FreezingCondishIcon.asset");
            PrefabManager.OnVanillaPrefabsAvailable += CraftingConditions.setConditionItems;
            PrefabManager.OnVanillaPrefabsAvailable += additems;
            


            var myOriginalMethods = harmony.GetPatchedMethods();
            foreach (var method in myOriginalMethods)
            {
                //harmonyLog.LogWarning(method.ReflectedType + ":" + method.Name + " is patched");
            }
        }

        //Calling in all Items
        public static void additems()
        {
            Capeofshaw();
            //addMeldCape2();
            AddMeldursonCape();
            addjmcgoddess();
            AddNecroTechCape();
            AddPyroCape();
            AddCryoCape();
            AddItsover9000();
            AddKIBLAST();
            Addperveysagecloud();
            Add1sdb();
            Add2sdb();
            Add3sdb();
            Add4sdb();
            Add5sdb();
            Add6sdb();
            Add7sdb();
            AddBBOS();
            AddMBOS();
            AddSENZU();
            Addbbksb();
            AddSpacePod();
            Addsbshield();
            AddIT2Duel();
            AddHeartotc();
            AddGungnir();
            AddMarsBC();
            AddNTGUN();
            AddNTServo();
            AddNTBow();
            AddNTSword();
            AddNTSTAFF();
            Addssj5fusion();
            AddMPSoul();
            AddNTGUNminion1();
            AddNTGUNminion2();
            AddNTGUNminion3();
            AddNTGUNminion4();
            Addshawesomeboat();
            Addgaara();
            AddGaaraCape();
            AddNaruto();







            PrefabManager.OnVanillaPrefabsAvailable -= additems;
        }









        //clonenaruto start
        public static void AddNaruto()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/KageBunshinnoJutsu.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //clonenaruto end

        //gaaracape Stuff Start

        public static void AddGaaraCape()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));
            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            CustomItem SSJCAPE = new CustomItem("SanDemonShukaku", "Demister", capeConfig);
            ItemManager.Instance.AddItem(SSJCAPE);

            //calling in the actual effect
            //StatusEffect infinityEffect = InfinityEffect().StatusEffect;

            //Assigning item ID
            var id = SSJCAPE.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Seal of Shukaku";
            id2.m_shared.m_description = "Legends say it was the Magics of the Sage Sir Parry Alot who sealed the Enraged spirit away, for its own saftey and peace...";
            id2.m_shared.m_armor = 40;
            id2.m_shared.m_armorPerLevel = 8;
            id2.m_shared.m_maxDurability = 9001;
            id2.m_shared.m_maxQuality = 4;
            id2.m_shared.m_eitrRegenModifier = 1;
        
            //changing whatever item you instatiated's item type [which square is it using]
            id2.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Shoulder;

            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.VeryResistant;
            poisonres.m_type = HitData.DamageType.Blunt;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair nofire = new HitData.DamageModPair();
            nofire.m_modifier = HitData.DamageModifier.VeryResistant;
            nofire.m_type = HitData.DamageType.Fire;
            id2.m_shared.m_damageModifiers.Add(nofire);

            HitData.DamageModPair nopoke = new HitData.DamageModPair();
            nopoke.m_modifier = HitData.DamageModifier.Immune;
            nopoke.m_type = HitData.DamageType.Pierce;
            id2.m_shared.m_damageModifiers.Add(nopoke);


            HitData.DamageModPair noslash = new HitData.DamageModPair();
            noslash.m_modifier = HitData.DamageModifier.Immune;
            noslash.m_type = HitData.DamageType.Slash;
            id2.m_shared.m_damageModifiers.Add(noslash);

            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] JMCicon = Shawcassets.LoadAssetWithSubAssets("Assets/Shawesome/Shukakuicon.png");
            id2.m_shared.m_icons[0] = JMCicon[1] as Sprite;
            //putting the below line under where we bring in the cape's icon to callin the effect function below
            id2.m_shared.m_equipStatusEffect = SandEffect(JMCicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = new GameObject().transform;
            NTcape1.name = "attach_skin";
            NTcape1.parent = PrefabManager.Instance.GetPrefab("SanDemonShukaku").transform;
            //Transform NTcape1 = PrefabManager.Instance.GetPrefab("Itsover9000")
            //Transform NTcape2 = NTcape1.Find("LoxCape").transform;

            //adding hood to cape
            /*Transform chest = PrefabManager.Instance.GetPrefab("ArmorLeatherChest").transform.Find("attach_skin").transform;
            Transform chest1 = chest.Find("shorts").transform;
            Transform chestcopy = CopyIntoParent(chest1, NTcape1);*/

            //adding body overlay to cape
            //Transform Body = PrefabManager.Instance.GetPrefab("body").transform;
            //Transform bodycopy = CopyIntoParent(Body, NTcape1);

            //adding robebottoms to cape
            
            //modify robebottoms copy material and look


            //assinging names to each piece in order to change their assigned -textures, Mesh, Metal, Emission, BM and stuff
            //assigning hood
            //SkinnedMeshRenderer NThoodskinmesh = chestcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning cape
            //SkinnedMeshRenderer NTcapeskinmesh = NTcape2.GetComponent<SkinnedMeshRenderer>();
            //assigning robe bottoms
            

            //assigning body skinned mesh renderer
            //SkinnedMeshRenderer Bodyskinmesh = bodycopy.GetComponent<SkinnedMeshRenderer>();

            //Material NTCmat = NTcapeskinmesh.material;
            //Material Bodymat = Bodyskinmesh.material;
            //Material NTHmat = NThoodskinmesh.material;
            
            //NTHmat.color = Color.white;
            //NTCmat.color = Color.white;
           
            UnityEngine.Object[] robetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmrobe_e.png");
            UnityEngine.Object[] robeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeDBZ/IronArmorLegs_d_GOGETA.png");
            UnityEngine.Object[] robemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_m.png");
            //UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            UnityEngine.Object[] hoodtexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_d.png");
            //UnityEngine.Object[] hoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] gokuchest = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeDBZ/LeatherArmourChest_d_GOGETA.png");
            UnityEngine.Object[] hoodbumpmap = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_bm.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_D.png");
            UnityEngine.Object[] capeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] capemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_M.png");
            //using woodvalk for dhakhars build temporary only for texture- 
            //UnityEngine.Object[] WOODVALK = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/woodvalk_d.png");

            /*//Adding textures and emissions to the cape
            NTCmat.SetTexture("_MainTex", capeemish[0] as Texture2D);
            NTCmat.EnableKeyword("_EMISSION");
            NTCmat.SetTexture("_EmissionMap", capeemish[0] as Texture2D);
            NTCmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTCmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_MetallicGlossMap", capeemish[0] as Texture2D);
            */
            //Bodyskinmesh.material = gokuchestmat;
            //Bodymat = gokuchestmat;

            // Adding textures and emissions & bm & metal to the hood- 

            /*NTHmat.SetTexture("_ChestTex", gokuchest[0] as Texture2D);
            NTHmat.EnableKeyword("_EMISSION");
            NTHmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_EmissionMap", gokuchest[0] as Texture2D);
            NTHmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
            NTHmat.SetTexture("_MetallicGlossMap", gokuchest[0] as Texture2D);*/




            // Adding textures and emissions & bm & metal to the robe bottoms
          

            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject jmCape = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/SanDemonShukaku.prefab");
            //jmCape.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //CopyIntoParent(jmCape.transform, NTcape1);




            //calling the "different bone attaches" from unity into the mod

            //CopyIntoParent(jmCape.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            CopyIntoParent(jmCape.transform.Find("attach_Spine2"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(jmCape.transform.Find("attach_skin"), NTcape1);


        }

        // Add new status effects
        private static SE_Stats SandEffect(Sprite sprite)
        {
            //cant use this effect with feath cape because it gets rid of the feather fall effect :[ so we commented out the call-in
            SE_Stats infinityEffect = new SE_Stats();
            infinityEffect.name = "SandyEffect";
            infinityEffect.m_name = "Jinchūriki";
            infinityEffect.m_category = "Tailed Beast";
            infinityEffect.m_tooltip = "You are able to control the sand under your feet taking no fall damage...";
            infinityEffect.m_startMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_startMessage = "LET ME FEEL ALIVE!!";
            infinityEffect.m_stopMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_stopMessage = "Shukaku slumbers...";
            infinityEffect.m_icon = sprite;
            //infinityEffect.m_maxMaxFallSpeed = .01f;
            infinityEffect.m_fallDamageModifier = -1;
            infinityEffect.m_jumpModifier = new Vector3(0, 0.30f, 0);

            //CustomStatusEffect infinityCapeEffect = new CustomStatusEffect(infinityEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(infinityCapeEffect);
            return infinityEffect;

        }
        //gaaracape Stuff End

        

        //Gaara start
        public static void Addgaara()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/ilyseirrafaawadanh.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);


        }
        //Gaara end

        //boat start
        public static void Addshawesomeboat()
        {
            PieceConfig boatConfig = new PieceConfig();
            boatConfig.PieceTable = PieceTables.Hammer;
            boatConfig.CraftingStation = CraftingStations.Workbench;
            boatConfig.Category = PieceCategories.Misc;

            boatConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            boatConfig.AddRequirement(new RequirementConfig("spearodingungnir", 1));
            boatConfig.AddRequirement(new RequirementConfig("PharoahSoulAtem", 1));
            boatConfig.AddRequirement(new RequirementConfig("NTMarsBC", 1));
            boatConfig.AddRequirement(new RequirementConfig("Spheredsss", 1));
            boatConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            boatConfig.AddRequirement(new RequirementConfig("SSJ5Fusion", 1));

            GameObject SBoat = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeGOW/Skidbladnir.prefab");
            CustomPiece batwing = new CustomPiece(SBoat, true, boatConfig);
            PieceManager.Instance.AddPiece(batwing);

        }
        //boat end

        //ntrifle(add) start
        public static void AddNTGUNminion1()
        {
            ItemConfig capeConfig = new ItemConfig();
            /*capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));*/



            

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/skeleton_bow3.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //ntrifle(add) end

        //ntrifle(add) start
        public static void AddNTGUNminion2()
        {
            ItemConfig capeConfig = new ItemConfig();
            /*capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));*/



            

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/skeleton_bow4.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //ntrifle(add) end

        //ntrifle(add) start
        public static void AddNTGUNminion3()
        {
            ItemConfig capeConfig = new ItemConfig();
            /*capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));*/



            

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/skeleton_sword3.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //ntrifle(add) end

        //ntrifle(add) start
        public static void AddNTGUNminion4()
        {
            ItemConfig capeConfig = new ItemConfig();
            /*capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));*/



            

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/skeleton_sword4.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //ntrifle(add) end



        //MPYGO Stuff Start

        public static void AddMPSoul()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("bbkmhmhsb", 1));
            capeConfig.AddRequirement(new RequirementConfig("DatGoodSHT", 1));
            capeConfig.AddRequirement(new RequirementConfig("Kiblastkam", 1));
            capeConfig.AddRequirement(new RequirementConfig("Spheredsss", 1));
            capeConfig.AddRequirement(new RequirementConfig("TurtleHermitQloud", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("Itsover9000", 1));
            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            CustomItem SSJCAPE = new CustomItem("PharoahSoulAtem", "Demister", capeConfig);
            ItemManager.Instance.AddItem(SSJCAPE);

            //calling in the actual effect
            //StatusEffect infinityEffect = InfinityEffect().StatusEffect;

            //Assigning item ID
            var id = SSJCAPE.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Millennium Puzzle";
            id2.m_shared.m_description = "Retrieved from the Tomb of the Nameless Pharaoh, This Relic holds the Power of the Shadow Realms and the Light of Hope...";
            id2.m_shared.m_armor = 40;
            id2.m_shared.m_armorPerLevel = 10;
            id2.m_shared.m_maxDurability = 9001;
            id2.m_shared.m_maxQuality = 4;
            id2.m_shared.m_eitrRegenModifier = 2;
            id2.m_shared.m_attackForce = 50;



            id2.m_shared.m_movementModifier = 0.1f;
            id2.m_shared.m_equipDuration = 0.01f;
            
            //changing whatever item you instatiated's item type [which square is it using]
            id2.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Utility;






            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Immune;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] JMCicon = Shawcassets.LoadAssetWithSubAssets("Assets/Shawesome/ygospiriticon.png");
            id2.m_shared.m_icons[0] = JMCicon[1] as Sprite;
            //putting the below line under where we bring in the cape's icon to callin the effect function below
            id2.m_shared.m_equipStatusEffect = plotarmorEffect(JMCicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = new GameObject().transform;
            NTcape1.name = "attach_skin";
            NTcape1.parent = PrefabManager.Instance.GetPrefab("PharoahSoulAtem").transform;
            //Transform NTcape1 = PrefabManager.Instance.GetPrefab("Itsover9000")
            //Transform NTcape2 = NTcape1.Find("LoxCape").transform;

            //adding hood to cape
            /*Transform chest = PrefabManager.Instance.GetPrefab("ArmorLeatherChest").transform.Find("attach_skin").transform;
            Transform chest1 = chest.Find("shorts").transform;
            Transform chestcopy = CopyIntoParent(chest1, NTcape1);*/

            //adding body overlay to cape
            //Transform Body = PrefabManager.Instance.GetPrefab("body").transform;
            //Transform bodycopy = CopyIntoParent(Body, NTcape1);

            //adding robebottoms to cape
            //Transform legs = PrefabManager.Instance.GetPrefab("ArmorIronLegs").transform.Find("attach_skin").transform;
            //Transform legs1 = legs.Find("SilverWolfArmor_Legs.001").transform;
            //Transform Legscopy = CopyIntoParent(legs1, NTcape1);
            //modify robebottoms copy material and look


            //assinging names to each piece in order to change their assigned -textures, Mesh, Metal, Emission, BM and stuff
            //assigning hood
            //SkinnedMeshRenderer NThoodskinmesh = chestcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning cape
            //SkinnedMeshRenderer NTcapeskinmesh = NTcape2.GetComponent<SkinnedMeshRenderer>();
            //assigning robe bottoms
            //SkinnedMeshRenderer NTrobeskinmesh = Legscopy.GetComponent<SkinnedMeshRenderer>();

            //assigning body skinned mesh renderer
            //SkinnedMeshRenderer Bodyskinmesh = bodycopy.GetComponent<SkinnedMeshRenderer>();

            //Material NTCmat = NTcapeskinmesh.material;
            //Material Bodymat = Bodyskinmesh.material;
            //Material NTHmat = NThoodskinmesh.material;
            //Material NTRmat = NTrobeskinmesh.material;
            //NTHmat.color = Color.white;
            //NTCmat.color = Color.white;
            //NTRmat.color = Color.white;
            UnityEngine.Object[] robetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmrobe_e.png");
            UnityEngine.Object[] robeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeDBZ/IronArmorLegs_d_GOGETA.png");
            UnityEngine.Object[] robemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_m.png");
            //UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            UnityEngine.Object[] hoodtexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_d.png");
            //UnityEngine.Object[] hoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] gokuchest = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeDBZ/LeatherArmourChest_d_GOGETA.png");
            UnityEngine.Object[] hoodbumpmap = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_bm.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_D.png");
            UnityEngine.Object[] capeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] capemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_M.png");
            //using woodvalk for dhakhars build temporary only for texture- 
            //UnityEngine.Object[] WOODVALK = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/woodvalk_d.png");

            /*//Adding textures and emissions to the cape
            NTCmat.SetTexture("_MainTex", capeemish[0] as Texture2D);
            NTCmat.EnableKeyword("_EMISSION");
            NTCmat.SetTexture("_EmissionMap", capeemish[0] as Texture2D);
            NTCmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTCmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_MetallicGlossMap", capeemish[0] as Texture2D);
            */
            //Bodyskinmesh.material = gokuchestmat;
            //Bodymat = gokuchestmat;

            // Adding textures and emissions & bm & metal to the hood- 

            /*NTHmat.SetTexture("_ChestTex", gokuchest[0] as Texture2D);
            NTHmat.EnableKeyword("_EMISSION");
            NTHmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_EmissionMap", gokuchest[0] as Texture2D);
            NTHmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
            NTHmat.SetTexture("_MetallicGlossMap", gokuchest[0] as Texture2D);*/




            // Adding textures and emissions & bm & metal to the robe bottoms
            /*NTRmat.SetTexture("_MainTex", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_EMISSION");
            NTRmat.SetTexture("_EmissionMap", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_Metallic");
            NTRmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));//comment this line out if robe is too much shine
            NTRmat.SetTexture("_MetallicGlossMap", robeemish[0] as Texture2D);*/


            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject jmCape = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/PharoahSoulAtem.prefab");
            //jmCape.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //CopyIntoParent(jmCape.transform, NTcape1);




            //calling the "different bone attaches" from unity into the mod

            CopyIntoParent(jmCape.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            CopyIntoParent(jmCape.transform.Find("attach_Spine2"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(jmCape.transform.Find("attach_skin"), NTcape1);


        }

        // Add new status effects
        private static SE_Stats plotarmorEffect(Sprite sprite)
        {
            //cant use this effect with feath cape because it gets rid of the feather fall effect :[ so we commented out the call-in
            SE_Stats infinityEffect = new SE_Stats();
            infinityEffect.name = "YGOEffect";
            infinityEffect.m_name = "King of Games";
            infinityEffect.m_category = "King";
            infinityEffect.m_tooltip = "Only the Champion of the Duelest Kingdom may lay claim to such a Title...";
            infinityEffect.m_startMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_startMessage = "Your confidence swells from within, you can do this!";
            infinityEffect.m_stopMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_stopMessage = "...Did I get shorter?...";
            infinityEffect.m_icon = sprite;
            //infinityEffect.m_maxMaxFallSpeed = .01f;
            infinityEffect.m_fallDamageModifier = -1;
            //infinityEffect.m_staminaRegenMultiplier = 1100;
            //infinityEffect.m_jumpModifier = new Vector3(0, 2, 0);
            //infinityEffect.m_skillLevel = Skills.SkillType.All;
            //infinityEffect.m_skillLevelModifier = 100;
            //infinityEffect.m_healthRegenMultiplier = 111;
            //infinityEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;
            //infinityEffect.m_attributes = StatusEffect.StatusAttribute.DoubleImpactDamage;
            //infinityEffect.m_runStaminaDrainModifier = -100;

            //CustomStatusEffect infinityCapeEffect = new CustomStatusEffect(infinityEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(infinityCapeEffect);
            return infinityEffect;

        }
        //MPygo Stuff End




        //SSJ5 Stuff Start

        public static void Addssj5fusion()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("bbkmhmhsb", 1));
            capeConfig.AddRequirement(new RequirementConfig("DatGoodSHT", 1));
            capeConfig.AddRequirement(new RequirementConfig("Kiblastkam", 1));
            capeConfig.AddRequirement(new RequirementConfig("Spheredsss", 1));
            capeConfig.AddRequirement(new RequirementConfig("TurtleHermitQloud", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("Itsover9000", 1));
            capeConfig.CraftingStation = CraftingStations.BlackForge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            CustomItem SSJCAPE = new CustomItem("SSJ5Fusion", "Demister", capeConfig);
            ItemManager.Instance.AddItem(SSJCAPE);

            //calling in the actual effect
            //StatusEffect infinityEffect = InfinityEffect().StatusEffect;

            //Assigning item ID
            var id = SSJCAPE.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "SSJ5 Ultra Instinct";
            id2.m_shared.m_description = "...You have reached it... The Zenith of the Universe...";
            id2.m_shared.m_armor = 100;
            id2.m_shared.m_armorPerLevel = 10;
            id2.m_shared.m_maxDurability = 9001;
            id2.m_shared.m_maxQuality = 4;
            id2.m_shared.m_eitrRegenModifier = 999;
            id2.m_shared.m_attackForce = 1500; 



            id2.m_shared.m_movementModifier = 4;
            id2.m_shared.m_equipDuration = 0.01f;
            Material gokuchestmat = Shawcassets.LoadAsset<Material>("Assets/ShawesomeCapeimage/GokuChestMat.mat");
            id2.m_shared.m_armorMaterial = gokuchestmat;
            //changing whatever item you instatiated's item type [which square is it using]
            id2.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Utility;






            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Immune;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair nofire = new HitData.DamageModPair();
            nofire.m_modifier = HitData.DamageModifier.Immune;
            nofire.m_type = HitData.DamageType.Fire;
            id2.m_shared.m_damageModifiers.Add(nofire);

            HitData.DamageModPair nopoke = new HitData.DamageModPair();
            nopoke.m_modifier = HitData.DamageModifier.Immune;
            nopoke.m_type = HitData.DamageType.Pierce;
            id2.m_shared.m_damageModifiers.Add(nopoke);

            HitData.DamageModPair zapres = new HitData.DamageModPair();
            zapres.m_modifier = HitData.DamageModifier.Immune;
            zapres.m_type = HitData.DamageType.Lightning;
            id2.m_shared.m_damageModifiers.Add(zapres);

            HitData.DamageModPair noslash = new HitData.DamageModPair();
            noslash.m_modifier = HitData.DamageModifier.Immune;
            noslash.m_type = HitData.DamageType.Slash;
            id2.m_shared.m_damageModifiers.Add(noslash);

            HitData.DamageModPair noblunt = new HitData.DamageModPair();
            noblunt.m_modifier = HitData.DamageModifier.Immune;
            noblunt.m_type = HitData.DamageType.Blunt;
            id2.m_shared.m_damageModifiers.Add(noblunt);


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] JMCicon = Shawcassets.LoadAssetWithSubAssets("Assets/Shawesome/ssj5icon1.png");
            id2.m_shared.m_icons[0] = JMCicon[1] as Sprite;
            //putting the below line under where we bring in the cape's icon to callin the effect function below
            id2.m_shared.m_equipStatusEffect = UIEffect(JMCicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = new GameObject().transform;
            NTcape1.name = "attach_skin";
            NTcape1.parent = PrefabManager.Instance.GetPrefab("SSJ5Fusion").transform;
            //Transform NTcape1 = PrefabManager.Instance.GetPrefab("Itsover9000")
            //Transform NTcape2 = NTcape1.Find("LoxCape").transform;

            //adding hood to cape
            /*Transform chest = PrefabManager.Instance.GetPrefab("ArmorLeatherChest").transform.Find("attach_skin").transform;
            Transform chest1 = chest.Find("shorts").transform;
            Transform chestcopy = CopyIntoParent(chest1, NTcape1);*/

            //adding body overlay to cape
            //Transform Body = PrefabManager.Instance.GetPrefab("body").transform;
            //Transform bodycopy = CopyIntoParent(Body, NTcape1);

            //adding robebottoms to cape
            Transform legs = PrefabManager.Instance.GetPrefab("ArmorIronLegs").transform.Find("attach_skin").transform;
            Transform legs1 = legs.Find("SilverWolfArmor_Legs.001").transform;
            Transform Legscopy = CopyIntoParent(legs1, NTcape1);
            //modify robebottoms copy material and look


            //assinging names to each piece in order to change their assigned -textures, Mesh, Metal, Emission, BM and stuff
            //assigning hood
            //SkinnedMeshRenderer NThoodskinmesh = chestcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning cape
            //SkinnedMeshRenderer NTcapeskinmesh = NTcape2.GetComponent<SkinnedMeshRenderer>();
            //assigning robe bottoms
            SkinnedMeshRenderer NTrobeskinmesh = Legscopy.GetComponent<SkinnedMeshRenderer>();

            //assigning body skinned mesh renderer
            //SkinnedMeshRenderer Bodyskinmesh = bodycopy.GetComponent<SkinnedMeshRenderer>();

            //Material NTCmat = NTcapeskinmesh.material;
            //Material Bodymat = Bodyskinmesh.material;
            //Material NTHmat = NThoodskinmesh.material;
            Material NTRmat = NTrobeskinmesh.material;
            //NTHmat.color = Color.white;
            //NTCmat.color = Color.white;
            NTRmat.color = Color.white;
            UnityEngine.Object[] robetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmrobe_e.png");
            UnityEngine.Object[] robeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeDBZ/IronArmorLegs_d_GOGETA.png");
            UnityEngine.Object[] robemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_m.png");
            //UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            UnityEngine.Object[] hoodtexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_d.png");
            //UnityEngine.Object[] hoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] gokuchest = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeDBZ/LeatherArmourChest_d_GOGETA.png");
            UnityEngine.Object[] hoodbumpmap = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_bm.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_D.png");
            UnityEngine.Object[] capeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] capemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_M.png");
            //using woodvalk for dhakhars build temporary only for texture- 
            //UnityEngine.Object[] WOODVALK = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/woodvalk_d.png");

            /*//Adding textures and emissions to the cape
            NTCmat.SetTexture("_MainTex", capeemish[0] as Texture2D);
            NTCmat.EnableKeyword("_EMISSION");
            NTCmat.SetTexture("_EmissionMap", capeemish[0] as Texture2D);
            NTCmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTCmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_MetallicGlossMap", capeemish[0] as Texture2D);
            */
            //Bodyskinmesh.material = gokuchestmat;
            //Bodymat = gokuchestmat;

            // Adding textures and emissions & bm & metal to the hood- 

            /*NTHmat.SetTexture("_ChestTex", gokuchest[0] as Texture2D);
            NTHmat.EnableKeyword("_EMISSION");
            NTHmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_EmissionMap", gokuchest[0] as Texture2D);
            NTHmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
            NTHmat.SetTexture("_MetallicGlossMap", gokuchest[0] as Texture2D);*/

            


            // Adding textures and emissions & bm & metal to the robe bottoms
            NTRmat.SetTexture("_MainTex", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_EMISSION");
            NTRmat.SetTexture("_EmissionMap", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_Metallic");
            NTRmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));//comment this line out if robe is too much shine
            NTRmat.SetTexture("_MetallicGlossMap", robeemish[0] as Texture2D);


            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject jmCape = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeDBZ/SSJ5Fusion.prefab");
            //jmCape.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //CopyIntoParent(jmCape.transform, NTcape1);




            //calling the "different bone attaches" from unity into the mod

            CopyIntoParent(jmCape.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            CopyIntoParent(jmCape.transform.Find("attach_Spine2"), NTcape1.parent);
            CopyIntoParent(jmCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(jmCape.transform.Find("attach_skin"), NTcape1);


        }

        // Add new status effects
        private static SE_Stats UIEffect(Sprite sprite)
        {
            //cant use this effect with feath cape because it gets rid of the feather fall effect :[ so we commented out the call-in
            SE_Stats infinityEffect = new SE_Stats();
            infinityEffect.name = "SS5Effect";
            infinityEffect.m_name = "Ultra Instinct";
            infinityEffect.m_category = "GOD";
            infinityEffect.m_tooltip = "Go further than you have ever gone before...";
            infinityEffect.m_startMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_startMessage = "HHHHHHHHHHAAAAAAAAAHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH!!!!!!";
            infinityEffect.m_stopMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_stopMessage = "Can't Maintain that state for too long...";
            infinityEffect.m_icon = sprite;
            //infinityEffect.m_maxMaxFallSpeed = .01f;
            infinityEffect.m_fallDamageModifier = -1;
            infinityEffect.m_staminaRegenMultiplier = 1100;
            infinityEffect.m_jumpModifier = new Vector3(0, 2, 0);
            infinityEffect.m_skillLevel = Skills.SkillType.All;
            infinityEffect.m_skillLevelModifier = 100;
            infinityEffect.m_healthRegenMultiplier = 111;
            //infinityEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;
            infinityEffect.m_attributes = StatusEffect.StatusAttribute.DoubleImpactDamage;
            infinityEffect.m_runStaminaDrainModifier = -100;

            //CustomStatusEffect infinityCapeEffect = new CustomStatusEffect(infinityEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(infinityCapeEffect);
            return infinityEffect;

        }
        //SSJ5 Stuff End

        //ntrifle() start
        public static void AddNTGUN()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/ntrifle.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //ntrifle() end

        //AddHeartotc() start
        public static void AddHeartotc()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/GodlyHandygo.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //AddHeartotc() end

        //AddNTstaff start
        public static void AddNTSTAFF()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/NTSTAFF.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //AddNTstaff end

        //AddNTServo() start
        public static void AddNTServo()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/NTServoDemister.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //AddNTServo() end

        //AddNTSword() start
        public static void AddNTSword()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/NecroTechSword.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //AddNTSword() end

        //NTBow start
        public static void AddNTBow()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/NecroTechBow.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //NTBow end

        //Mars Stuff Start

        public static void AddMarsBC()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));
            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape

            CustomItem SSJCAPE = new CustomItem("NTMarsBC", "Demister", capeConfig);

            ItemManager.Instance.AddItem(SSJCAPE);

            //calling in the actual effect
            //StatusEffect infinityEffect = InfinityEffect().StatusEffect;

            //Assigning item ID
            var id = SSJCAPE.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Battle Barge";
            id2.m_shared.m_description = "...Big Ship Go Vroom...";
            id2.m_shared.m_armor = 10;
            id2.m_shared.m_armorPerLevel = 10;
            id2.m_shared.m_maxDurability = 9000;
            id2.m_shared.m_maxQuality = 4;
            id2.m_shared.m_eitrRegenModifier = 11;
            id2.m_shared.m_helmetHideHair = ItemDrop.ItemData.HelmetHairType.Hidden;


            id2.m_shared.m_movementModifier = 1;
            id2.m_shared.m_equipDuration = 0.01f;
            //Material gokuchestmat = Shawcassets.LoadAsset<Material>("Assets/ShawesomeCapeimage/GokuChestMat.mat");
            //id2.m_shared.m_armorMaterial = gokuchestmat;
            //changing whatever item you instatiated's item type [which square is it using]
            id2.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Shoulder;






            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Immune;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] JMCicon = Shawcassets.LoadAssetWithSubAssets("Assets/Shawesome/battlebargeicon.png");
            id2.m_shared.m_icons[0] = JMCicon[1] as Sprite;
            //putting the below line under where we bring in the cape's icon to callin the effect function below
            id2.m_shared.m_equipStatusEffect = MarsEffect(JMCicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = new GameObject().transform;
            NTcape1.name = "attach_skin";
            NTcape1.parent = PrefabManager.Instance.GetPrefab("NTMarsBC").transform;
            //Transform NTcape1 = PrefabManager.Instance.GetPrefab("Itsover9000")
            //Transform NTcape2 = NTcape1.Find("LoxCape").transform;

            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject jmCape = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/NecroTechMARSBC.prefab");




            //calling the "different bone attaches" from unity into the mod

            //CopyIntoParent(jmCape.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_RightShoulder"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(jmCape.transform.Find("attach_skin"), NTcape1);


        }

        // Add new status effects
        private static SE_Stats MarsEffect(Sprite sprite)
        {
            //cant use this effect with feath cape because it gets rid of the feather fall effect :[ so we commented out the call-in
            SE_Stats infinityEffect = new SE_Stats();
            infinityEffect.name = "MarsEffect";
            infinityEffect.m_name = "SPACE";
            infinityEffect.m_startMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_startMessage = "Engines Start";
            infinityEffect.m_stopMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_stopMessage = "Engines Stop";
            infinityEffect.m_icon = sprite;
            infinityEffect.m_maxMaxFallSpeed = .01f;
            infinityEffect.m_fallDamageModifier = -1;
            infinityEffect.m_staminaRegenMultiplier = 110;
            infinityEffect.m_jumpModifier = new Vector3(0, 7, 0);
            //infinityEffect.m_skillLevel = Skills.SkillType.All;
            //infinityEffect.m_skillLevelModifier = 40;
            //infinityEffect.m_healthRegenMultiplier = 11;
            infinityEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;
            //infinityEffect.m_attributes = StatusEffect.StatusAttribute.DoubleImpactDamage;
            infinityEffect.m_runStaminaDrainModifier = -100;

            //CustomStatusEffect infinityCapeEffect = new CustomStatusEffect(infinityEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(infinityCapeEffect);
            return infinityEffect;

        }
        //Mars Stuff End

        //Gungnir start
        public static void AddGungnir()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("FireSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("frostSHWSMBlade", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/spearodingungnir.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //Gungnir end

        //IT2Duel start
        public static void AddIT2Duel()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/IT2Duel.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //IT2Duel end

        //sunblock start
        public static void Addsbshield()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/SunBlock4000.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //sunblock end

        //saiyan pod Stuff Start

        public static void AddSpacePod()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));
            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape

            CustomItem SSJCAPE = new CustomItem("Spheredsss", "Demister", capeConfig);

            ItemManager.Instance.AddItem(SSJCAPE);

            //calling in the actual effect
            //StatusEffect infinityEffect = InfinityEffect().StatusEffect;

            //Assigning item ID
            var id = SSJCAPE.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Saiyan Pod";
            id2.m_shared.m_description = "...Great on Gas...";
            id2.m_shared.m_armor = 10;
            id2.m_shared.m_armorPerLevel = 10;
            id2.m_shared.m_maxDurability = 9001;
            id2.m_shared.m_maxQuality = 4;
            id2.m_shared.m_eitrRegenModifier = 11;
            id2.m_shared.m_helmetHideHair = ItemDrop.ItemData.HelmetHairType.Hidden;
            



            id2.m_shared.m_movementModifier = 1;
            id2.m_shared.m_equipDuration = 0.01f;
            //Material gokuchestmat = Shawcassets.LoadAsset<Material>("Assets/ShawesomeCapeimage/GokuChestMat.mat");
            //id2.m_shared.m_armorMaterial = gokuchestmat;
            //changing whatever item you instatiated's item type [which square is it using]
            id2.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Shoulder;






            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Immune;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair nofire = new HitData.DamageModPair();
            nofire.m_modifier = HitData.DamageModifier.Immune;
            nofire.m_type = HitData.DamageType.Fire;
            id2.m_shared.m_damageModifiers.Add(nofire);

            HitData.DamageModPair nopoke = new HitData.DamageModPair();
            nopoke.m_modifier = HitData.DamageModifier.Immune;
            nopoke.m_type = HitData.DamageType.Pierce;
            id2.m_shared.m_damageModifiers.Add(nopoke);

            HitData.DamageModPair zapres = new HitData.DamageModPair();
            zapres.m_modifier = HitData.DamageModifier.Immune;
            zapres.m_type = HitData.DamageType.Lightning;
            id2.m_shared.m_damageModifiers.Add(zapres);

            HitData.DamageModPair noslash = new HitData.DamageModPair();
            noslash.m_modifier = HitData.DamageModifier.Immune;
            noslash.m_type = HitData.DamageType.Slash;
            id2.m_shared.m_damageModifiers.Add(noslash);


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] JMCicon = Shawcassets.LoadAssetWithSubAssets("Assets/Shawesome/SPodIcon1.png");
            id2.m_shared.m_icons[0] = JMCicon[1] as Sprite;
            //putting the below line under where we bring in the cape's icon to callin the effect function below
            id2.m_shared.m_equipStatusEffect = SpacePodEffect(JMCicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = new GameObject().transform;
            NTcape1.name = "attach_skin";
            NTcape1.parent = PrefabManager.Instance.GetPrefab("Spheredsss").transform;
            //Transform NTcape1 = PrefabManager.Instance.GetPrefab("Itsover9000")
            //Transform NTcape2 = NTcape1.Find("LoxCape").transform;

            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject jmCape = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome1/Spheredsss.prefab");




            //calling the "different bone attaches" from unity into the mod

            //CopyIntoParent(jmCape.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_RightShoulder"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(jmCape.transform.Find("attach_skin"), NTcape1);


        }

        // Add new status effects
        private static SE_Stats SpacePodEffect(Sprite sprite)
        {
            //cant use this effect with feath cape because it gets rid of the feather fall effect :[ so we commented out the call-in
            SE_Stats infinityEffect = new SE_Stats();
            infinityEffect.name = "SpacePod";
            infinityEffect.m_name = "UFO";
            infinityEffect.m_startMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_startMessage = "...Are we there yet?!";
            infinityEffect.m_stopMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_stopMessage = "Finally!";
            infinityEffect.m_icon = sprite;
            infinityEffect.m_maxMaxFallSpeed = .01f;
            infinityEffect.m_fallDamageModifier = -1;
            infinityEffect.m_staminaRegenMultiplier = 110;
            infinityEffect.m_jumpModifier = new Vector3(0, 7, 0);
            //infinityEffect.m_skillLevel = Skills.SkillType.All;
            //infinityEffect.m_skillLevelModifier = 40;
            //infinityEffect.m_healthRegenMultiplier = 11;
            infinityEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;
            //infinityEffect.m_attributes = StatusEffect.StatusAttribute.DoubleImpactDamage;
            infinityEffect.m_runStaminaDrainModifier = -100;
            

            //CustomStatusEffect infinityCapeEffect = new CustomStatusEffect(infinityEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(infinityCapeEffect);
            return infinityEffect;

        }
        //spod Stuff End

        //bbksb start
        public static void Addbbksb()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome/bbkmhmhsb.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //bbksb end

        //SENZU start
        public static void AddSENZU()
        {
            ItemConfig capeConfig = new ItemConfig();
            
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Dandelion", 700));
            




            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome/DatGoodSHT.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //SENZU end

        //BBOS start    
        public static void AddBBOS()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Eyescream", 10, 4));
            capeConfig.AddRequirement(new RequirementConfig(CraftingConditions.freezing_prefNname[0], 1, 1));




            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome/FrostSHWSMBlade.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //BBOS end

        //MBOS start
        public static void AddMBOS()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Flametal", 10, 4));
            capeConfig.AddRequirement(new RequirementConfig(CraftingConditions.onfire_prefNname[0], 5, 2));




            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/Shawesome/FireSHWSMBlade.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //MBOS end

        //7sdb start
        public static void Add7sdb()
        {
            ItemConfig itmConfig = new ItemConfig();


            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/EtaSun.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, itmConfig);
            ItemManager.Instance.AddItem(calmgloves);

            //creating a new "drop list" that will come from specified creature
            CharacterDrop.Drop DBdrop = new CharacterDrop.Drop();
            DBdrop.m_chance = .1f;
            DBdrop.m_dontScale = true;
            DBdrop.m_levelMultiplier = false;
            DBdrop.m_prefab = PrefabManager.Instance.GetPrefab(calmgloves.ItemPrefab.name);

            //specifying which creature's character drop we want to put the new "drop list" in
            GameObject BossDrop = PrefabManager.Instance.GetPrefab("Serpent");
            BossDrop.GetComponent<CharacterDrop>().m_drops.Add(DBdrop);


        }
        //7sdb end

        //6sdb start
        public static void Add6sdb()
        {
            ItemConfig itmConfig = new ItemConfig();
            

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/ZetaSun.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, itmConfig);
            ItemManager.Instance.AddItem(calmgloves);

            //creating a new "drop list" that will come from specified creature
            CharacterDrop.Drop DBdrop = new CharacterDrop.Drop();
            DBdrop.m_chance = .1f;
            DBdrop.m_dontScale = true;
            DBdrop.m_levelMultiplier = false;
            DBdrop.m_prefab = PrefabManager.Instance.GetPrefab(calmgloves.ItemPrefab.name);

            //specifying which creature's character drop we want to put the new "drop list" in
            GameObject BossDrop = PrefabManager.Instance.GetPrefab("SeekerQueen");
            BossDrop.GetComponent<CharacterDrop>().m_drops.Add(DBdrop);


        }
        //6sdb end

        //5sdb start
        public static void Add5sdb()
        {
            ItemConfig itmConfig = new ItemConfig();
            

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/EpsilonSun.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, itmConfig);
            ItemManager.Instance.AddItem(calmgloves);

            //creating a new "drop list" that will come from specified creature
            CharacterDrop.Drop DBdrop = new CharacterDrop.Drop();
            DBdrop.m_chance = .1f;
            DBdrop.m_dontScale = true;
            DBdrop.m_levelMultiplier = false;
            DBdrop.m_prefab = PrefabManager.Instance.GetPrefab(calmgloves.ItemPrefab.name);

            //specifying which creature's character drop we want to put the new "drop list" in
            GameObject BossDrop = PrefabManager.Instance.GetPrefab("GoblinKing");
            BossDrop.GetComponent<CharacterDrop>().m_drops.Add(DBdrop);

        }
        //5sdb end

        //4sdb start
        public static void Add4sdb()
        {
            ItemConfig itmConfig = new ItemConfig();

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/DeltaSun.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, itmConfig);
            ItemManager.Instance.AddItem(calmgloves);

            //creating a new "drop list" that will come from specified creature
            CharacterDrop.Drop DBdrop = new CharacterDrop.Drop();
            DBdrop.m_chance = .1f;
            DBdrop.m_dontScale = true;
            DBdrop.m_levelMultiplier = false;
            DBdrop.m_prefab = PrefabManager.Instance.GetPrefab(calmgloves.ItemPrefab.name);

            //specifying which creature's character drop we want to put the new "drop list" in
            GameObject BossDrop = PrefabManager.Instance.GetPrefab("Dragon");
            BossDrop.GetComponent<CharacterDrop>().m_drops.Add(DBdrop);

        }
        //4sdb end

        //3sdb start
        public static void Add3sdb()
        {
            ItemConfig itmConfig = new ItemConfig();

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/GammaSun.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, itmConfig);
            ItemManager.Instance.AddItem(calmgloves);

            //creating a new "drop list" that will come from specified creature
            CharacterDrop.Drop DBdrop = new CharacterDrop.Drop();
            DBdrop.m_chance = .1f;
            DBdrop.m_dontScale = true;
            DBdrop.m_levelMultiplier = false;
            DBdrop.m_prefab = PrefabManager.Instance.GetPrefab(calmgloves.ItemPrefab.name);

            //specifying which creature's character drop we want to put the new "drop list" in
            GameObject BossDrop = PrefabManager.Instance.GetPrefab("Bonemass");
            BossDrop.GetComponent<CharacterDrop>().m_drops.Add(DBdrop);

        }
        //3sdb end

        //2sdb start
        public static void Add2sdb()
        {
            ItemConfig itmConfig = new ItemConfig();


            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/BetaSun.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, itmConfig);
            ItemManager.Instance.AddItem(calmgloves);

            //creating a new "drop list" that will come from specified creature
            CharacterDrop.Drop DBdrop = new CharacterDrop.Drop();
            DBdrop.m_chance = .1f;
            DBdrop.m_dontScale = true;
            DBdrop.m_levelMultiplier = false;
            DBdrop.m_prefab = PrefabManager.Instance.GetPrefab(calmgloves.ItemPrefab.name);

            //specifying which creature's character drop we want to put the new "drop list" in
            GameObject BossDrop = PrefabManager.Instance.GetPrefab("gd_king");
            BossDrop.GetComponent<CharacterDrop>().m_drops.Add(DBdrop);

        }
        //2sdb end

        //1sdb start
        public static void Add1sdb()
        {
            ItemConfig itmConfig = new ItemConfig();

            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/AlphaSun.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, itmConfig);
            ItemManager.Instance.AddItem(calmgloves);

            //creating a new "drop list" that will come from specified creature
            CharacterDrop.Drop DBdrop = new CharacterDrop.Drop();
            DBdrop.m_chance = .1f;
            DBdrop.m_dontScale = true;
            DBdrop.m_levelMultiplier = false;
            DBdrop.m_prefab = PrefabManager.Instance.GetPrefab(calmgloves.ItemPrefab.name);

            //specifying which creature's character drop we want to put the new "drop list" in
            GameObject BossDrop = PrefabManager.Instance.GetPrefab("Eikthyr");
            BossDrop.GetComponent<CharacterDrop>().m_drops.Add(DBdrop);

        }
        //1sdb end


        //kiblast start
        public static void AddKIBLAST()
        {
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));



            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            
            GameObject GlovesPrefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/Kiblastkam.prefab");
            CustomItem calmgloves = new CustomItem(GlovesPrefab, true, capeConfig);
            ItemManager.Instance.AddItem(calmgloves);
        }
        //kiblast end

        //Nimbus Stuff Start

        public static void Addperveysagecloud()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));
            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            
            CustomItem SSJCAPE = new CustomItem("TurtleHermitQloud", "Demister", capeConfig);
            
            ItemManager.Instance.AddItem(SSJCAPE);

            //calling in the actual effect
            //StatusEffect infinityEffect = InfinityEffect().StatusEffect;

            //Assigning item ID
            var id = SSJCAPE.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Nimbus";
            id2.m_shared.m_description = "...The legendary Somersault Cloud can only be mastered by those Pure of Heart...";
            id2.m_shared.m_armor = 10;
            id2.m_shared.m_armorPerLevel = 10;
            id2.m_shared.m_maxDurability = 9001;
            id2.m_shared.m_maxQuality = 4;
            id2.m_shared.m_eitrRegenModifier = 11;
            id2.m_shared.m_helmetHideHair = ItemDrop.ItemData.HelmetHairType.Hidden;


            id2.m_shared.m_movementModifier = 1;
            id2.m_shared.m_equipDuration = 0.01f;
            //Material gokuchestmat = Shawcassets.LoadAsset<Material>("Assets/ShawesomeCapeimage/GokuChestMat.mat");
            //id2.m_shared.m_armorMaterial = gokuchestmat;
            //changing whatever item you instatiated's item type [which square is it using]
            id2.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Shoulder;






            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Immune;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair nofire = new HitData.DamageModPair();
            nofire.m_modifier = HitData.DamageModifier.Immune;
            nofire.m_type = HitData.DamageType.Fire;
            id2.m_shared.m_damageModifiers.Add(nofire);

            HitData.DamageModPair nopoke = new HitData.DamageModPair();
            nopoke.m_modifier = HitData.DamageModifier.Immune;
            nopoke.m_type = HitData.DamageType.Pierce;
            id2.m_shared.m_damageModifiers.Add(nopoke);

            HitData.DamageModPair zapres = new HitData.DamageModPair();
            zapres.m_modifier = HitData.DamageModifier.Immune;
            zapres.m_type = HitData.DamageType.Lightning;
            id2.m_shared.m_damageModifiers.Add(zapres);

            HitData.DamageModPair noslash = new HitData.DamageModPair();
            noslash.m_modifier = HitData.DamageModifier.Immune;
            noslash.m_type = HitData.DamageType.Slash;
            id2.m_shared.m_damageModifiers.Add(noslash);


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] JMCicon = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/Nimbusicon.png");
            id2.m_shared.m_icons[0] = JMCicon[1] as Sprite;
            //putting the below line under where we bring in the cape's icon to callin the effect function below
            id2.m_shared.m_equipStatusEffect = CloudEffect(JMCicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = new GameObject().transform;
            NTcape1.name = "attach_skin";
            NTcape1.parent = PrefabManager.Instance.GetPrefab("TurtleHermitQloud").transform;
            //Transform NTcape1 = PrefabManager.Instance.GetPrefab("Itsover9000")
            //Transform NTcape2 = NTcape1.Find("LoxCape").transform;

            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject jmCape = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/Nimbus.prefab");
            



            //calling the "different bone attaches" from unity into the mod

            //CopyIntoParent(jmCape.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_RightShoulder"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(jmCape.transform.Find("attach_skin"), NTcape1);


        }

        // Add new status effects
        private static SE_Stats CloudEffect(Sprite sprite)
        {
            //cant use this effect with feath cape because it gets rid of the feather fall effect :[ so we commented out the call-in
            SE_Stats infinityEffect = new SE_Stats();
            infinityEffect.name = "CloudEffect";
            infinityEffect.m_name = "Kinto'un";
            infinityEffect.m_startMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_startMessage = "Nimbus Come to me!";
            infinityEffect.m_stopMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_stopMessage = "Thank you Nimbus!";
            infinityEffect.m_icon = sprite;
            infinityEffect.m_maxMaxFallSpeed = .01f;
            infinityEffect.m_fallDamageModifier = -1;
            infinityEffect.m_staminaRegenMultiplier = 110;
            infinityEffect.m_jumpModifier = new Vector3(0, 7, 0);
            //infinityEffect.m_skillLevel = Skills.SkillType.All;
            //infinityEffect.m_skillLevelModifier = 40;
            //infinityEffect.m_healthRegenMultiplier = 11;
            infinityEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;
            //infinityEffect.m_attributes = StatusEffect.StatusAttribute.DoubleImpactDamage;
            infinityEffect.m_runStaminaDrainModifier = -100;

            //CustomStatusEffect infinityCapeEffect = new CustomStatusEffect(infinityEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(infinityCapeEffect);
            return infinityEffect;

        }
        //Nimbus Stuff End



        //SSJ Stuff Start

        public static void AddItsover9000()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("AlphaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("BetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("GammaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("DeltaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("EpsilonSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("ZetaSun", 1));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1));
            capeConfig.AddRequirement(new RequirementConfig("EtaSun", 1));
            capeConfig.CraftingStation = CraftingStations.Forge; ;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            CustomItem SSJCAPE = new CustomItem("Itsover9000", "Demister", capeConfig);
            ItemManager.Instance.AddItem(SSJCAPE);

            //calling in the actual effect
            //StatusEffect infinityEffect = InfinityEffect().StatusEffect;

            //Assigning item ID
            var id = SSJCAPE.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "SSJ2";
            id2.m_shared.m_description = "...This is what is known as a super saiyan that has ascended above a super saiyan. or, you could just call this a super saiyan two...";
            id2.m_shared.m_armor = 10;
            id2.m_shared.m_armorPerLevel = 10;
            id2.m_shared.m_maxDurability = 9001;
            id2.m_shared.m_maxQuality = 4;
            id2.m_shared.m_eitrRegenModifier = 11;
            id2.m_shared.m_helmetHideHair = ItemDrop.ItemData.HelmetHairType.Hidden;
            


            id2.m_shared.m_movementModifier = 1;
            id2.m_shared.m_equipDuration = 0.01f;
            Material gokuchestmat = Shawcassets.LoadAsset<Material>("Assets/ShawesomeCapeimage/GokuChestMat.mat");
            id2.m_shared.m_armorMaterial = gokuchestmat;
            //changing whatever item you instatiated's item type [which square is it using]
            id2.m_shared.m_itemType = ItemDrop.ItemData.ItemType.Utility;






            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Resistant;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair nofire = new HitData.DamageModPair();
            nofire.m_modifier = HitData.DamageModifier.Resistant;
            nofire.m_type = HitData.DamageType.Fire;
            id2.m_shared.m_damageModifiers.Add(nofire);

            

            HitData.DamageModPair zapres = new HitData.DamageModPair();
            zapres.m_modifier = HitData.DamageModifier.Resistant;
            zapres.m_type = HitData.DamageType.Lightning;
            id2.m_shared.m_damageModifiers.Add(zapres);

           


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] JMCicon = Shawcassets.LoadAssetWithSubAssets("Assets/Shawesome/ssj2iconnew.png");
            id2.m_shared.m_icons[0] = JMCicon[1] as Sprite;
            //putting the below line under where we bring in the cape's icon to callin the effect function below
            id2.m_shared.m_equipStatusEffect = BeyondEffect(JMCicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = new GameObject().transform;
            NTcape1.name = "attach_skin";
            NTcape1.parent = PrefabManager.Instance.GetPrefab("Itsover9000").transform;
            //Transform NTcape1 = PrefabManager.Instance.GetPrefab("Itsover9000")
            //Transform NTcape2 = NTcape1.Find("LoxCape").transform;

            //adding hood to cape
            Transform chest = PrefabManager.Instance.GetPrefab("ArmorLeatherChest").transform.Find("attach_skin").transform;
            Transform chest1 = chest.Find("shorts").transform;
            Transform chestcopy = CopyIntoParent(chest1, NTcape1);
            //adding body overlay to cape
            Transform Body = PrefabManager.Instance.GetPrefab("body").transform;
            Transform bodycopy = CopyIntoParent(Body, NTcape1);
            //adding robebottoms to cape
            Transform legs = PrefabManager.Instance.GetPrefab("ArmorIronLegs").transform.Find("attach_skin").transform;
            Transform legs1 = legs.Find("SilverWolfArmor_Legs.001").transform;
            Transform Legscopy = CopyIntoParent(legs1, NTcape1);
            //modify robebottoms copy material and look

            
            //assinging names to each piece in order to change their assigned -textures, Mesh, Metal, Emission, BM and stuff
            //assigning hood
            SkinnedMeshRenderer NThoodskinmesh = chestcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning cape
            //SkinnedMeshRenderer NTcapeskinmesh = NTcape2.GetComponent<SkinnedMeshRenderer>();
            //assigning robe bottoms
            SkinnedMeshRenderer NTrobeskinmesh = Legscopy.GetComponent<SkinnedMeshRenderer>();
            //assigning body skinned mesh renderer
            SkinnedMeshRenderer Bodyskinmesh = bodycopy.GetComponent<SkinnedMeshRenderer>();
            //Material NTCmat = NTcapeskinmesh.material;
            //Material Bodymat = Bodyskinmesh.material;
            Material NTHmat = NThoodskinmesh.material;
            Material NTRmat = NTrobeskinmesh.material;
            NTHmat.color = Color.white;
            //NTCmat.color = Color.white;
            NTRmat.color = Color.white;
            UnityEngine.Object[] robetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmrobe_e.png");
            UnityEngine.Object[] robeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/IronArmorLegs_d_GOKU.png");
            UnityEngine.Object[] robemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_m.png");
            //UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            UnityEngine.Object[] hoodtexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_d.png");
            //UnityEngine.Object[] hoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] gokuchest = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/LeatherArmourChest_d_GOKU.png");
            UnityEngine.Object[] hoodbumpmap = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_bm.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_D.png");
            UnityEngine.Object[] capeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] capemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_M.png");
            //using woodvalk for dhakhars build temporary only for texture- 
            //UnityEngine.Object[] WOODVALK = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/woodvalk_d.png");

            /*//Adding textures and emissions to the cape
            NTCmat.SetTexture("_MainTex", capeemish[0] as Texture2D);
            NTCmat.EnableKeyword("_EMISSION");
            NTCmat.SetTexture("_EmissionMap", capeemish[0] as Texture2D);
            NTCmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTCmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_MetallicGlossMap", capeemish[0] as Texture2D);
            */
            Bodyskinmesh.material = gokuchestmat;
            //Bodymat = gokuchestmat;

            // Adding textures and emissions & bm & metal to the hood- 
            NTHmat.SetTexture("_ChestTex", gokuchest[0] as Texture2D);
            NTHmat.EnableKeyword("_EMISSION");
            NTHmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_EmissionMap", gokuchest[0] as Texture2D);
            NTHmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
            NTHmat.SetTexture("_MetallicGlossMap", gokuchest[0] as Texture2D);
            //NTHmat.SetTexture("_BumpMap", hoodbumpmap[0] as Texture2D);

            
            // Adding textures and emissions & bm & metal to the robe bottoms
            NTRmat.SetTexture("_MainTex", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_EMISSION");
            NTHmat.SetTexture("_EmissionMap", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_Metallic");
            NTRmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));//comment this line out if robe is too much shine
            NTRmat.SetTexture("_MetallicGlossMap", robeemish[0] as Texture2D);


            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject jmCape = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/SSJ.prefab");
            //jmCape.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //CopyIntoParent(jmCape.transform, NTcape1);

          

            
            //calling the "different bone attaches" from unity into the mod

            //CopyIntoParent(jmCape.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            //CopyIntoParent(jmCape.transform.Find("attach_RightShoulder"), NTcape1.parent);
            CopyIntoParent(jmCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(jmCape.transform.Find("attach_skin"), NTcape1);


        }

        // Add new status effects
        private static SE_Stats BeyondEffect(Sprite sprite)
        {
            //cant use this effect with feath cape because it gets rid of the feather fall effect :[ so we commented out the call-in
            SE_Stats infinityEffect = new SE_Stats();
            infinityEffect.name = "SSEffect";
            infinityEffect.m_name = "The Great Sage, Heaven's Equal.";
            infinityEffect.m_category = "Indomitable Spirt";
            infinityEffect.m_tooltip = "One Must be Pure of Heart to Achieve this state of True Enlightenment...";
            infinityEffect.m_startMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_startMessage = "You manage to go Beyond the Limits of even the Gods!!!!";
            infinityEffect.m_stopMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_stopMessage = "You're Tired and Hungry... You could use a Senzu...";
            infinityEffect.m_icon = sprite;
            //infinityEffect.m_maxMaxFallSpeed = .01f;
            infinityEffect.m_fallDamageModifier = -1;
            infinityEffect.m_staminaRegenMultiplier = 110;
            infinityEffect.m_jumpModifier = new Vector3(0, 1.5f, 0);
            infinityEffect.m_skillLevel = Skills.SkillType.All;
            infinityEffect.m_skillLevelModifier = 40;
            infinityEffect.m_healthRegenMultiplier = 11;
            //infinityEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;
            infinityEffect.m_attributes = StatusEffect.StatusAttribute.DoubleImpactDamage;
            infinityEffect.m_runStaminaDrainModifier = -100;

            //CustomStatusEffect infinityCapeEffect = new CustomStatusEffect(infinityEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(infinityCapeEffect);
            return infinityEffect;

        }
        //SSJ Stuff End


        //CryoCape stuff Start
        public static void AddCryoCape()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("TrophyUlv", 4, 1));
            capeConfig.AddRequirement(new RequirementConfig(CraftingConditions.freezing_prefNname[0], 1, 1));
            capeConfig.AddRequirement(new RequirementConfig("YmirRemains", 42));
            capeConfig.AddRequirement(new RequirementConfig("TrophyDragonQueen", 4, 1));
            //capeConfig.AddRequirement(new RequirementConfig("Haseitrrefinery_Item", 3, 2));
            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            CustomItem Cryocape = new CustomItem("FrostGodJotnarCape", "CapeLox", capeConfig);
            ItemManager.Instance.AddItem(Cryocape);

            //calling in the actual effect- note needed
            //StatusEffect PyroEffect = AddCryoEffects(Sprite sprite).StatusEffect;

            //Assigning item ID
            var id = Cryocape.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Vafþrúðnir's Regalia";
            id2.m_shared.m_description = "In the frigid heart of Ginnungagap, Ymir slumbers, the Ancient Frost, from whose Dreams and Tears, the Cosmos takes its First Breath, and the World is Born Anew...";
            id2.m_shared.m_armor = 8;
            id2.m_shared.m_armorPerLevel = 1;
            id2.m_shared.m_maxDurability = 5000;
            id2.m_shared.m_maxQuality = 7;
            id2.m_shared.m_eitrRegenModifier = 0.90f;
            



            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Resistant;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair fireres = new HitData.DamageModPair();
            fireres.m_modifier = HitData.DamageModifier.Resistant;
            fireres.m_type = HitData.DamageType.Fire;
            id2.m_shared.m_damageModifiers.Add(fireres);

            HitData.DamageModPair noslash = new HitData.DamageModPair();
            noslash.m_modifier = HitData.DamageModifier.Immune;
            noslash.m_type = HitData.DamageType.Slash;
            id2.m_shared.m_damageModifiers.Add(noslash);


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] cryoicon = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/CryoCapeicon.png");
            id2.m_shared.m_icons[0] = cryoicon[1] as Sprite;

            id2.m_shared.m_equipStatusEffect = AddCryoEffects(cryoicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = PrefabManager.Instance.GetPrefab("FrostGodJotnarCape").transform.Find("attach_skin").transform;
            Transform NTcape2 = NTcape1.Find("LoxCape").transform;
            //adding hood to cape
            Transform hood = PrefabManager.Instance.GetPrefab("HelmetFenring").transform.Find("attach_skin").transform;
            Transform hood1 = hood.Find("FenringHood").transform;
            Transform hoodcopy = CopyIntoParent(hood1, NTcape1);

            //modify hood copy material and look

            //adding robebottoms to cape
            //Transform chest = PrefabManager.Instance.GetPrefab("ArmorMageChest").transform.Find("attach_skin").transform;
            //Transform chest1 = chest.Find("MageArmorBody.001").transform;
            //Transform chestcopy = CopyIntoParent(chest1, NTcape1);
            //modify robebottoms copy material and look

            //assinging names to each piece in order to change their assigned -textures, Mesh, Metal, Emission, BM and stuff
            //assigning hood
            SkinnedMeshRenderer NThoodskinmesh = hoodcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning cape
            SkinnedMeshRenderer NTcapeskinmesh = NTcape2.GetComponent<SkinnedMeshRenderer>();
            //assigning robe bottoms
            //SkinnedMeshRenderer NTrobeskinmesh = chestcopy.GetComponent<SkinnedMeshRenderer>();
            Material NTCmat = NTcapeskinmesh.material;
            Material NTHmat = NThoodskinmesh.material;
            //Material NTRmat = NTrobeskinmesh.material;

            //these colors dont really matter when you have custom textures/emish being used
            //NTHmat.color = Color.red; //red
            //NTCmat.color = Color.red; //red
            //NTRmat.color = Color.white; //white
            //UnityEngine.Object[] robetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_d.png");
            UnityEngine.Object[] robehoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/CryoRobeHood_e.png");
            //UnityEngine.Object[] robemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_m.png");
            //UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            //UnityEngine.Object[] hoodtexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_d.png");
            //UnityEngine.Object[] hoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_e.png");
            //UnityEngine.Object[] hoodmetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_m.png");
            //UnityEngine.Object[] hoodbumpmap = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_bm.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            //UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_D.png");
            UnityEngine.Object[] cryocapeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/CryoCape_E.png");
            //UnityEngine.Object[] capemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_M.png");

            //Adding textures and emissions to the cape
            NTCmat.SetTexture("_MainTex", cryocapeemish[0] as Texture2D);
            NTCmat.EnableKeyword("_EMISSION");
            NTCmat.SetTexture("_EmissionMap", cryocapeemish[0] as Texture2D);
            NTCmat.SetColor("_EmissionColor", new Color(0, 0.5f, 1, 1));
            NTCmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_MetallicGlossMap", cryocapeemish[0] as Texture2D);
            // Adding textures and emissions & bm & metal to the hood- cant find magehood texture :[
            NTHmat.SetTexture("_MainTex", robehoodemish[0] as Texture2D);
            NTHmat.EnableKeyword("_EMISSION");
            NTHmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_EmissionMap", robehoodemish[0] as Texture2D);
            NTHmat.SetColor("_EmissionColor", new Color(0, 0.5f, 1, 1));
            NTHmat.SetTexture("_MetallicGlossMap", robehoodemish[0] as Texture2D);
            //NTHmat.SetTexture("_BumpMap", hoodbumpmap[0] as Texture2D);

            // Adding textures and emissions & bm & metal to the robe bottoms
            //NTRmat.SetTexture("_MainTex", robehoodemish[0] as Texture2D);
            //NTRmat.EnableKeyword("_EMISSION");
            //NTRmat.SetTexture("_EmissionMap", robehoodemish[0] as Texture2D);
            //NTRmat.EnableKeyword("_Metallic");
            //NTRmat.SetColor("_EmissionColor", new Color(0, 0.5f, 1, 1));//comment this line out if robe is too much shine
            //NTRmat.SetTexture("_MetallicGlossMap", robehoodemish[0] as Texture2D);


            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject cryoCapeunityprefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/CryoCape.prefab");
            //PyroCapeunityprefab.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //CopyIntoParent(PyroCapeunityprefab.transform, NTcape1);

            //adding the different attaches
            //CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            //CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_RightShoulder"), NTcape1.parent);
            //CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_Head"), NTcape1.parent);
            CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_Spine2"), NTcape1.parent);
            //CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_LeftHand"), NTcape1.parent);
            CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_RightArm"), NTcape1.parent);
            CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_RightForeArm"), NTcape1.parent);
            CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_RightHand"), NTcape1.parent);
            //CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_RightFoot"), NTcape1.parent);
            //CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_LeftFoot"), NTcape1.parent);
            //CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_Hips"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            //CopyIntoParent(cryoCapeunityprefab.transform.Find("attach_skin"), NTcape1);








        }

        // Add new status effects
        private static SE_Stats AddCryoEffects(Sprite sprite)
        {
            //StatusEffect CryoEffect = ScriptableObject.CreateInstance<StatusEffect>();
            SE_Stats CryoEffect = new SE_Stats();
            CryoEffect.name = "Frost God's Respite";
            CryoEffect.m_name = "Ymir's Chosen";
            CryoEffect.m_icon = sprite;
            CryoEffect.m_startMessageType = MessageHud.MessageType.Center;
            CryoEffect.m_startMessage = "The Ice seems to be Alive...";
            CryoEffect.m_stopMessageType = MessageHud.MessageType.Center;
            CryoEffect.m_stopMessage = "The Favor of the Frost God fades...";
            CryoEffect.m_skillLevel = Skills.SkillType.Swim;
            CryoEffect.m_skillLevelModifier = 70;
            CryoEffect.m_skillLevel2 = Skills.SkillType.ElementalMagic;
            CryoEffect.m_skillLevelModifier2 = 40;
            CryoEffect.m_fallDamageModifier = -.6f;
            CryoEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;

            //CustomStatusEffect CryoCapeEffect = new CustomStatusEffect(CryoEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(CryoCapeEffect);
            return CryoEffect;


        }
        //CryoCape stuff END


        //PyroCapeCape stuff Start
        public static void AddPyroCape()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("Flametal", 10, 4));
            capeConfig.AddRequirement(new RequirementConfig(CraftingConditions.onfire_prefNname[0], 5, 2));
            capeConfig.AddRequirement(new RequirementConfig("TrophyCultist", 3, 1));
            capeConfig.AddRequirement(new RequirementConfig("TrophyGoblinKing", 1, 1)); 
            //capeConfig.AddRequirement(new RequirementConfig("Haseitrrefinery_Item", 3, 2));
            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            CustomItem Pyrocape = new CustomItem("FireGodSurturCape", "CapeLox", capeConfig);
            ItemManager.Instance.AddItem(Pyrocape);

            //calling in the actual effect- we wont need this line because its being calle din right after the capes icon-sprite is loaded below
            //StatusEffect PyroEffect = AddPyroEffects().StatusEffect;

            //Assigning item ID
            var id = Pyrocape.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Flaming Phoenix Feather";
            id2.m_shared.m_description = "Set your Heart Ablaze! Enveloped by the Flames of Ragnorak, you Alone hold the Power to bring about the End of Times...";
            id2.m_shared.m_armor = 4;
            id2.m_shared.m_armorPerLevel = 1;
            id2.m_shared.m_maxDurability = 5000;
            id2.m_shared.m_maxQuality = 7;
            id2.m_shared.m_eitrRegenModifier = 0.80f;
            
            


            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Resistant;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair nofire = new HitData.DamageModPair();
            nofire.m_modifier = HitData.DamageModifier.Immune;
            nofire.m_type = HitData.DamageType.Fire;
            id2.m_shared.m_damageModifiers.Add(nofire);


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] Pyroicon = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/PyroCapeicon.png");
            id2.m_shared.m_icons[0] = Pyroicon[1] as Sprite;

            id2.m_shared.m_equipStatusEffect = AddPyroEffect(Pyroicon[1] as Sprite);



            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = PrefabManager.Instance.GetPrefab("FireGodSurturCape").transform.Find("attach_skin").transform;
            Transform NTcape2 = NTcape1.Find("LoxCape").transform;
            //adding hood to cape
            Transform hood = PrefabManager.Instance.GetPrefab("HelmetFenring").transform.Find("attach_skin").transform;
            Transform hood1 = hood.Find("FenringHood").transform;
            Transform hoodcopy = CopyIntoParent(hood1, NTcape1);

            //modify hood copy material and look

            //adding robebottoms to cape
            Transform chest = PrefabManager.Instance.GetPrefab("ArmorMageChest").transform.Find("attach_skin").transform;
            Transform chest1 = chest.Find("MageArmorBody.001").transform;
            Transform chestcopy = CopyIntoParent(chest1, NTcape1);
            //modify robebottoms copy material and look

            //assinging names to each piece in order to change their assigned -textures, Mesh, Metal, Emission, BM and stuff
            //assigning hood
            SkinnedMeshRenderer NThoodskinmesh = hoodcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning cape
            SkinnedMeshRenderer NTcapeskinmesh = NTcape2.GetComponent<SkinnedMeshRenderer>();
            //assigning robe bottoms
            SkinnedMeshRenderer NTrobeskinmesh = chestcopy.GetComponent<SkinnedMeshRenderer>();
            Material NTCmat = NTcapeskinmesh.material;
            Material NTHmat = NThoodskinmesh.material;
            Material NTRmat = NTrobeskinmesh.material;

            //these colors dont really matter when you have custome textures being used
            NTHmat.color = Color.red; //red
            NTCmat.color = Color.red; //red
            NTRmat.color = Color.white; //white
            //UnityEngine.Object[] robetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_d.png");
            UnityEngine.Object[] robehoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/PyroRobeHood_e.png");
            //UnityEngine.Object[] robemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_m.png");
            //UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            //UnityEngine.Object[] hoodtexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_d.png");
            UnityEngine.Object[] hoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/PryoHood_e.png");
            //UnityEngine.Object[] hoodmetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_m.png");
            //UnityEngine.Object[] hoodbumpmap = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_bm.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            //UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_D.png");
            UnityEngine.Object[] pyrocapeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/PyroCape_E.png");
            //UnityEngine.Object[] capemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_M.png");

            //Adding textures and emissions to the cape
            NTCmat.SetTexture("_MainTex", pyrocapeemish[0] as Texture2D);
            NTCmat.EnableKeyword("_EMISSION");
            NTCmat.SetTexture("_EmissionMap", pyrocapeemish[0] as Texture2D);
            NTCmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
            NTCmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_MetallicGlossMap", pyrocapeemish[0] as Texture2D);
            // Adding textures and emissions & bm & metal to the hood- cant find magehood texture :[
            NTHmat.SetTexture("_MainTex", hoodemish[0] as Texture2D);
            NTHmat.EnableKeyword("_EMISSION");
            NTHmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_EmissionMap", hoodemish[0] as Texture2D);
            NTHmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));
            NTHmat.SetTexture("_MetallicGlossMap", hoodemish[0] as Texture2D);
            //NTHmat.SetTexture("_BumpMap", hoodbumpmap[0] as Texture2D);

            // Adding textures and emissions & bm & metal to the robe bottoms
            NTRmat.SetTexture("_MainTex", robehoodemish[0] as Texture2D);
            NTRmat.EnableKeyword("_EMISSION");
            NTHmat.SetTexture("_EmissionMap", robehoodemish[0] as Texture2D);
            NTRmat.EnableKeyword("_Metallic");
            NTRmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));//comment this line out if robe is too much shine
            NTRmat.SetTexture("_MetallicGlossMap", robehoodemish[0] as Texture2D);


            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject PyroCapeunityprefab = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/PyroCape.prefab");
            //PyroCapeunityprefab.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //CopyIntoParent(PyroCapeunityprefab.transform, NTcape1);

            //adding the different attaches
            //CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_Hips"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_RightShoulder"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_Head"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_Spine2"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_LeftHand"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_RightHand"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_RightFoot"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_LeftFoot"), NTcape1.parent);
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_Hips"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(PyroCapeunityprefab.transform.Find("attach_skin"), NTcape1);
            







        }

        // Add new status effects
        private static SE_Stats AddPyroEffect(Sprite sprite)
        {
            //StatusEffect PyroEffect = ScriptableObject.CreateInstance<StatusEffect>();
            SE_Stats PyroEffect = new SE_Stats();
            PyroEffect.m_icon = sprite;
            PyroEffect.name = "Majesty of the Fire God";
            PyroEffect.m_name = "Surtr's Chosen";
            PyroEffect.m_startMessageType = MessageHud.MessageType.Center;
            PyroEffect.m_startMessage = "The Will of the Fire God Twists and Rages Through You!";
            PyroEffect.m_stopMessageType = MessageHud.MessageType.Center;
            PyroEffect.m_stopMessage = "The Realms Breathe a sigh of Relief, for now...";
            PyroEffect.m_healthRegenMultiplier = 2;
            PyroEffect.m_skillLevel = Skills.SkillType.Bows;
            PyroEffect.m_skillLevelModifier = 40;
            PyroEffect.m_skillLevel2 = Skills.SkillType.ElementalMagic;
            PyroEffect.m_skillLevelModifier2 = 40;
            PyroEffect.m_hitType = HitData.HitType.Burning;
            PyroEffect.m_fallDamageModifier = -.6f;
            PyroEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;

            //CustomStatusEffect PyroCapeEffect = new CustomStatusEffect(PyroEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(PyroCapeEffect);
            return PyroEffect;


        }
        //PyroCape stuff END


        //NecroTechCape stuff Start
        public static void AddNecroTechCape()
        {
            
            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("JuteRed", 10, 4));
            capeConfig.AddRequirement(new RequirementConfig("HasRefiner_Item", 3, 2));
            capeConfig.AddRequirement(new RequirementConfig("Chain", 5, 2));
            capeConfig.AddRequirement(new RequirementConfig("MechanicalSpring", 3, 1));
            capeConfig.AddRequirement(new RequirementConfig("DvergrNeedle", 1, 2));
            
            capeConfig.CraftingStation = CraftingStations.Forge;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            CustomItem NTcape = new CustomItem("NecroTechCape", "CapeLox", capeConfig);
            ItemManager.Instance.AddItem(NTcape);
            
            //calling in the actual effect
            //StatusEffect evilEffect = AddEvilEffects(Sprite sprite).StatusEffect;

            //Assigning item ID
            var id = NTcape.ItemDrop;
            var id2 = id.m_itemData;
            
            // Adding stats
            id2.m_shared.m_name = "Robes of the Arch Magos";
            id2.m_shared.m_description = "There is no truth in flesh, only betrayal. There is no strength in flesh, only weakness. There is no constancy in flesh, only decay. There is no certainty in flesh but death.";
            id2.m_shared.m_armor = 4;
            id2.m_shared.m_armorPerLevel = 1;
            id2.m_shared.m_maxDurability = 4000;
            id2.m_shared.m_maxQuality = 6;
            id2.m_shared.m_eitrRegenModifier = 0.50f;

            Material NTchestmat = Shawcassets.LoadAsset<Material>("Assets/ShawesomeCapeimage/NTChestMat.mat");
            id2.m_shared.m_armorMaterial = NTchestmat;


            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.VeryResistant;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair zapres = new HitData.DamageModPair();
            zapres.m_modifier = HitData.DamageModifier.VeryResistant;
            zapres.m_type = HitData.DamageType.Lightning;
            id2.m_shared.m_damageModifiers.Add(zapres);


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] NTCicon = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCapeicon.png");
            id2.m_shared.m_icons[0] = NTCicon[1] as Sprite;

            id2.m_shared.m_equipStatusEffect = AddEvilEffects(NTCicon[1] as Sprite);


            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = PrefabManager.Instance.GetPrefab("NecroTechCape").transform.Find("attach_skin").transform;
            Transform NTcape2 = NTcape1.Find("LoxCape").transform;
            //adding hood to cape- fenring chest
            Transform hood = PrefabManager.Instance.GetPrefab("HelmetMage").transform.Find("attach_skin").transform;
            Transform hood1 = hood.Find("MageArmorHelmet").transform;
            Transform hoodcopy = CopyIntoParent(hood1, NTcape1);

            //adding body overlay to cape
            Transform Body = PrefabManager.Instance.GetPrefab("body").transform;
            Transform bodycopy = CopyIntoParent(Body, NTcape1);

            //adding fenring chest
            Transform fchest = PrefabManager.Instance.GetPrefab("ArmorFenringChest").transform.Find("attach_skin").transform;
            Transform fchest1 = fchest.Find("FenringPants").transform;
            Transform fchestcopy = CopyIntoParent(fchest1, NTcape1);

            //modify hood copy material and look

            //adding robebottoms to cape
            Transform chest = PrefabManager.Instance.GetPrefab("ArmorMageChest").transform.Find("attach_skin").transform;
            Transform chest1 = chest.Find("MageArmorBody.001").transform;
            Transform chestcopy = CopyIntoParent(chest1, NTcape1);
            //modify robebottoms copy material and look
            
            //assinging names to each piece in order to change their assigned -textures, Mesh, Metal, Emission, BM and stuff
            //assigning hood
            SkinnedMeshRenderer NThoodskinmesh = hoodcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning cape
            SkinnedMeshRenderer NTcapeskinmesh = NTcape2.GetComponent<SkinnedMeshRenderer>(); 
            //assigning robe bottoms
            SkinnedMeshRenderer NTrobeskinmesh = chestcopy.GetComponent<SkinnedMeshRenderer>();
            //asigning fen chest
            SkinnedMeshRenderer NTchestskinmesh = fchestcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning complete fen chest
            SkinnedMeshRenderer Bodyskinmesh = bodycopy.GetComponent<SkinnedMeshRenderer>();

            Material NTfcmat = NTchestskinmesh.material;
            Material NTCmat = NTcapeskinmesh.material;
            Material NTHmat = NThoodskinmesh.material;
            Material NTRmat = NTrobeskinmesh.material;
            NTHmat.color = Color.red; //red
            NTCmat.color = Color.red; //red
            NTRmat.color = Color.white; //white
            NTfcmat.color = Color.red; 
            UnityEngine.Object[] robetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_d.png");
            UnityEngine.Object[] robeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_e.png");
            UnityEngine.Object[] robemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_m.png");
            //UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            UnityEngine.Object[] hoodtexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_d.png");
            UnityEngine.Object[] hoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_e.png");
            UnityEngine.Object[] hoodmetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_m.png");
            UnityEngine.Object[] hoodbumpmap = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_bm.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_D.png");
            UnityEngine.Object[] capeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_E.png");
            UnityEngine.Object[] capemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_M.png");
            UnityEngine.Object[] fchestemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTchest_d.png");
            UnityEngine.Object[] NTchest = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTChestMat.png");


            Bodyskinmesh.material = NTchestmat;

            //Adding textures and emissions to the chest-chest
            //NTchestmat.SetTexture("_MainTex", NTchest[0] as Texture2D);
            //NTchestmat.EnableKeyword("_EMISSION");
            //NTchestmat.SetTexture("_EmissionMap", NTchest[0] as Texture2D);
            //NTchestmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            //NTchestmat.EnableKeyword("_Metallic");
            //NTchestmat.SetTexture("_MetallicGlossMap", NTchest[0] as Texture2D);

            //Adding textures and emissions to the chest
            NTfcmat.SetTexture("_MainTex", fchestemish[0] as Texture2D);
            NTfcmat.EnableKeyword("_EMISSION");
            NTfcmat.SetTexture("_EmissionMap", fchestemish[0] as Texture2D);
            NTfcmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTfcmat.EnableKeyword("_Metallic");
            NTfcmat.SetTexture("_MetallicGlossMap", fchestemish[0] as Texture2D);
            //Adding textures and emissions to the cape
            NTCmat.SetTexture("_MainTex", capetexture[0] as Texture2D);
            NTCmat.EnableKeyword("_EMISSION");
            NTCmat.SetTexture("_EmissionMap", capeemish[0] as Texture2D);
            NTCmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTCmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_MetallicGlossMap", capetexture[0] as Texture2D);
            // Adding textures and emissions & bm & metal to the hood- cant find magehood texture :[
            NTHmat.SetTexture("_MainTex", robetexture[0] as Texture2D);
            NTHmat.EnableKeyword("_EMISSION");
            NTHmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_EmissionMap", robeemish[0] as Texture2D);
            NTHmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTHmat.SetTexture("_MetallicGlossMap", robetexture[0] as Texture2D);
            //NTHmat.SetTexture("_BumpMap", hoodbumpmap[0] as Texture2D);

            // Adding textures and emissions & bm & metal to the robe bottoms
            NTRmat.SetTexture("_MainTex", robetexture[0] as Texture2D);
            NTRmat.EnableKeyword("_EMISSION");
            NTHmat.SetTexture("_EmissionMap", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_Metallic");
            NTRmat.SetColor("_EmissionColor", new Color(0, 0, 0, 1));//comment this line out if robe is too much shine
            NTRmat.SetTexture("_MetallicGlossMap", robetexture[0] as Texture2D);


            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject NTCape = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/NTCape.prefab");
            NTCape.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //CopyIntoParent(NTCape.transform, NTcape1);

            CopyIntoParent(NTCape.transform.Find("attach_Hips"), NTcape1.parent);
            //CopyIntoParent(NTCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            CopyIntoParent(NTCape.transform.Find("attach_Spine2"), NTcape1.parent);
            CopyIntoParent(NTCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(NTCape.transform.Find("attach_skin"), NTcape1);

            //if set in unity to skinmesh rend using melds code from utils 1&2 this wont matter and wont be needed- in this case we are  using it
            
            //NTCape.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //NTCape.transform.localPosition = new Vector3(0.1f, 1, 0.3f);




        }

        // Add new status effects
        private static SE_Stats AddEvilEffects(Sprite sprite)
        {
            //StatusEffect evilEffect = ScriptableObject.CreateInstance<StatusEffect>();
            SE_Stats evilEffect = new SE_Stats();
            evilEffect.m_icon = sprite;
            evilEffect.name = "EvilEffect";
            evilEffect.m_name = "Blessing of the Omnissiah";
            evilEffect.m_startMessageType = MessageHud.MessageType.Center;
            evilEffect.m_startMessage = "The Machine Spirit is pleased...";
            evilEffect.m_stopMessageType = MessageHud.MessageType.Center;
            evilEffect.m_stopMessage = "The Machine Spirit grows weary...";
            evilEffect.m_skillLevel = Skills.SkillType.BloodMagic;
            evilEffect.m_skillLevelModifier = 40;
            evilEffect.m_addMaxCarryWeight = 300;
            evilEffect.m_hitType = HitData.HitType.Smoke;
            evilEffect.m_fallDamageModifier = -.6f;
            evilEffect.m_skillLevel2 = Skills.SkillType.Swim;
            evilEffect.m_skillLevelModifier2 = -40;
            evilEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;


            //CustomStatusEffect EvilCapeEffect = new CustomStatusEffect(evilEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(EvilCapeEffect);
            return evilEffect;
            

        }
        //NecroTechCape stuff END

        //JayMaeCape Stuff Start
        public static void addjmcgoddess()
        {

            //add recipe
            ItemConfig capeConfig = new ItemConfig();
            capeConfig.AddRequirement(new RequirementConfig("NecroTechCape", 1));
            capeConfig.AddRequirement(new RequirementConfig("ShawesomeCape", 1));
            capeConfig.AddRequirement(new RequirementConfig("MeldursonCape", 1));
            capeConfig.AddRequirement(new RequirementConfig("FireGodSurturCape", 1));
            capeConfig.AddRequirement(new RequirementConfig(CraftingConditions.onfire_prefNname[0], 5, 2));
            capeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1, 1));
            capeConfig.AddRequirement(new RequirementConfig("FrostGodJotnarCape", 1));
            capeConfig.AddRequirement(new RequirementConfig(CraftingConditions.tamednearby_prefNname[0], 3, 3));
            capeConfig.CraftingStation = CraftingStations.Forge; ;
            capeConfig.MinStationLevel = 1;
            //add new cust item from the base of lox cape
            CustomItem jmgCape = new CustomItem("JMCGoddess", "CapeFeather", capeConfig);
            ItemManager.Instance.AddItem(jmgCape);

            //calling in the actual effect
            //StatusEffect infinityEffect = InfinityEffect().StatusEffect;

            //Assigning item ID
            var id = jmgCape.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Valkyrie's Grace";
            id2.m_shared.m_description = "The Skies Twist and the Heavens Roar! Omnipotence ebbs and flows through You...";
            id2.m_shared.m_armor = 10;
            id2.m_shared.m_armorPerLevel = 10;
            id2.m_shared.m_maxDurability = 7770;
            id2.m_shared.m_maxQuality = 7;
            id2.m_shared.m_eitrRegenModifier = 11;
            //id2.m_shared.m_equipStatusEffect = infinityEffect;
            id2.m_shared.m_movementModifier = 1;
            id2.m_shared.m_equipDuration = 0.01f;

            
            
            


            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Immune;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair nofire = new HitData.DamageModPair();
            nofire.m_modifier = HitData.DamageModifier.Immune;
            nofire.m_type = HitData.DamageType.Fire;
            id2.m_shared.m_damageModifiers.Add(nofire);

            HitData.DamageModPair nopoke = new HitData.DamageModPair();
            nopoke.m_modifier = HitData.DamageModifier.Immune;
            nopoke.m_type = HitData.DamageType.Pierce;
            id2.m_shared.m_damageModifiers.Add(nopoke);

            HitData.DamageModPair zapres = new HitData.DamageModPair();
            zapres.m_modifier = HitData.DamageModifier.Immune;
            zapres.m_type = HitData.DamageType.Lightning;
            id2.m_shared.m_damageModifiers.Add(zapres);

            HitData.DamageModPair noslash = new HitData.DamageModPair();
            noslash.m_modifier = HitData.DamageModifier.Immune;
            noslash.m_type = HitData.DamageType.Slash;
            id2.m_shared.m_damageModifiers.Add(noslash);


            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] JMCicon = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcapeicon.png");
            id2.m_shared.m_icons[0] = JMCicon[1] as Sprite;
            //putting the below line under where we bring in the cape's icon to callin the effect function below
            id2.m_shared.m_equipStatusEffect = InfinityEffect(JMCicon[1] as Sprite);

            //This would be the transform of the cape once you create a custom items from it
            Transform NTcape1 = PrefabManager.Instance.GetPrefab("JMCGoddess").transform.Find("attach_skin").transform;
            Transform NTcape2 = NTcape1.Find("MageCape").transform;
            

            //adding hood to cape
            Transform hood = PrefabManager.Instance.GetPrefab("HelmetMage").transform.Find("attach_skin").transform;
            Transform hood1 = hood.Find("MageArmorHelmet").transform;
            Transform hoodcopy = CopyIntoParent(hood1, NTcape1);

            //adding robebottoms to cape
            Transform chest = PrefabManager.Instance.GetPrefab("ArmorMageChest").transform.Find("attach_skin").transform;
            Transform chest1 = chest.Find("MageArmorBody.001").transform;
            Transform chestcopy = CopyIntoParent(chest1, NTcape1);
            //modify robebottoms copy material and look

            //assinging names to each piece in order to change their assigned -textures, Mesh, Metal, Emission, BM and stuff
            //assigning hood
            SkinnedMeshRenderer NThoodskinmesh = hoodcopy.GetComponent<SkinnedMeshRenderer>();
            //assigning cape
            SkinnedMeshRenderer NTcapeskinmesh = NTcape2.GetComponent<SkinnedMeshRenderer>();
            //assigning robe bottoms
            SkinnedMeshRenderer NTrobeskinmesh = chestcopy.GetComponent<SkinnedMeshRenderer>();
            Material NTCmat = NTcapeskinmesh.material;
            Material NTHmat = NThoodskinmesh.material;
            Material NTRmat = NTrobeskinmesh.material;
            NTHmat.color = Color.white;
            NTCmat.color = Color.white;
            NTRmat.color = Color.white;
            UnityEngine.Object[] robetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmrobe_e.png");
            UnityEngine.Object[] robeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmrobe_e.png");
            UnityEngine.Object[] robemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTrobe_m.png");
            //UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            UnityEngine.Object[] hoodtexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_d.png");
            //UnityEngine.Object[] hoodemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] hoodmetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_m.png");
            UnityEngine.Object[] hoodbumpmap = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NThood_bm.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_D.png");
            UnityEngine.Object[] capeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/jmcape_e.png");
            UnityEngine.Object[] capemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/NTCape_M.png");
            //using woodvalk for dhakhars build temporary only for texture- 
            //UnityEngine.Object[] WOODVALK = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/woodvalk_d.png");

            //Adding textures and emissions to the cape
            NTCmat.SetTexture("_MainTex", capeemish[0] as Texture2D);
            NTCmat.EnableKeyword("_EMISSION");
            NTCmat.SetTexture("_EmissionMap", capeemish[0] as Texture2D);
            NTCmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTCmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_MetallicGlossMap", capeemish[0] as Texture2D);
            // Adding textures and emissions & bm & metal to the hood- cant find magehood texture :[
            NTHmat.SetTexture("_MainTex", robeemish[0] as Texture2D);
            NTHmat.EnableKeyword("_EMISSION");
            NTHmat.EnableKeyword("_Metallic");
            NTHmat.SetTexture("_EmissionMap", robeemish[0] as Texture2D);
            NTHmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));
            NTHmat.SetTexture("_MetallicGlossMap", robeemish[0] as Texture2D);
            //NTHmat.SetTexture("_BumpMap", hoodbumpmap[0] as Texture2D);

            // Adding textures and emissions & bm & metal to the robe bottoms
            NTRmat.SetTexture("_MainTex", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_EMISSION");
            NTHmat.SetTexture("_EmissionMap", robeemish[0] as Texture2D);
            NTRmat.EnableKeyword("_Metallic");
            NTRmat.SetColor("_EmissionColor", new Color(1, 1, 1, 1));//comment this line out if robe is too much shine
            NTRmat.SetTexture("_MetallicGlossMap", robeemish[0] as Texture2D);


            //Making NTCape game object from unity visable (NTCape variable = NTCape.prefab file from unity - so i can remember-)
            UnityEngine.GameObject jmCape = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/jmcgoddess.prefab");
            //jmCape.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //CopyIntoParent(jmCape.transform, NTcape1);

            //if set in unity to skinmesh rend using melds code from utils 1&2 this wont matter and wont be needed- in this case we are not using it
            
            //jmCape.transform.localScale = new Vector3(1.5f, 2.3f, 1.5f);
            //jmCape.transform.localPosition = new Vector3(0.1f, 1, 0.3f);


            //calling the "different bone attaches" from unity into the mod
            
            CopyIntoParent(jmCape.transform.Find("attach_Hips"), NTcape1.parent);
            CopyIntoParent(jmCape.transform.Find("attach_LeftShoulder"), NTcape1.parent);
            CopyIntoParent(jmCape.transform.Find("attach_RightShoulder"), NTcape1.parent);
            CopyIntoParent(jmCape.transform.Find("attach_Head"), NTcape1.parent);
            //only line needed for the stuff in unity's hierarchy that you dont want to "stick" to the player
            CopyIntoParent(jmCape.transform.Find("attach_skin"), NTcape1);


        }

        // Add new status effects
        private static SE_Stats InfinityEffect(Sprite sprite)
        {
            //cant use this effect with feath cape because it gets rid of the feather fall effect :[ so we commented out the call-in
            SE_Stats infinityEffect = new SE_Stats();
            infinityEffect.name = "InfinityEffect";
            infinityEffect.m_name = "Infinity";
            infinityEffect.m_startMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_startMessage = "You Grasp Hold of Infinity";
            infinityEffect.m_stopMessageType = MessageHud.MessageType.Center;
            infinityEffect.m_stopMessage = "Infinity Fades...";
            infinityEffect.m_icon = sprite;
            infinityEffect.m_maxMaxFallSpeed =2f;
            infinityEffect.m_fallDamageModifier = -1;
            infinityEffect.m_staminaRegenMultiplier = 11;
            infinityEffect.m_jumpModifier = new Vector3(0, 5, 0);
            infinityEffect.m_skillLevel = Skills.SkillType.All;
            infinityEffect.m_skillLevelModifier = 25;
            infinityEffect.m_healthRegenMultiplier = 11;
            infinityEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;
            infinityEffect.m_attributes = StatusEffect.StatusAttribute.DoubleImpactDamage;
            infinityEffect.m_runStaminaDrainModifier = -100;

            //CustomStatusEffect infinityCapeEffect = new CustomStatusEffect(infinityEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(infinityCapeEffect);
            return infinityEffect;

        }
        //JayMaeCape Stuff End

        //MeldursonCape Stuff START
        public static void AddMeldCape2()
        {
            ItemConfig tamestickConfig = new ItemConfig();
            tamestickConfig.AddRequirement(new RequirementConfig("HasTamed_Item", 3, 3));
            tamestickConfig.AddRequirement(new RequirementConfig("TrophySGolem", 1));
            tamestickConfig.AddRequirement(new RequirementConfig("WolfPelt", 10, 5));
            tamestickConfig.AddRequirement(new RequirementConfig("DragonEgg", 1, 1));
            //tamestickConfig.AddRequirement(new RequirementConfig("Resin,", 0, 5));
            //tamestickConfig.AddRequirement(new RequirementConfig("Resin", 1));
            tamestickConfig.CraftingStation = CraftingStations.Forge;
            tamestickConfig.MinStationLevel = 1;
            CustomItem capecust = new CustomItem("MeldursonCape", "CapeLinen", tamestickConfig);
            ItemManager.Instance.AddItem(capecust);
            var id = capecust.ItemDrop;
            var id2 = id.m_itemData;
            id2.m_shared.m_name = "Mantle of the Beast King";
            id2.m_shared.m_description = "..The fiercest of beasts became his steeds, the deadliest of predators his allies, and the gentlest of creatures his companions. Through his will alone, he summoned the spirit of the wild, uniting all creatures under the banner of the Beast King...";
            Transform capetransform = Jotunn.Managers.PrefabManager.Instance.GetPrefab("MeldursonCape").transform.Find("attach_skin").transform;
            GameObject sparksOBJ = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/Meldursoncape.prefab");

            Transform newSpark = CopyIntoParent(sparksOBJ.transform, capetransform);

            //Get Linen Cape
            Transform LinenCape = capetransform.Find("cape1");
            SkinnedMeshRenderer LininCapeSKMesh = LinenCape.GetComponent<SkinnedMeshRenderer>();
            Mesh linenMesh = LininCapeSKMesh.sharedMesh;

            //Create New Wide Cape
            //modify mesh to scale from top to bottom
            //x is width, y is depth, z is length
            Vector3 scaleTop = new Vector3(0.6f, 0.9f, 0.5f);
            Vector3 scaleBot = new Vector3(1.9f, 1.9f, 1.9f);
            LininCapeSKMesh.sharedMesh = Utils2.createScaledCape(linenMesh, scaleTop, scaleBot);

            //gets the cloth component of the cape (will have to be modified depending on base cape)
            Cloth capeCloth = LinenCape.GetComponent<Cloth>();
            //Cloth capeCloth = capetransform.Find("WolfCape_Cloth").Find("WolfCape_cloth").GetComponent<Cloth>();


            //add custom activemesh to GameObject and set the mesh to the cloth found in previous step
            ActiveClothMesh activeMesh = newSpark.gameObject.AddComponent<ActiveClothMesh>();
            activeMesh.clothref = capeCloth;
            //activeMesh.partsys = particlesys;
            //create a copy of the capes SkinnedMeshRenderer and set the mesh to the ActiveClothMesh
            //SkinnedMeshRenderer clothSKMesh = capeCloth.transform.GetComponent<SkinnedMeshRenderer>();
            SkinnedMeshRenderer newskinned = newSpark.gameObject.AddComponent(capeCloth.transform.GetComponent<SkinnedMeshRenderer>());
            newskinned.sharedMesh = activeMesh.clothmesh;

            Utils2.updateSKMesh(newSpark, newskinned);

            UnityEngine.Object[] loxtoptexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/MeldLoxCape_D.png");

            SkinnedMeshRenderer clothSKMesh = capeCloth.transform.GetComponent<SkinnedMeshRenderer>();


            //Texture2D MeldCapeTex = tamingAssets.LoadAsset<Texture2D>(Plugin.assetPath + "MeldCape_D.png");
            //Material mat = clothSKMesh.material;
            //mat.SetTexture("_MainTex", MeldCapeTex);
        }
        public static void AddMeldursonCape()
        {

            UnityEngine.Object[] iconsprites = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/MeldCapeIcons.png");
            ItemConfig tamestickConfig = new ItemConfig();
            tamestickConfig.AddRequirement(new RequirementConfig("RawMeat", 1));
            tamestickConfig.AddRequirement(new RequirementConfig(CraftingConditions.tamednearby_prefNname[0], 6, 6));
            tamestickConfig.AddRequirement(new RequirementConfig("Mushroom", 1));
            tamestickConfig.AddRequirement(new RequirementConfig("Carrot", 10, 5));
            tamestickConfig.AddRequirement(new RequirementConfig("DragonEgg", 1, 1));
            
            //tamestickConfig.AddRequirement(new RequirementConfig("Resin", 1));
            tamestickConfig.CraftingStation = "piece_workbench";
            tamestickConfig.MinStationLevel = 1;
            CustomItem capecust = new CustomItem("MeldursonCape", "CapeLinen", tamestickConfig);
            ItemManager.Instance.AddItem(capecust);
            var id = capecust.ItemDrop;
            var id2 = id.m_itemData;
            id2.m_shared.m_name = "Mantle of the Beast King";
            id2.m_shared.m_description = "...The fiercest of beasts became his steeds, the deadliest of predators his allies, and the gentlest of creatures his companions. Through his will alone, he summoned the spirit of the wild, uniting all creatures under the banner of the Beast King...";
            id2.m_shared.m_armor = 4;
            id2.m_shared.m_armorPerLevel = 1;
            id2.m_shared.m_maxDurability = 4000;
            id2.m_shared.m_maxQuality = 7;
            id2.m_shared.m_movementModifier = 0.20f;
            


            //load sprites into icons
            Sprite[] sprites = Array.ConvertAll(iconsprites.Skip(1).ToArray(), item => (Sprite)item);
            id2.m_shared.m_icons = sprites;
            id2.m_shared.m_variants = sprites.Length;

            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Immune;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);


            HitData.DamageModPair frostres = new HitData.DamageModPair();
            frostres.m_modifier = HitData.DamageModifier.VeryResistant;
            frostres.m_type = HitData.DamageType.Frost;
            id2.m_shared.m_damageModifiers.Add(frostres);

            HitData.DamageModPair spiritres = new HitData.DamageModPair();
            spiritres.m_modifier = HitData.DamageModifier.VeryResistant;
            spiritres.m_type = HitData.DamageType.Spirit;
            id2.m_shared.m_damageModifiers.Add(spiritres);

            Transform capetransform = Jotunn.Managers.PrefabManager.Instance.GetPrefab("MeldursonCape").transform.Find("attach_skin").transform;

            GameObject MeldAttach = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/Meldursoncape.prefab");

            Transform newSpark = CopyIntoParent(MeldAttach.transform.Find("attach_skin"), capetransform);

            //Transform beltitems = newSpark.Find("attach_skin").transform.Find("BeltItems").transform;
            //Align sticks to belt and is less resource intensive to calculate as does not use bone weights
            //Utils2.alignMeshAllChidren(beltitems, capetransform);

            //adding lox cape to melds cape- will add additinoal line to remove lox cape bottom - we only want the top- already removed wolf cape top
            Transform loxtop = PrefabManager.Instance.GetPrefab("CapeLox").transform.Find("attach_skin").transform;
            Transform loxtop1 = loxtop.Find("LoxCape").transform;
            Transform Loxtopcopy = CopyIntoParent(loxtop1, capetransform);

            //adding wolf hands to the model - not using
            /*Transform wolfhands = PrefabManager.Instance.GetPrefab("FistFenrirClaw").transform.Find("attach_skin").transform;
            Transform wolfhands1 = wolfhands.Find("WolfClaw2.006").transform;
            Transform wolfhandscopy = CopyIntoParent(wolfhands1, capetransform);

            SkinnedMeshRenderer wolfhandsskinmesh = wolfhandscopy.GetComponent<SkinnedMeshRenderer>();
            Material wolfhandsmat = wolfhandsskinmesh.material;
            wolfhandsmat.color = Color.white;
            UnityEngine.Object[] wolfhandstexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/wolfhandstexture_d.png");
            //Adding textures etc. to the claws
            wolfhandsmat.SetTexture("_MainTex", wolfhandstexture[0] as Texture2D);
            wolfhandsmat.EnableKeyword("_EMISSION");
            wolfhandsmat.SetTexture("_EmissionMap", wolfhandstexture[0] as Texture2D);
            wolfhandsmat.SetColor("_EmissionColor", new Color(0.2f, 0, 0.2f, 1));
            wolfhandsmat.EnableKeyword("_Metallic");
            wolfhandsmat.SetTexture("_MetallicGlossMap", wolfhandstexture[0] as Texture2D);*/


            //Get Linen Cape
            Transform LinenCape = capetransform.Find("cape1");
            SkinnedMeshRenderer LininCapeSKMesh = LinenCape.GetComponent<SkinnedMeshRenderer>();
            Mesh linenMesh = LininCapeSKMesh.sharedMesh;



            //Create New Wide Cape
            //modify mesh to scale from top to bottom
            //x is width, y is depth, z is length
            Vector3 scaleTop = new Vector3(0.6f, 0.9f, 0.5f);
            Vector3 scaleBot = new Vector3(1.9f, 1.9f, 1.9f);
            LininCapeSKMesh.sharedMesh = Utils2.createScaledCape(linenMesh, scaleTop, scaleBot);

            //gets the cloth component of the cape (will have to be modified depending on base cape)
            Cloth capeCloth = LinenCape.GetComponent<Cloth>();
            //Cloth capeCloth = capetransform.Find("WolfCape_Cloth").Find("WolfCape_cloth").GetComponent<Cloth>();
            //add custom activemesh to GameObject and set the mesh to the cloth found in previous step
            ActiveClothMesh activeMesh = newSpark.gameObject.AddComponent<ActiveClothMesh>();
            activeMesh.clothref = capeCloth;




            //ParticleSystem particlesys = newSpark.GetComponent<ParticleSystem>();
            //remove the default wolf shoulders
            //capetransform.Find("WolfCape").gameObject.SetActive(false);

            //adding belt to cape
            Transform sbelt = PrefabManager.Instance.GetPrefab("BeltStrength").transform.Find("attach_skin").transform;
            Transform sbelt1 = sbelt.Find("belt1").transform;
            //added belt into the hierarchy of cape


            

            //adding wolf cape
            //Transform capetransform1 = capetransform.Find("WolfCape_Cloth").Find("WolfCape_cloth").transform;
            Transform sbelt2 = CopyIntoParent(sbelt1, capetransform);
            
            //UnityEngine.GameObject MeldAttach = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/ShawCape.prefab");
            CopyIntoParent(MeldAttach.transform.Find("attach_Hips"), capetransform.parent);
            CopyIntoParent(MeldAttach.transform.Find("attach_LeftShoulder"), capetransform.parent);
            CopyIntoParent(MeldAttach.transform.Find("attach_RightShoulder"), capetransform.parent);
            Transform attach_head = CopyIntoParent(MeldAttach.transform.Find("attach_Head"), capetransform.parent);
            Transform attach_LH = CopyIntoParent(MeldAttach.transform.Find("attach_LeftHand"), capetransform.parent);
            Transform attach_RH = CopyIntoParent(MeldAttach.transform.Find("attach_RightHand"), capetransform.parent);
            Transform attach_RF = CopyIntoParent(MeldAttach.transform.Find("attach_RightFoot"), capetransform.parent);
            Transform attach_LF = CopyIntoParent(MeldAttach.transform.Find("attach_LeftFoot"), capetransform.parent);
            CopyIntoParent(MeldAttach.transform.Find("attach_Spine2"), capetransform.parent);



            //get the materials that we want to add styles to
            Material matWolfHead = attach_head.Find("Crown- head").Find("wolf head spirit hologram").GetComponent<ParticleSystemRenderer>().material;
            //add the style colors for the particles
            Color[] styleColors = new Color[]
            { new Color(0.34f, 0, 0.57f, 0.3f),
                new Color(0.56f, 0, 0.1f, 0.3f),
                new Color(0, 0.2f, 0.02f, 0.3f),
                new Color(1, 0.8f, 0.8f, 0.05f),
                new Color(0.3f, 0, 0, 0.2f),
                new Color(0.5f, 0.2f, 0, 0.2f),
                new Color(0.1f, 0.1f, 0.1f, 0.4f),
                new Color(1, 1, 0.6f, 0.1f)};

            Color[] HologramColors = new Color[]
            { new Color(0.3f, 0.2f, 0.4f, 0.6f),
                new Color(0.45f, 0.05f, 0.1f, 0.6f),
                new Color(0, 0.2f, 0.02f, 0.6f),
                new Color(1, 0.8f, 0.8f, 0.4f),
                new Color(0.3f, 0, 0, 0.5f),
                new Color(0.31f, 0.17f, 0.12f, 0.6f),
                new Color(0.1f, 0.1f, 0.1f, 0.6f),
                new Color(1, 1, 0.6f, 0.4f)};
            matWolfHead.SetColorArray("_StyleTints", HologramColors);
            Transform LH_Claws = attach_LH.Find("hologramClaws");
            //add styles to claws
            for (int i = 0; i < LH_Claws.childCount; i++)
            {
                Material mat = LH_Claws.GetChild(i).GetComponent<ParticleSystemRenderer>().material;
                mat.SetColorArray("_StyleTints", HologramColors);
            }

            Transform RH_Claws = attach_RH.Find("hologramClaws");
            for (int i = 0; i < RH_Claws.childCount; i++)
            {
                Material mat = RH_Claws.GetChild(i).GetComponent<ParticleSystemRenderer>().material;
                mat.SetColorArray("_StyleTints", HologramColors);
            }

            Transform LF_Claws = attach_LF.Find("hologramClaws");
            for (int i = 0; i < LF_Claws.childCount; i++)
            {
                Material mat = LF_Claws.GetChild(i).GetComponent<ParticleSystemRenderer>().material;
                mat.SetColorArray("_StyleTints", HologramColors);
            }
            Transform RF_Claws = attach_RF.Find("hologramClaws");
            for (int i = 0; i < RF_Claws.childCount; i++)
            {
                Material mat = RF_Claws.GetChild(i).GetComponent<ParticleSystemRenderer>().material;
                mat.SetColorArray("_StyleTints", HologramColors);
            }
            //add styles to flames
            attach_LH.Find("flames meld styled").GetComponent<ParticleSystemRenderer>().material.SetColorArray("_StyleGradient0", styleColors);
            attach_RH.Find("flames meld styled").GetComponent<ParticleSystemRenderer>().material.SetColorArray("_StyleGradient0", styleColors);
            attach_LF.Find("flames meld styled").GetComponent<ParticleSystemRenderer>().material.SetColorArray("_StyleGradient0", styleColors);
            attach_RF.Find("flames meld styled").GetComponent<ParticleSystemRenderer>().material.SetColorArray("_StyleGradient0", styleColors);
            //still need to add styles to cape particles,right hand, and feet
            newSpark.Find("Sparkleshine_constant").GetComponent< ParticleSystemRenderer>().material.SetColorArray("_StyleTints", styleColors);
            newSpark.Find("Sparkleshine_flourish").GetComponent<ParticleSystemRenderer>().material.SetColorArray("_StyleTints", styleColors);
            newSpark.Find("Sigil").GetComponent<ParticleSystemRenderer>().material.SetColorArray("_StyleTints", styleColors);


            SkinnedMeshRenderer loxtopskinmesh = Loxtopcopy.GetComponent<SkinnedMeshRenderer>();
            Material loxtopmat = loxtopskinmesh.material;
            loxtopmat.color = Color.white;
            UnityEngine.Object[] loxtoptexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/MeldLoxCape_D.png");
            //Adding textures to the loxtop part of the cape
            

            

            SkinnedMeshRenderer mcskinmesh = LininCapeSKMesh;// capetransform1.GetComponent<SkinnedMeshRenderer>();
           
            SkinnedMeshRenderer mbskinmesh = sbelt2.GetComponent<SkinnedMeshRenderer>();

           

            Material mcmat = mcskinmesh.material;
            
            Material mbmat = mbskinmesh.material;

            mbmat.color = Color.gray;
            //mcmat.color = Color.magenta;
            


            //change folder name in string at the end to match  the new stuff you make for meld in unity
            UnityEngine.Object[] mbelttexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/mbelt1_d.png");
            UnityEngine.Object[] mbeltemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/mbelt1_e.png");
            UnityEngine.Object[] mbeltmetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/mbelt1_m.png");
            UnityEngine.Object[] mbeltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/mbelt1_bm.png");
            UnityEngine.Object[] mcapetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/MeldCapeStyles.png");
            //UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            UnityEngine.Object[] mcapeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/MeldCape_E.png");
            UnityEngine.Object[] mcapemetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/MeldCape_M.png");
            Texture2D MeldShoulderStyleTex = Shawcassets.LoadAsset<Texture2D>("Assets/ShawesomeCapeimage/MeldCapeShoulderColors.png");


            //changes texture of linen cape -since linen cape vanil already has styles enabled
            Utils2.setStyleTex(mcmat, mcapetexture[0] as Texture2D);
            //adding style optin - referencing utils 2
            Loxtopcopy.gameObject.GetOrAddComponent<ItemStyle>();
            loxtopmat.SetTexture("_MainTex", loxtoptexture[0] as Texture2D);
            //adding other textures via unity png name - need to add the variable like 9 line above this
            Utils2.setStyleTex(loxtopmat, MeldShoulderStyleTex);

            /*
            mcmat.EnableKeyword("_USESTYLES_ON");
            mcmat.SetFloat("_Style", 0f);
            mcmat.SetFloat("_UseStyles", 1f);
            mcmat.SetTexture("_StyleTex", mcapetexture[0] as Texture2D);
            */


            /*//Adding textures and emissions to the cape
            mcmat.SetTexture("_StyleTex", mcapetexture[0] as Texture2D);
            mcmat.EnableKeyword("_EMISSION");
            mcmat.EnableKeyword("_Metallic");
            mcmat.SetTexture("_MetallicGlossMap", mcapetexture[0] as Texture2D);
            mcmat.SetTexture("_EmissionMap", mcapetexture[0] as Texture2D);
            mcmat.SetColor("_EmissionColor", new Color(1, 1, 0, 1));
            //scmat.SetTexture("_BumpMap", capebm[0] as Texture2D);*/

            // Adding textures and emissions & bm & metal to the belt
            mbmat.SetTexture("_MainTex", mbelttexture[0] as Texture2D);
            //mbmat.EnableKeyword("_EMISSION");
            mbmat.EnableKeyword("_Metallic");
            //mbmat.SetTexture("_EmissionMap", mbeltemish[0] as Texture2D);
            //mbmat.SetColor("_EmissionColor", new Color(1, 0, 1, 1));
            mbmat.SetTexture("_MetallicGlossMap", mbeltmetal[0] as Texture2D);
            mbmat.SetTexture("_BumpMap", mbeltbm[0] as Texture2D);
            

            //gets the cloth component of the cape (will have to be modified depending on base cape)
            //Cloth capeCloth = capetransform.Find("LoxCape").GetComponent<Cloth>();
            
            

            //create a copy of the capes SkinnedMeshRenderer and set the mesh to the ActiveClothMesh
            SkinnedMeshRenderer newskinned = newSpark.gameObject.AddComponent<SkinnedMeshRenderer>();
            newskinned.sharedMesh = activeMesh.clothmesh;
            Utils2.updateSKMesh(newSpark, newskinned);



            // Add new status effects
            //StatusEffect BeastKingEffect = ScriptableObject.CreateInstance<StatusEffect>();replaced with line under this one
            SE_Stats BeastKingEffect = new SE_Stats();
            BeastKingEffect.m_addMaxCarryWeight = 250;
            BeastKingEffect.m_icon = iconsprites[1] as Sprite;

            BeastKingEffect.m_skillLevel = Skills.SkillType.Sneak;
            BeastKingEffect.m_skillLevelModifier = 40;
            BeastKingEffect.m_skillLevel2 = Skills.SkillType.Knives;
            BeastKingEffect.m_skillLevelModifier2 = 40;
            BeastKingEffect.m_runStaminaDrainModifier = -.5f;
            BeastKingEffect.name = "Beast King";
            BeastKingEffect.m_name = "King of Beast";
            BeastKingEffect.m_startMessageType = MessageHud.MessageType.Center;
            BeastKingEffect.m_startMessage = "All Creatures bow before your Prescense...";
            BeastKingEffect.m_stopMessageType = MessageHud.MessageType.Center;
            BeastKingEffect.m_stopMessage = "The creatures respectfully carry on about their lives...";
            BeastKingEffect.m_fallDamageModifier = -.6f;
            BeastKingEffect.m_attributes = StatusEffect.StatusAttribute.ColdResistance;
            BeastKingEffect.m_jumpStaminaUseModifier = -.5f;
            

            //removed these 2 lines 
            //CustomStatusEffect BeastKing = new CustomStatusEffect(BeastKingEffect, fixReference: false);  // We dont need to fix refs here, because no mocks were used
            //ItemManager.Instance.AddStatusEffect(BeastKing);
            id2.m_shared.m_equipStatusEffect = BeastKingEffect;


        }
        //MeldursonCape Stuff END

        //ShawesomeCape Stuff START
        private static void AddRecipes()
        {
            // Create a custom recipe with a RecipeConfig
            ItemConfig scapeConfig = new ItemConfig();

            scapeConfig.CraftingStation = CraftingStations.Forge;
            scapeConfig.AddRequirement(new RequirementConfig("Flametal", 4, 4));
            scapeConfig.AddRequirement(new RequirementConfig("MushroomYellow", 20));
            scapeConfig.AddRequirement(new RequirementConfig("Thunderstorm_Item", 1, 1));
            scapeConfig.AddRequirement(new RequirementConfig("CapeLinen", 2));
            scapeConfig.AddRequirement(new RequirementConfig("Silver", 4, 4));
            scapeConfig.AddRequirement(new RequirementConfig("TrophyWolf", 1));
            scapeConfig.AddRequirement(new RequirementConfig("Coins", 404, 400));
            scapeConfig.AddRequirement(new RequirementConfig("Chain", 1));
            //this is the only line needed to bring in a new item
            CustomItem ShawesomeCape = new CustomItem("ShawesomeCape", "CapeWolf", scapeConfig);
            //adding the custom item we just made the variable for above
            ItemManager.Instance.AddItem(ShawesomeCape);

            
            

            //StatusEffect dripeffect = AdddripEffects().StatusEffect;

            //Assigning item ID
            var id = ShawesomeCape.ItemDrop;
            var id2 = id.m_itemData;

            // Adding stats
            id2.m_shared.m_name = "Shroud of Shawesome";
            id2.m_shared.m_description = "Crafted by Odin, Consecrated in the Blood of Jörmungandr and Blessed by Thor. Gifts from the Gods to the Mighty Shawesome for his Ascension to the Godly Realm...Sages say Shawesome was blessed to be able to briefly channel the infinite Eitr unseen by mortal eyes...";
            id2.m_shared.m_armor = 4;
            id2.m_shared.m_armorPerLevel = 1;
            id2.m_shared.m_maxDurability = 4000;
            id2.m_shared.m_maxQuality = 7;
            id2.m_shared.m_eitrRegenModifier = 0.44f;
            id2.m_shared.m_weight = 4;
            
            
            id2.m_shared.m_durabilityPerLevel = 400;
            id2.m_shared.m_movementModifier = 0.25f;
            


            

            //add resistances and stuff...
            HitData.DamageModPair poisonres = new HitData.DamageModPair();
            poisonres.m_modifier = HitData.DamageModifier.Resistant;
            poisonres.m_type = HitData.DamageType.Poison;
            id2.m_shared.m_damageModifiers.Add(poisonres);

            HitData.DamageModPair zapres = new HitData.DamageModPair();
            zapres.m_modifier = HitData.DamageModifier.Immune;
            zapres.m_type = HitData.DamageType.Lightning;
            id2.m_shared.m_damageModifiers.Add(zapres);

            //adding the icon in the inventory (pathing via file explorer to the desired file)
            UnityEngine.Object[] scicon = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/Scapeimage.png");
            id2.m_shared.m_icons[0] = scicon[1] as Sprite;

            id2.m_shared.m_equipStatusEffect = AdddripEffects(scicon[1] as Sprite);






        }

        // making copy of sparcs and set the parent of the copy to wherever you want it to be
        public static T CopyIntoParent<T>(T go, T parent) where T : Component
        {
            var CompCopy = InstantiatePrefab.Instantiate(go);
            CompCopy.name = go.name;
            CompCopy.transform.parent = parent.transform;
            CompCopy.transform.localPosition = new Vector3(0, 0, 0);
            return CompCopy;
        }

        public static void Capeofshaw()
        {
            AddRecipes();


            //adding belt to ShawesomeCape
            Transform sbelt = PrefabManager.Instance.GetPrefab("BeltStrength").transform.Find("attach_skin").transform;
            Transform sbelt1 = sbelt.Find("belt1").transform;

            //adding cape to ShawesomeCape
            Transform CapeTransform = PrefabManager.Instance.GetPrefab("ShawesomeCape").transform.Find("attach_skin").transform;
            Transform CapeTransform1 = CapeTransform.Find("WolfCape_Cloth").transform;
            Transform CapeTransform2 = CapeTransform1.Find("WolfCape_cloth").transform;

            

            SkinnedMeshRenderer scskinmesh = CapeTransform2.GetComponent<SkinnedMeshRenderer>();
            //added belt into the hierarchy of cape
            Transform sbeltcopy = CopyIntoParent(sbelt1, CapeTransform1);
            

            //textures, Mesh, Metal, Emission, BM and stuff
            SkinnedMeshRenderer sbskinmesh = sbeltcopy.GetComponent<SkinnedMeshRenderer>();
            Material scmat = scskinmesh.material;
            
            Material sbmat = sbskinmesh.material;
            sbmat.color = Color.red;
            scmat.color = Color.red;
            
            UnityEngine.Object[] belttexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_d.png");
            UnityEngine.Object[] beltemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_e.png");
            UnityEngine.Object[] beltmetal = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_m.png");
            UnityEngine.Object[] beltbm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/sbelt1_bm.png");
            UnityEngine.Object[] capetexture = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_d.png");
            UnityEngine.Object[] capebm = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_bm.png");
            UnityEngine.Object[] capeemish = Shawcassets.LoadAssetWithSubAssets("Assets/ShawesomeCapeimage/ShawCape_e.png");
            
            
            //Adding textures and emissions to the cape
            scmat.SetTexture("_MainTex", capetexture[0] as Texture2D);
            scmat.EnableKeyword("_EMISSION");
            scmat.SetTexture("_EmissionMap", capeemish[0] as Texture2D);
            scmat.SetColor("_EmissionColor", new Color(1, 1, 0, 1));
            //scmat.SetTexture("_BumpMap", capebm[0] as Texture2D);
            // Adding textures and emissions & bm & metal to the belt
            sbmat.SetTexture("_MainTex", belttexture[0] as Texture2D);
            sbmat.EnableKeyword("_EMISSION");
            sbmat.EnableKeyword("_Metallic");
            sbmat.SetTexture("_EmissionMap", beltemish[0] as Texture2D);
            sbmat.SetColor("_EmissionColor", new Color(1, 1, 0, 1));
            sbmat.SetTexture("_MetallicGlossMap", beltmetal[0] as Texture2D);
            sbmat.SetTexture("_BumpMap", beltbm[0] as Texture2D);


            //Making sparcs game object from unity visable 
            UnityEngine.GameObject sparcsAttach = Shawcassets.LoadAsset<GameObject>("Assets/ShawesomeCapeimage/ShawCape.prefab");
            CopyIntoParent(sparcsAttach.transform.Find("attach_Hips"), CapeTransform.parent);
            CopyIntoParent(sparcsAttach.transform.Find("attach_LeftShoulder"), CapeTransform.parent);
            CopyIntoParent(sparcsAttach.transform.Find("attach_RightShoulder"), CapeTransform.parent);
            CopyIntoParent(sparcsAttach.transform.Find("attach_Head"), CapeTransform.parent);
            CopyIntoParent(sparcsAttach.transform.Find("attach_Spine2"), CapeTransform.parent);
            CopyIntoParent(sparcsAttach.transform.Find("attach_LeftHand"), CapeTransform.parent);
            CopyIntoParent(sparcsAttach.transform.Find("attach_RightHand"), CapeTransform.parent);
            CopyIntoParent(sparcsAttach.transform.Find("attach_skin"), CapeTransform.parent);

            
        }

        // Add new status effects
        private static SE_Stats AdddripEffects(Sprite sprite)
        {
            //StatusEffect maxDripEffect = ScriptableObject.CreateInstance<StatusEffect>();
            SE_Stats maxDripEffect = new SE_Stats();
            maxDripEffect.name = "MaximumDrip";
            maxDripEffect.m_name = "Maximum Drip";
            maxDripEffect.m_category = "Demi-God";
            maxDripEffect.m_tooltip = "Your natural Affinity with lightening increases your body's speed and reaction time.";
            maxDripEffect.m_icon = sprite;
            maxDripEffect.m_skillLevel = Skills.SkillType.Swords;
            maxDripEffect.m_skillLevelModifier = 40;
            maxDripEffect.m_skillLevel2 = Skills.SkillType.Spears;
            maxDripEffect.m_skillLevelModifier2 = 40;
            maxDripEffect.m_runStaminaDrainModifier = -.44f;
             
            maxDripEffect.m_startMessageType = MessageHud.MessageType.Center;
            maxDripEffect.m_startMessage = "The Gods Remark at your Impeccable Drip...";
            maxDripEffect.m_stopMessageType = MessageHud.MessageType.Center;
            maxDripEffect.m_stopMessage = "Your Drip Fades...";
            maxDripEffect.m_fallDamageModifier = -0.8f;

            return maxDripEffect;
        }
        //ShawesomeCape Stuff END











        #region Utils

        #endregion

        #region Transpilers

        #endregion

        #region Patches 

        #endregion
    }
}