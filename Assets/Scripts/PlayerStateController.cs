using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PlayerStateController : MonoBehaviour
{
    [Header("States")]
    [SerializeField] public IdleState idleState;
    [SerializeField] public MovingState movingState;
    [SerializeField] public FishingState fishingState;
    [Inject] private PlayerController playerController;

    private PlayerState currentState;
    void Start()
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
