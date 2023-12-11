using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAgent : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    Canvas ui;
    [SerializeField] UnityEngine.InputSystem.PlayerInput playerInput;

    int currentTrigger;


    [SerializeField] List<TutorialTrigger> triggers = new List<TutorialTrigger>();
    // Start is called before the first frame update
    void Start()
    {
        currentTrigger = -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTUT()
    {
        EnableTrigger(1);
    }

    public void EnableNextTrigger()
    {
        EnableTrigger(currentTrigger + 1);
    }

    public void EnableTrigger(int i)
    {
        triggers[i].gameObject.SetActive(true);
        currentTrigger = i;
    }

    public void TriggerTutBeat(TutorialTrigger trigger, AudioClip clip, Canvas canvas, bool cantMove, float autoDestroy)
    {
        if(ui != null) Destroy(ui.gameObject);
        triggers[currentTrigger].gameObject.SetActive(false);
        currentTrigger = triggers.IndexOf(trigger);

        if (audio != null)
        {
            audio.clip = clip;
            audio.Play();
        }
        
        if (canvas != null)
        {
            ui = Instantiate(canvas);
            if (autoDestroy > 0)
            {
                Invoke("ResetUI", autoDestroy);
            }
        }

        if (!cantMove)
        {
            playerInput.enabled = true;
        }
        else
        {
            playerInput.enabled = false;
        }

    }


    private void ResetUI()
    {
        Destroy(ui.gameObject);
    }

}
