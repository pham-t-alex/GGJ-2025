using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;
    public static AudioController Instance
    {
        get
        {
            return instance;
        }
    }
    public AudioSource audioSource
    {
        get
        {
            return GetComponent<AudioSource>();
        }
    }

    float delay = 0;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            if (delay == 0)
            {
                delay = 2;
            }
            if (delay > 0)
            {
                delay -= Time.deltaTime;
                if (delay < 0)
                {
                    audioSource.Play();
                    delay = 0;
                }
            }
        }
    }
}
