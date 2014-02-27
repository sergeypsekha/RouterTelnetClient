using System.Configuration;

namespace RouterTelnetClient.Business
{
    internal class AppSettings : IAppSettings
    {
        private const string AppSettingHost = "Host";

        private const string AppSettingPort = "Port";

        private const string AppSettingUserName = "UserName";

        private const string AppSettingPassword = "Password";

        private const string AppSettingTimeoutSeconds = "TimeoutSeconds";

        private const string AppSettingsVirtualScreenHeight = "VirtualScreenHeight";

        private const string AppSettingsVirtualScreenWidth = "VirtualScreenWidth";

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int TimeoutSeconds { get; set; }

        public int VirtualScreenHeight { get; set; }

        public int VirtualScreenWidth { get; set; }

        public AppSettings()
        {
            this.InitializeHost();
            this.InitializePort();
            this.InitializeUserName();
            this.InitializePassword();
            this.InitializeTimeoutSeconds();
            this.InitializeVirtualScreenHeight();
            this.InitializeVirtualScreenWidth();
        }

        private void InitializeHost()
        {
            this.Host = this.GetStringValueFromConfiguration(AppSettingHost);
        }

        private void InitializePort()
        {
            this.Port = this.GetIntValueFromConfiguration(AppSettingPort);
        }

        private void InitializeUserName()
        {
            this.UserName = this.GetStringValueFromConfiguration(AppSettingUserName);
        }

        private void InitializePassword()
        {
            this.Password = this.GetStringValueFromConfiguration(AppSettingPassword);
        }

        private void InitializeTimeoutSeconds()
        {
            this.TimeoutSeconds = this.GetIntValueFromConfiguration(AppSettingTimeoutSeconds);
        }

        private void InitializeVirtualScreenWidth()
        {
            this.VirtualScreenHeight = this.GetIntValueFromConfiguration(AppSettingsVirtualScreenHeight);
        }

        private void InitializeVirtualScreenHeight()
        {
            this.VirtualScreenWidth = this.GetIntValueFromConfiguration(AppSettingsVirtualScreenWidth);
        }

        private int GetIntValueFromConfiguration(string appSettingKey)
        {
            var value = this.GetStringValueFromConfiguration(appSettingKey);
            int timeoutSeconds;

            if (!int.TryParse(value, out timeoutSeconds))
            {
                throw new ConfigurationErrorsException(
                    string.Format(
                        "{0} should be specified as unsigned integer value. But '{1}' is provided",
                        appSettingKey,
                        value));
            }

            return timeoutSeconds;
        }

        private string GetStringValueFromConfiguration(string appSettingsKey)
        {
            var value = ConfigurationManager.AppSettings[appSettingsKey];
            if (string.IsNullOrEmpty(value))
            {
                throw new ConfigurationErrorsException(
                    string.Format("{0} should be set in configuration file.", appSettingsKey));
            }

            return value;
        }
    }
}