using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField] CanvasGroup blind;
    [SerializeField] AudioSource fireSound;
    [SerializeField] AudioSource footSteps;
    [SerializeField] Rigidbody playerRB;

    public UnityEngine.InputSystem.PlayerInput playerInput;
    public TutorialAgent agent;
    public PlayerData player;
    public bool isCamp;

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


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        if (isCamp)
        {
            agent.EnableTrigger(0);
            Invoke("OpeningSequence", 12);
        }
        else
        {
            blind.DOFade(0f, 3f).SetEase(Ease.Linear);
            footSteps.DOFade(1f, 3f).SetEase(Ease.Linear);
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            player.canDash = true;
        }

    }



    // Update is called once per frame
    void Update()
    {

    }

    public void DeathStuff()
    {
        GameManager.Instance.playerInput.enabled = false;
        if (isCamp)
        {
            agent.timeLeft += 10;
        }
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            SceneManager.LoadScene(5);
        }
    }

    public void ResetCheckpoint()
    {
        Debug.Log("RESETTITNG");
        player.gameObject.transform.position = agent.GetCheckpoint().position;
        playerRB.velocity = Vector3.zero;
        playerInput.enabled = true;
        agent.ResetTrigger();

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
        agent.EnableTrigger(1);
        Invoke("DelayedStart", 2);

    }
    
    public void ClosingSequence()
    {
        blind.gameObject.SetActive(true);
        fireSound.DOFade(0f, 3f).SetEase(Ease.Linear);
        blind.DOFade(1f, 3f).SetEase(Ease.Linear);
        Invoke("EndFirstTut", 3);
    }
    void DelayedStart()
    {
        agent.EnableNextTrigger();
    }
    void EndFirstTut()
    {
        SceneManager.LoadScene(3);
    }

    public void EnableDash()
    {
        player.canDash = true;
    }
}
