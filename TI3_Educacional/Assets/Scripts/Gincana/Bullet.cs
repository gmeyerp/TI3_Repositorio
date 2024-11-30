using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] float lifeTime = 10f;
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        rb.MovePosition(transform.position + (direction * speed * Time.fixedDeltaTime));
    }

    public void Shoot(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(gameObject.name);
            GincanaLevelManager.instance.HitPlayer(gameObject.name);
        }
        Destroy(gameObject);
    }
}
