using UnityEngine;
using System;
using System.Collections;

public class NavigationTechnique : MonoBehaviour {

    public Action OnNavigationEnd;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public virtual void BeginNavigation(Transform position)
    {

    }
}
