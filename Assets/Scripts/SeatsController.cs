using UnityEngine;
using System.Collections;
using System.IO;

public class SeatsController : MonoBehaviour {

    public SeatSelectorController SeatSelector;
    public TextAsset SeatsMapAsset;
    public Material SelectedMaterial;
    public Material OccupedMaterial;
    public Transform SelectedSeat;
    public GameObject[] Audience;
    public Vector3 Offset;
    public int NumAudience;
    public bool UseSeatNames;

    protected GameObject m_Player;
    protected Material m_Material;

    protected int m_PreviousIndex = -1;
    protected string[] m_SeatMap;

    void Awake()
    {
        if(SeatSelector != null)
            SeatSelector.OnSeatSelected += OnSeatSelected;
    }

	// Use this for initialization
	void Start () {

        m_Player = GameObject.FindGameObjectWithTag("Player");

        LoadSeatData[] data = this.GetComponents<LoadSeatData>();

        for (int i = 0; i < data.Length; i++)
            data[i].Load(Offset);

        SetSeatMapNames();
        SetAudience();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SetSeatMapNames()
    {
        StringReader reader = new StringReader(SeatsMapAsset.text);

        m_SeatMap = new string[transform.childCount];
        string info = reader.ReadLine();
        int i = 0;
        while (info != null)
        {
            string[] data = info.Split(',');
            m_SeatMap[i] = data[2];

            if(UseSeatNames)
                transform.GetChild(i).name = m_SeatMap[i];

            info = reader.ReadLine();
            i++;
        }
    }

    void SetAudience()
    {

        for (int i = 0; i < NumAudience; i++)
        {
            int pos = Random.Range(0, this.transform.childCount);

            Transform seat = this.transform.GetChild(pos);
            if (seat.childCount == 0)
            {
                int index = Random.Range(0, Audience.Length);
                GameObject person = GameObject.Instantiate(Audience[index], seat.transform.position, seat.transform.rotation) as GameObject;

                if (seat.name.StartsWith("chaise"))
                    person.transform.Rotate(Vector3.up, -110);

                person.transform.parent = seat.transform;
            }
        }
    }

    void OnSeatSelected(int index)
    {
        if (index != m_PreviousIndex)
        {
            if (m_PreviousIndex != -1)
                this.transform.GetChild(m_PreviousIndex).GetComponent<MeshRenderer>().material = m_Material;

            SelectedSeat = this.transform.GetChild(index);
            m_Material = SelectedSeat.GetComponent<MeshRenderer>().material;

            if(SelectedSeat.childCount == 0)
                SelectedSeat.GetComponent<MeshRenderer>().material = SelectedMaterial;
            else
                SelectedSeat.GetComponent<MeshRenderer>().material = OccupedMaterial;

            m_PreviousIndex = index;            
        }
    }

    public string GetSeatMapName(int i)
    {
        return m_SeatMap[i];
    }
}
