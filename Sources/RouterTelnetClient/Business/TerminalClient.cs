using System;

using RouterTelnetClient.TelnetClient;

namespace RouterTelnetClient.Business
{
    public class TerminalClient : ITerminalClient
    {
        private readonly IAppSettings appSettings;

        public TerminalClient(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
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
