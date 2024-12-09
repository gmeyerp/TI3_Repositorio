using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casket : Bullet
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name);
            GincanaLevelManager.instance.HitPlayer(ObstacleType.Jump, gameObject.name);
            Destroy(gameObject);
        }
    }
}
