using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour
{

	public static StoryManager instance;

	public List<Sprite> sprites = new List<Sprite> ();
	public List<int> spritesKeys = new List<int> ();
	//	public List<bool> spritesUnlock = new List<bool> ();

	void Awake ()
	{
		Object.DontDestroyOnLoad (gameObject);
		if (StoryManager.instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
