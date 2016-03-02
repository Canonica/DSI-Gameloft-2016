using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BeforeFight : MonoBehaviour
{

    public int m_second = 3;

    private Text _text;
    private float _alpha;
    public bool _ready=false;

    // Use this for initialization
    void Start()
    {
        _text = GetComponent<Text>();
        StartCoroutine(StartCDBeforeFight());
    }


    IEnumerator StartCDBeforeFight()
    {
        float timer = 5;
        while (m_second >= 0)
        {
            timer += Time.deltaTime * 500;
            yield return new WaitForSeconds(1f);
            if(m_second == 3)
            {
                
                _text.CrossFadeAlpha(1f, 0f, false);
                _text.CrossFadeAlpha(0.1f, 0.9f, false);
            }
            else if (m_second == 2)
            {
                _text.CrossFadeAlpha(1f, 0f, false);
                _text.CrossFadeAlpha(0.1f, 0.9f, false);
            }
            else if (m_second == 1)
            {
                _text.CrossFadeAlpha(1f, 0f, false);
                _text.CrossFadeAlpha(0.1f, 0.9f, false);
            }
            _text.fontSize += Mathf.RoundToInt(timer);
            _text.text = m_second.ToString();
            _alpha = GetComponent<CanvasGroup>().alpha;
            m_second--;
        }
        _text.CrossFadeAlpha(1f, 0.2f, false);
        
        _text.text = "FIGHT";
        _text.color = Color.red;
        if (_text.text == "FIGHT")
        {
            _text.CrossFadeAlpha(1f, 0f, false);
            _text.CrossFadeAlpha(0f, 1f, false);
        }
        _ready = true;
        yield return new WaitForSeconds(0.5f);
        //GameManager.instance.currentGamestate = GameManager.gameState.Playing;
    }
}
