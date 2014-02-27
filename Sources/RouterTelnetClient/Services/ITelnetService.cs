using RouterTelnetClient.Models;

namespace RouterTelnetClient.Services
{
    public interface ITelnetService
    {
        void Submit(VoiceProfileViewModel viewModel);
    }
}