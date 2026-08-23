using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using VContainer;
using TMPro;
using Unity.VisualScripting;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;


namespace Jing.Feature.Options
{
    public class GameSettingView : BasePageView
    {

        #region ::: GetUI :::
        private TMP_Dropdown resolution_dropdown => GetUI<TMP_Dropdown>("Resolution_Dropdown");
        private TMP_Dropdown displayMode_dropdown => GetUI<TMP_Dropdown>("DisplayMode_Dropdown");
        private TMP_Dropdown language_dropdown => GetUI<TMP_Dropdown>("Language_Dropdown");
        private Slider background_music_slider => GetUI<Slider>("BackgroundMusic_Slider");
        private Slider sound_effects_slider => GetUI<Slider>("SoundEffects_Slider");
        private Slider character_voice_slider => GetUI<Slider>("CharacterVoice_Slider");

        private Button btn_default => GetUI<Button>("Btn_Default");
        private Button btn_save => GetUI<Button>("Btn_Save");

        #endregion


        #region ::: Inject :::

        private GameSetting vm;
        [Inject]
        public void Construct(GameSetting gameSetting)
        {
            vm = gameSetting;
        }

        #endregion

        #region ::: Override :::

        public override void Show()
        {
            base.Show();
            vm.GetSetting();
        }

        public override void Close()
        {
            base.Close();
        }

        #endregion

        #region ::: Listener :::
        protected override void AddListener()
        {
            base.AddListener();
            vm.Action_GetSetting += GetSetting;
            vm.Action_ShowLanguage += ShowLanguage;
            {
                resolution_dropdown.onValueChanged.AddListener(UpdateResolution);
                displayMode_dropdown.onValueChanged.AddListener(UpdateDisplayMode);
                background_music_slider.onValueChanged.AddListener(UpdateBGM);
                sound_effects_slider.onValueChanged.AddListener(UpdateSE);
                character_voice_slider.onValueChanged.AddListener(UpdateCV);
                language_dropdown.onValueChanged.AddListener(UpdateLanguage);
            }
            {
                btn_default.onClick.AddListener(BtnDefault);
                btn_save.onClick.AddListener(BtnSave);
            }
        }


        protected override void RemoveListener()
        {
            base.RemoveListener();
            vm.Action_GetSetting -= GetSetting;
            vm.Action_ShowLanguage -= ShowLanguage;
            {
                resolution_dropdown.onValueChanged.RemoveListener(UpdateResolution);
                displayMode_dropdown.onValueChanged.RemoveListener(UpdateDisplayMode);
                background_music_slider.onValueChanged.RemoveListener(UpdateBGM);
                sound_effects_slider.onValueChanged.RemoveListener(UpdateSE);
                character_voice_slider.onValueChanged.RemoveListener(UpdateCV);
                language_dropdown.onValueChanged.RemoveListener(UpdateLanguage);
            }
            {
                btn_default.onClick.RemoveListener(BtnDefault);
                btn_save.onClick.RemoveListener(BtnSave);
            }
        }

        #endregion

        #region Private Mothods :::

        /// <summary>
        /// Initial Setting
        /// </summary>
        private void GetSetting(Vector2Int resolution, bool is_fullscreen,
                                float bgm, float se, float cv,
                                int language)
        {
            ShowResolution(resolution);
            ShowDisplayMode(is_fullscreen);

            background_music_slider.SetValueWithoutNotify(bgm);
            sound_effects_slider.SetValueWithoutNotify(se);
            character_voice_slider.SetValueWithoutNotify(cv);

            ShowLanguage(language);
        }

        private void ShowResolution(Vector2Int resolution)
        {
            switch (resolution.x)
            {
                case 1920: resolution_dropdown.SetValueWithoutNotify(0); break;
                case 2560: resolution_dropdown.SetValueWithoutNotify(1); break;
                case 3840: resolution_dropdown.SetValueWithoutNotify(2); break;
            }
        }

        private void ShowDisplayMode(bool is_fullscreen)
        {
            displayMode_dropdown.SetValueWithoutNotify(is_fullscreen ? 0 : 1);
        }

        private void ShowLanguage(int language)
        {
            switch (language)
            {
                case 1: language_dropdown.SetValueWithoutNotify(0); break;
                case 0: language_dropdown.SetValueWithoutNotify(1); break;
                case 2: language_dropdown.SetValueWithoutNotify(2); break;
                case 3: language_dropdown.SetValueWithoutNotify(3); break;
            }

            language_dropdown.interactable = true;
            language_dropdown.onValueChanged.AddListener(UpdateLanguage);
        }
        #endregion

        #region ::: Button :::

        #region - DisplaySetting
    
        private void UpdateResolution(int size)
        {
            switch (size)
            {
                case 0: vm.SetResolution(new Vector2Int(1920, 1080)); break;
                case 1: vm.SetResolution(new Vector2Int(2560, 1440)); break;
                case 2: vm.SetResolution(new Vector2Int(3840, 2160)); break;
            }
        }

        private void UpdateDisplayMode(int mode)
        {
            switch (mode)
            {
                case 0: vm.SetDisplayMode(true); break;  //full screen
                case 1: vm.SetDisplayMode(false); break; //windows
            }

        }
        #endregion

        #region - SoundSetting 
        /// <summary>
        /// Update Background Music Volume
        /// </summary>
        private void UpdateBGM(float value)
        {
            vm.ChangeBGMVolume(value);
        }

        /// <summary>
        /// Update Sound Effects Volume
        /// </summary>
        private void UpdateSE(float value)
        {
            vm.ChangeSEVolume(value);
        }

        /// <summary>
        /// Update Character Voice Volume
        /// </summary>
        private void UpdateCV(float value)
        {
            vm.ChangeCVVolume(value);
        }

        #endregion

        #region - SystemSetting
        private async void UpdateLanguage(int language)
        {
            language_dropdown.onValueChanged.RemoveListener(UpdateLanguage);
            language_dropdown.interactable = false;
            switch (language)
            {
                case 0: await vm.ChangeLanguage(1); break;
                case 1: await vm.ChangeLanguage(0); break;
                case 2: await vm.ChangeLanguage(2); break;
                case 3: await vm.ChangeLanguage(3); break;
            }
        }
        #endregion

        /// <summary>
        /// Default Setting
        /// </summary>
        private void BtnDefault()
        {
            vm.SetResolution(new Vector2Int(1920, 1080));
            vm.SetDisplayMode(false);
            vm.ChangeBGMVolume(1);
            vm.ChangeSEVolume(1);
            vm.ChangeCVVolume(1);
            vm.ChangeLanguage(1).Forget();
            vm.GetSetting();
        }
        private void BtnSave()
        {
            vm.Save();
        }


        #endregion
    }
}
