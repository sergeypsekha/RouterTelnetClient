using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace RouterTelnetClient.Configuration
{
    public class VoiceServiceConfigurationSection : ConfigurationSection
    {
        private static class Key
        {
            internal const string VoiceProfile = "VoiceProfile";
        }

        [ConfigurationProperty(Key.VoiceProfile)]
        public VoiceProfileConfigurationElement VoiceProfile
        {
            get
            {
                return (VoiceProfileConfigurationElement)this[Key.VoiceProfile];
            }
            set
            {
                this[Key.VoiceProfile] = value;
            }
        }
    }
}
