using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VivePlayerController : MonoBehaviour {
    public SeatsController Seat;
    public SteamVRInputController VRInput;
    public GameObject PositionAvatar;
    public float AngularSpeed;
	public float MaxSpeed;

    protected GameObject m_Avatar;
    protected Transform m_PreviousSelection;

    // Use this for initialization
    void Start () {
        OmniManager omni = new OmniManager();
        if(!omni.FindOmni())
        {
            this.GetComponent<OmniMovementComponent>().enabled = false;
            this.GetComponent<OmniMovementCalibration>().enabled = false;
            this.GetComponent<OmniController_Example>().enabled = false;
        }

        m_Avatar = GameObject.Instantiate(PositionAvatar, this.transform.position, Quaternion.identity) as GameObject;
        m_Avatar.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if(Vector3.Distance(this.transform.position, m_Avatar.transform.position) < 1)
            m_Avatar.SetActive(false);

        if (Seat.SelectedSeat != null && m_PreviousSelection != Seat.SelectedSeat)
        {
            SetAvatar();
            m_PreviousSelection = Seat.SelectedSeat;
        }

        if (Seat.SelectedSeat != null && (VRInput.GetAnyTriggerUp() || Input.GetMouseButtonUp(0)))
        {		
			this.transform.rotation = Seat.SelectedSeat.rotation; 
			this.transform.Rotate (this.transform.up, -90.0f);
            this.transform.position = Seat.SelectedSeat.position;

            UnityEngine.XR.InputTracking.Recenter();
            this.GetComponent<CharacterController>().Move(Vector3.one * 0.01f);
            m_Avatar.SetActive(false);
        }

		float moveHorizontal = VRInput.GetAnyAxis ().x;
		float moveVertical = VRInput.GetAnyAxis ().y;

		this.transform.Rotate(this.transform.up, moveHorizontal * AngularSpeed * Time.deltaTime);

		Vector3 forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
		this.GetComponent<CharacterController>().Move(moveVertical * Camera.main.transform.forward * MaxSpeed * Time.deltaTime);

		if(this.GetComponentInChildren<RayCastingSelection>() != null)
			this.GetComponentInChildren<RayCastingSelection>().SetEnabled ();
	}

    void SetAvatar()
    {
        if(!m_Avatar.activeSelf)
            m_Avatar.SetActive(true);

        m_Avatar.transform.position = Seat.SelectedSeat.position + Vector3.up;
        m_Avatar.transform.rotation = Seat.SelectedSeat.rotation;

        if (Seat.SelectedSeat.name.StartsWith("fauteuil"))
            m_Avatar.transform.Rotate(Vector3.up, -90);
        else
            m_Avatar.transform.Rotate(Vector3.up, 180);
    }
}
