using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Customer : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform[] waypoints;
    int currentWaypoint;
    [SerializeField] float speed; //vai pegar o valor do level manager
    [SerializeField] bool pingPong;
    [SerializeField] LayerMask isPlayer;
    [SerializeField] float onPathAudioFrequency = 2f;
    [SerializeField] AudioClip[] playerOnPathSFX;
    [SerializeField] AudioClip[] collisionSFX;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject model;
    [SerializeField] Animator animator;
    UnityEvent changeWaypoint = new UnityEvent();
    bool isWaiting;
    float mag;
    bool isOnPath;
    float audioTimer;
    int modifier = 1;
    // Start is called before the first frame update
    void Awake()
    {
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    private void Start()
    {
        speed = FeiraLevelManager.instance.NPCSpeed;
        changeWaypoint.AddListener(NewWaypoint);

        if (FeiraLevelManager.instance.customerStops)
        {
            changeWaypoint.AddListener(StartWaiting);
        }
    }
    private void Update()
    {
        audioTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        Vector3 aim = new Vector3(waypoints[currentWaypoint].position.x, waypoints[currentWaypoint].position.y + 1, waypoints[currentWaypoint].position.z);
        Debug.DrawRay(origin, direction, Color.red);
        mag = (aim - origin).magnitude;
        isOnPath = Physics.Raycast(origin, direction, (aim - origin).magnitude, isPlayer);
        Movement(direction);
    }

    private void Movement(Vector3 direction)
    {
        if (isOnPath)
        {
            if (audioTimer < 0)
            {
                audioTimer = onPathAudioFrequency;
                int sfx = Random.Range(0, playerOnPathSFX.Length);
                audioSource.PlayOneShot(playerOnPathSFX[sfx]);
            }
        }
        if (!isWaiting)
        {
            characterController.SimpleMove(direction.normalized * speed);
        }
        if (direction.magnitude < 0.1f)
        {
            changeWaypoint.Invoke();
        }
    }

    public void NewWaypoint()
    {
        if (!pingPong) 
        {
            currentWaypoint++;
            currentWaypoint %= waypoints.Length;
        }
        else
        {
            currentWaypoint += modifier;
            if (currentWaypoint >= waypoints.Length || currentWaypoint < 0)
            {
                modifier *= -1;
                currentWaypoint += modifier;
            }
        }

        model.transform.LookAt(waypoints[currentWaypoint]);
    }

    public void StartWaiting()
    {
        animator.SetBool("isWalking", false);
        isWaiting = true;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.gameObject.CompareTag("Player"))
        {

            animator.SetBool("isHit", true);
            isWaiting = true;

            FeiraLevelManager.instance.PlayerHit();
            if (!FeiraLevelManager.instance.isInvulnerable)
            {
                int sfx = Random.Range(0, collisionSFX.Length);
                audioSource.PlayOneShot(collisionSFX[sfx]);
                if (AnalyticsTest.instance != null)
                {
                    AnalyticsTest.instance.AddAnalytics(gameObject.name, "Jogador Atingido", "true");
                }
            }            
        }
    }

    public void InterruptionOver()
    {
        isWaiting = false;
        animator.SetBool("isWalking", true);
        animator.SetBool("isHit", false);
    }
}
