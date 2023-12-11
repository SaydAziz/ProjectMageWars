using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] TutorialAgent agent;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] TutorialBeat beat;

    bool triggered;
    private void Start()
    {
        triggered = false;
        playerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == playerLayer && !triggered)
        {
            triggered = true;
            agent.TriggerTutBeat(this, beat.voiceInstruction, beat.uiInstruction, beat.cantMove, beat.autoDestroy, beat.canFight);
            this.gameObject.SetActive(false);
        }
    }
}
