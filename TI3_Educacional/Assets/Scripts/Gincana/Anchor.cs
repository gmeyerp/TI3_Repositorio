using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    void Start()
    {
        speed *= System.Convert.ToSingle(ProfileManager.GetCurrent(ProfileInfo.Info.floatAnchorSpeed));

        Animator animator = GetComponent<Animator>();
        animator.speed = speed;
    }
}
