using Game.Scripts.L10n;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Game.Scripts
{
    public class Installer : LifetimeScope
    {
        [SerializeField] private GameConfig _config;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_config).AsSelf();
            builder.Register<LocalizationService>(Lifetime.Singleton).AsSelf();
        }
    }
}