using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VRTutorial : MonoBehaviour
{
    [SerializeField] SpriteRenderer completeCircle;
    [SerializeField] SpriteRenderer timerCircle;
    bool isSeen;
    [SerializeField] float interactTime = 2f;
    float timer;
    [SerializeField] UnityEvent onComplete;

    // Update is called once per frame
    void Update()
    {
        if (isSeen)
        {
            timer += Time.deltaTime;
            timerCircle.gameObject.transform.Rotate(new Vector3(360 * Time.deltaTime / interactTime, 0, 0));
        }
        if (timer >= interactTime)
        {
            onComplete.Invoke();
        }
    }

    public void OnPointerEnter()
    {
        isSeen = true;
        completeCircle.gameObject.SetActive(true);  
    }

    public void OnPointerExit()
    {
        isSeen = false;
        timerCircle.gameObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        completeCircle.gameObject.SetActive(false);
        timer = 0;
    }
}
