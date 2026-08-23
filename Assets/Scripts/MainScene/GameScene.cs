using VContainer;
using UnityEngine;
using VContainer.Unity;
using Cysharp.Threading.Tasks;
using Jing.Game.Data;
using Jing.Feature.Options;

namespace Jing.Game
{
    //Main Process
    public class GameScene : BaseScene, IInitializable
    {

        #region ::: Inject :::
        private GameSetting gameSetting;
        private LoadGameRepository loadGameRepository;

        [Inject]
        public void Construct(GameSetting gameSetting, LoadGameRepository loadGameRepository)
        {
            this.gameSetting = gameSetting;
            this.loadGameRepository = loadGameRepository;
        }

        #endregion

        public void Initialize()
        {
            popWindow.uiKey_lable = "PopWindows";
            ImplementAsync().Forget();
        }

        protected override async UniTask ImplementAsync()
        {
            mainUI.uiKey_lable = "GameScene";
            await base.ImplementAsync();
            await loadGameRepository.Load();

            ShowGameHome();
        }

        /// <summary>
        /// Shows the initial scene UI
        /// </summary>
        private void ShowGameHome()
        {
            uiManager.ShowPage("GameHome");

        }



    }
}

