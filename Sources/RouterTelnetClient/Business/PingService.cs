using System;
using System.Net.NetworkInformation;

namespace RouterTelnetClient.Business
{
    public class PingService : IPingService
    {
        private readonly IAppSettings appSettings;

        public PingService(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public void Send()
        {
            if (!this.appSettings.PingEnabled)
            {
                return;
            }

            var ping = new Ping();
            var pingReply = ping.Send(this.appSettings.Host, this.appSettings.TimeoutSeconds);
            if (pingReply == null)
            {
                throw new ArgumentNullException("pingReply");
            }

            if (pingReply.Status == IPStatus.Success)
            {
                return;
            }

            var message = string.Format(
                "Ping provider can't establish a connection with host: {0}. Reply status: '{1}'",
                this.appSettings.Host,
                pingReply.Status);
            throw new InvalidOperationException(message);
        }
    }
}
