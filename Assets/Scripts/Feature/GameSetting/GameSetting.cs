using UnityEngine;
using VContainer;

using Jing.Feature.Audio;
using Jing.Game.Data;
using System;
using UnityEngine.Localization.Settings;
using Cysharp.Threading.Tasks;

namespace Jing.Feature.Options
{
    public class GameSetting
    {

        public Action<Vector2Int, bool, float, float, float, int> Action_GetSetting;
        public Action<int> Action_ShowLanguage;  // Switching languages resets the settings, so it is forcefully set here.

        private Vector2Int current_resolution;
        private bool current_display_mode;
        private float current_bgm;
        private float current_se;
        private float current_cv;
        private int current_language;

        #region ::: Default :::
        private readonly Vector2Int default_resolution = new Vector2Int(1920, 1080);
        private readonly bool default_display_mode = false;
        private readonly float default_bgm = 1f;
        private readonly float default_se = 1f;
        private readonly float default_cv = 1f;
        private readonly int default_language = 1;
        #endregion

        #region ::: Inject :::
        private Data_GameInfo data_gameInfo;
        private IAudioManager audioManager;

        [Inject]
        public void Construct(Data_GameInfo data_gameInfo, IAudioManager audioManager)
        {
            this.data_gameInfo = data_gameInfo;
            this.audioManager = audioManager;
        }

        #endregion

        #region Public Mothods :::
        /// <summary>
        /// Restores current settings at scene initialization.
        /// </summary>
        public async void InitShow()
        {
            Screen.SetResolution(data_gameInfo.Resolution.x, data_gameInfo.Resolution.y, data_gameInfo.FullScreen);
            audioManager.UpdateMainMusicVolume(data_gameInfo.BGMVolume);
            audioManager.UpdateSoundVolume(data_gameInfo.SEVolume);
            audioManager.UpdateVoiceVolume(data_gameInfo.CVVolume);

            await LocalizationSettings.InitializationOperation.ToUniTask();
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[data_gameInfo.Language];
        }

        public void GetSetting()
        {
            Action_GetSetting?.Invoke(data_gameInfo.Resolution, data_gameInfo.FullScreen,
            data_gameInfo.BGMVolume, data_gameInfo.SEVolume, data_gameInfo.CVVolume,
            data_gameInfo.Language);
        }


        /// <summary>
        /// Resolution
        /// </summary>
        public void SetResolution(Vector2Int size)
        {
            Screen.SetResolution(size.x, size.y, Screen.fullScreenMode);
            current_resolution = size;
        }
        /// <summary>
        /// Sets fullscreen or windowed mode
        /// </summary>
        public void SetDisplayMode(bool isfull)
        {
            Screen.fullScreenMode = isfull ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
            current_display_mode = isfull;
        }

        /// <summary>
        /// Changes the background music volume
        /// </summary>
        public void ChangeBGMVolume(float volume)
        {

            audioManager.UpdateMainMusicVolume(volume);
            current_bgm = volume;
        }
        /// <summary>
        /// Changes the sound effect volume.
        /// </summary>
        public void ChangeSEVolume(float volume)
        {
            audioManager.UpdateSoundVolume(volume);
            current_se = volume;
        }
        /// <summary>
        /// Changes the character voice volume.
        /// </summary>
        public void ChangeCVVolume(float volume)
        {

            current_cv = volume;
        }

        /// <summary>
        /// Changes the languageg
        /// </summary>
        public async UniTask ChangeLanguage(int languageg)
        {
            current_language = languageg;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageg];
            await LocalizationSettings.SelectedLocaleAsync.ToUniTask();
            await UniTask.Delay(System.TimeSpan.FromSeconds(0.3f), ignoreTimeScale: true);
            Action_ShowLanguage?.Invoke(languageg);
        }

        /// <summary>
        /// Save data
        /// </summary>
        public void Save()
        {
            data_gameInfo.Resolution = current_resolution;
            data_gameInfo.FullScreen = current_display_mode;
            data_gameInfo.BGMVolume = current_bgm;
            data_gameInfo.SEVolume = current_se;
            data_gameInfo.CVVolume = current_cv;
            data_gameInfo.Language = current_language;
        }

        /// <summary>
        /// Returns to defaults
        /// </summary>
        public void Default()
        {
            data_gameInfo.Resolution = default_resolution;
            data_gameInfo.FullScreen = default_display_mode;
            data_gameInfo.BGMVolume = default_bgm;
            data_gameInfo.SEVolume = default_se;
            data_gameInfo.CVVolume = default_cv;
            data_gameInfo.Language = default_language;
            GetSetting();
        }

        #endregion
    }
}
