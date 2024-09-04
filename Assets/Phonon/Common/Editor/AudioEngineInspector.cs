/************************************************************************/
/* Copyright (C) 2011-2015 Impulsonic Inc. All Rights Reserved.         */
/*                                                                      */
/* The source code, information  and  material ("Material") contained   */
/* herein is owned  by Impulsonic Inc. or its suppliers or licensors,   */
/* and title to such  Material remains  with Impulsonic  Inc.  or its   */
/* suppliers or licensors. The Material contains proprietary informa-   */
/* tion  of  Impulsonic or  its  suppliers and licensors. No  part of   */
/* the Material may be used, copied, reproduced, modified, published,   */
/* uploaded, posted, transmitted, distributed or disclosed in any way   */
/* without Impulsonic's prior express written permission. No  license   */
/* under  any patent, copyright or other intellectual property rights   */
/* in the Material is  granted  to  or  conferred  upon  you,  either   */
/* expressly, by implication, inducement, estoppel or otherwise.  Any   */
/* license  under  such intellectual property rights must  be express   */
/* and approved by Impulsonic in writing.                               */
/*                                                                      */
/* Third Party trademarks are the property of their respective owners.  */
/*                                                                      */
/* Unless otherwise  agreed upon by Impulsonic  in  writing, you  may   */
/* not remove or  alter this  notice or any other  notice embedded in   */
/* Materials by Impulsonic or Impulsonic's  suppliers or licensors in   */
/* any way.                                                             */
/************************************************************************/

using UnityEditor;
using UnityEngine;


//
//	AudioEngineInspector
//	Custom inspector for a AudioEngineComponent component.
//

[CustomEditor(typeof(Phonon.AudioEngineComponent))]
public class AudioEngineInspector : Editor
{
	//
	//	Draws the inspector.
	//
	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		Phonon.PhononGUI.SectionHeader("Audio Engine Integration");

		string[] engines = {"Unity Audio", "Audiokinetic Wwise", "FMOD Studio"};
        var audioEngineProperty = serializedObject.FindProperty("audioEngine");
        audioEngineProperty.enumValueIndex = EditorGUILayout.Popup("Audio Engine", audioEngineProperty.enumValueIndex, engines);

        EditorGUILayout.Space();

		serializedObject.ApplyModifiedProperties();
    }

    int EnumValueToPopupIndex(Phonon.AudioEngine enumValue)
	{
		switch (enumValue)
		{
		case Phonon.AudioEngine.Unity:
			return 0;
		case Phonon.AudioEngine.Wwise:
			return 1;
		case Phonon.AudioEngine.FMODStudio:
			return 2;
		default:
			return -1;
		}
	}

	Phonon.AudioEngine PopupIndexToEnumValue(int popupIndex)
	{
		switch (popupIndex)
		{
		case 0:
			return Phonon.AudioEngine.Unity;
		case 1:
			return Phonon.AudioEngine.Wwise;
		case 2:
			return Phonon.AudioEngine.FMODStudio;
		default:
			return Phonon.AudioEngine.Unity;
		}
	}
}