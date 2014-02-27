using System;
using System.Runtime.InteropServices;

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
            throw new NotImplementedException();
        }

        public bool SendResponse(string response, bool endLine)
        {
            throw new NotImplementedException();
        }

        public string WaitForString(string searchFor)
        {
            throw new NotImplementedException();
        }
    }
}
