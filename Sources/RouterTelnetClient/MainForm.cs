using System.Windows.Forms;
using RouterTelnetClient.Business;

namespace RouterTelnetClient
{
    public partial class MainForm : Form
    {
        private IAppSettings appSettings = null;
        private ITerminalClient terminalClient = null;

        public MainForm()
        {
            this.InitializeComponent();
            this.Initialize();
        }

        private void Initialize()
        {
            this.InitializeApplicationSettings();
            this.InitializeTerminal();
        }

        private void InitializeApplicationSettings()
        {
            this.appSettings = new AppSettings();
        }

        private void InitializeTerminal()
        {
            this.terminalClient = new TerminalClient(this.appSettings);
        }
    }
}
