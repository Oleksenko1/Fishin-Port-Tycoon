using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

public class PlayerStateController : MonoBehaviour
{
    public IdleState idleState;
    public MovingState movingState;
    public FishingState fishingState;
    [Inject] private PlayerController playerController;
    [Inject] private FishingController fishingController;

    private PlayerState currentState;
    void Start()
    {
        idleState = new IdleState(playerController, this);
        idleState.InitializeState();

        movingState = new MovingState(playerController, this);
        movingState.InitializeState();
        
        fishingState = new FishingState(playerController, this, fishingController);
        fishingState.InitializeState();

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

        newState.OnEnter();
    }
}
