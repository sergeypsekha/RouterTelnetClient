using System.Configuration;

namespace RouterTelnetClient.Business
{
    internal class AppSettings : IAppSettings
    {
        private const string AppSettingHost = "Host";

        private const string AppSettingPort = "Port";

        private const string AppSettingUserName = "UserName";

        private const string AppSettingPassword = "Password";

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public AppSettings()
        {
            this.InitializeHost();
            this.InitializePort();
            this.InitializeUserName();
            this.InitializePassword();
        }

        private void InitializeHost()
        {
            this.Host = this.GetSettingFromConfiguration(AppSettingHost);
        }

        private void InitializePort()
        {
            var value = this.GetSettingFromConfiguration(AppSettingPort);
            int port;
            if (!int.TryParse(value, out port))
            {
                throw new ConfigurationErrorsException(
                    string.Format(
                        "Port should be specified as unsigned integer value. But '{0}' is specified.",
                        value));
            }
            this.Port = port;
        }

        private void InitializeUserName()
        {
            this.UserName = this.GetSettingFromConfiguration(AppSettingUserName);
        }

        private void InitializePassword()
        {
            this.Password = this.GetSettingFromConfiguration(AppSettingPassword);
        }

        private string GetSettingFromConfiguration(string appSettingsKey)
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