using RouterTelnetClient.Models;

namespace RouterTelnetClient.Business
{
    public interface IValidationService
    {
        void Validate(VoiceProfileModel model);
    }
}