using VContainer;
using VContainer.Unity;
using UnityEngine;

using Jing.Feature.Audio;
using Jing.Feature.UI;
using Jing.Tools;
using Jing.Game.Data;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Jing.VContainerSetting
{
    public class GlobalLifetimeScope : LifetimeScope
    {

        private List<object> _allSettings = new List<object>();

        protected override void Awake()
        {
            InitializeGlobalSettings().Forget();
        }
        private async UniTaskVoid InitializeGlobalSettings()
        {

            IList<object> results = null;
            try
            {
                string labelKey = "Settings";
                results = await Addressables.LoadAssetsAsync<object>(
                    labelKey,
                    _ => { }).ToUniTask();
            }
            catch (UnityEngine.AddressableAssets.InvalidKeyException)
            {
                Debug.LogError($"Cannot find the corresponding resource! Please ensure the Labels are configured correctly.");
                return;
            }

            if (this == null || !gameObject.activeInHierarchy)
            {
                return;
            }

            _allSettings = results.ToList();
            this.Build();
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<AliveForever>();
            Singleton(builder);
            Settings(builder);
            CameraAndCanvas(builder);
            RegisterObj(builder);
            Transient(builder);
        }

        private void Singleton(IContainerBuilder builder)
        {
            builder.Register<UIManager>(Lifetime.Singleton);
            builder.Register<Data_GameInfo>(Lifetime.Singleton);
            builder.Register<AddressableTools>(Lifetime.Singleton).As<IAddressableTools>();
        }
        private void Settings(IContainerBuilder builder)
        {
            foreach (var setting in _allSettings)
            {
                builder.RegisterInstance(setting).AsSelf().AsImplementedInterfaces();
            }
        }

        private void CameraAndCanvas(IContainerBuilder builder)
        {
            var allCameras = transform.parent.GetComponentsInChildren<Camera>(true);
            var allCanvases = transform.parent.GetComponentsInChildren<Canvas>(true);

            Camera mainCam = allCameras.FirstOrDefault(c => c.gameObject.name == "MainCamera");

            Canvas mainCanvas = allCanvases.FirstOrDefault(c => c.gameObject.name == "MainCanvas");
            Canvas storyCanvas = allCanvases.FirstOrDefault(c => c.gameObject.name == "StoryCanvas");
            Canvas popCanvas = allCanvases.FirstOrDefault(c => c.gameObject.name == "PopWindowCanvas");


            var cam_canvas = new GlobalCamsAndCanvas
            {
                MainCamera = mainCam,
                MainCanvas = mainCanvas,
                StoryCanvas = storyCanvas,
                PopWindowCanvas = popCanvas

            };
            builder.RegisterInstance(cam_canvas);
        }

        private void RegisterObj(IContainerBuilder builder)
        {
            AudioManager audioManager = transform.parent.GetComponentInChildren<AudioManager>(true);
            if (audioManager != null)
            {
                builder.RegisterComponent(audioManager).As<AudioManager>().AsImplementedInterfaces();
            }

            LoadingView loadingView = transform.parent.GetComponentInChildren<LoadingView>(true);
            if (loadingView != null)
            {

                builder.RegisterComponent(loadingView);
            }
        }

        private void Transient(IContainerBuilder builder)
        {
        }
    }
}