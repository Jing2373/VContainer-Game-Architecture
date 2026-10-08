using System;
using Cysharp.Threading.Tasks;
using Jing.Game.Services;

using VContainer;

namespace Jing.LoginAndRegister
{
    public class LoginAndRegisterViewModel
    {
        #region ::: Inject :::
        private IUserDataService userDataService;
        [Inject]
        public virtual void Construct(IUserDataService userDataService)
        {
            this.userDataService = userDataService;
        }
        #endregion

        #region ::: Public Methods :::
        public async UniTask<string> Register(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
            {
                return "Error: Username and password are required."; ;
            }

            // Call server API to register account.
            return string.Empty;
        }

        public async UniTask<string> Login(string account, string password)
        {
            if (string.IsNullOrWhiteSpace(account) || string.IsNullOrWhiteSpace(password))
            {
                return "Error: Username and password are required."; ;
            }

            // Call server API to login account.
            return string.Empty;
        }
        #endregion

    }
}
