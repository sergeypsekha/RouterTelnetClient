using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterTelnetClient.Business
{
    public class TelnetService : ITelnetService
    {
        private IPingService pingService = null;
        private IAppSettings appSettings = null;
        private ITerminalClient terminalClient = null;

        public TelnetService()
        {
            this.Initialize();
        }

        public void Connect()
        {
            this.pingService.Send();
            this.terminalClient.Connect();
        }

        private void Initialize()
        {
            this.InitializeApplicationSettings();
            this.InitializeKeepAliveService();
            this.InitializeTerminalClient();
        }

        private void InitializeKeepAliveService()
        {
            this.pingService = new PingService(this.appSettings);
        }

        private void InitializeApplicationSettings()
        {
            this.appSettings = new AppSettings();
        }

        private void InitializeTerminalClient()
        {
            this.terminalClient = new TerminalClient(this.appSettings);
        }
    }
}
