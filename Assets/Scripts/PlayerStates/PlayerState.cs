using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public PlayerStateController stateController;
    public PlayerState(PlayerController playerController, PlayerStateController stateController)
    {
        this.playerController = playerController;
        this.stateController = stateController;
    }
    public virtual void InitializeState() {}
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
