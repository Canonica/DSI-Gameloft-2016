using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

class TempalteWindow : EditorWindow
{
	// [START_REPLACE]
	private string _editorName = "TempalteWindow";
	private string _loadingPath = "ReplacePath";
	private Type type = typeof(Int64);

	[MenuItem ("Window/TempalteWindow")]
	public static void  ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(TempalteWindow));

	}
	// [END_REPLACE]


	private int _fieldWidth = 128;

	private List<FieldInfo> _values = new List<FieldInfo> ();

	private string[] _prefabs;
	private List<GameObject> _gameObjects = new List<GameObject> ();

	private int tick = 0;
	private bool loaded = false;
	private Vector2 scrollPos = Vector2.zero;






	void FetchValues ()
	{
		_values.Clear ();

		System.Reflection.FieldInfo[] fields = type.GetFields ();

		for (int i = 0; i <= fields.Length - 1; i++) {
			if (fields [i].GetCustomAttributes (typeof(Tweakable), true).Length == 1) {
				_values.Add (fields [i]);
			}
		}

	}

	void OnFocus ()
	{
		LoadPrefabs ();
		FetchValues ();		
		loaded = true;
	}

	void OnGUI ()
	{
		tick++;

		// Assets loading
		if (tick == 0 || tick % 60 == 0 || !loaded) {
			LoadPrefabs ();
			FetchValues ();		
			loaded = true;
		}


		// Styles creation
		GUIStyle h1 = new GUIStyle (EditorStyles.boldLabel);
		h1.fontSize = 24;
		h1.alignment = TextAnchor.UpperCenter;
		GUIStyle h2 = new GUIStyle (EditorStyles.boldLabel);
		h2.fontSize = 16;
		h2.alignment = TextAnchor.UpperCenter;


		if (loaded) {
			int perLine = (int)(Mathf.Floor (position.width / 280f)); 
			GUILayout.Label (_editorName, h1);

			GUILayout.Space (20);

			scrollPos = EditorGUILayout.BeginScrollView (scrollPos, GUILayout.MinWidth (position.width));
			GUILayout.BeginHorizontal ();
			for (int i = 0; i <= _gameObjects.Count - 1; i++) {
				string life;
				Component u = _gameObjects [i].GetComponent (type.ToString ());

				GUILayout.BeginVertical (GUILayout.MaxWidth (280));
				GUILayout.Label (u.name, h2);


				for (int j = 0; j <= _values.Count - 1; j++) {
					GUILayout.BeginHorizontal ();
					if (_values [j].Name == "img" && _values [j].GetValue (u).GetType () == typeof(string)) {
						/*
						 * THUMBNAIL
						 * */
						Texture2D image = new Texture2D (256, 256);

						image = AssetDatabase.LoadAssetAtPath (_values [j].GetValue (u).ToString (), typeof(Texture2D)) as Texture2D;
						GUILayout.Label (image);
					} else if (_values [j].GetValue (u).GetType () == typeof(int)) {
						/*
						 * INT
						 */
						GUILayout.Label (varNameToLabel (_values [j].Name) + ": ", GUILayout.Width (_fieldWidth)); 
						_values [j].SetValue (u, int.Parse (GUILayout.TextField (_values [j].GetValue (u).ToString (), GUILayout.MinWidth (256 - 56 - _fieldWidth))));		

						if (GUILayout.Button ("+", GUILayout.Width (24))) {
							int nv = int.Parse (_values [j].GetValue (u).ToString ()) + 1;
							_values [j].SetValue (u, nv);
						}

						if (GUILayout.Button ("-", GUILayout.Width (24))) {
							int nv = int.Parse (_values [j].GetValue (u).ToString ()) - 1;
							_values [j].SetValue (u, nv);
						}
					} else if (_values [j].GetValue (u).GetType () == typeof(float)) {
						/*
						 * FLOAT
						 */
						GUILayout.Label (varNameToLabel (_values [j].Name) + ": ", GUILayout.Width (_fieldWidth)); 

						_values [j].SetValue (u, (EditorGUILayout.FloatField (float.Parse (_values [j].GetValue (u).ToString ()), GUILayout.Width (256 - 56 - _fieldWidth))));		

						if (GUILayout.Button ("+", GUILayout.Width (24))) {
							float nv = float.Parse (_values [j].GetValue (u).ToString ()) + 0.1f;
							_values [j].SetValue (u, nv);
							GUI.FocusControl ("");
						}

						if (GUILayout.Button ("-", GUILayout.Width (24))) {
							float nv = float.Parse (_values [j].GetValue (u).ToString ()) - 0.1f;
							_values [j].SetValue (u, nv);
							GUI.FocusControl ("");
						}
					}



					GUILayout.EndVertical ();
				}



				GUILayout.EndVertical ();

				if ((i + 1) % perLine == 0) {
					GUILayout.EndVertical ();
					GUILayout.BeginHorizontal ();
				}
			}
			EditorGUILayout.EndScrollView ();

		}
	}

	void LoadPrefabs ()
	{
		_prefabs = Directory.GetFiles (Application.dataPath + "/" + _loadingPath, "*.prefab", SearchOption.AllDirectories);
		_gameObjects = new List<GameObject> ();

		foreach (string p in _prefabs) {
			string pPath = "Assets" + p.Replace (Application.dataPath, "").Replace ("\\", "/");
			GameObject g = AssetDatabase.LoadAssetAtPath (pPath, typeof(GameObject)) as GameObject;
			_gameObjects.Add (g);
		}
	}

	string varNameToLabel (string s)
	{
		string r = s.Replace ("_", "");
		r = r.Substring (0, 1).ToUpper () + r.Substring (1, r.Length - 1);
		string[] rArray = Regex.Split (r, "(?=[A-Z])");
		return string.Join (" ", rArray);
	}

}
