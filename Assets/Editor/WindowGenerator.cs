using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

class WindowGenerator : EditorWindow
{

	private string _editorName = "Window Generator";
	private string _windowName = "";
	private string _gameobjectPath = "";
	private string _componentType = "";

	[MenuItem ("Window/Window Generator")]
	public static void  ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(WindowGenerator));

	}

	void OnGUI ()
	{
		
		GUIStyle h1 = new GUIStyle (EditorStyles.boldLabel);
		h1.fontSize = 24;
		h1.alignment = TextAnchor.UpperCenter;
		GUIStyle h2 = new GUIStyle (EditorStyles.boldLabel);
		h2.fontSize = 16;
		h2.alignment = TextAnchor.UpperCenter;

		GUILayout.Label (_editorName, h1);

		GUILayout.Label ("Window Name: ");
		_windowName = GUILayout.TextField (_windowName);

		GUILayout.Label ("Game Object Path: ");
		_gameobjectPath = GUILayout.TextField (_gameobjectPath);

		GUILayout.Label ("Component Type: ");
		_componentType = GUILayout.TextField (_componentType);

		if (GUILayout.Button ("Generate!")) {
			if (_windowName != "" && _gameobjectPath != "" && _gameobjectPath != "") {
				StreamReader tempalteReader = new System.IO.StreamReader (Application.dataPath + "/Editor/TemplateWindow.cs");
				string tempalte = tempalteReader.ReadToEnd ();

				tempalte = tempalte.Replace ("TempalteWindow", _windowName);
				tempalte = tempalte.Replace ("ReplacePath", _gameobjectPath);
				tempalte = tempalte.Replace ("Int64", _componentType);
				if (File.Exists (Application.dataPath + "/Editor/" + _windowName + ".cs")) {
					File.Delete (Application.dataPath + "/Editor/" + _windowName + ".cs");
				}
				File.WriteAllText (Application.dataPath + "/Editor/" + _windowName + ".cs", tempalte);
				AssetDatabase.Refresh ();
			}
		}

		GUILayout.Space (20);


		GUILayout.BeginHorizontal ();

		GUILayout.EndHorizontal ();

	}

}
