using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayerSoundType { Ability, Foot}

public class PlayerAudioManager : MonoBehaviour
{
    public static PlayerAudioManager Instance;

    [SerializeField]
    GameObject AbilitySource, FootstepSource;

    //should switch to array 
    [SerializeField, Header("Player Audio Files")]
    List<AudioData> AudioData;

    BasicSoundPlayer abilitySoundPlayer, footstepSoundPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        abilitySoundPlayer = AbilitySource.GetComponent<BasicSoundPlayer>();
        footstepSoundPlayer = FootstepSource.GetComponent<BasicSoundPlayer> ();
    }

    public void PlaySoundClip(string clipID)
    {
        if(AudioData.Count > 0)
        {
            if (HasID(clipID, out AudioData data))
            {
                switch (data.soundType)
                {
                    case PlayerSoundType.Ability:
                        abilitySoundPlayer.PlayAudioData(data); break;
                    case PlayerSoundType.Foot:
                        footstepSoundPlayer.PlayAudioData(data); break;
                    default:
                        abilitySoundPlayer.PlayAudioData(data); break;
                }
                
            }
        }
    }

    public void PlayFootstep(string StepType)
    {
        Ray ray = new Ray(transform.position, -transform.up * 7);
        Physics.Raycast(ray, out RaycastHit hit);
        Material[] m = hit.transform.gameObject.GetComponent<MeshRenderer>().materials;
        PlayFootstepSound(SurfaceChecker.Instance.GetSurfaceTypeFromMaterials(m), StepType);
    }

    protected void PlayFootstepSound(SurfaceType type, string Prefix)
    {
        if (AudioData.Count > 0)
        {
            switch (type)
            {
                case SurfaceType.Wood:
                    PlaySoundClip(Prefix + "_Wood");
                    break;
                case SurfaceType.Water:
                    break;
                case SurfaceType.Stone:
                    PlaySoundClip(Prefix + "_Stone");
                    break;
                default:
                    PlaySoundClip(Prefix + "_Stone");
                    break;
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
