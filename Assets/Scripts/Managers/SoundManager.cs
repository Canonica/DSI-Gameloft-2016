using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    static SoundManager instance;
    public GameObject speakerPrefab;
    private GameObject speaker;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;

    // getter
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
        instance = this;
        speakerPrefab = Resources.Load<GameObject>("Speaker");
    }

    public void playSound(AudioClip myclip, float volume, bool pLoop = false)
    {
        speaker = (GameObject)Instantiate(speakerPrefab, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = speaker.GetComponent<AudioSource>();
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        audioSource.pitch = randomPitch;
        audioSource.clip = myclip;
        audioSource.Play();
        audioSource.volume = volume;
        audioSource.loop = pLoop;
    }

    public GameObject playTheme(AudioClip myclip, float volume, bool pLoop = false)
    {
        speaker = (GameObject)Instantiate(speakerPrefab, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = speaker.GetComponent<AudioSource>();
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        audioSource.pitch = randomPitch;
        audioSource.clip = myclip;
        audioSource.Play();
        audioSource.volume = volume;
        audioSource.loop = pLoop;
        return speaker;
    }

    public void RandomizeSfx( bool pLoop = false, params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        speaker = (GameObject)Instantiate(speakerPrefab, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = speaker.GetComponent<AudioSource>();
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        audioSource.pitch = randomPitch;
        audioSource.clip = clips[randomIndex];
        audioSource.Play();
        audioSource.loop = pLoop;
    }
}