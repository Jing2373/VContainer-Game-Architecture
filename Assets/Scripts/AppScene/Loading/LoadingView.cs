using Jing.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Jing.Game.Loading
{
    public class LoadingView : MonoBehaviour, IInitializable
    {
        #region ::: Inject :::
        private ILoadingService vm;

        [Inject]
        public virtual void Construct(ILoadingService vm)
        {
            this.vm = vm;
        }
        #endregion

        public void Initialize()
        {
            vm.Action_IsShow += IsShow;
            IsShow(false);
        }

        private void IsShow(bool is_show)
        {
            gameObject.SetActive(is_show);
        }


    }
}