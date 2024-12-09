using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] float distance = 5f;
    [SerializeField] float maxAngle = 45f;
    [SerializeField] float time = 5f;
    [SerializeField] GameObject targetPrefab;
    [SerializeField] bool spawnUp = true;
    [SerializeField] bool spawnDown = true;
    [SerializeField] bool spawnLeft = true;
    [SerializeField] bool spawnRight = true;
    // Start is called before the first frame update
    void Start()
    {
        if (ProfileManager.IsManaging)
        {
            int maxAngle = System.Convert.ToInt32(ProfileManager.GetCurrent(ProfileInfo.Info.intMaxAngle));
            this.maxAngle = maxAngle;

            bool canUp = System.Convert.ToBoolean(ProfileManager.GetCurrent(ProfileInfo.Info.boolCanUp));
            bool canDown = System.Convert.ToBoolean(ProfileManager.GetCurrent(ProfileInfo.Info.boolCanDown));
            bool canLeft = System.Convert.ToBoolean(ProfileManager.GetCurrent(ProfileInfo.Info.boolCanLeft));
            bool canRight  = System.Convert.ToBoolean(ProfileManager.GetCurrent(ProfileInfo.Info.boolCanRight));

            spawnUp = canUp;
            spawnDown = canDown;
            spawnLeft = canLeft;
            spawnRight = canRight;
        }

        InvokeRepeating(nameof(SpawnTarget), 0, time);
    }

    public void SpawnTarget()
    {
        float angle = Random.Range(10, maxAngle);
        float x = Mathf.Cos(angle * 3.14f / 180);
        float y = Mathf.Sin(angle * 3.14f / 180);

        if (spawnUp && spawnDown)
        {
            y *= GetPositive();
        }
        else if (spawnDown)
        {
            y *= -1;
        }

        if (spawnLeft && spawnRight)
        {
            x *= GetPositive();
        }
        else if (spawnLeft)
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
