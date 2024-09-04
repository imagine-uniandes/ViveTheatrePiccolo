using UnityEngine;
using System.Collections;

public class ComputeSeatDistances : MonoBehaviour {
    public string[] SeatSetA;
    public string[] SeatSetB;
    public Transform StartPosition;
    public Transform Seats;
    protected bool IsComputed = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (!IsComputed && Seats.childCount > 0)
        {
            ComputeDistances(SeatSetA);
            ComputeDistances(SeatSetB);
            IsComputed = true;
        }
    }

    void ComputeDistances(string[] SeatSet)
    {
        float dx, dy, dz, dist, angle;

        for (int i = 0; i < SeatSet.Length; i++)
        {
            Transform seat = Seats.Find(SeatSet[i]);
            if (i == 0)
            {
                dx = Mathf.Abs(seat.position.x - StartPosition.position.x);
                dy = Mathf.Abs(seat.position.y - StartPosition.position.y);
                dz = Mathf.Abs(seat.position.y - StartPosition.position.z);
                dist = Vector3.Distance(seat.position, StartPosition.position);
                angle = Mathf.Atan2(seat.position.z - StartPosition.position.z, seat.position.x - StartPosition.position.x) * Mathf.Rad2Deg;
                Debug.Log(string.Format("{0},dist={1},angle={2}", i, dist, angle));
            }
            else
            {
                Transform previous = Seats.Find(SeatSet[i - 1]);
                dx = Mathf.Abs(seat.position.x - previous.position.x);
                dy = Mathf.Abs(seat.position.y - previous.position.y);
                dist = Vector3.Distance(seat.position, previous.position);
                angle = Mathf.Atan2(seat.position.z - previous.position.z, seat.position.x - previous.position.x) * Mathf.Rad2Deg;
                Debug.Log(string.Format("{0},dist={1},angle={2}", i, dist, angle));
            }
        }

    }
}
