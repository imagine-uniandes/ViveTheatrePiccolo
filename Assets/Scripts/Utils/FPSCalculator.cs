using UnityEngine;
using System.Collections;

public class FPSCalculator : MonoBehaviour {
    private const string DISPLAY_TEXT_FORMAT = "{0} msf\n({1} FPS)";
    private const string MSF_FORMAT = "#.#";
    private const float MS_PER_SEC = 1000f;
    private float fps = 60;
    string data = string.Empty;
    

    // Use this for initialization
    void Start () {
	
	}

    void Update()
    {
        if (Input.GetButtonUp("Cancel"))
        {
            Debug.Log("FPS data saved "  + Application.persistentDataPath + "/fps.txt");
            System.IO.File.WriteAllText(Application.persistentDataPath + "/fps.txt", data);
        }
    }

    // Update is called once per frame
    void LateUpdate () {
        float interp = Time.deltaTime / (0.5f + Time.deltaTime);
        float currentFPS = 1.0f / Time.deltaTime;
        fps = Mathf.Lerp(fps, currentFPS, interp);
        float msf = MS_PER_SEC / fps;
        //Debug.Log(string.Format(DISPLAY_TEXT_FORMAT, msf.ToString(MSF_FORMAT), Mathf.RoundToInt(fps)));

        data += string.Format("{0};{1}\r\n", msf, Mathf.RoundToInt(fps));
    }
}
