using UnityEngine;
using System.Collections;

public class MainMusic : MonoBehaviour
{
    public AudioClip audioclipMusic;
    private GameObject speakerMainMusic;
    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.playSound(audioclipMusic, 1, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
