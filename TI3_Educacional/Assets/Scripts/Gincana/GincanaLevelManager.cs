using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType { Jump, Dodge }    
public class GincanaLevelManager : MonoBehaviour
{
    public static GincanaLevelManager instance;
    [SerializeField] GameObject player;
    [SerializeField] PlayerController playerController;
    public Vector3 safeSpot;
    public int hitCount = 0;
    public float timer = 0;
    public bool isPaused;
    [SerializeField] AudioClip hitSFX;
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

    private void Start()
    {
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
        safeSpot = player.transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void HitPlayer()
    {
        Gerenciador_Audio.TocarSFX(hitSFX);
        hitCount++;
        player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
    }

    public void HitPlayer(ObstacleType type)
    {
        if (type == ObstacleType.Jump)
        {
            if (playerController.IsGroundCheck())
            {
                Gerenciador_Audio.TocarSFX(hitSFX);
                hitCount++;
                player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
            }
        }
        else if (type == ObstacleType.Dodge)
        {
            if (!playerController.isDodging)
            {
                Gerenciador_Audio.TocarSFX(hitSFX);
                hitCount++;
                player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
            }
        }
    }
}
