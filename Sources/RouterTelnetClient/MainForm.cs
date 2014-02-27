using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using RouterTelnetClient.Business;
using RouterTelnetClient.Forms;
using RouterTelnetClient.Models;

namespace RouterTelnetClient
{
    public partial class MainForm : Form
    {
        private ITelnetService telnetService = null;

        private IValidationService validationService = null;

        public MainForm()
        {
            this.InitializeComponent();
            this.Initialize();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            this.telnetService.Disconnect();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.telnetService.Connect();
        }

        private void Initialize()
        {
            this.validationService = new ValidationService();
            this.telnetService = new TelnetService();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDigitMap.ReadOnly = !this.cbDigitMapEnable.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var model = this.GetViewProfileModel();
            var validationResult = this.validationService.Validate(model);
            if (validationResult.Any())
            {
                var form = new ValidationForm(validationResult);
                form.ShowDialog();
                return;
            }

            this.telnetService.Submit(model);
        }

        private VoiceProfileViewModel GetViewProfileModel()
        {
            var line1Model = this.GetLine1Model();

            var line2Model = this.GetLine2Model();

            var voiceProfileModel = new VoiceProfileViewModel
                                        {
                                            DigitMapEnable = this.cbDigitMapEnable.Checked,
                                            DigitMap = this.txtDigitMap.Text,
                                            UserAgentDomain = this.txtUserAgentDomain.Text,
                                            ProxyServer = this.txtProxyServer.Text,
                                            RegistrarServer = this.txtRegistrarServer.Text,
                                            OutboundProxy = this.txtOutboundProxy.Text,
                                            RegistrationPeriod = this.txtRegistrationPeriod.Text,
                                            Lines =
                                                new List<LineViewModel>(new[] { line1Model, line2Model })
                                        };
            return voiceProfileModel;
        }

        private LineViewModel GetLine2Model()
        {
            var line2Model = new LineViewModel
                                 {
                                     RegUserName = this.txtLine2RegUserName.Text,
                                     AuthUserName = this.txtLine2AuthUserName.Text,
                                     AuthPassword = this.txtLine2AuthPassword.Text
                                 };
            return line2Model;
        }

        private LineViewModel GetLine1Model()
        {
            var line1Model = new LineViewModel
                                 {
                                     RegUserName = this.txtLine1RegUserName.Text,
                                     AuthUserName = this.txtLine1AuthUserName.Text,
                                     AuthPassword = this.txtLine1AuthPassword.Text
                                 };
            return line1Model;
        }
    }
}