using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] CanvasGroup blind;
    [SerializeField] AudioSource fireSound;
    [SerializeField] AudioSource footSteps;
    public TutorialAgent agent;
    public PlayerData player;

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        player.health = 1;
        agent.EnableTrigger(0);
        Invoke("OpeningSequence", 12);
    }

    // Update is called once per frame
    void Update()
    {

    }



    void OpeningSequence()
    {
        footSteps.enabled = true;
        blind.DOFade(0f, 3f).SetEase(Ease.Linear);
        fireSound.DOFade(1f, 3f).SetEase(Ease.Linear);
        Invoke("StartTut", 3);
    }

    void StartTut()
    {
        blind.gameObject.SetActive(false);
        agent.EnableTrigger(1);
    }
    
    public void ClosingSequence()
    {
        blind.gameObject.SetActive(true);
        fireSound.DOFade(0f, 3f).SetEase(Ease.Linear);
        blind.DOFade(1f, 3f).SetEase(Ease.Linear);
        Invoke("EndFirstTut", 3);
    }

    void EndFirstTut()
    {
        SceneManager.LoadScene(3);
    }
}
