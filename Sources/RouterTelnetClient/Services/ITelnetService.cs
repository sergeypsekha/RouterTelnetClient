using RouterTelnetClient.Models;

namespace RouterTelnetClient.Services
{
    public interface ITelnetService
    {
        void Connect();

        void Submit(VoiceProfileViewModel viewModel);

        void Disconnect();
    }
}