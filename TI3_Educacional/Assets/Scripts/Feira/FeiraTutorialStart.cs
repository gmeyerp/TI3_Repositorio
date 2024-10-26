using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeiraTutorialStart : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float correctAngle;
    [SerializeField] Animator tutorialSpriteAnim;
    [SerializeField] TextMeshProUGUI txt;

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
