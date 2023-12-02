using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BasicSoundPlayer : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField, Header("AudioFiles")]
    AudioClip[] SoundsToPlay;
    [SerializeField]
    float PitchRandomization;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound()
    {
        audioSource.pitch = 1 + Random.Range(-PitchRandomization, PitchRandomization);
        audioSource.PlayOneShot(getRandomClip(SoundsToPlay));
    }

    AudioClip getRandomClip(AudioClip[] clipArray)
    {
        if(clipArray.Length <= 0)
        {
            Debug.LogError(clipArray + " has no audio files to play.");
        }
        return clipArray[Random.Range(0,clipArray.Length)];
    }
}
