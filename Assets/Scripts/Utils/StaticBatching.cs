using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StaticBatching : MonoBehaviour {

    protected bool m_IsOptimized = false; 
	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

        if (!m_IsOptimized && this.transform.childCount > 0)
        {
            GameObject[] childs = new GameObject[this.transform.childCount];

            for (int i = 0; i < this.transform.childCount; i++)
                childs[i] = this.transform.GetChild(i).gameObject;
            
            StaticBatchingUtility.Combine(childs, this.gameObject);

            m_IsOptimized = true;
        }       
    }
}
