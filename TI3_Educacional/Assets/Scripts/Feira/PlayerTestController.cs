using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTestController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;

    public InputActionAsset inputActions;
    InputActionMap playerActions;
    InputAction moveAction;
    Vector3 movement;

    // Start is called before the first frame update
    void Awake()
    {
        playerActions = inputActions.FindActionMap("Player");

        moveAction = playerActions.FindAction("Move");

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementInput = moveAction.ReadValue<Vector2>();
        movement = characterController.transform.forward * movementInput.y;
        characterController.gameObject.transform.Rotate(0, movementInput.x * rotationSpeed * Time.deltaTime, 0);
        Debug.Log(movementInput);
    }

    private void FixedUpdate()
    {
        characterController.Move(movement * speed * Time.fixedDeltaTime);
    }
}
