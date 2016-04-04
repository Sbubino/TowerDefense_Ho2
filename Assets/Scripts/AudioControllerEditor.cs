using UnityEngine;
using System.Collections;
using UnityEditor;


public class AudioControllerEditor : Editor {
	public string[] soundTrack;
	public string[] efx;

	SerializedProperty audioController;

	void OnEnabled()
	{
		audioController = serializedObject.FindProperty("audioController");
	}



}
