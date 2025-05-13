using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    public abstract void OnEnter();
    public abstract void OnUpdate();
    public abstract void OnExit();
}
