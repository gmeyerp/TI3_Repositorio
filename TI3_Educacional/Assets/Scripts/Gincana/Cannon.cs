using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform spawner;
    [SerializeField] GameObject bullet;
    [SerializeField] float speed = 4f;
    [SerializeField] float cooldown = 2f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Shoot), 0, cooldown);
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(bullet, spawner.position, spawner.rotation);
        Bullet bulletScript = bulletGO.GetComponent<Bullet>();
        bulletScript.Shoot(spawner.forward, speed);
    }

}
