using GitFlowVS.Extension.ViewModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GitlabBuilds
{
    public class ProjectViewModel : ViewModelBase
    {
        private Project _selectedProject;
        private ObservableCollection<Project> _projects = new ObservableCollection<Project>();
        private ObservableCollection<Pipeline> _pipelines = new ObservableCollection<Pipeline>();
        private RestClient _client;
        private LoginViewModel _loginViewModel;

        public string PrivateToken { get; set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand LoginCommand { get; private set; }

        public bool CanRefresh => true;
        public bool CanLogin => true;

        public ProjectViewModel()
        {
            RefreshCommand = new RelayCommand(p => Refresh(), p => CanRefresh);
            LoginCommand = new RelayCommand(p => Login(), p => CanLogin);
            _client = new RestClient("");
            _loginViewModel = new LoginViewModel();
        }

        private void Login()
        {
            Application.Current.Dispatcher.Invoke((Action)delegate {

                var xamlDialog = new XamlDialog("Microsoft.VisualStudio.PlatformUI.DialogWindow", _loginViewModel)
                {
                    HasMinimizeButton = false,
                    HasMaximizeButton = false
                };

                var r = xamlDialog.ShowModal();

                var res = xamlDialog.DialogResult;

                PrivateToken = xamlDialog.PrivateToken;
            });
        }
        private void Refresh()
        {
            RaisePropertyChangedEvent("Pipelines");
        }

        public Project SelectedProject
        {
            get { return _selectedProject; }
            set
            {
                if (Equals(value, _selectedProject)) return;
                _selectedProject = value;
                RaisePropertyChangedEvent("Pipelines");
            }
        }

        public IEnumerable<Pipeline> Pipelines
        {
            get
            {
                if (_selectedProject != null)
                {
                    _pipelines = GetPipelines(_selectedProject.Id);
                }
                return _pipelines;
            }
        }

        public IEnumerable<Project> Projects
        {
            get
            {
                _projects = GetProjects();
                return _projects;
            }
        }

        public ObservableCollection<Pipeline> GetPipelines(int projectId)
        {
            var request = new RestRequest($"/projects/{projectId}/builds", Method.GET);
            request.AddHeader("PRIVATE-TOKEN", "usasf5KExKAD8PPoVFhb");

            var list = _client.Execute<List<Build>>(request).Data
                .GroupBy(p =>p.Pipeline.Id)
                .Select(x => new Pipeline
                {
                    Id = x.Key,
                    Ref = x.First().Pipeline.Ref,// commi
                    Status = x.First().Pipeline.Status,
                    Commit = $"{x.First().Commit.Title}",
                    Commit_id = x.First().Commit.Short_Id,
                    Finished_at = x.First().finished_at,
                    UserName = x.First().User.Name,
                    Builds = x.ToList()
                });

            return new ObservableCollection<Pipeline>(list);
        }

        public ObservableCollection<Project> GetProjects()
        {
            var request = new RestRequest("/projects", Method.GET);
            request.AddHeader("PRIVATE-TOKEN", "usasf5KExKAD8PPoVFhb");

            var list = _client.Execute<List<Project>>(request).Data.OrderByDescending(p => p.last_activity_at);
            return new ObservableCollection<Project>(list);
        }
    }
}