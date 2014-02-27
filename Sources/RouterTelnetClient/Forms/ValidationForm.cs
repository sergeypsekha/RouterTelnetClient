using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using RouterTelnetClient.Business;

namespace RouterTelnetClient.Forms
{
    public partial class ValidationForm : Form
    {
        public ValidationForm(IEnumerable<ValidationResult> validationResults)
        {
            InitializeComponent();
            this.Initalize(validationResults);
        }

        private void Initalize(IEnumerable<ValidationResult> validationResults)
        {
            var sb = new StringBuilder();
            foreach (var validationResult in validationResults)
            {
                sb.AppendFormat("{0}: {1}", validationResult.PropertyName, validationResult.Message).AppendLine();
            }
            this.richTextBox1.Text = sb.ToString();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}