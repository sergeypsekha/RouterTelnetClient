using System;
using System.Windows.Forms;
using RouterTelnetClient.Business;

namespace RouterTelnetClient
{
    public partial class MainForm : Form
    {
        private ITelnetService telnetService = null;

        public MainForm()
        {
            this.InitializeComponent();
            this.Initialize();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this.telnetService.Connect();
        }

        private void Initialize()
        {
            this.telnetService = new TelnetService();
        }
    }
}
