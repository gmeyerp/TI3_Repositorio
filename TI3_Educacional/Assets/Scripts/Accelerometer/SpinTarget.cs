using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinTarget : MonoBehaviour
{
    [SerializeField] int pointValue = 1;
    [SerializeField] float lifeTime = 4f;
    [SerializeField] Color startingColor;
    [SerializeField] Color endingColor;
    [SerializeField] Material material;
    float counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        if (material == null)
        {
            material = GetComponent<MeshRenderer>().material;
        }
    }

    private void Update()
    {
        counter += Time.deltaTime;
        ChangeColor();
    }
    public void DestroyTarget() //colocar outras funcionalidades no momento da destruição como vfx e sfx
    {
        Destroy(gameObject);
    }

    public int GetScore()
    {
        return pointValue;
    }

    public void ChangeColor()
    {
        material.color = Color.Lerp(startingColor, endingColor, counter / lifeTime);
    }
}
