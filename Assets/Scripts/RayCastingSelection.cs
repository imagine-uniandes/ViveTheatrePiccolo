using UnityEngine;
using System;
using System.Collections;

public class RayCastingSelection : SelectionTechnique
{
    public float MaxDistance = 15;
    public Action<object[]> OnOcclusionBegin;
    public Action OnOcclusionEnd;

    protected bool m_hasSelectedSeat = false;
    protected bool m_IsEnabled = false;
    protected Transform m_SelectedSeat;
    protected ArrayList m_Occluders;

    // Use this for initialization
    void Start () {
        m_Occluders = new ArrayList();
    }

    // Update is called once per frame
    void Update () {

        if (m_IsEnabled)
        {

            RaycastHit[] hits = Physics.RaycastAll(this.transform.position, this.transform.forward, MaxDistance);
			this.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
		    this.GetComponent<LineRenderer>().SetPosition(1, this.transform.position + this.transform.forward * MaxDistance);
            this.GetComponent<LineRenderer>().SetPosition(1, this.transform.position + this.transform.forward * MaxDistance);
           
            m_hasSelectedSeat = false;

            m_Occluders.Clear();

            for (int i = 0; i < hits.Length; i++)
            {
                if (!m_hasSelectedSeat && hits[i].collider.tag == "Seat")
                {                  
                    m_SelectedSeat = hits[i].collider.transform;
                    m_hasSelectedSeat = true;
                }

                if (hits[i].collider.tag == "Occluder")
                    m_Occluders.Add(hits[i].transform);
            }

            if (m_Occluders.Count > 0 && OnOcclusionBegin != null)
                OnOcclusionBegin(m_Occluders.ToArray());
            else if(OnOcclusionEnd != null)  
                OnOcclusionEnd();

            if (m_hasSelectedSeat)
                OnSeatSelected(m_SelectedSeat.GetSiblingIndex());
        }
    }

    public override void SetEnabled()
    {
        base.SetEnabled();
        m_IsEnabled = true;
        this.GetComponent<LineRenderer>().enabled = true;
    }

    public override void SetDisabled()
    {
        base.SetDisabled();
        m_IsEnabled = false;
        this.GetComponent<LineRenderer>().enabled = false;
    }
}
