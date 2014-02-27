using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RouterTelnetClient.Business;
using RouterTelnetClient.TelnetClient;

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
