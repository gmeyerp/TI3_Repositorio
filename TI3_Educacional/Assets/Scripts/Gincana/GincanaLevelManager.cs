using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GincanaLevelManager : MonoBehaviour
{
    public static GincanaLevelManager instance;
    [SerializeField] GameObject player;
    public Vector3 safeSpot;
    public int hitCount = 0;
    public float timer = 0;
    public bool isPaused;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void HitPlayer()
    {
        hitCount++;
        player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
    }
}
