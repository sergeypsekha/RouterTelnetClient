namespace RouterTelnetClient.Business
{
    internal class AppSettings : IAppSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}