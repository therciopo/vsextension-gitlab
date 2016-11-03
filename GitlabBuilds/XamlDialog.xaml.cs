using Microsoft.VisualStudio.PlatformUI;

namespace GitlabBuilds
{
    /// <summary>
    /// Interaction logic for XamlDialog.xaml
    /// </summary>
    public partial class XamlDialog : DialogWindow
    {
        public LoginViewModel _model { get; set; }

        public string PrivateToken { get; set; }

        public XamlDialog(string helpTopic, LoginViewModel model) : base(helpTopic)
        {
            this._model = model;

            InitializeComponent();
            DataContext = model;
        }

        public XamlDialog(LoginViewModel model)
        {
            this._model = model;
            InitializeComponent();
            DataContext = model;
        }
    }
}


