using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouterTelnetClient.Configuration
{
    public class UserProfileConfiguration
    {
        public UserProfileConfiguration(VoiceProfileConfigurationElement configuration)
        {
            this.DigitMapEnable = configuration.DigitMapEnable;
            this.DigitMap = configuration.DigitMap;
            this.UserAgentDomain = configuration.UserAgentDomain;
            this.ProxyServer = configuration.ProxyServer;
            this.RegistrarServer = configuration.RegistrarServer;
            this.OutboundProxy = configuration.OutboundProxy;
            this.RegistrationPeriod = configuration.RegistrationPeriod;
        }

        public bool DigitMapEnable { get; set; }

        public string DigitMap { get; set; }

        public string UserAgentDomain { get; set; }

        public string ProxyServer { get; set; }

        public string RegistrarServer { get; set; }

        public string OutboundProxy { get; set; }

        public int RegistrationPeriod { get; set; }
    }
}
