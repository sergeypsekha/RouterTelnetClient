using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace RouterTelnetClient.Configuration
{
    public class UserServiceConfiguration
    {
        private VoiceServiceConfigurationSection configuration;

        public UserProfileConfiguration UserProfileConfiguration { get; private set; }

        public UserServiceConfiguration()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            this.InitializeConfigurationSection();
            this.InitializeVoiceProfileConfiguration();
        }

        private void InitializeVoiceProfileConfiguration()
        {
            this.UserProfileConfiguration = new UserProfileConfiguration(this.configuration.VoiceProfile);
        }

        private void InitializeConfigurationSection()
        {
            this.configuration = ConfigurationManager.GetSection("VoiceService") as VoiceServiceConfigurationSection;
            if (this.configuration == null)
            {
                throw new ConfigurationErrorsException("VoiceService section hasn't found.");
            }
        }
    }
}