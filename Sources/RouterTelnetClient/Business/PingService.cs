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

            throw new InvalidOperationException(string.Format("Reply status: '{0}'", pingReply.Status));
        }
    }
}
