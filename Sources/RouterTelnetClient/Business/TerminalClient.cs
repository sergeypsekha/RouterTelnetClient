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
            return this.terminal.Connect();
        }

        public bool SendResponse(string response, bool endLine)
        {
            return this.terminal.SendResponse(response, endLine: true);
        }

        public string WaitForString(string searchFor)
        {
            return this.terminal.WaitForString(searchFor);
        }
    }
}
