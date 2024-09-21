using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTarget : MonoBehaviour
{
    [SerializeField] LayerMask isPlayer;
    [SerializeField] int pointValue = 1;
    [SerializeField] float lifeTime = 4f;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }


    public void DestroyTarget() //colocar outras funcionalidades no momento da destruição como vfx e sfx
    {
        Destroy(gameObject);
    }

    public int GetScore()
    {
        return pointValue;
    }
}
