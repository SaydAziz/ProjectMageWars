using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] TutorialAgent agent;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] TutorialBeat beat;
    private void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer)
        {
            agent.TriggerTutBeat(beat.voiceInstruction, beat.uiInstruction);
        }
    }
}
