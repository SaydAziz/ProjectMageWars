using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{

    [SerializeField] CanvasGroup blind;
    [SerializeField] AudioSource fireSound;
    [SerializeField] TutorialAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent.EnableTrigger(0);
        Invoke("OpeningSequence", 10);
    }

    // Update is called once per frame
    void Update()
    {

    }


    void OpeningSequence()
    {
        blind.DOFade(0f, 3f).SetEase(Ease.Linear);
        fireSound.DOFade(1f, 3f).SetEase(Ease.Linear);
        Invoke("StartTut", 3);
    }

    void StartTut()
    {
        agent.EnableTrigger(1);
        blind.enabled = false;
    }
}
