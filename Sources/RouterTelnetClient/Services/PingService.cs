using System;
using System.Net.NetworkInformation;

using RouterTelnetClient.Business;
using RouterTelnetClient.Forms;

namespace RouterTelnetClient.Services
{
    public class PingService : IPingService
    {
        private readonly IAppSettings appSettings;

        private readonly IProgressCallback progressCallback;

        public PingService(IAppSettings appSettings, IProgressCallback progressCallback)
        {
            this.appSettings = appSettings;
            this.progressCallback = progressCallback;
        }

        public void Send()
        {
            if (!this.appSettings.PingEnabled)
            {
                return;
            }

            this.progressCallback.SetText(string.Format("Ping '{0}'", this.appSettings.Host));
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
