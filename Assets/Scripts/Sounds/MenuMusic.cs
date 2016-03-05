using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour
{
    public AudioClip audioclipMusic;
    private GameObject speakerMainMusic;
    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.playSound(audioclipMusic, 100, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
