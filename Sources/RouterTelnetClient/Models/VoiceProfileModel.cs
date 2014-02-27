using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouterTelnetClient.Models
{
    public class VoiceProfileModel
    {
        public VoiceProfileModel()
        {
            this.Lines = new List<LineModel>();
        }

        public bool DigitMapEnable { get; set; }

        public string DigitMap { get; set; }

        public string UserAgentDomain { get; set; }

        public string ProxyServer { get; set; }

        public string RegistrarServer { get; set; }

        public string OutboundProxy { get; set; }

        public string RegistrationPeriod { get; set; }

        public List<LineModel> Lines { get; set; }
    }
}
