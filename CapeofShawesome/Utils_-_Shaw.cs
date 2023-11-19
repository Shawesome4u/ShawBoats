using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HarmonyLib;
using BepInEx;



public class ActiveClothMesh : MonoBehaviour
{

    public Mesh clothmesh = new Mesh();
    public Cloth clothref;
    public float roughness = 0.001f;
    private float lastTime = 0;
    public List<ParticleSystem> partsys;
    // Start is called before the first frame update
    void Start()
    {
        // if not assigned a cloth then will attempt to find one
        if (!(bool)clothref)
        {
            //AllTameable.DBG.blogDebug("Creating Mesh");
            clothref = transform.parent.GetComponentInChildren<Cloth>();
            //AllTameable.DBG.blogDebug("Found Cloth="+ (bool)clothref);
        }
        clothmesh.name = "ClothMesh";
        clothmesh.vertices = clothref.vertices;
        clothmesh.normals = clothref.normals;
        // if not assigned a particle system then will get one

        partsys = gameObject.GetComponentsInChildren<ParticleSystem>().ToList();
        //partsys.Add(gameObject.GetComponent<ParticleSystem>());
        foreach (ParticleSystem sys in partsys)
        {
            //AllTameable.DBG.blogDebug("sys.name="+sys.name);
            // if the particle system is not connected to the skinned mesh then will connect them
            if (!(bool)sys.shape.skinnedMeshRenderer)
            {
                //AllTameable.DBG.blogDebug("Setting SKMesh");
                ParticleSystem.ShapeModule partshape = sys.shape;
                partshape.skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
            }

        }

    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - lastTime > roughness)
        {
            //shawcape.Shawesomes_Divine_Armaments.harmonyLog.LogWarning("lasttime=" + lastTime);
            if ((bool)clothref)
            {
                // updates the moving mesh
                //shawcape.Shawesomes_Divine_Armaments.harmonyLog.LogWarning("updating mesh");
                clothmesh.vertices = clothref.vertices;
                //shawcape.Shawesomes_Divine_Armaments.harmonyLog.LogWarning("got vert");
                clothmesh.normals = clothref.normals;
                //shawcape.Shawesomes_Divine_Armaments.harmonyLog.LogWarning("got norm");
            }
            else
            {
                //shawcape.Shawesomes_Divine_Armaments.harmonyLog.LogWarning("getting cloth");
                //AllTameable.DBG.blogDebug("setting clothref");
                clothref = transform.parent.GetComponentInChildren<Cloth>();
                //AllTameable.DBG.blogDebug("Found Cloth=" + (bool)clothref);
            }
            lastTime = Time.time;
            
        }
        //shawcape.Shawesomes_Divine_Armaments.harmonyLog.LogWarning("end of update");
    }
}



