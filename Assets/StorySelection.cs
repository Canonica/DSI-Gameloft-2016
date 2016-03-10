using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class StorySelection : MonoBehaviour
{
	private int j;
	private int right = 0;
	private int displayedRight = 0;

	private bool Inputable = true;
	private int NbKids;

	[SerializeField]
	private Sprite locked;
	// Use this for initialization
	void Start ()
	{
		PlayerPrefs.SetInt ("STORY_KEY_39", 1);

		GameObject template = GetComponentInChildren<Image> ().gameObject;
		Debug.Log (template.name);

		List<int> keys = StoryManager.instance.spritesKeys;
		for (int i = 0; i <= keys.Count - 1; i++) {
			if (PlayerPrefs.GetInt ("STORY_KEY_" + keys [i]) == 1) {
				GameObject n = Instantiate (template) as GameObject;
				n.GetComponent<Image> ().sprite = StoryManager.instance.sprites [i];
				n.transform.parent = transform;
				n.transform.localScale = new Vector3 (1, 1, 1);
			} else {
				GameObject n = Instantiate (template) as GameObject;
				n.GetComponent<Image> ().sprite = locked;
				n.transform.parent = transform;
				n.transform.localScale = new Vector3 (1, 1, 1);
			}
		}

		Destroy (template);
		NbKids = GetComponentsInChildren<Image> ().Length - 2;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Mathf.Abs (Input.GetAxis ("L_XAxis_0")) >= 0.9f && Inputable) {
			Inputable = false;
			Invoke ("InputBack", 0.2f);
            float yAxis = XInput.instance.getYStick(1);
			if (2760 * (int)Mathf.Sign (Input.GetAxis ("L_XAxis_0")) + right >= 0 && right + 2760 * (int)Mathf.Sign (Input.GetAxis ("L_XAxis_0")) <= NbKids * 2760)
				right += 2760 * (int)Mathf.Sign (Input.GetAxis ("L_XAxis_0"));
		}

		displayedRight = (right + displayedRight * 29) / 30;
		GetComponent<GridLayoutGroup> ().padding = new RectOffset (0, displayedRight, 0, 0);
	}

	public void Select (GameObject o)
	{

//		transform.parent.GetComponent<Canvas> ().GetComponent<RectTransform> ().sizeDelta.x;
	}

	void InputBack ()
	{
		Inputable = true;
	}
}
