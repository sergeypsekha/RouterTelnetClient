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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.telnetService.Connect();
        }

        private void Initialize()
        {
            this.telnetService = new TelnetService();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDigitMap.ReadOnly = !this.checkBox1.Checked;
        }
    }
}
