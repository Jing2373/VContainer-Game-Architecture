using VContainer;
using VContainer.Unity;

using Jing.Game;
using Jing.Feature.Options;

namespace Jing.VContainerSetting
{
    public class StartLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<StartScene>().AsImplementedInterfaces();

            Scoped(builder);
            Transient(builder);
        }

        private void Scoped(IContainerBuilder builder)
        {

        }

        private void Transient(IContainerBuilder builder)
        {
            builder.Register<GameSetting>(Lifetime.Transient);
        }
    }
}