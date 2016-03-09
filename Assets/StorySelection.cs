using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class StorySelection : MonoBehaviour
{
	int right = 0;
	int displayedRight = 0;

	bool Inputable = true;
	int NbKids;
	// Use this for initialization
	void Start ()
	{
		NbKids = GetComponentsInChildren<Image> ().Length - 1;
	}
	
	// Update is called once per frame
	void Update ()
	{
		

		if (Mathf.Abs (Input.GetAxis ("L_XAxis_0")) >= 0.9f && Inputable) {
			Inputable = false;
			Invoke ("InputBack", 0.2f);
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
