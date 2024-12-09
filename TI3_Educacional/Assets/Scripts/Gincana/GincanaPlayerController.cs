using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class GincanaPlayerController : PlayerController
//{
//    [SerializeField] Rigidbody rb;
//    [SerializeField] float jumpPower = 10f;
//
//    private void Awake()
//    {
//        Screen.orientation = ScreenOrientation.LandscapeLeft;
//    }
//    public override void Start()
//    {
//        base.Start();
//    }
//
//    public override void Update()
//    {
//        if (GincanaLevelManager.instance.isPaused)
//        {
//            return;
//        }
//        DetectMovement();
//    }
//    public override void FixedUpdate()
//    {
//        if (GincanaLevelManager.instance.isPaused)
//        {
//            return;
//        }
//        CheckMovement();
//    }
//
//    public override void Step()
//    {
//        rb.MovePosition(movement);
//        Gerenciador_Audio.TocarSFX(stepSFX);
//    }
//
//    public override void Jump()
//    {
//        base.Jump();
//        rb.AddForce(Vector3.up * jumpPower);
//        Debug.Log("Jump");
//    }
//}
