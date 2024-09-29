using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] float distance = 5f;
    [SerializeField] float maxAngle = 45f;
    [SerializeField] float time = 5f;
    [SerializeField] GameObject targetPrefab;
    [SerializeField] bool isDoubleSide;
    [SerializeField] bool isRight;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnTarget), 0, time);
    }

    public void SpawnTarget()
    {
        float angle = Random.Range(0, maxAngle);
        float x = Mathf.Cos(angle * 3.14f / 180);
        float y = Mathf.Sin(angle * 3.14f / 180);
        y *= GetPositive();
        if (isDoubleSide)
        {
            x *= GetPositive();
        }
        else if (!isRight)
        {
            x *= -1;
        }
        Vector3 position = new Vector3(x * distance, y * distance, 0);
        GameObject target = Instantiate(targetPrefab, position, transform.rotation);
    }

    public int GetPositive()
    {
        int value = Random.Range(0, 2);
        if (value == 0)
            return 1;
        else
            return -1;
    }
}
