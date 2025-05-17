using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope, IStartable
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerStateController playerStateController;
    [SerializeField] private FishingController fishingController;
    [Space(10)]
    [SerializeField] private Ocean ocean;
    [Space(10)]
    [SerializeField] private UIFishing fishingUI;
    [SerializeField] private UIFishingBar fishingBarUI;
    [SerializeField] private UICatchedFish catchedFishUI;
    [Space(10)]
    [SerializeField] private FloatingJoystick joystick;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(playerController).AsSelf();
        builder.RegisterComponent(playerInventory).AsSelf();
        builder.RegisterComponent(playerMovement).AsSelf();

        builder.RegisterComponent(ocean).AsSelf();

        builder.RegisterComponent(joystick).AsSelf();

        builder.RegisterComponent(fishingUI).AsSelf();
        builder.RegisterComponent(fishingBarUI).AsSelf();
        builder.RegisterComponent(catchedFishUI).AsSelf();
    }

    public void Start()
    {
        Container.Inject(fishingController);

        Container.Inject(playerController);
        Container.Inject(playerMovement);
        Container.Inject(playerStateController);
    }
}
