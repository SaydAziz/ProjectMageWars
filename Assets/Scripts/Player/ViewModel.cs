using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewModel : MonoBehaviour
{
    Vector3 origin;

    //sway
    float swayClamp = 0.05f;
    float smoothing = 3f;
    Vector2 input;

    //bob
    float bobScale = 0.1f;
    float bobSpeed = 7f;



    // Start is called before the first frame update
    void Start()
    {
        origin = transform.localPosition;
    }

    public void doBOB()
    {
       
        float scale = 2 / (3 - Mathf.Cos(2 * Time.deltaTime));
        transform.localPosition = new Vector3(
            transform.localPosition.x + bobScale * (scale * Mathf.Cos(Time.time * bobSpeed) * Time.deltaTime),
            transform.localPosition.y + bobScale * (scale * Mathf.Sin(2 * Time.time * bobSpeed) / 2 * Time.deltaTime),
            transform.localPosition.z);
    }


    public void setInputVal(Vector2 value)
    {
        input = value;
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Mathf.Clamp(input.x, -swayClamp, swayClamp);
        input.y = Mathf.Clamp(input.y, -swayClamp, swayClamp);

        Vector3 target = new Vector3(-input.x, -input.y, 0);

        transform.localPosition = Vector3.Lerp(transform.localPosition, target + origin, Time.deltaTime * smoothing);
    }
}
