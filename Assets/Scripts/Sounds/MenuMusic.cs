using UnityEngine;
using System.Collections;

public class MenuMusic : MonoBehaviour
{
    public AudioClip audioclipMusic;
    private GameObject speakerMainMusic;
    // Use this for initialization
    void Start()
    {
        SoundManager.Instance.playSound(audioclipMusic, 0.5f, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
