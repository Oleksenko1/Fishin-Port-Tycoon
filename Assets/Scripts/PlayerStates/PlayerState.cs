using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public PlayerStateController stateController;
    public void InitializeState(PlayerController playerController, PlayerStateController stateController)
    {
        this.playerController = playerController;
        this.stateController = stateController;

        OnInitializeState();
    }
    public virtual void OnInitializeState(){}
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
