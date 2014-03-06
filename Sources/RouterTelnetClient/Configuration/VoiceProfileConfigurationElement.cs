using System.Configuration;

namespace RouterTelnetClient.Configuration
{
    public class VoiceProfileConfigurationElement : ConfigurationElement
    {
        private static class Key
        {
            internal const string DigitMapEnable = "DigitMapEnable";

            internal const string DigitMap = "DigitMap";

            internal const string UserAgentDomain = "UserAgentDomain";

            internal const string ProxyServer = "ProxyServer";

            internal const string RegistrarServer = "RegistrarServer";

            internal const string OutboundProxy = "OutboundProxy";

            internal const string RegistrationPeriod = "RegistrationPeriod";
        }


        [ConfigurationProperty(Key.DigitMapEnable, DefaultValue = "True", IsRequired = true)]
        public bool DigitMapEnable
        {
            get
            {
                return (bool)this[Key.DigitMapEnable];
            }
            set
            {
                this[Key.DigitMapEnable] = value;
            }
        }

        [ConfigurationProperty(Key.DigitMap, DefaultValue = "1xx|[2-8]xxxxxx|0xxxxxxxx|07xxxxxxxxx|00x.s",
            IsRequired = true)]
        public string DigitMap
        {
            get
            {
                return (string)this[Key.DigitMap];
            }
            set
            {
                this[Key.DigitMap] = value;
            }
        }

        [ConfigurationProperty(Key.UserAgentDomain, DefaultValue = "itpc1.com", IsRequired = true)]
        public string UserAgentDomain
        {
            get
            {
                return (string)this[Key.UserAgentDomain];
            }
            set
            {
                this[Key.UserAgentDomain] = value;
            }
        }

        [ConfigurationProperty(Key.ProxyServer, DefaultValue = "172.16.5.3", IsRequired = true)]
        public string ProxyServer
        {
            get
            {
                return (string)this[Key.ProxyServer];
            }
            set
            {
                this[Key.ProxyServer] = value;
            }
        }

        [ConfigurationProperty(Key.RegistrarServer, DefaultValue = "itpc1.com", IsRequired = true)]
        public string RegistrarServer
        {
            get
            {
                return (string)this[Key.RegistrarServer];
            }
            set
            {
                this[Key.RegistrarServer] = value;
            }
        }

        [ConfigurationProperty(Key.OutboundProxy, DefaultValue = "172.16.5.3", IsRequired = true)]
        public string OutboundProxy
        {
            get
            {
                return (string)this[Key.OutboundProxy];
            }
            set
            {
                this[Key.OutboundProxy] = value;
            }
        }

        [ConfigurationProperty(Key.RegistrationPeriod, DefaultValue = "3600", IsRequired = true)]
        public int RegistrationPeriod
        {
            get
            {
                return (int)this[Key.RegistrationPeriod];
            }
            set
            {
                this[Key.RegistrationPeriod] = value;
            }
        }
    }
}