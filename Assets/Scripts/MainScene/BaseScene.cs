using VContainer;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

using Jing.Feature.UI;
using Jing.Tools;
using Unity.VisualScripting;

namespace Jing.Game
{
    public class BaseScene : MonoBehaviour
    {
        protected UIKey mainUI = new UIKey();
        protected UIKey popWindow = new UIKey();

        #region ::: Inject :::
        [Inject] protected GlobalCamsAndCanvas globalCamsAndCanvas;
        protected IObjectResolver resolver;
        protected UIManager uiManager;

        [Inject]
        public virtual void Construct(IObjectResolver resolver, UIManager uiManager)
        {
            this.resolver = resolver;
            this.uiManager = uiManager;
        }
        #endregion

        /// <summary> Executes the main process </summary>
        protected virtual async UniTask ImplementAsync()
        {
            popWindow.uiKey_lable = "PopWindows";
            await LoadFromAddressableAsync();
            SetUIManager();
        }

        protected virtual async UniTask LoadFromAddressableAsync()
        {
            IList<GameObject> main_results = null;
            IList<GameObject> pop_results = null;

            try
            {
                main_results = await Addressables.LoadAssetsAsync<GameObject>(
                    mainUI.uiKey_lable,
                    _ => { },
                    Addressables.MergeMode.Union).ToUniTask();

                pop_results = await Addressables.LoadAssetsAsync<GameObject>(
                    popWindow.uiKey_lable,
                    _ => { },
                    Addressables.MergeMode.Union).ToUniTask();
            }
            catch (InvalidKeyException)
            {
                Debug.LogError($"Cannot find the corresponding resource! Please ensure the Labels are configured correctly");
                return;
            }

            mainUI.uiObj = new Dictionary<string, GameObject>();
            foreach (var prefab in main_results)
            {
                mainUI.uiObj.Add(prefab.name, prefab);
            }

            popWindow.uiObj = new Dictionary<string, GameObject>();
            foreach (var prefab in pop_results)
            {
                popWindow.uiObj.Add(prefab.name, prefab);
            }
        }


        /// <summary>
        /// Sets up the UI Manager.
        /// </summary>
        protected virtual void SetUIManager()
        {
            uiManager?.Init(globalCamsAndCanvas.MainCanvas, globalCamsAndCanvas.PopWindowCanvas, mainUI.uiObj, popWindow.uiObj, resolver);
        }

        public void OnDisable()
        {
            uiManager?.Close();
        }

    }

    public class UIKey
    {
        public string uiKey_lable = string.Empty;
        public Dictionary<string, GameObject> uiObj;
    }
}

