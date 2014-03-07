using System.Windows.Forms;

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
            this.InitializeKeepAliveService(viewModel);
            this.InitializeTerminalClient(viewModel);

            this.pingService.Send();
            this.telnet.Send(viewModel);
        }

        private void Initialize()
        {
            this.InitializeApplicationSettings();
        }

        private void InitializeKeepAliveService(VoiceProfileViewModel viewModel)
        {
            var settings = new FormAppSettings
                               {
                                   Host = viewModel.IpAddress,
                                   PingEnabled = viewModel.PingEnabled,
                               };
            this.pingService = new PingService(settings);
        }

        private void InitializeApplicationSettings()
        {
            this.appSettings = new AppSettings();
        }

        private void InitializeTerminalClient(VoiceProfileViewModel viewModel)
        {
            var settings = new FormAppSettings
                               {
                                   Host = viewModel.IpAddress,
                                   UserName = viewModel.UserName,
                                   Password = viewModel.Password,
                                   Port = this.appSettings.Port,
                                   TimeoutSeconds = this.appSettings.TimeoutSeconds,
                                   VirtualScreenHeight = this.appSettings.VirtualScreenHeight,
                                   VirtualScreenWidth = this.appSettings.VirtualScreenWidth
                               };

            this.telnet = new Telnet(settings);
        }
    }
}