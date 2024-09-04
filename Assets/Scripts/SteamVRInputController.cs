using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVRInputController : MonoBehaviour
{
    public SteamVR_TrackedObject LeftController;
    public SteamVR_TrackedObject RightController;

    protected SteamVR_Controller.Device m_LeftInput;
    protected SteamVR_Controller.Device m_RightInput;

    // Use this for initialization
    void Start()
    {
#if UNITY_EDITOR
        m_LeftInput = null;
        m_RightInput = null;
#else
        m_LeftInput = SteamVR_Controller.Input((int)LeftController.index);
        m_RightInput = SteamVR_Controller.Input((int)RightController.index);
#endif

    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        m_LeftInput = null;
        m_RightInput = null;
#else   
        if(InputReady())
        {     
            m_LeftInput = SteamVR_Controller.Input((int)LeftController.index);
            m_RightInput = SteamVR_Controller.Input((int)RightController.index);
        }
#endif

    }

    public bool InputReady()
    {        
        return LeftController.index != SteamVR_TrackedObject.EIndex.None && RightController.index != SteamVR_TrackedObject.EIndex.None;
    }

    public Vector2 GetAnyAxis()
    {
        return GetLeftAxis() + GetRightAxis();
    }

    public bool GetAnyTriggerUp()
    {
        return GetLeftTriggerUp() || GetRightTriggerUp();
    }

    public bool GetAnyTriggerDown()
    {
        return GetLeftTriggerUp() || GetRightTriggerUp();
    }

    public Vector2 GetLeftAxis()
    {
        float x = (m_LeftInput != null && m_LeftInput.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad)) ? m_LeftInput.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x : 0;
        float y = (m_LeftInput != null && m_LeftInput.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad)) ? m_LeftInput.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y : 0;

        return new Vector2(x, y);
    }

    public Vector2 GetRightAxis()
    {
        float x = (m_RightInput != null && m_RightInput.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad)) ? m_RightInput.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).x : 0;
        float y = (m_RightInput != null && m_RightInput.GetPress(EVRButtonId.k_EButton_SteamVR_Touchpad)) ? m_RightInput.GetAxis(EVRButtonId.k_EButton_SteamVR_Touchpad).y : 0;

        return new Vector2(x, y);
    }

    public bool GetLeftTriggerUp()
    {
        return (m_LeftInput != null && m_LeftInput.GetHairTriggerUp());
    }

    public bool GetRightTriggerUp()
    {
        return (m_RightInput != null && m_RightInput.GetHairTriggerUp());
    }

    public bool GetRightTriggerDown()
    {
        return (m_RightInput != null && m_RightInput.GetHairTriggerDown());
    }

    public bool GetLeftTriggerDown()
    {
        return (m_LeftInput != null && m_LeftInput.GetHairTriggerDown());
    }

    public Vector3 GetLeftPosition()
    {
        return LeftController.transform.position;
    }

    public Vector3 GetRightPosition()
    {
        return RightController.transform.position;
    }

    public Quaternion GetLeftOrientation()
    {
        return LeftController.transform.rotation;
    }

    public Quaternion GetRightOrientation()
    {
        return RightController.transform.rotation;
    }

    public bool GetLeftButtonUp(EVRButtonId button)
    {
        return (m_LeftInput != null && m_LeftInput.GetPressUp(button));
    }

    public bool GetRightButtonUp(EVRButtonId button)
    {
        return (m_RightInput != null && m_RightInput.GetPressUp(button));
    }

    public bool GetButtonUp(EVRButtonId button)
    {
        return GetLeftButtonUp(button) || GetRightButtonUp(button);
    }
}

