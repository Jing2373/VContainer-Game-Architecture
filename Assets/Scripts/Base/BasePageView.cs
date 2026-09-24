using UnityEngine.UI;
using VContainer;

using Jing.Feature.UI;
using System.Diagnostics;


namespace Jing.Feature
{
    public class BasePageView : BaseView
    {

        #region ::: GetUI :::
        protected Button btn_Back;
        #endregion


        #region ::: Inject :::
        protected IUIManager uiManager;

        [Inject]
        public void Construct(IUIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        #endregion

        #region ::: Override :::
        protected override void AddAndGetComponent()
        {
           base.AddAndGetComponent();
           btn_Back = collector.GetUI<Button>("Btn_Back");
        }

        #endregion

        #region ::: Listener :::

        protected override void AddListener()
        {
            base.AddListener();
            btn_Back?.onClick.AddListener(BtnBack);
        }

        protected override void RemoveListener()
        {
            base.RemoveListener();
            btn_Back?.onClick.RemoveListener(BtnBack);
        }

        #endregion


        #region :::  Button Click  :::
        /// <summary>
        /// Back
        /// </summary>
        protected virtual void BtnBack()
        {
            uiManager.ClosePage();
        }

        /// <summary>
        /// ButtonEnter Show
        /// </summary>
        protected virtual void BtnEnter() { }

        #endregion

    }
}