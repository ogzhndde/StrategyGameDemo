using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlacementManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<ScrollContent>().FromComponentInHierarchy().AsSingle();
    }
}
