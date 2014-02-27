namespace RouterTelnetClient.Business
{
    public interface IAppSettings
    {
        string Host { get; set; }

        int Port { get; set; }

        string UserName { get; set; }

        string Password { get; set; }
    }
}
