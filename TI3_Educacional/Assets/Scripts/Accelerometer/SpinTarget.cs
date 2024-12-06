using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SpinTarget : MonoBehaviour
{
    [SerializeField] int pointValue = 1;
    [SerializeField] float lifeTime = 4f;
    [SerializeField] Color startingColor;
    [SerializeField] Color endingColor;
    [SerializeField] Material material;
    public bool isCollected;
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
    public void DestroyTarget() //colocar outras funcionalidades no momento da destrui��o como vfx e sfx
    {
        if (!isCollected)
        {
            GameTracker.instance.AddMiss();
        }
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
