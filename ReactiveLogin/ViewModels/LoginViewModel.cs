using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UltraDBSimulator.Models;

namespace UltraDBSimulator.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        private readonly Login _login;
        private bool _isBusy;
        private string _password;

        private string _userName;

        public LoginViewModel(Login login)
        {
            _login = login;

            var canLogin = this.WhenAnyValue(x => x.UserName, x => x.Password, x => x.IsBusy,
                (u, p, b) => !b && !string.IsNullOrEmpty(u) && !string.IsNullOrEmpty(p));

            LoginCommand = ReactiveCommand.CreateFromTask<string, bool>(_ => DoLogin(), canLogin);
            LoginCommand.Subscribe(CheckLogin);
            this.WhenAnyValue(x => x.UserName).Subscribe(n => _login.UserName = n);
            this.WhenAnyValue(x => x.Password).Subscribe(p => _login.Password = p);
        }

        public string UserName
        {
            get { return _userName; }
            set { this.RaiseAndSetIfChanged(ref _userName, value); }
        }


        public string Password
        {
            get { return _password; }
            set { this.RaiseAndSetIfChanged(ref _password, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { this.RaiseAndSetIfChanged(ref _isBusy, value); }
        }

        public ReactiveCommand<string, bool> LoginCommand { get; }

        private async Task<bool> DoLogin()
        {
            IsBusy = true;
            return await _login.DoLogin();
        }

        private void CheckLogin(bool b)
        {
            IsBusy = false;
        }
    }
}
