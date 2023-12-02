using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public static PlayerAudioManager Instance;

    [SerializeField]
    GameObject AbilitySource;

    BasicSoundPlayer abilitySoundPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        abilitySoundPlayer = AbilitySource.GetComponent<BasicSoundPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDashSFX()
    {
        abilitySoundPlayer.PlaySound();
    }
}
