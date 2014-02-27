using System.Collections.Generic;

using RouterTelnetClient.Models;

namespace RouterTelnetClient.Business
{
    public interface IValidationService
    {
        IEnumerable<ValidationResult> Validate(VoiceProfileModel model);
    }
}