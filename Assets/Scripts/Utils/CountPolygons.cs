using UnityEngine;
using System.IO;
using System.Collections;

public class CountPolygons : MonoBehaviour {

	// Use this for initialization
	void Start () {

        MeshFilter[] filters = this.GetComponentsInChildren<MeshFilter>();
        string[] lines = new string[filters.Length];

        for (int i = 0; i < filters.Length; i++)
            lines[i] = string.Format("{0};{1}", filters[i].gameObject.name, filters[i].mesh.triangles.Length);

        File.WriteAllLines("d:/polygons.csv", lines);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
