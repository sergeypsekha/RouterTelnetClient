using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RouterTelnetClient.Models;

namespace RouterTelnetClient.Business
{
    public class ValidationService : IValidationService
    {
        public IEnumerable<ValidationResult> Validate(VoiceProfileModel model)
        {
            var result = new List<ValidationResult>();

            result.AddRange(this.ValidateDigitMap(model));
            result.AddRange(this.ValidateUserAgentDomain(model));
            result.AddRange(this.ValidateProxyServer(model));
            result.AddRange(this.ValidateRegistrarServer(model));
            result.AddRange(this.ValidateOutboundProxy(model));
            result.AddRange(this.ValidateRegistrationPeriod(model));

            return result;
        }

        private IEnumerable<ValidationResult> ValidateDigitMap(VoiceProfileModel model)
        {
            if (model.DigitMapEnable && string.IsNullOrWhiteSpace(model.DigitMap))
            {
                yield return new ValidationResult("DigitMap", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateUserAgentDomain(VoiceProfileModel model)
        {
            if (string.IsNullOrEmpty(model.UserAgentDomain))
            {
                yield return new ValidationResult("UserAgentDomain", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateProxyServer(VoiceProfileModel model)
        {   

            if (string.IsNullOrEmpty(model.ProxyServer))
            {
                yield return new ValidationResult("ProxyServer", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateRegistrarServer(VoiceProfileModel model)
        {
            if (string.IsNullOrEmpty(model.RegistrarServer))
            {
                yield return new ValidationResult("RegistrarServer", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateOutboundProxy(VoiceProfileModel model)
        {
            if (string.IsNullOrEmpty(model.OutboundProxy))
            {
                yield return new ValidationResult("OutboundProxy", "Should be specified");
            }
        }

        private IEnumerable<ValidationResult> ValidateRegistrationPeriod(VoiceProfileModel model)
        {
            if (string.IsNullOrEmpty(model.RegistrationPeriod))
            {
                yield return new ValidationResult("RegistrationPeriod", "Should be specified");
            }

            uint value;
            if (!uint.TryParse(model.RegistrationPeriod, out value))
            {
                yield return new ValidationResult("RegistrationPeriod", "Should be specified as unsigned int");
            }
        }
    }
}
