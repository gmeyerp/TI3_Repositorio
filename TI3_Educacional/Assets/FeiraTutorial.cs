using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeiraTutorial : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float correctAngle;
    [SerializeField] Animator tutorialSpriteAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FeiraLevelManager.instance.isStartDone)
        {
            Destroy(gameObject);
        }

        if (player.transform.position.z >= transform.position.z)
        {
            FeiraLevelManager.instance.isStartDone = true;
        }

        float yAngle = player.transform.rotation.eulerAngles.y;
        Debug.Log(yAngle);
        if (yAngle < correctAngle || yAngle > 360 - correctAngle)
        {
            tutorialSpriteAnim.Play("Default");
        }
        else if (yAngle < 360 - correctAngle && yAngle > 180)
        {
            tutorialSpriteAnim.Play("LookRight");
        }
        else
        {
            tutorialSpriteAnim.Play("LookLeft");
        }
    }
}
