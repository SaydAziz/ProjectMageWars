using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Beat", menuName = "Tutorial/Tutorial Beat")]
public class TutorialBeat : ScriptableObject
{
    public AudioClip voiceInstruction;
    public Canvas uiInstruction;
}
