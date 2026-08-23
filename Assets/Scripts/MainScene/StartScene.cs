using Cysharp.Threading.Tasks;
using Jing.Feature.Options;
using VContainer;
using VContainer.Unity;


//開始主流程
namespace Jing.Game
{

    public class StartScene : BaseScene, IInitializable
    {

        #region ::: Inject :::
        private GameSetting gameSetting;
        [Inject]
        public void Construct(GameSetting gameSetting)
        {
            this.gameSetting = gameSetting;
        }

        #endregion


        public void Initialize()
        {
            ImplementAsync().Forget();
        }

        protected override async UniTask ImplementAsync()
        {
            mainUI.uiKey_lable = "StartScene";
            await base.ImplementAsync();
            gameSetting.InitShow();
            ShowMainMenu();

        }

        /// <summary>
        /// Shows the initial scene UI
        /// </summary>
        private void ShowMainMenu()
        {
            uiManager.ShowPage("StartMenu");
        }
    }

}
