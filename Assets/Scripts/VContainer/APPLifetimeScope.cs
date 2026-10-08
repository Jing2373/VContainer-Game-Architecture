using Jing.LoginAndRegister;
using Jing.UI;
using VContainer;
using VContainer.Unity;
namespace Jing.VContainer
{
    public class APPLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<MainScene>().As<IInitializable>();
            Singleton(builder);
            Transient(builder);
        }

        private void Singleton(IContainerBuilder builder)
        {
            builder.Register<UIManager>(Lifetime.Singleton).As<IUIManager>().AsSelf(); ;

        }
        private void Transient(IContainerBuilder builder)
        {
            builder.Register<LoginAndRegisterViewModel>(Lifetime.Transient);
        }
    }

}