using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using HarmonyLib;


public static class Utils2
    {
    private const BindingFlags bindingFlags = BindingFlags.Public;


   


    public static T GetCopyOf<T>(this Component comp, T other) where T : Component
    {
        Type type = comp.GetType();
        if (type != other.GetType())
        {
            return null;
        }
        List<Type> list = new List<Type>();
        Type baseType = type.BaseType;
        while (baseType != null && !(baseType == typeof(MonoBehaviour)))
        {
            list.Add(baseType);
            baseType = baseType.BaseType;
        }
        IEnumerable<PropertyInfo> enumerable = type.GetProperties(BindingFlags.Public);
        foreach (Type item in list)
        {
            enumerable = enumerable.Concat(item.GetProperties(BindingFlags.Public));
        }
        enumerable = from property in enumerable
                        where !(type == typeof(Rigidbody)) || !(property.Name == "inertiaTensor")
                        where !property.CustomAttributes.Any((CustomAttributeData attribute) => attribute.AttributeType == typeof(ObsoleteAttribute))
                        select property;
        foreach (PropertyInfo pinfo in enumerable)
        {
            if (pinfo.CanWrite && !enumerable.Any((PropertyInfo e) => e.Name == $"shared{char.ToUpper(pinfo.Name[0])}{pinfo.Name.Substring(1)}"))
            {
                try
                {
                    pinfo.SetValue(comp, pinfo.GetValue(other, null), null);
                }
                catch
                {
                }
            }
        }
        IEnumerable<FieldInfo> enumerable2 = type.GetFields(BindingFlags.Public);
        foreach (FieldInfo finfo in enumerable2)
        {
            foreach (Type item2 in list)
            {
                if (!enumerable2.Any((FieldInfo e) => e.Name == $"shared{char.ToUpper(finfo.Name[0])}{finfo.Name.Substring(1)}"))
                {
                    enumerable2 = enumerable2.Concat(item2.GetFields(BindingFlags.Public));
                }
            }
        }
        foreach (FieldInfo item3 in enumerable2)
        {
            item3.SetValue(comp, item3.GetValue(other));
        }
        enumerable2 = enumerable2.Where((FieldInfo field) => field.CustomAttributes.Any((CustomAttributeData attribute) => attribute.AttributeType == typeof(ObsoleteAttribute)));
        foreach (FieldInfo item4 in enumerable2)
        {
            item4.SetValue(comp, item4.GetValue(other));
        }
        return comp as T;
    }

    public static T AddComponent<T>(this GameObject go, T toAdd) where T : Component
    {
        return go.AddComponent(toAdd.GetType()).GetCopyOf(toAdd);
    }

    public static void updateSKMesh(Transform parent,SkinnedMeshRenderer skmesh)
    {
        List<ParticleSystem> partSystems = parent.GetComponentsInChildren<ParticleSystem>().ToList();
        //partSystems.Add(parent.GetComponent<ParticleSystem>());
        foreach (ParticleSystem sys in partSystems)
        {
            //AllTameable.DBG.blogDebug("sys.name=" + sys.name);
            // if the particle system is not connected to the skinned mesh then will connect them
            if (!(bool)sys.shape.skinnedMeshRenderer)
            {
                //AllTameable.DBG.blogDebug("Setting SKMesh");
                ParticleSystem.ShapeModule partshape = sys.shape;
                partshape.skinnedMeshRenderer = skmesh;
            }

        }
    }


    public static SkinnedMeshRenderer createSkMeshRen(Transform objectTrans, Transform attach_skin)
    {
        if (!objectTrans.gameObject.TryGetComponent(out SkinnedMeshRenderer skmesh))
        {
            skmesh = objectTrans.gameObject.AddComponent<SkinnedMeshRenderer>();
        }
        skmesh.rootBone = attach_skin.Find("Armature").Find("Hips");
        return skmesh;
    }
    public static Mesh createBonedMesh(Mesh newmesh, Mesh copymesh, Vector3 pos, Vector3 rot, Vector3 scale, bool stretch = true)
    {
        Mesh alignedMesh = AlignedSkinMesh(newmesh, pos, rot, scale);
        return copyBoneWeights(alignedMesh, copymesh, stretch);
    }

    public static Mesh AlignedSkinMesh(Mesh meshtomodify, Vector3 pos, Vector3 rot, Vector3 scale)
    {
        Mesh newMesh = Mesh.Instantiate(meshtomodify);
        List<Vector3> verts = new List<Vector3>();
        for (int i = 0; i < meshtomodify.vertices.Count(); i++)
        {
            Vector3 rotVert = Quaternion.Euler(rot) * new Vector3(meshtomodify.vertices[i].x * scale.x, meshtomodify.vertices[i].y * scale.y, meshtomodify.vertices[i].z * scale.z);
            verts.Add(new Vector3(rotVert.x + pos.x, rotVert.y + pos.y, rotVert.z + pos.z));
        }
        newMesh.SetVertices(verts);
        newMesh.name = "copymesh";
        return newMesh;
    }

    public static Mesh copyBoneWeights(Mesh newmesh, Mesh copymesh, bool stretch = true)
    {
        //Vector3 avgloc = Vector3.zero;
        BoneWeight[] bnweight = new BoneWeight[newmesh.vertexCount];
        newmesh.bindposes = copymesh.bindposes;
        if (stretch)
        {
            for (int i = 0; i < newmesh.vertexCount; i++)
            {
                int min_inx = getClosestVert(newmesh.vertices[i], copymesh.vertices);
                bnweight[i] = copymesh.boneWeights[min_inx];
            }
        }
        else
        {
            Vector3 totPos = Vector3.zero;
            for (int i = 0; i < newmesh.vertexCount; i++)
            {
                totPos += newmesh.vertices[i];
            }
            //avgloc = totPos / newmesh.vertexCount;
            int min_inx = getClosestVert(totPos / newmesh.vertexCount, copymesh.vertices);
            for (int i = 0; i < newmesh.vertexCount; i++)
            {
                bnweight[i] = copymesh.boneWeights[min_inx];
            }
        }
        newmesh.boneWeights = bnweight;
        return newmesh;
    }

    public static int getClosestVert(Vector3 vert, Vector3[] basemesh)
    {
        float mindist = 99999;
        int min_inx = -1;
        for (int j = 0; j < basemesh.Length; j++)
        {
            float dist = Vector3.Distance(vert, basemesh[j]);
            if (dist < mindist)
            {
                mindist = dist;
                min_inx = j;
            }
        }
        return min_inx;
    }


    public static void alignMeshAllChidren(Transform objectTrans, Transform basetransform)
    {
        MeshFilter[] filts = objectTrans.GetComponentsInChildren<MeshFilter>();
        ///DBG.blogDebug("filts=" + filts.Length);
        for (int i = 0; i < filts.Length; i++)
        {
            //DBG.blogDebug("AlignObject=" + filts[i].name);
            Transform temptrans = filts[i].transform;
            Vector3 posTot = temptrans.localPosition;
            Vector3 rotTot = temptrans.transform.localEulerAngles;
            Vector3 scaleTot = temptrans.transform.localScale;
            ///DBG.blogDebug("pos=" + (posTot * 10000).ToString());
            ///DBG.blogDebug("rot=" + rotTot.ToString());
            ///DBG.blogDebug("scale=" + (scaleTot * 10000).ToString());
            //DBG.blogDebug("filt2=" + filts[i].name);
            for (int h = 0; h < 20 && temptrans.parent != objectTrans; h++)
            {
                //DBG.blogDebug(h+ ":temptrans.parent=" + temptrans.parent);
                temptrans = temptrans.parent;
                scaleTot = Vector3.Scale(scaleTot, temptrans.localScale);
                posTot = Vector3.Scale(scaleTot, posTot) + temptrans.localPosition;
                rotTot = rotTot + temptrans.localEulerAngles;
            }
            //adjust pos
            //posTot = posTot + new Vector3(0, -0.00007026f, -0.0096f);
            ///DBG.blogDebug("posTot=" + (posTot * 10000).ToString());
            ///DBG.blogDebug("rotTot=" + rotTot.ToString());
            ///DBG.blogDebug("scaleTot=" + (scaleTot * 10000).ToString());
            SkinnedMeshRenderer skmesh = createSkMeshRen(filts[i].transform, basetransform);
            skmesh.sharedMesh = AlignedSkinMesh(skmesh.sharedMesh, posTot, rotTot, scaleTot);

        }
    }
    public static void boneMeshAllChidren(Transform objectTrans, Transform basetransform, Mesh baseMesh, bool stretch = true)
    {
        Transform rootTrans = basetransform.Find("Armature").Find("Hips");
        MeshFilter[] filts = objectTrans.GetComponentsInChildren<MeshFilter>();
        ///DBG.blogDebug("filts=" + filts.Length);
        for (int i = 0; i < filts.Length; i++)
        {
            //DBG.blogDebug("BoneObject=" + filts[i].name + (stretch ? " stretch" : ""));
            Transform temptrans = filts[i].transform;
            Vector3 posTot = temptrans.localPosition;
            Vector3 rotTot = temptrans.transform.localEulerAngles;
            Vector3 scaleTot = temptrans.transform.localScale;
            ///DBG.blogDebug("pos=" + (posTot * 10000).ToString());
            ///DBG.blogDebug("rot=" + rotTot.ToString());
            ///DBG.blogDebug("scale=" + (scaleTot * 10000).ToString());
            //DBG.blogDebug("filt2=" + filts[i].name);
            for (int h = 0; h < 20 && temptrans.parent != objectTrans; h++)
            {
                //DBG.blogDebug(h+ ":temptrans.parent=" + temptrans.parent);
                temptrans = temptrans.parent;
                scaleTot = Vector3.Scale(scaleTot, temptrans.localScale);
                posTot = Vector3.Scale(scaleTot, posTot) + temptrans.localPosition;
                rotTot = rotTot + temptrans.localEulerAngles;
            }
            //adjust pos
            posTot = posTot + new Vector3(0, -rootTrans.localPosition.y, -rootTrans.localPosition.z);
            ///DBG.blogDebug("posTot=" + (posTot * 10000).ToString());
            ///DBG.blogDebug("rotTot=" + rotTot.ToString());
            ///DBG.blogDebug("scaleTot=" + (scaleTot * 10000).ToString());
            SkinnedMeshRenderer skmesh = createSkMeshRen(filts[i].transform, basetransform);
            skmesh.sharedMesh = createBonedMesh(skmesh.sharedMesh, baseMesh, posTot, rotTot, scaleTot, stretch);

        }
    }
    public static void setStyleTex(Material mat, Texture2D styles)
    {
        mat.EnableKeyword("_USESTYLES_ON");
        mat.SetFloat("_Style", 0f);
        mat.SetFloat("_UseStyles", 1f);
        mat.SetTexture("_StyleTex", styles);
    }

    public static Mesh createScaledCape(Mesh originalMesh, Vector3 scaleTop, Vector3 scaleBot, Vector3 offset = new Vector3())
    {
        Vector3 maxvert = Vector3.zero;
        Vector3 minvert = Vector3.one;
        Mesh newMesh = Mesh.Instantiate(originalMesh);
        List<Vector3> verts = new List<Vector3>();
        for (int i = 0; i < originalMesh.vertices.Count(); i++)
        {
            Vector3 vert = originalMesh.vertices[i];
            maxvert = new Vector3(Mathf.Max(maxvert.x, vert.x), Mathf.Max(maxvert.y, vert.y), Mathf.Max(maxvert.z, vert.z));
            minvert = new Vector3(Mathf.Min(minvert.x, vert.x), Mathf.Min(minvert.y, vert.y), Mathf.Min(minvert.z, vert.z));
        }
        //DBG.blogDebug("maxvert=" + (maxvert * 1000).ToString());
        //DBG.blogDebug("minvert=" + (minvert * 1000).ToString());
        for (int i = 0; i < originalMesh.vertices.Count(); i++)
        {
            Vector3 vert = originalMesh.vertices[i];
            float vertx = vert.x * (scaleBot.x + ((scaleTop.x - scaleBot.x) * (vert.z - minvert.z) / (maxvert.z - minvert.z)));
            float verty = vert.y * (scaleBot.y + ((scaleTop.y - scaleBot.y) * (vert.z - minvert.z) / (maxvert.z - minvert.z)));
            Vector3 scaleVert = new Vector3(vertx, verty, vert.z);
            //DBG.blogDebug("verts=" + vertx * 1000 + ", " + verty * 1000 + "diff=" + (vertx - vert.x) * 1000 + ", " + (verty - vert.y) * 1000);
            verts.Add(new Vector3(scaleVert.x + offset.x, scaleVert.y + offset.y, scaleVert.z + offset.z));
        }
        newMesh.SetVertices(verts);
        newMesh.name = "scaled_" + newMesh.name;
        return newMesh;
    }
    public static void resolveMocksinChildren(Transform parentTrans)
    {
        for (int i = parentTrans.childCount - 1; i >= 0; i--)
        {

            Transform child = parentTrans.GetChild(i);
            //DBG.blogDebug("attempting resolve " + child.name);
            if (!child.name.StartsWith("ATmock_"))
            {
                continue;
            }
            GameObject mockedChild = getmockGo(child.name, parentTrans);
            if (!(bool)mockedChild)
            {
                continue;
            }

            Transform copychild = shawcape.Shawesomes_Divine_Armaments.CopyIntoParent(mockedChild.transform, parentTrans);
            copychild.localPosition = child.localPosition;
            copychild.localEulerAngles = child.localEulerAngles;
            copychild.localScale = child.localScale;
            copychild.name = child.name.Split('.')[0];
            child.parent = null;

        }

    }

    public static GameObject getmockGo(string mockname, Transform newParent)
    {
        //DBG.blogDebug("mockname=" + mockname);
        string[] splitAddress = mockname.Split('.');
        if (!splitAddress[0].StartsWith("ATmock_"))
        {
            //DBG.blogWarning(mockname + " does not start \"with ATmock_\",please start name with this");
            return null;
        }
        Transform tempTrans;
        try { tempTrans = Jotunn.Managers.PrefabManager.Instance.GetPrefab(splitAddress[0].Replace("ATmock_", "")).transform; }
        catch
        {
            //DBG.blogWarning("Failed to find Prefab with name \"" + splitAddress[0].Replace("ATmock_", "") + "\"");
            return null;
        }
        for (int i = 1; i < splitAddress.Length; i++)
        {
            try
            {
                tempTrans = tempTrans.Find(splitAddress[i]).transform;
                //DBG.blogDebug("Found child named " + tempTrans.name);
            }
            catch { //DBG.blogWarning("Did not find child named +" + splitAddress[i]);
                    }
        }
        return tempTrans.gameObject;
        }
    }





    

