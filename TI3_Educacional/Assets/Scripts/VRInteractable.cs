using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class VRInteractable : MonoBehaviour
{
    public RectTransform completeCircle;
    public RectTransform timerCircle;
    public bool isSeen;
    [SerializeField] public float interactTime = 2f;
    public float timer;
    [SerializeField] public UnityEvent onComplete;

    // Update is called once per frame
    void Start()
    {
        completeCircle = GameObject.Find("Clock").GetComponent<RectTransform>();
        timerCircle = GameObject.Find("ClockHand").GetComponent<RectTransform>();
    }
    void Update()
    {
        if (isSeen)
        {
            timer += Time.deltaTime;
            timerCircle.Rotate(new Vector3(0, 0, -360 * Time.deltaTime / interactTime));
        }
        if (timer >= interactTime)
        {
            onComplete.Invoke();
            timer = 0;
        }
    }

    //public void OnPointerEnter()
    //{
    //    isSeen = true;
    //    completeCircle.gameObject.SetActive(true);  
    //}
    //
    //public void OnPointerExit()
    //{
    //    isSeen = false;
    //    timerCircle.rotation = Quaternion.Euler(Vector3.zero);
    //    completeCircle.gameObject.SetActive(false);
    //    timer = 0;
    //}
}
