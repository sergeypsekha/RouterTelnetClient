using System.Configuration;

namespace RouterTelnetClient.Business
{
    internal class AppSettings : IAppSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int TimeoutSeconds { get; set; }

        public int VirtualScreenHeight { get; set; }

        public int VirtualScreenWidth { get; set; }

        public bool PingEnabled { get; set; }

        public AppSettings()
        {
            this.InitializeHost();
            this.InitializePort();
            this.InitializeUserName();
            this.InitializePassword();
            this.InitializeTimeoutSeconds();
            this.InitializeVirtualScreenHeight();
            this.InitializeVirtualScreenWidth();
            this.InitializePingEnabled();
        }

        private void InitializeHost()
        {
            this.Host = this.GetStringValueFromConfiguration(AppSettingsKey.Host);
        }

        private void InitializePort()
        {
            this.Port = this.GetIntValueFromConfiguration(AppSettingsKey.Port);
        }

        private void InitializeUserName()
        {
            this.UserName = this.GetStringValueFromConfiguration(AppSettingsKey.UserName);
        }

        private void InitializePassword()
        {
            this.Password = this.GetStringValueFromConfiguration(AppSettingsKey.Password);
        }

        private void InitializeTimeoutSeconds()
        {
            this.TimeoutSeconds = this.GetIntValueFromConfiguration(AppSettingsKey.TimeoutSeconds);
        }

        private void InitializeVirtualScreenWidth()
        {
            this.VirtualScreenHeight = this.GetIntValueFromConfiguration(AppSettingsKey.VirtualScreenHeight);
        }

        private void InitializeVirtualScreenHeight()
        {
            this.VirtualScreenWidth = this.GetIntValueFromConfiguration(AppSettingsKey.VirtualScreenWidth);
        }

        private void InitializePingEnabled()
        {
            var value = this.GetStringValueFromConfiguration(AppSettingsKey.PingEnabled);
            bool pingEnabled;
            if (bool.TryParse(value, out pingEnabled))
            {
                this.PingEnabled = pingEnabled;
                return;
            }

            var message = string.Format(
                "'{0}' shold be specified as boolean value. But '{1}' is privided",
                AppSettingsKey.PingEnabled,
                value);
            throw new ConfigurationErrorsException(message);
        }

        private int GetIntValueFromConfiguration(string appSettingKey)
        {
            var value = this.GetStringValueFromConfiguration(appSettingKey);
            int timeoutSeconds;

            if (int.TryParse(value, out timeoutSeconds))
            {
                return timeoutSeconds;
            }

            var message = string.Format(
                "{0} should be specified as unsigned integer value. But '{1}' is provided",
                appSettingKey,
                value);
            throw new ConfigurationErrorsException(message);
        }

        private string GetStringValueFromConfiguration(string appSettingsKey)
        {
            var value = ConfigurationManager.AppSettings[appSettingsKey];
            if (!string.IsNullOrEmpty(value))
            {
                return value;
            }

            var message = string.Format("{0} should be set in configuration file.", appSettingsKey);
            throw new ConfigurationErrorsException(message);
        }
    }
}