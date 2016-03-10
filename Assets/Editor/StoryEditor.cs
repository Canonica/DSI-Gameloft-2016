using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

class StoryEditor : EditorWindow
{
	// [START_REPLACE]
	private string _loadingPath = "Assets/Prefabs/StoryManager.prefab";

	private GameObject rStoryManager;
	private StoryManager _storyManager;

	[MenuItem ("Window/Story Editor")]
	public static void ShowWindow ()
	{
		EditorWindow.GetWindow (typeof(StoryEditor));

	}


	void OnGUI ()
	{
		rStoryManager = AssetDatabase.LoadAssetAtPath ("Assets/Prefabs/StoryManager.prefab", (typeof(GameObject))) as GameObject;
		_storyManager = rStoryManager.GetComponent<StoryManager> ();

//		int c = Mathf.Max (_storyManager.sprites.Count, _storyManager.spritesUnlock.Count);


		GUIStyle h1 = new GUIStyle (EditorStyles.boldLabel);
		h1.fontSize = 24;
		h1.alignment = TextAnchor.UpperCenter;
		GUIStyle h2 = new GUIStyle (EditorStyles.boldLabel);
		h2.fontSize = 16;
		h2.alignment = TextAnchor.UpperCenter;
		GUIStyle alignCenter = new GUIStyle (GUI.skin.label);
		//		alignCenter.alignment = TextAnchor.MiddleCenter;

		GUILayout.Label ("Story Editor", h1);

		GUILayout.Space (20);

		if (GUILayout.Button ("+", GUILayout.Width (40))) {
			PlayerPrefs.SetInt ("STORY_KEY", PlayerPrefs.GetInt ("STORY_KEY") + 1);
			_storyManager.sprites.Add (new Sprite ());
			_storyManager.spritesKeys.Add (PlayerPrefs.GetInt ("STORY_KEY"));
		}

		List<int> keys = _storyManager.spritesKeys;
		for (int i = 0; i <= keys.Count - 1; i++) {
			Debug.Log (keys [i]);
//			if (_storyManager.sprites [i].name != null) {
			GUILayout.BeginHorizontal ();

			GUILayout.Label ("#" + keys [i], GUILayout.Width (40));
//			_storyManager.spritesUnlock [i] = EditorGUILayout.Toggle (_storyManager.spritesUnlock [i], GUILayout.Width (20));
			PlayerPrefs.SetInt ("STORY_KEY_" + keys [i], Convert.ToInt32 (EditorGUILayout.Toggle ((PlayerPrefs.GetInt ("STORY_KEY_" + keys [i]) == 1), GUILayout.Width (20))));
			_storyManager.sprites [i] = EditorGUILayout.ObjectField (_storyManager.sprites [i], typeof(Sprite), false) as Sprite;
			if (GUILayout.Button ("X", GUILayout.Width (20))) {
				_storyManager.sprites.RemoveAt (i);
				_storyManager.spritesKeys.RemoveAt (i);
			}
			GUILayout.EndHorizontal ();
		}

//		}
	}

}
