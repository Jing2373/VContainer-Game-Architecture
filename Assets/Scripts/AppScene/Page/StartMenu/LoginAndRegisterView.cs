using Cysharp.Threading.Tasks;
using Jing.UI;
using Jing.UI.Popup;
using TMPro;
using UnityEngine.UI;
using VContainer;

namespace Jing.LoginAndRegister
{

    public class LoginAndRegisterView : UIBaseView
    {
        #region ::: Object From InitGetUI:::
        private TMP_InputField input_account;
        private TMP_InputField input_password;
        private Button btn_register;
        private Button btn_login;
        private TMP_Text text_note;
        #endregion

        #region ::: Inject :::
        private UIManager uiManager;
        private LoginAndRegisterViewModel vm;

        [Inject]
        public virtual void Construct(UIManager uiManager, LoginAndRegisterViewModel vm)
        {
            this.uiManager = uiManager;
            this.vm = vm;
        }
        #endregion

        #region ::: Override :::
        protected override void InitGetUI()
        {
            base.InitGetUI();
            input_account = collector.GetUI<TMP_InputField>("Input_Account");
            input_password = collector.GetUI<TMP_InputField>("Input_Password");
            btn_register = collector.GetUI<Button>("Button_Register");
            btn_login = collector.GetUI<Button>("Button_Login");
            text_note = collector.GetUI<TMP_Text>("Text_Note");
        }

        protected override void InitSet()
        {
            base.InitSet();
            input_account.text = string.Empty;
            input_password.text = string.Empty;
            text_note.text = string.Empty;
        }

        protected override void AddListence()
        {
            RemoveListence();
            base.AddListence();

            btn_register.onClick.AddListener(Register);
            btn_login.onClick.AddListener(Login);
        }

        protected override void RemoveListence()
        {
            base.RemoveListence();
            btn_register.onClick.RemoveListener(Register);
            btn_login.onClick.RemoveListener(Login);
        }

        protected override void Run()
        {

        }
        #endregion

        #region ::: Button OnClick :::
        private async void Register()
        {
            loading.Show();
            string feedback = await vm.Register(input_account.text, input_password.text);

            if (feedback == string.Empty)
            {
                Login();
            }
            text_note.text = feedback;
            loading.Hide();
        }
        private async void Login()
        {
            loading.Show();
            string feedback = await vm.Login(input_account.text, input_password.text);
            if (feedback == string.Empty)
            {
                JumpToMainMenu();
            }
            text_note.text = feedback;
            loading.Hide();
        }

        #endregion

        #region ::: Private Methods :::
        private void JumpToMainMenu()
        {
            loading.Show();
            PopupCommonContentOnly popup = uiManager.OpenPopup<PopupCommonContentOnly>("Popup_Common_ContentOnly");
            popup.Show("Login Successful!", AfterLogin);
            loading.Hide();
            InitSet();
        }
        private void AfterLogin()
        {
            uiManager.ClosePopup();
        }
        #endregion

    }
}
