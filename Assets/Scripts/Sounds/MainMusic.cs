using UnityEngine;
using System.Collections;

public class MainMusic : MonoBehaviour
{
    public AudioClip[] audioclipTheme;
    public AudioClip audioclipStart;
    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.playSound(audioclipStart, 0.5f);

        StartCoroutine(BattleMusic());
    }

    IEnumerator BattleMusic()
    {
        yield return new WaitForSeconds(audioclipStart.length - 0.1f);

        while (true)
        {
            int rnd = Random.Range(0, audioclipTheme.Length);
            SoundManager.Instance.playSound(audioclipTheme[rnd], 0.3f);
            yield return new WaitForSeconds(audioclipTheme[rnd].length - 0.1f);
        }

        
    }

    

    // Update is called once per frame
    void Update()
    {

    }
}
