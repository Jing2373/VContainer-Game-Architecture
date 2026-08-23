using UnityEngine.UI;
using VContainer;

using Jing.Feature.UI;
using System.Diagnostics;


namespace Jing.Feature
{
    public class BasePageView : BaseView
    {

        #region ::: GetUI :::
        protected Button btn_Back => GetUI<Button>("Btn_Back");
        #endregion


        #region ::: Inject :::
        protected UIManager uiManager;

        [Inject]
        public void Construct(UIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        #endregion

        #region ::: 繼承 :::
        /// <summary>
        /// 顯示
        /// </summary>
        public override void Show()
        {
            AddListener();
        }

        /// <summary>
        /// 關閉
        /// </summary>
        public override void Close()
        {
            RemoveListener();
        }
        #endregion

        #region ::: Listener 監聽 :::

        protected virtual void AddListener()
        {
            btn_Back?.onClick.AddListener(BtnBack);
        }

        protected virtual void RemoveListener()
        {
            btn_Back?.onClick.RemoveListener(BtnBack);
        }

        #endregion


        #region :::  按鈕區  :::
        /// <summary>
        /// 返回
        /// </summary>
        protected virtual void BtnBack()
        {
            uiManager.Back();
        }

        /// <summary>
        /// 按鈕滑入（可能會有提示視窗）
        /// </summary>
        protected virtual void BtnEnter() { }

        #endregion

    }
}