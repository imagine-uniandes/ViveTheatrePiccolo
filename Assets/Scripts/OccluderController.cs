using UnityEngine;
using System.Collections;

public class OccluderController : MonoBehaviour {

    public float Alpha = 0.15f;
    public string[] OccluderNames;
    public Material TransparentMaterial;
    public RayCastingSelection Selection;    
    protected Material[] m_Materials;
    protected GameObject[] m_Occluders;


    // Use this for initialization
    void Start () {

        m_Materials = new Material[OccluderNames.Length];
        m_Occluders = new GameObject[OccluderNames.Length];

        for (int i = 0; i < OccluderNames.Length; i++)
        {
            GameObject occluder = GameObject.Find(OccluderNames[i]) as GameObject;
            occluder.tag = "Occluder";
            occluder.AddComponent<MeshCollider>();

            m_Occluders[i] = occluder;
            m_Materials[i] = occluder.GetComponent<MeshRenderer>().material;
        }

        Selection.OnOcclusionBegin += OnOcclusionBegin;
        Selection.OnOcclusionEnd += OnOcclusionEnd;
    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnOcclusionBegin(object[] occluders)
    {
        for (int i=0; i < occluders.Length; i++)
        {
            Transform occluder = occluders[i] as Transform;
            occluder.GetComponent<MeshRenderer>().material = TransparentMaterial;
        }
    }

    void OnOcclusionEnd()
    {
        for (int i = 0; i < m_Occluders.Length; i++)
            m_Occluders[i].GetComponent<MeshRenderer>().material = m_Materials[i];
    }
}
