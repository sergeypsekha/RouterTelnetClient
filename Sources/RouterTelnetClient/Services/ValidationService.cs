using System.Collections.Generic;

using RouterTelnetClient.Business;
using RouterTelnetClient.Models;

namespace RouterTelnetClient.Services
{
    public class ValidationService : IValidationService
    {
        public IEnumerable<ValidationResult> Validate(VoiceProfileViewModel viewModel)
        {
            var result = new List<ValidationResult>();

            result.AddRange(this.ValidateDigitMap(viewModel));
            result.AddRange(this.ValidateUserAgentDomain(viewModel));
            result.AddRange(this.ValidateProxyServer(viewModel));
            result.AddRange(this.ValidateRegistrarServer(viewModel));
            result.AddRange(this.ValidateOutboundProxy(viewModel));
            result.AddRange(this.ValidateRegistrationPeriod(viewModel));

            return result;
        }

        private IEnumerable<ValidationResult> ValidateDigitMap(VoiceProfileViewModel viewModel)
        {
            if (viewModel.DigitMapEnable && string.IsNullOrWhiteSpace(viewModel.DigitMap))
            {
                yield return new ValidationResult("DigitMap", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateUserAgentDomain(VoiceProfileViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.UserAgentDomain))
            {
                yield return new ValidationResult("UserAgentDomain", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateProxyServer(VoiceProfileViewModel viewModel)
        {   

            if (string.IsNullOrEmpty(viewModel.ProxyServer))
            {
                yield return new ValidationResult("ProxyServer", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateRegistrarServer(VoiceProfileViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.RegistrarServer))
            {
                yield return new ValidationResult("RegistrarServer", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateOutboundProxy(VoiceProfileViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.OutboundProxy))
            {
                yield return new ValidationResult("OutboundProxy", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateRegistrationPeriod(VoiceProfileViewModel viewModel)
        {
            if (string.IsNullOrEmpty(viewModel.RegistrationPeriod))
            {
                yield return new ValidationResult("RegistrationPeriod", "Should be specified");
            }

            uint value;
            if (!uint.TryParse(viewModel.RegistrationPeriod, out value))
            {
                yield return new ValidationResult("RegistrationPeriod", "Should be specified as unsigned int");
            }
        }
    }
}
