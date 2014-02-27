using System;

using RouterTelnetClient.Models;
using RouterTelnetClient.TelnetClient;

namespace RouterTelnetClient.Business
{
    public class TerminalClient : ITerminalClient
    {
        private readonly IAppSettings appSettings;

        private readonly Terminal terminal;

        public TerminalClient(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
            this.terminal = new Terminal(
                this.appSettings.Host,
                this.appSettings.Port,
                this.appSettings.TimeoutSeconds,
                this.appSettings.VirtualScreenHeight,
                this.appSettings.VirtualScreenWidth);
        }

        public bool Connect()
        {
            if (this.terminal.Connect())
            {
                return true;
            }

            var message = string.Format(
                "Terminal client Can't establish a connection with host: {0}:{1}",
                this.appSettings.Host,
                this.appSettings.Port);

            throw new InvalidOperationException(message);
        }

        public void Login()
        {
            return;
        }

        public void Send(VoiceProfileModel model)
        {
            throw new NotImplementedException();
        }
    }
}
