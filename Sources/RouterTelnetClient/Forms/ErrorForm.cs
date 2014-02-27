using System;
using System.Windows.Forms;

namespace RouterTelnetClient.Forms
{
    public partial class ErrorForm : Form
    {
        public ErrorForm(string message)
        {
            InitializeComponent();
            this.InitializeControls(message);
        }

        private void InitializeControls(string message)
        {
            this.richTextBox1.Text = message;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
        }
    }
}
