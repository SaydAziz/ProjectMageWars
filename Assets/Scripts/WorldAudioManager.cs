using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WorldAudioManager : MonoBehaviour
{
    public static WorldAudioManager Instance;
    [SerializeField]
    int MaxAvailablePlayers = 10;

    Queue<BasicSoundPlayer> audioSources;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioSources = new Queue<BasicSoundPlayer>();
        SetupSources();
        if(audioSources.Count < 1)
        {
            Debug.LogWarning("Not Enough Audio Sources on WorldAudioManager. Increase MaxAvailablePlayers.");
        }
    }

    private void SetupSources()
    {
        for(int i = 0; i < MaxAvailablePlayers; i++)
        {
            GameObject g = Instantiate(new GameObject(), transform);
            AudioSource s = g.AddComponent<AudioSource>();
            s.playOnAwake = false;
            s.loop = false;
            s.spatialBlend = 1;
            s.minDistance = 10;
            s.maxDistance = 1000;
            s.rolloffMode = AudioRolloffMode.Logarithmic;
            //maybe set mixer output group here as well
            BasicSoundPlayer bs = g.AddComponent<BasicSoundPlayer>();
            audioSources.Enqueue(bs);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundAtPoint(AudioClip clipToPlay, Vector3 worldPositon, float pitchRandomization)
    {
        BasicSoundPlayer s = audioSources.Dequeue();
        s.transform.position = worldPositon;
        s.PlayClip(clipToPlay, pitchRandomization);
        audioSources.Enqueue(s);
    }
}
