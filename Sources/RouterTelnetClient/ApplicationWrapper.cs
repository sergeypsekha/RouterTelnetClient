using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Microsoft.VisualBasic.ApplicationServices;

using RouterTelnetClient.Forms;

using UnhandledExceptionEventArgs = Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs;

namespace RouterTelnetClient
{
    internal class ApplicationWrapper : WindowsFormsApplicationBase
    {
        public ApplicationWrapper()
        {
            this.UnhandledException += UnhandledExceptionHandler;
        }

        protected override void OnCreateMainForm()
        {
            this.MainForm = new MainForm();
        }

        private void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            var form = new ErrorForm(e.Exception.Message);
            form.ShowDialog();
            Environment.Exit(1);
        }
    }
}