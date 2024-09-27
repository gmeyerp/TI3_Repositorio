using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform[] waypoints;
    int currentWaypoint;
    [SerializeField] float speed; //vai pegar o valor do level manager
    [SerializeField] bool pingPong;
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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        characterController.SimpleMove(direction.normalized * speed);
        if (direction.magnitude < 0.1f)
        {
            NewWaypoint();
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
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.gameObject.CompareTag("Player"))
        {
            FeiraLevelManager.instance.PlayerHit();
        }
    }
}
