using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpellSelection : MonoBehaviour {

    public GameObject prefabSlot;
    public GameObject spellInventory;
    public Image[] imageArray;

    public int nbOfSpellsByLine;

    public int nbOfLines;

    public int x = -375;
    public int y = 175;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ShowSpell()
    {
        for(int i =0; i < imageArray.Length; i++)
        {
            for(int j = 0; j<nbOfLines; j++)
            {
                GameObject slots = Instantiate(prefabSlot) as GameObject;
                slots.transform.parent = spellInventory.transform;
                slots.GetComponent<RectTransform>().localPosition = new Vector3();
            }
        }
    }
}
