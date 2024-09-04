using UnityEngine;
using System.Collections;
using System.IO;

public class ChairMapData : MonoBehaviour {

    public TextAsset[] DataAssets;
    public TextAsset MapAsset;
    public GameObject Cube;
    public Transform Parent;
    public float Size;
    public float Scale;
    public string FileName;
    public bool IsSaveMode;

	// Use this for initialization
	void Start () {
        if (IsSaveMode)
            SaveMap();
        else
            LoadMap();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SaveMap()
    {
        string[] data = null;
        string line = string.Empty;
        int count = 0;
        Vector3 pos = Vector3.zero;
        StringReader reader = null;
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);

        for (int i = 0; i < DataAssets.Length; i++)
        {
            reader = new StringReader(DataAssets[i].text);
            //Compute bounds

            line = reader.ReadLine();
            while (line != null)
            {
                data = line.Split(',');
                pos = new Vector3(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]));
                bounds.Encapsulate(pos);
                count++;
                line = reader.ReadLine();
            }

            reader.Close();
        }

        //calculate normal positions and save
        string[] result = new string[count];

        for (int i = 0; i < DataAssets.Length; i++)
        {          
            reader = new StringReader(DataAssets[i].text);
            line = reader.ReadLine();
            while (line != null)
            {
                data = line.Split(',');
                pos = new Vector3(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]));
                pos = new Vector3(pos.x / bounds.size.x, pos.y / bounds.size.y, pos.z / bounds.size.z);
                result[i] = string.Format("{0},{1},{2}", pos.x, pos.y, pos.z);
                i++;
                line = reader.ReadLine();
            }
        }

        File.WriteAllLines(FileName, result);
    }

    public void LoadMap()
    {
        StringReader reader = new StringReader(MapAsset.text);
        string line = reader.ReadLine();

        while (line != null)
        {
            string[] data = line.Split(',');
            Vector3 pos = new Vector3(float.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2]));
            GameObject chair = GameObject.Instantiate(Cube, pos, Quaternion.identity) as GameObject;
            chair.transform.localScale = Vector3.one * Scale;
        
            chair.transform.parent = Parent;
            line = reader.ReadLine();
        }
    }
}
