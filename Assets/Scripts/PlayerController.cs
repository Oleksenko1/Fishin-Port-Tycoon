using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerInventory inventory;

    public void Update()
    {
        movement.HandleInput();
    }
    public void MovePlayer()
    {
        movement.MovePlayer();
    }
    public PlayerMovement GetPlayerMovement() => movement;
    public PlayerInventory GetPlayerInventory() => inventory;
}
