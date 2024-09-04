using UnityEngine;
using System.IO;
using System.Collections;

public class LoadSeatData : MonoBehaviour
{
    public GameObject ChairPrefab;
    public TextAsset DataAsset;
    public string OutPuthPath;
    public float ScaleDistance;
    public bool UseVertexDifference;
    public bool IgnoreRotation;

    // Use this for initialization
    void Start()
    {

    }
	
    // Update is called once per frame
    void Update()
    {
        /*if (!IsSaveMode)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform child = this.transform.GetChild(i);
                Vector3[] vertices = child.GetComponent<MeshFilter>().mesh.vertices;
                Debug.DrawLine(child.transform.position + vertices[0], child.transform.position + vertices[1], Color.red);

                vertices = ChairPrefab.GetComponent<MeshFilter>().sharedMesh.vertices;       
                Debug.DrawLine(child.transform.position + vertices[0], child.transform.position + vertices[1], Color.green);
            }
        }*/
    }


    public void Save()
    {        
        string[] info = new string[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);

            float angle = 0;

            if (UseVertexDifference)
            {
                Vector3[] vertices = child.GetComponent<MeshFilter>().mesh.vertices;
                Vector3 dir = (child.position + vertices[1]) - (child.position + vertices[0]);
                vertices = ChairPrefab.GetComponent<MeshFilter>().sharedMesh.vertices;       
                Vector3 dir2 = (child.position + vertices[1]) - (child.position + vertices[0]);
                Vector3 delta = (dir - dir2).normalized;

                angle = Vector3.Angle(dir, dir2);
                float sign = -Mathf.Sign(Vector3.Cross(dir, dir2).y);
                angle = angle * sign;
            }
            else
            {
                angle = child.rotation.eulerAngles.y;
            }

            info[i] = string.Format("{0},{1},{2},{3},{4},{5}", child.position.x, child.position.y, child.position.z, 0, angle, 0);
        }

        File.WriteAllLines(OutPuthPath, info); 
    }

    public void Load(Vector3 offset)
    {
        StringReader reader = new StringReader(DataAsset.text);

        string info = reader.ReadLine();

        while (info != null)
        {
            string[] data = info.Split(',');
            Vector3 pos = new Vector3(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2])) * ScaleDistance;
            Vector3 euler = IgnoreRotation ? Vector3.zero : new Vector3(float.Parse(data[3]), float.Parse(data[4]), float.Parse(data[5]));

            GameObject chair = GameObject.Instantiate(ChairPrefab, pos + offset, Quaternion.Euler(euler)) as GameObject;
            chair.transform.parent = this.transform;

            info = reader.ReadLine();
        }
    }
}
