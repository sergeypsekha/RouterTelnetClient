using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace RouterTelnetClient.Configuration
{
    public class LineConfigurationElement: ConfigurationElement
    {
        private static class Key
        {
            internal const string RegUserName = "RegUserName";

            internal const string AuthUserName = "AuthUserName";

            internal const string AuthPassword = "AuthPassword";
        }

        [ConfigurationProperty(Key.RegUserName, IsRequired = true)]
        public string RegUserName
        {
            get
            {
                return (string)this[Key.RegUserName];
            }
            set
            {
                this[Key.RegUserName] = value;
            }
        }

        [ConfigurationProperty(Key.AuthUserName, IsRequired = true)]
        public string AuthUserName
        {
            get
            {
                return (string)this[Key.AuthUserName];
            }
            set
            {
                this[Key.AuthUserName] = value;
            }
        }

        [ConfigurationProperty(Key.AuthPassword, IsRequired = true)]
        public string AuthPassword
        {
            get
            {
                return (string)this[Key.AuthPassword];
            }
            set
            {
                this[Key.AuthPassword] = value;
            }
        }
    }
}
