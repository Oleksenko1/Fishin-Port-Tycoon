using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [Header("States")]
    [SerializeField] public IdleState idleState;
    [SerializeField] public MovingState movingState;
    [SerializeField] public FishingState fishingState;

    private PlayerState currentState;
    void Awake()
    {
        EnterState(idleState);
    }
    void Update()
    {
        currentState.OnUpdate();
    }
    public void EnterState(PlayerState newState)
    {
        currentState?.OnExit();

        currentState = newState;

        if (currentState.playerController == null) newState.InitializeState(playerController, this);

        newState.OnEnter();
    }
}
