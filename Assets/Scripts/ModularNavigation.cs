using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;

public class ModularNavigation : MonoBehaviour
{
    private int j;
    public int right = 0;
    private int displayedRight = 0;

    public bool Inputable = false;
    private int NbKids;

    // Use this for initialization
    void Start()
    {
        NbKids = GetComponentsInChildren<Image>().Length-1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis("L_XAxis_0")) >= 0.9f && Inputable)
        {
            Inputable = false;
            StartCoroutine(InputBack(0.5f));
            float yAxis = XInput.instance.getYStick(1);
            if (2760 * (int)Mathf.Sign(Input.GetAxis("L_XAxis_0")) + right >= 0 && right + 2760 * (int)Mathf.Sign(Input.GetAxis("L_XAxis_0")) <= NbKids * 2760)
                right += 2760 * (int)Mathf.Sign(Input.GetAxis("L_XAxis_0"));

        }

        displayedRight = (right + displayedRight * 29) / 30;
        GetComponent<GridLayoutGroup>().padding = new RectOffset(0, displayedRight, 0, 0);
    }

    public void Select(GameObject o)
    {

        //		transform.parent.GetComponent<Canvas> ().GetComponent<RectTransform> ().sizeDelta.x;
    }

    public IEnumerator InputBack(float delay)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + delay)
        {
            yield return null;
        }

        Inputable = true;
    }
}
