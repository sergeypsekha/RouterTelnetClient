using RouterTelnetClient.Forms;
using System;
using System.Windows.Forms;

namespace RouterTelnetClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            InitializeApplication();
            TryRunApplication();
        }

        private static void TryRunApplication()
        {
            var application = new ApplicationWrapper();
            application.Run(new string[] { });
        }

        private static void InitializeApplication()
        {
            Application.ThreadException += Application_ThreadException;
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string message = GetExceptionMessage(e);
            var form = new ErrorForm(message);
            form.ShowDialog();
            Environment.Exit(1);
        }

        private static string GetExceptionMessage(UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            return exception == null ? "Can't exctract an exception's message. Possible general fault." : exception.Message;
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
