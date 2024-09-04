using UnityEngine;
using System.Collections;

public class SeatSelectorController : MonoBehaviour
{
    //public float MinDistance;
    //public float MaxDistance;
    //public float GrowRatio;
    public SelectionTechnique[] Interactions;
    public System.Action<int> OnSeatSelected;
    protected int InteractionIndex = 0;

    //protected Transform m_Selected;
    //protected Transform m_Previous;
    //protected Material m_Material;    

    // Use this for initialization
    void Start()
    {
        Interactions[InteractionIndex].OnSeatSelected += OnSeatSelected;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetEnabled()
    {
        Interactions[InteractionIndex].SetEnabled();
    }

    public void SetDisabled()
    {
        Interactions[InteractionIndex].SetDisabled();
    }
}
