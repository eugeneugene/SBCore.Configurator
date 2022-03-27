using Shared;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SBCore.Configurator.Models
{
    internal class AuthModel : INotifyPropertyChanged
    {
        private readonly Random rnd = new(DateTime.Now.Millisecond);

        private bool disableDecrypt = false;
        private bool disableEncrypt = false;

        public bool GenerateSalt { get; set; } = false;

        private string login;
        public string Login
        {
            get => login;
            set
            {
                if (login != value)
                {
                    login = value;

                    if (!disableEncrypt)
                    {
                        if (GenerateSalt)
                        {
                            disableEncrypt = true;
                            Salt = LoginCrypt.MakeSalt(rnd.Next(3, 5));
                            disableEncrypt = false;
                        }

                        disableDecrypt = true;
                        Auth = LoginCrypt.EncryptLoginPwd(login, password, salt);
                        AuthValid = true;
                        disableDecrypt = false;
                    }

                    NotifyPropertyChanged(nameof(Login));
                }
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;

                    if (!disableEncrypt)
                    {
                        if (GenerateSalt)
                        {
                            disableEncrypt = true;
                            Salt = LoginCrypt.MakeSalt(rnd.Next(3, 5));
                            disableEncrypt = false;
                        }

                        disableDecrypt = true;
                        Auth = LoginCrypt.EncryptLoginPwd(login, password, salt);
                        AuthValid = true;
                        disableDecrypt = false;
                    }

                    NotifyPropertyChanged(nameof(Password));
                }
            }
        }

        private string salt;
        public string Salt
        {
            get => salt;
            set
            {
                if (salt != value)
                {
                    salt = value;

                    if (!disableEncrypt)
                    {
                        disableDecrypt = true;
                        Auth = LoginCrypt.EncryptLoginPwd(login, password, salt);
                        AuthValid = true;
                        disableDecrypt = false;
                    }

                    NotifyPropertyChanged(nameof(Salt));
                }
            }
        }

        private string auth;
        public string Auth
        {
            get => auth;
            set
            {
                if (auth != value)
                {
                    auth = value;

                    if (!disableDecrypt)
                    {
                        disableEncrypt = true;
                        if (LoginCrypt.TryDecryptLoginPwd(auth, out string login, out string pwd, out string slt))
                        {
                            Login = login;
                            Password = pwd;
                            Salt = slt;
                            AuthValid = true;
                        }
                        else
                        {
                            Login = string.Empty;
                            Password = string.Empty;
                            Salt = string.Empty;
                            AuthValid = false;
                        }
                        disableEncrypt = false;
                    }

                    NotifyPropertyChanged(nameof(Auth));
                }
            }
        }

        private bool authValid;
        public bool AuthValid
        {
            get => authValid;

            set
            {
                if (authValid != value)
                {
                    authValid = value;
                    NotifyPropertyChanged(nameof(AuthValid));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (string.IsNullOrEmpty(propertyName))
                return;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
