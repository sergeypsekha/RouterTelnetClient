using RouterTelnetClient.Models;

namespace RouterTelnetClient.Business
{
    public interface ITelnet
    {
        /// <summary>
        /// Connect to the telnet server
        /// </summary>
        /// <returns>true if connection was successful</returns>
        bool Connect();

        void Disconnect();

        void Send(VoiceProfileViewModel model);

        void Login();
    }
}