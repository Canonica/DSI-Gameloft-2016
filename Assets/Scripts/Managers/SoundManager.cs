using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioClip swarmSFX;
    static SoundManager instance;
    public GameObject speakerPrefab;
    private GameObject speaker;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    float swarmCount = 0;
    float lastAttack = 0;
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
        swarmCount = 0;
        StartCoroutine(swarmGroupSound());
        instance = this;
        speakerPrefab = Resources.Load<GameObject>("Speaker");
    }


    public void swarmSound()
    {
        lastAttack = Time.time;
        swarmCount += 0.01f;
        swarmCount = Mathf.Min(swarmCount, 1);
    }

    IEnumerator swarmGroupSound()
    {
        speaker = (GameObject)Instantiate(speakerPrefab, Vector3.zero, Quaternion.identity);
        AudioSource audioSource = speaker.GetComponent<AudioSource>();
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        audioSource.pitch = randomPitch;
        audioSource.clip = swarmSFX;
        audioSource.Play();
        audioSource.loop = true;
        while (true)
        {
            audioSource.volume = swarmCount;
            yield return new WaitForSeconds(0.1f);
            if (lastAttack + 1f < Time.time)
            {
                if (swarmCount > 0.1f)
                    swarmCount = Mathf.Lerp(0, swarmCount, 0.9f);
                else
                    swarmCount = 0;
                //swarmCount -= 0.08f;
                //swarmCount= Mathf.Max(swarmCount, 0);
            }
        }
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