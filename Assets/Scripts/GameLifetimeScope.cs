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
    [SerializeField] private FishingZone fishingZone;
    [Space(10)]
    [SerializeField] private UIFishing fishingUI;
    [SerializeField] private UIFishingBar fishingBarUI;
    [SerializeField] private UICatchedFish catchedFishUI;
    [SerializeField] private UICatchedFishIcon catchedFishIconUI;
    [Space(10)]
    [SerializeField] private FloatingJoystick joystick;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(playerController).AsSelf();
        builder.RegisterComponent(playerInventory).AsSelf();
        builder.RegisterComponent(playerMovement).AsSelf();
        builder.RegisterComponent(playerStateController).AsSelf();
        builder.RegisterComponent(fishingController).AsSelf();

        builder.RegisterComponent(ocean).AsSelf();

        builder.RegisterComponent(joystick).AsSelf();

        builder.RegisterComponent(fishingUI).AsSelf();
        builder.RegisterComponent(fishingBarUI).AsSelf();
        builder.RegisterComponent(catchedFishUI).AsSelf();
        builder.RegisterComponent(catchedFishIconUI).AsSelf();
    }

    public void Start()
    {
        Container.Inject(playerController);
        Container.Inject(playerMovement);
        Container.Inject(playerStateController);

        Container.Inject(fishingController);
        Container.Inject(fishingZone);

        Container.Inject(catchedFishUI);
    }
}
