using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialAgent : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerTutBeat(AudioClip clip, Canvas canvas)
    {
        audio.clip = clip;
        audio.Play();

        Canvas ui = Instantiate(canvas);
    }

}
