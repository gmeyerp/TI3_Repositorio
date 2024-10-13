using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float speed = 1;
    [SerializeField] AudioClip stepSFX;

    private enum StepState
    {
        Done,
        Incoming,
        Waiting
    }
    private StepState stepState;
    [SerializeField] private float stepSensorThreshhold = .2f;
    private float timeSinceLastStepUpdate;
    [SerializeField] private float stepResetCooldown = .5f;
    private float timeSinceLastStep;
    private Vector3 movement;

    private void Start()
    {
        stepState = StepState.Waiting;
        timeSinceLastStepUpdate = 0;
    }

    private float CossineSimilarity(Vector3 a, Vector3 b)
    {
        float cossineSimilarity = (Vector3.Angle(a, b) - 90) / 90;
        return cossineSimilarity;
    }

    private void Update()
    {
        if (FeiraLevelManager.instance.isPaused) return;
        Accelerometer accelerometer = Accelerometer.current;
        GravitySensor gravitySensor = GravitySensor.current;
        if (!accelerometer.enabled)
        {
            InputSystem.EnableDevice(Accelerometer.current);
            Debug.Log("Linear Acceleration enabled");
        }
        else if (!gravitySensor.enabled)
        {
            InputSystem.EnableDevice(GravitySensor.current);
            Debug.Log("Linear Acceleration enabled");
        }
        else
        {
            Vector3 gravity = gravitySensor.gravity.value;
            Vector3 acceleration = accelerometer.acceleration.value - gravity;

            // A porcentagem do qu�o vertical � o movimento, usando a gravidade de refer�ncia
            float verticalWeight = CossineSimilarity(acceleration, gravity);

            // Multiplicando pelo movimento pra ter um valor mais preciso
            float verticalAcceleration = verticalWeight * acceleration.magnitude;

            if (stepState != StepState.Incoming && verticalAcceleration > stepSensorThreshhold)
            {
                stepState = StepState.Incoming;
                timeSinceLastStepUpdate = 0;
            }
            else if (stepState == StepState.Incoming && verticalAcceleration < -stepSensorThreshhold)
            {
                stepState = StepState.Done;
                timeSinceLastStepUpdate = 0;

                Vector3 XZMovement = Vector3.Scale(playerCamera.transform.forward, Vector3.right + Vector3.forward).normalized;
                movement = XZMovement * speed;

                timeSinceLastStep = 0;
            }
            else if (timeSinceLastStepUpdate < stepResetCooldown)
            {
                timeSinceLastStepUpdate += Time.deltaTime;
            }
            else if (stepState != StepState.Waiting)
            {
                stepState = StepState.Waiting;
                timeSinceLastStepUpdate = 0;
                timeSinceLastStep = 0;
            }

            timeSinceLastStep += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (FeiraLevelManager.instance.isPaused) return;
        if (movement != Vector3.zero)
        {
            player.Move(movement);
            Gerenciador_Audio.TocarSFX(stepSFX);
            movement = Vector3.zero;
        }

    }
}
