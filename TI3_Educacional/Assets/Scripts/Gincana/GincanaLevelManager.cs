using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType { Jump, Dodge }    
public class GincanaLevelManager : MonoBehaviour
{
    public static GincanaLevelManager instance;
    [SerializeField] GameObject player;
    [SerializeField] Rigidbody playerRB;
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
        if (playerRB == null)
        {
            playerRB = player.GetComponent<Rigidbody>();
        }

        safeSpot = player.transform.position;
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }

    public void HitPlayer(string name)
    {
        Gerenciador_Audio.TocarSFX(hitSFX);
        hitCount++;
        playerRB.velocity = Vector3.zero;
        player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(name, "Jogador Atingido", "true");
        }
    }

    public void HitPlayer(ObstacleType type, string name)
    {
        if (type == ObstacleType.Jump)
        {
            if (!playerController.IsJumpSafe())
            {
                Gerenciador_Audio.TocarSFX(hitSFX);
                hitCount++;
                playerRB.velocity = Vector3.zero;
                player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
            }
        }
        else if (type == ObstacleType.Dodge)
        {
            if (!playerController.isDodging)
            {
                Gerenciador_Audio.TocarSFX(hitSFX);
                hitCount++;
                playerRB.velocity = Vector3.zero;
                player.transform.position = new Vector3(safeSpot.x, safeSpot.y + player.transform.position.y, safeSpot.z);
            }
        }
    }

    public void FixedUpdate()
    {
        if (player.transform.position.y <= - 3f)
        {
            HitPlayer("FallTrigger");
            GincanaTutorial.instance.FallTutorial();
        }
    }

    private void OnDestroy()
    {
        if (AnalyticsTest.instance != null)
        {
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Gincana Numero de Atingido", hitCount.ToString());
            AnalyticsTest.instance.AddAnalytics(gameObject.name, "Gincana Tempo", timer.ToString());
        }
    }
}