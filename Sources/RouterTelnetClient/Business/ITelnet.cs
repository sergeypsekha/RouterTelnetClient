using RouterTelnetClient.Models;

namespace RouterTelnetClient.Business
{
    public interface ITelnet
    {
        void Send(VoiceProfileViewModel model);
    }
}