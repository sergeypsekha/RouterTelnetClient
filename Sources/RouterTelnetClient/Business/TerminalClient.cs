using System;

using Microsoft.VisualBasic.CompilerServices;

using RouterTelnetClient.Models;
using RouterTelnetClient.TelnetClient;

namespace RouterTelnetClient.Business
{
    public class TerminalClient : ITerminalClient
    {
        private readonly IAppSettings appSettings;

        private readonly Terminal terminal;

        public TerminalClient(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
            this.terminal = new Terminal(
                this.appSettings.Host,
                this.appSettings.Port,
                this.appSettings.TimeoutSeconds,
                this.appSettings.VirtualScreenHeight,
                this.appSettings.VirtualScreenWidth);
        }

        public bool Connect()
        {
            if (this.terminal.Connect())
            {
                return true;
            }

            var message = string.Format(
                "Terminal client Can't establish a connection with host: {0}:{1}",
                this.appSettings.Host,
                this.appSettings.Port);

            throw new InvalidOperationException(message);
        }

        public void Disconnect()
        {
            this.terminal.Dispose();
        }

        public void Login()
        {
            var output = this.terminal.WaitForString("Login");
            if (string.IsNullOrWhiteSpace(output))
            {
                throw new TerminalException("No login possible");
            }

            this.terminal.SendResponse(this.appSettings.UserName, true);
            output = this.terminal.WaitForString("Password");
            if (string.IsNullOrWhiteSpace(output))
            {
                throw new TerminalException("No password prompt found");
            }

            this.terminal.SendResponse(this.appSettings.Password, true); 
            output = this.terminal.WaitForString(">");
            if (string.IsNullOrWhiteSpace(output))
            {
                throw new TerminalException("No > prompt found");
            }
        }

        public void Send(VoiceProfileViewModel model)
        {
            this.SendVoiceProfileModel(model);
            this.SendVoiceProfileModelLines(model);
        }

        private void SendVoiceProfileModel(VoiceProfileViewModel model)
        {
            this.SendVoiceProfileModelHeader(model);
            this.SendVoiceProfileModelBody(model);
            this.SendVoiceProfileModelFooter(model);
        }

        private void SendVoiceProfileModelHeader(VoiceProfileViewModel model)
        {
            throw new NotImplementedException();
        }

        private void SendVoiceProfileModelFooter(VoiceProfileViewModel model)
        {
            throw new NotImplementedException();
        }

        private void SendVoiceProfileModelBody(VoiceProfileViewModel model)
        {
            throw new NotImplementedException();
        }

        private void SendVoiceProfileModelLines(VoiceProfileViewModel model)
        {
            foreach (var line in model.Lines)
            {
                this.SendVoiceProfileModelLine(line);
            }
        }

        private void SendVoiceProfileModelLine(LineViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
