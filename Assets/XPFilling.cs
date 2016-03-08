using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class XPFilling : MonoBehaviour
{

	private Image Mana_Full;

	[SerializeField]
	private Motherbase Motherbase;
	// Use this for initialization
	void Start ()
	{
		Mana_Full = gameObject.GetComponentsInChildren<Image> () [1];
		Mana_Full.fillAmount = 0.5f;

	}
	
	// Update is called once per frame
	void Update ()
	{
		for (int i = 0; i <= Motherbase.experienceLevel.Count - 1; i++) {
			if (Motherbase.experienceLevel [i] > 0) {
				Mana_Full.fillAmount = (1f - (Motherbase.experienceLevel [i] * 1f / Motherbase.maxExperienceLevel [i] * 1f));
				i = Motherbase.experienceLevel.Count;
			}
		}
	}
}
