using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    [SerializeField] private AudioSource[] _audioClips;

    [SerializeField] private float countdownToSound = 1000;
    [SerializeField] private float lowerBoundCountdown = 20;
    [SerializeField] private float upperBoundCountdown = 300;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        TryPlayAudioClip();
    }

    void TryPlayAudioClip()
    {
        if (_audioClips != null && countdownToSound <= 0)
        {
                _audioClips[Random.Range(0, _audioClips.Length)].Play();
        }
        if (countdownToSound > 0)
        {
            countdownToSound -= Random.Range(0.001f, 0.5f);
        }
        else
        {
            countdownToSound = Random.Range(lowerBoundCountdown, upperBoundCountdown);
        }
    }
}
