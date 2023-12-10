using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAgent : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    Canvas ui;
    [SerializeField] UnityEngine.InputSystem.PlayerInput playerInput;


    [SerializeField] List<TutorialTrigger> triggers = new List<TutorialTrigger>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableTrigger(int i)
    {
        triggers[i].gameObject.SetActive(true);
    }

    public void TriggerTutBeat(AudioClip clip, Canvas canvas, bool cantMove)
    {
        if (audio != null)
        {
            audio.clip = clip;
            audio.Play();
        }
        
        if (canvas != null)
        {
            ui = Instantiate(canvas);
            Invoke("ResetUI", 7);
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
