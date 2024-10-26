using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private CharacterController player;
    [SerializeField] private Camera playerCamera;
    [SerializeField] AudioClip stepSFX;

    [Header("Values")]
    [SerializeField] private float speed = 1;
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
    private Vector3 movement;

    [Header("Events")]
    [SerializeField] private UnityEvent onStep;
    [SerializeField] private UnityEvent onPrepare;
    [SerializeField] private UnityEvent onStop;

    private void Start()
    {
        stepState = StepState.Waiting;
        timeSinceLastStepUpdate = 0;
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

            // A porcentagem do quão vertical é o movimento, usando a gravidade de referência
            float verticalWeight = CossineSimilarity(acceleration, gravity);

            // Multiplicando pelo movimento pra ter um valor mais preciso
            float verticalAcceleration = verticalWeight * acceleration.magnitude;

            // Verificando quando o celular sobe (preparando o passo)
            if (stepState != StepState.Incoming && verticalAcceleration > stepSensorThreshhold)
            {
                stepState = StepState.Incoming;
                timeSinceLastStepUpdate = 0;

                onPrepare.Invoke();
            }
            // Verificando quando o celular desce depois de subir (executando o passo)
            else if (stepState == StepState.Incoming && verticalAcceleration < -stepSensorThreshhold)
            {
                stepState = StepState.Done;
                timeSinceLastStepUpdate = 0;

                Vector3 XZMovement = Vector3.Scale(playerCamera.transform.forward, Vector3.right + Vector3.forward).normalized;
                movement = XZMovement * speed;

                onStep.Invoke();
            }
            // Contando o tempo sem atividade de passos
            else if (timeSinceLastStepUpdate < stepResetCooldown)
            {
                timeSinceLastStepUpdate += Time.deltaTime;
            }
            // Quando passa o cooldown, muda o estado para o de espera
            else if (stepState != StepState.Waiting)
            {
                stepState = StepState.Waiting;
                timeSinceLastStepUpdate = 0;

                onStop.Invoke();
            }
        }
    }

    private void FixedUpdate()
    {
        if (FeiraLevelManager.instance.isPaused) return;

        if (movement != Vector3.zero)
        {
            Step();
            movement = Vector3.zero;
        }
    }

    private float CossineSimilarity(Vector3 a, Vector3 b)
    {
        float cossineSimilarity = (Vector3.Angle(a, b) - 90) / 90;
        return cossineSimilarity;
    }

    private void Step()
    {
        player.Move(movement);
        Gerenciador_Audio.TocarSFX(stepSFX);
    }
}
