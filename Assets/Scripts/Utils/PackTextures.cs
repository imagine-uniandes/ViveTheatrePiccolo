using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PackTextures : MonoBehaviour
{
    public Texture2D[] Atlas;
    public Material[] Materials;
    public int ImageSize;    
    protected Dictionary<string, Material> m_Materials;
    protected Dictionary<string, List<MeshFilter>> m_Filters;
    protected bool m_IsPacking = false;


    // Use this for initialization
    void Start()
    {
        m_Materials = new Dictionary<string, Material>();
        m_Filters = new Dictionary<string, List<MeshFilter>>();

        Pack();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Pack()
    {
        MeshRenderer[] renderers = this.GetComponentsInChildren<MeshRenderer>();
        MeshFilter[] filters = this.GetComponentsInChildren<MeshFilter>();

        for (int i = 0; i < renderers.Length; i++)
        {
            Material material = renderers[i].material;

            if (!m_Materials.ContainsKey(material.name) && material.mainTexture != null)
            {
                m_Materials.Add(material.name, material);
                m_Filters.Add(material.name, new List<MeshFilter>());
            }

            if(m_Materials.ContainsKey(material.name))
                m_Filters[material.name].Add(filters[i]);
        }

        List<Material> materials = new List<Material>();
        foreach (Material material in m_Materials.Values)
        {
            materials.Add(material);
        }

        Debug.Log(materials.Count);

        PackGroup(materials.GetRange(0, 45).ToArray(), 0);
        PackGroup(materials.GetRange(46, 45).ToArray(), 1);       
    }

    void PackGroup(Material[] materials, int index)
    {
        Texture2D[] textures = new Texture2D[materials.Length];

        for (int i = 0; i < materials.Length; i++)
            textures[i] = materials[i].mainTexture as Texture2D;

        Atlas[index] = new Texture2D(ImageSize, ImageSize, TextureFormat.ETC2_RGB, false);
        Rect[] rect = Atlas[index].PackTextures(textures, 0, ImageSize);

        //byte[] data = Atlas[index].EncodeToPNG();
        //System.IO.File.WriteAllBytes(string.Format("E:/Textures{0}.png", index), data);

        Materials[index].mainTexture = Atlas[index];

        for (int i = 0; i < materials.Length; i++)
        {
            MeshFilter[] filters = m_Filters[materials[i].name].ToArray();

            for (int j = 0; j < filters.Length; j++)
            {
                Vector2[] uva = filters[j].mesh.uv;

                Vector2[] uvb = new Vector2[uva.Length];
                for (int k = 0; k < uva.Length; k++)
                    uvb[k] = new Vector2((uva[k].x * rect[i].width) + rect[i].x, (uva[k].y * rect[i].height) + rect[i].y);

                filters[j].mesh.uv = uvb;
                filters[j].gameObject.GetComponent<MeshRenderer>().material = Materials[index];
            }
        }
    }
}
