using RouterTelnetClient.Models;

namespace RouterTelnetClient.Business
{
    public interface ITelnetService
    {
        void Connect();

        void Submit(VoiceProfileModel model);
    }
}