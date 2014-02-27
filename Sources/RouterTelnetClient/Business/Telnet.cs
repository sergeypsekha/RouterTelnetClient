using System;

using RouterTelnetClient.Models;
using RouterTelnetClient.Services;
using RouterTelnetClient.TelnetClient;

namespace RouterTelnetClient.Business
{
    public class Telnet : ITelnet
    {
        private readonly IAppSettings appSettings;

        private Terminal terminal;

        public Telnet(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public void Disconnect()
        {
            this.terminal.Dispose();
        }

        public void Send(VoiceProfileViewModel model)
        {
            using (this.terminal = new Terminal(
                    this.appSettings.Host,
                    this.appSettings.Port,
                    this.appSettings.TimeoutSeconds,
                    this.appSettings.VirtualScreenHeight,
                    this.appSettings.VirtualScreenWidth))
            {
                this.Connect();
                this.Login();

                this.SendVoiceProfileModel(model);
                this.SendVoiceProfileModelLines(model);
            }
        }

        private void Login()
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

        private bool Connect()
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

        private void SendVoiceProfileModel(VoiceProfileViewModel model)
        {
            this.SendVoiceProfileModelHeader(model);
            this.SendVoiceProfileModelBody(model);
            this.SendVoiceProfileModelFooter(model);
        }

        private void SendVoiceProfileModelHeader(VoiceProfileViewModel model)
        {
            this.WriteMessage("enable");
            this.WriteMessage("/system/tr069");
            this.WriteMessage("add InternetGatewayDevice.Services.VoiceService");
            this.WriteMessage("add InternetGatewayDevice.Services.VoiceService.1.VoiceProfile");
        }

        private void SendVoiceProfileModelFooter(VoiceProfileViewModel model)
        {
            this.WriteMessage("set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.Enable Enabled");
        }

        private void SendVoiceProfileModelBody(VoiceProfileViewModel model)
        {
            this.WriteDigitMapEnabled(model);
            this.WriteUserAgentDomain(model);
            this.WriteProxyServer(model);
            this.WriteRegistrarServer(model);
            this.WriteOutboundProxy(model);
            this.WriteRegistrationPeriod(model);
        }
        
        private void WriteDigitMapEnabled(VoiceProfileViewModel model)
        {
            if (!model.DigitMapEnable)
            {
                return;
            }

            this.WriteMessage("set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.DigitMapEnable Enabled");

            var message = string.Join(
                " ",
                "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.DigitMap",
                model.DigitMap);
            this.WriteMessage(message);
        }

        private void WriteUserAgentDomain(VoiceProfileViewModel model)
        {
            var message = string.Join(
                " ",
                "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.SIP.UserAgentDomain",
                model.UserAgentDomain);

            this.WriteMessage(message);
        }

        private void WriteProxyServer(VoiceProfileViewModel model)
        {
            var message = string.Join(
                " ",
                "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.SIP.ProxyServer",
                model.ProxyServer);

            this.WriteMessage(message);
        }

        private void WriteRegistrarServer(VoiceProfileViewModel model)
        {
            var message = string.Join(
                " ",
                "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.SIP.RegistrarServer",
                model.RegistrarServer);

            this.WriteMessage(message);
        }

        private void WriteOutboundProxy(VoiceProfileViewModel model)
        {
            var message = string.Join(
                " ",
                "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.SIP.OutboundProxy",
                model.OutboundProxy);

            this.WriteMessage(message);
        }

        private void WriteRegistrationPeriod(VoiceProfileViewModel model)
        {
            var message = string.Join(
                " ",
                "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.SIP.RegistrationPeriod",
                model.RegistrationPeriod);

            this.WriteMessage(message);
        }

        private void SendVoiceProfileModelLines(VoiceProfileViewModel model)
        {
            for (int i = 0; i < model.Lines.Count; i++)
            {
                var index = i + 1;
                var line = model.Lines[i];
                this.SendVoiceProfileModelLine(line, index);
            }
        }

        private void SendVoiceProfileModelLine(LineViewModel model, int index)
        {
            this.WriteAddLine(model);

            this.WriteRegUserName(model, index);
            this.WriteAuthUserName(model, index);
            this.WriteAuthPassword(model, index);

            this.WriteEnableLine(model);
        }

        private void WriteAddLine(LineViewModel model)
        {
            var message = "add InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.Line";
            this.WriteMessage(message);
        }

        private void WriteRegUserName(LineViewModel model, int index)
        {
            var message =
                string.Format(
                    "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.Line.{0}.SIP.RegUserName {1}",
                    index,
                    model.RegUserName);

            this.WriteMessage(message);
        }

        private void WriteAuthUserName(LineViewModel model, int index)
        {
            var message =
                string.Format(
                    "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.Line.{0}.SIP.AuthUserName {1}",
                    index,
                    model.AuthUserName);
            this.WriteMessage(message);
        }

        private void WriteAuthPassword(LineViewModel model, int index)
        {
            var message =
                string.Format(
                    "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.Line.{0}.SIP.AuthPassword {1}",
                    index,
                    model.AuthPassword);
            this.WriteMessage(message);
        }

        private void WriteEnableLine(LineViewModel model)
        {
            var message = string.Join(
                " ",
                "set InternetGatewayDevice.Services.VoiceService.1.VoiceProfile.1.Line.1.Enable",
                "Enabled");
            this.WriteMessage(message);
        }

        private void WriteMessage(string message)
        {
            this.terminal.SendResponse(message, endLine: true);
            if (!this.terminal.WaitForChangedScreen())
            {
                throw new InvalidOperationException("Can't send message: " + message);
            }
        }
    }
}
