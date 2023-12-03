using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct AudioData
{
    [SerializeField]
    public string ID;
    [SerializeField]
    List<AudioClip> Clips;
    [SerializeField]
    public float randomPitchOffset;
    [SerializeField]
    public bool looping;
    [SerializeField]
    public float stereoPan;
    [SerializeField]
    public PlayerSoundType soundType;

    public AudioClip GetRandomClip()
    {
        return Clips[UnityEngine.Random.Range(0, Clips.Count - 1)];
    }
}

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
        audioSource.pitch = 1 + UnityEngine.Random.Range(-PitchRandomization, PitchRandomization);
        audioSource.PlayOneShot(getRandomClip(SoundsToPlay));
    }

    AudioClip getRandomClip(AudioClip[] clipArray)
    {
        if(clipArray.Length <= 0)
        {
            Debug.LogError(clipArray + " has no audio files to play.");
        }
        return clipArray[UnityEngine.Random.Range(0,clipArray.Length)];
    }

    public void PlayClip(AudioClip clipToPlay, float randomPitchOffset)
    {
        if(clipToPlay== null) { return; }
        audioSource.pitch = 1 + UnityEngine.Random.Range(-randomPitchOffset, randomPitchOffset);
        audioSource.PlayOneShot(clipToPlay);
    }

    public void PlayAudioData(AudioData data)
    {
        audioSource.clip = data.GetRandomClip();
        audioSource.panStereo = data.stereoPan;
        audioSource.pitch = 1 + UnityEngine.Random.Range(-data.randomPitchOffset, data.randomPitchOffset);
        audioSource.loop = data.looping;
        audioSource.Play();
    }
}
