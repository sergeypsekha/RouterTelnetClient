using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RouterTelnetClient.Services
{
    class FormAppSettings : IAppSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public int TimeoutSeconds { get; set; }

        public int VirtualScreenHeight { get; set; }

        public int VirtualScreenWidth { get; set; }

        public bool PingEnabled { get; set; }
    }
}
