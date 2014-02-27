namespace RouterTelnetClient.Services
{
    public interface IAppSettings
    {
        string Host { get; set; }

        int Port { get; set; }

        string UserName { get; set; }

        string Password { get; set; }

        int TimeoutSeconds { get; set; }

        int VirtualScreenHeight { get; set; }

        int VirtualScreenWidth { get; set; }

        bool PingEnabled { get; set; }
    }
}
