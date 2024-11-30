using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] float maxDistance = 10f;
    [SerializeField] LayerMask interactableLayer;
    VRInteractable interactable;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxDistance, interactableLayer, QueryTriggerInteraction.Ignore))
        {
            interactable = hit.collider.gameObject.GetComponent<VRInteractable>();
            if (interactable != null)
            {
                interactable.isSeen = true;
                interactable.completeCircle.gameObject.SetActive(true);
            }
        }
        else
        {
            if (interactable != null)
            {
                interactable.isSeen = false;
                interactable.timerCircle.rotation = Quaternion.Euler(Vector3.zero);
                interactable.completeCircle.gameObject.SetActive(false);
                interactable.timer = 0;
            }
        }
    }
}
