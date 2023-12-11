using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] TutorialAgent agent;
    [SerializeField] int playerLayer = 7;
    [SerializeField] TutorialBeat beat;

    bool triggered;
    private void Start()
    {
        triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && !triggered)
        {
            triggered = true;
            agent.TriggerTutBeat(this, beat.voiceInstruction, beat.uiInstruction, beat.cantMove, beat.autoDestroy, beat.canFight);
            this.gameObject.SetActive(false);
        }
    }
}
