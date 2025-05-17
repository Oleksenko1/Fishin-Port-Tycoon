using VContainer;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Inject] private PlayerMovement movement;
    [Inject] private PlayerInventory inventory;

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
