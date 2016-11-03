using GitFlowVS.Extension.ViewModels;
using RestSharp;
using System.Windows.Input;

namespace GitlabBuilds
{
    public class LoginViewModel: ViewModelBase
    {
        public ICommand LoginCommand2 { get; private set; }
        public bool CanLogin => true;
        private string _username;
        private string _password;

        private RestClient _client;

        public string PrivateToken { get; set; }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChangedEvent("Username");
            }

        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChangedEvent("Password");
            }
        }

        public LoginViewModel()
        {
            _client = new RestClient("");
            LoginCommand2 = new RelayCommand(p => Login(), p => CanLogin);
        }

        private void Login()
        {
            var request = new RestRequest($"/session", Method.POST);
            request.AddParameter("login", Username);
            request.AddParameter("password", Password);

            var r = _client.Execute<LoginResponse>(request);
            if (r.StatusCode == System.Net.HttpStatusCode.Created)
            {
                PrivateToken = r.Data.Private_Token;
            }
        }
    }
}