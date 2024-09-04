using UnityEngine;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

public class GetDataXML : AbsGetData
{

    // Use this for initialization
    void Start()
    {
        GameObject[] objs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        List<GameObject> lobjs = new List<GameObject>();
        lobjs.AddRange(objs);
        lobjs.Sort(delegate (GameObject x, GameObject y)
        {
            if (GetGameObjectPath(x) == null && GetGameObjectPath(y) == null) return 0;
            else if (GetGameObjectPath(x) == null) return -1;
            else if (GetGameObjectPath(y) == null) return 1;
            else return GetGameObjectPath(x).CompareTo(GetGameObjectPath(y));
        });

        XDocument doc = new XDocument(new XElement("app",
            new XAttribute("name", Application.productName),
            new XAttribute("curTime", System.DateTime.Now),
            new XAttribute("deviceType", SystemInfo.deviceType),
            from obj in lobjs
            select new XElement("Object",
                new XAttribute("name", GetGameObjectPath(obj)),
                    from c in obj.GetComponents<Component>()
                    select new XElement("Component",
                        new XAttribute("name", c.GetType().Name),
                        new XAttribute("basetype", c.GetType().BaseType))
                    )
                ));

        Debug.Log("Save!"); 
        doc.Save("E:/objects.xml");
    }

}
