using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameLifetimeScope : LifetimeScope, IStartable
{
    [Header("What to inject")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] private Ocean ocean;
    [Space(10)]
    [SerializeField] private UIFishing fishingUI;
    [SerializeField] private UIFishingBar fishingBarUI;
    [SerializeField] private UICatchedFish catchedFishUI;
    [Header("Where to inject")]
    [SerializeField] private FishingController fishingController;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(playerController).AsSelf();
        builder.RegisterComponent(playerInventory).AsSelf();
        builder.RegisterComponent(ocean).AsSelf();

        builder.RegisterComponent(fishingUI).AsSelf();
        builder.RegisterComponent(fishingBarUI).AsSelf();
        builder.RegisterComponent(catchedFishUI).AsSelf();
    }

    public void Start()
    {
        Container.Inject(fishingController);
    }
}
