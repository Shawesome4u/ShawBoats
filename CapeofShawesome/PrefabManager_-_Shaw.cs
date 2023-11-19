using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Utils;
using System.Reflection;
using UnityEngine;

namespace AllTameable
{
    public class PrefabManager : MonoBehaviour
    {
        public static AssetBundle tamingAssets;
        public static ParticleSystem spark;

        private void Awake()
        {
            tamingAssets = AssetUtils.LoadAssetBundleFromResources(Plugin.assetBundleName, Assembly.GetExecutingAssembly());
        }

        public static void ItemReg()
        {

            addCustCape();

            Jotunn.Managers.PrefabManager.OnVanillaPrefabsAvailable -= PrefabManager.ItemReg;

        }
        
        public static void addMeldursonCape()
        {
            


            ItemConfig tamestickConfig = new ItemConfig();
            tamestickConfig.AddRequirement(new RequirementConfig("RawMeat", 1));
            tamestickConfig.AddRequirement(new RequirementConfig("Mushroom", 1));
            tamestickConfig.AddRequirement(new RequirementConfig("Carrot", 10, 5));
            tamestickConfig.AddRequirement(new RequirementConfig("DragonEgg", 1, 1));
            //tamestickConfig.AddRequirement(new RequirementConfig("Resin,", 0, 5));
            //tamestickConfig.AddRequirement(new RequirementConfig("Resin", 1));
            tamestickConfig.CraftingStation = "piece_workbench";
            tamestickConfig.MinStationLevel = 1;
            CustomItem capecust = new CustomItem("MeldursonCape", "CapeLox", tamestickConfig);
            ItemManager.Instance.AddItem(capecust);

            var id = capecust.ItemDrop;
            var id2 = id.m_itemData;
            id2.m_shared.m_name = "MeldursonCape";
            id2.m_shared.m_description = "Hold in hand while attmpting to tame a creature to see its taming requirements";
            Transform capetransform = Jotunn.Managers.PrefabManager.Instance.GetPrefab("MeldersonCape").transform.Find("attach_skin").transform;
            
            GameObject sparksOBJ = Shawcassets.LoadAsset("Assets/ShawesomeCapeimage/Meldursoncape.prefab");

            Transform newSpark = CopyIntoParent(sparksOBJ.transform, capetransform);

            ParticleSystem particlesys = newSpark.GetComponent<ParticleSystem>();

            //gets the cloth component of the cape (will have to be modified depending on base cape)
            Cloth capeCloth = capetransform.Find("LoxCape").GetComponent<Cloth>();
            //Cloth capeCloth = capetransform.Find("WolfCape_Cloth").Find("WolfCape_cloth").GetComponent<Cloth>();

            //add custom activemesh to GameObject and set the mesh to the cloth found in previous step
            ActiveClothMesh activeMesh = CapeTransform.gameObject.AddComponent<ActiveClothMesh>();
            activeMesh.clothref = capeCloth;
            activeMesh.partsys = particlesys;
            

            //create a copy of the capes SkinnedMeshRenderer and set the mesh to the ActiveClothMesh
            SkinnedMeshRenderer newskinned = newSpark.gameObject.AddComponent(capeCloth.transform.GetComponent<SkinnedMeshRenderer>());
            newskinned.sharedMesh = activeMesh.clothmesh;

            
            /*change the particle system shape to SkinnedMeshRenderer (not needed if already set in unity)
            ParticleSystem.ShapeModule particleshape = particlesys.shape;
            particleshape.shapeType = ParticleSystemShapeType.SkinnedMeshRenderer;
            particleshape.scale = Vector3.one;*/

            //Set the partcileshape to the new SkinnedMeshRenderer we made
            particleshape.skinnedMeshRenderer = newskinned;

            /*Change particle system atributes (not needed if already set in unity)
            particlesys.startSize = 0.1f;
            particlesys.startLifetime = 0.2f;
            particlesys.emissionRate = 3; //3
            particlesys.startDelay = 0.2f;
            particlesys.maxParticles = 10; //10*/



        }


        public static T CopyIntoParent<T>(T go, T parent) where T : Component
        {
            var CompCopy = InstantiatePrefab.Instantiate(go);
            CompCopy.name = go.name;
            CompCopy.transform.parent = parent.transform;
            CompCopy.transform.localPosition = new Vector3(0, 0, 0);
            return CompCopy;
        }
    
    }
}
