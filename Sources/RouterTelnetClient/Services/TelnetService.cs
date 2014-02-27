using RouterTelnetClient.Business;
using RouterTelnetClient.Models;

namespace RouterTelnetClient.Services
{
    public class TelnetService : ITelnetService
    {
        private IPingService pingService = null;

        private IAppSettings appSettings = null;

        private ITelnet telnet = null;

        public TelnetService()
        {
            this.Initialize();
        }

        public void Submit(VoiceProfileViewModel viewModel)
        {
            this.pingService.Send();
            this.telnet.Send(viewModel);
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
            this.telnet = new Telnet(this.appSettings);
        }
    }
}