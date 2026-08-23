using Jing.Game;
using Jing.Game.Data;
using VContainer;
using VContainer.Unity;


namespace Jing.VContainerSetting
{
    public class GameLifetimeScope : LifetimeScope
    {

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<GameScene>().As<IInitializable>();

            Singleton(builder);
            Transient(builder);
        }

        private void Singleton(IContainerBuilder builder)
        {
            builder.Register<LoadGameRepository>(Lifetime.Singleton);
            builder.Register<StaticData_GameRepository>(Lifetime.Singleton);
        }

        private void Transient(IContainerBuilder builder)
        {

        }

    }

}