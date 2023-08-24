using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ZenjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<UIManager>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlacementManager>().FromComponentInHierarchy().AsSingle();
    }
}
