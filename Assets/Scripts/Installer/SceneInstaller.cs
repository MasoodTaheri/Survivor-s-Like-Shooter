using Enemy;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private UIHandler _uiHandler;
        [SerializeField] private LootManager _lootManager;
        [SerializeField] private EnemiesController _enemyController;

        public override void InstallBindings()
        {
            Container.Bind<UIHandler>().FromInstance(_uiHandler).AsSingle();
            Container.Bind<LootManager>().FromInstance(_lootManager).AsSingle();
            Container.Bind<EnemiesController>().FromInstance(_enemyController).AsSingle();
        }
    }
}