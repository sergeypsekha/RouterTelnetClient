using System.Collections.Generic;

using RouterTelnetClient.Business;
using RouterTelnetClient.Models;

namespace RouterTelnetClient.Services
{
    public interface IValidationService
    {
        IEnumerable<ValidationResult> Validate(VoiceProfileViewModel viewModel);
    }
}