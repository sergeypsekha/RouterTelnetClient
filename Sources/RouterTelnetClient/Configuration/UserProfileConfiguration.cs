using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
            this.Lines = new List<LineConfiguration>();
            foreach (var lineConfigurationElement in configuration.Lines)
            {
                var lineConfiguration = new LineConfiguration
                                            {
                                                RegUserName = lineConfigurationElement.RegUserName,
                                                AuthUserName = lineConfigurationElement.AuthUserName,
                                                AuthPassword = lineConfigurationElement.AuthPassword
                                            };
                this.Lines.Add(lineConfiguration);
            }
            if (this.Lines.Count >= 2)
            {
                return;
            }

            var message =
                string.Format(
                    "At least two lines have to be specified in the VoiceService/VoiceProfile/Lines configuration section.\n\tBut '{0}' was specified",
                    this.Lines.Count);
            throw new ConfigurationErrorsException(message);
        }

        public bool DigitMapEnable { get; set; }

        public string DigitMap { get; set; }

        public string UserAgentDomain { get; set; }

        public string ProxyServer { get; set; }

        public string RegistrarServer { get; set; }

        public string OutboundProxy { get; set; }

        public int RegistrationPeriod { get; set; }

        public List<LineConfiguration> Lines { get; set; }
    }

    public class LineConfiguration
    {
        public string RegUserName { get; set; }

        public string AuthUserName { get; set; }

        public string AuthPassword { get; set; }
    }
}
