using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDecks : MonoBehaviour
{
    [SerializeField] Transform moveToPosition;
    Vector3 finalPosition;
    Vector3 initialPosition;
    [SerializeField] float speed = 2f;
    Vector3 currentTarget;
    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        finalPosition = moveToPosition.position;
        currentTarget = finalPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = currentTarget - transform.position;
        rb.MovePosition((transform.position + direction.normalized * speed * Time.fixedDeltaTime));
        if (direction.magnitude < 0.1f)
        {
            ChangeTarget();
        }
    }

    void ChangeTarget()
    {
        if (currentTarget == finalPosition)
        {
            currentTarget = initialPosition;
        }
        else
        {
            currentTarget = finalPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.parent = null;
        }
    }
}
