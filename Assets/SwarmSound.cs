using UnityEngine;
using System.Collections;

public class SwarmSound : MonoBehaviour {
    static SwarmSound instance;
    public GameObject speakerPrefab;
    private GameObject speaker;
    public float lowPitchRange = .95f;
    public float highPitchRange = 1.05f;
    public AudioClip swarmSFX;
    float swarmCount = 0;
    float lastAttack = 0;

    // getter
    public static SwarmSound Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {


        instance = this;
        speakerPrefab = Resources.Load<GameObject>("Speaker");
    }

    void Start () {
        swarmCount = 0;
        StartCoroutine(swarmGroupSound());
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
}
