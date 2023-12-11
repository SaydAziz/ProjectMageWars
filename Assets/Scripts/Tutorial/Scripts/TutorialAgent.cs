using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialAgent : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    Canvas ui;
    [SerializeField] UnityEngine.InputSystem.PlayerInput playerInput;
    [SerializeField] DummyEnemy dummy;

    private float timeLeft = 10;
    private float currentTime;
    private bool timerOn;
    [SerializeField] TMP_Text timerTxt;

    int currentTrigger;
    [SerializeField] List<TutorialTrigger> triggers = new List<TutorialTrigger>();
    // Start is called before the first frame update
    void Start()
    {
        currentTrigger = -1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timerOn)
        {
            if(timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                TimerTick(timeLeft);
            }
            else
            {
                Debug.Log("Timer Done.");
                timeLeft = 0;
                timerOn = false;
                timerTxt.gameObject.SetActive(false);
                EnableNextTrigger();
            }
        }
    }

    void TimerTick(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);

    }

    public void EnableNextTrigger()
    {
        if (currentTrigger == triggers.Count - 2)
        {
            Invoke("DelayedEnd", 8);
        }
            EnableTrigger(currentTrigger + 1);

    }

    private void DelayedEnd()
    {
        GameManager.Instance.ClosingSequence();
    }

    public void EnableTrigger(int i)
    {
        triggers[i].gameObject.SetActive(true);
        currentTrigger = i;
    }

    public void TriggerTutBeat(TutorialTrigger trigger, AudioClip clip, Canvas canvas, bool cantMove, float autoDestroy, bool canFight)
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

        if(canFight)
        {
            dummy.EnableFight(true);
            timerTxt.gameObject.SetActive(true);
            timerOn = true;
        }
        else
        {
            dummy.EnableFight(false);
        }
    }

    private void ResetUI()
    {
        Destroy(ui.gameObject);
    }
}
