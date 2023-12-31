using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialAgent : MonoBehaviour
{
    [SerializeField] AudioSource audioS;
    [SerializeField] AudioSource questSound;
    Canvas ui;
    [SerializeField] DummyEnemy dummy;

    public float timeLeft = 12;
    private float currentTime;
    private bool timerOn;
    [SerializeField] TMP_Text timerTxt;

    int currentTrigger;
    [SerializeField] List<TutorialTrigger> triggers = new List<TutorialTrigger>();
    // Start is called before the first frame update
    void Start()
    {
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
                timeLeft = 0;
                timerOn = false;
                timerTxt.gameObject.SetActive(false);
                EnableNextTrigger();
            }
        }
    }

    public void ResetTrigger()
    {
        CancelInvoke();
        Time.timeScale = 1;   
        if (ui != null)
        {
            ResetUI();

        }
        if(GameManager.Instance.isCamp)
        {
            triggers[currentTrigger].triggered = false;
            currentTrigger--;
            dummy.EnableFight(false);
            timerOn = false;
            timeLeft = 14;
        }

        triggers[currentTrigger].gameObject.SetActive(true);
        audioS.Stop();
        triggers[currentTrigger].triggered = false;

    }
    public Transform GetCheckpoint()
    {
        return triggers[currentTrigger].checkpoint;
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
        Debug.Log("NEXT TRIGGER.");
        CancelInvoke(); //MIGHT BE A BAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAD IDEA
        if (GameManager.Instance.isCamp && currentTrigger == triggers.Count - 2)
        {
            Invoke("DelayedEnd", 5);
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

    void ResetSlomo()
    {
        Time.timeScale = 1;
    }

    public void TriggerTutBeat(TutorialTrigger trigger, AudioClip clip, Canvas canvas, bool cantMove, float autoDestroy, bool canFight, float slowTime, float autoNext)
    {
        if(ui != null) Destroy(ui.gameObject);
        triggers[currentTrigger].gameObject.SetActive(false);
        currentTrigger = triggers.IndexOf(trigger);

        if (slowTime > 0)
        {
            Time.timeScale = slowTime;
            Invoke("ResetSlomo", 5);
        }

        if (audioS != null)
        {
            audioS.clip = clip;
            audioS.Play();
        }
        
        if (canvas != null)
        {
            ui = Instantiate(canvas);;
            questSound.Play();
            if (autoDestroy > 0)
            {
                Invoke("ResetUI", autoDestroy);
            }
        }

        if (!cantMove)
        {
            GameManager.Instance.playerInput.enabled = true;
        }
        else
        {
            GameManager.Instance.playerInput.enabled = false;
        }

        if (GameManager.Instance.isCamp)
        {
            if (currentTrigger == triggers.Count - 3)
            {
                dummy.EnableCollider(true);
            }


            if (canFight)
            {
                dummy.EnableFight(true);
                //timerTxt.gameObject.SetActive(true);
                timerOn = true;
            }
            else
            {
                dummy.EnableFight(false);
            }

            
        }
        else
        {
            if (currentTrigger == triggers.Count - 4)
            {
                GameManager.Instance.EnableDash();
            }
        }

        if (autoNext > 0)
        {
            Invoke("EnableNextTrigger", autoNext);
        }
        
    }

    private void ResetUI()
    {
        Destroy(ui.gameObject);
    }
}
