using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PlayerAudioManager : MonoBehaviour
{
    public static PlayerAudioManager Instance;

    [SerializeField]
    GameObject AbilitySource;

    [SerializeField, Header("Player Audio Files")]
    List<AudioData> AudioData;

    BasicSoundPlayer abilitySoundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        abilitySoundPlayer = AbilitySource.GetComponent<BasicSoundPlayer>();
    }

    public void PlaySoundClip(string clipID)
    {
        if(AudioData.Count > 0)
        {
            if (HasID(clipID, out AudioData data))
            {
                abilitySoundPlayer.PlayAudioData(data);
            }
        }
    }

    protected bool HasID(string id, out AudioData data)
    {
        foreach (AudioData audioData in AudioData)
        {
            if (audioData.ID == id)
            {
                data = audioData;
                return true;
            }
        }
        Debug.LogWarning(id + " does not exist. Check the AudioData list on the player audiomanager and confirm there is an entry with this ID");
        data = AudioData[0];
        return false;
    }

    protected AudioClip GetRandomClipFromID(string id)
    {
        foreach(AudioData audioData in AudioData)
        {
            if(audioData.ID == id)
            {
                return audioData.GetRandomClip();
            }
        }
        return null;
    }


}
