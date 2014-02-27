using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RouterTelnetClient.Business;
using RouterTelnetClient.Models;

namespace RouterTelnetClient
{
    public partial class MainForm : Form
    {
        private ITelnetService telnetService = null;

        public MainForm()
        {
            this.InitializeComponent();
            this.Initialize();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.telnetService.Connect();
        }

        private void Initialize()
        {
            this.telnetService = new TelnetService();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.txtDigitMap.ReadOnly = !this.cbDigitMapEnable.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var voiceProfileModel = this.GetViewProfileModel();
            this.telnetService.Submit(voiceProfileModel);
        }

        private VoiceProfileModel GetViewProfileModel()
        {
            var line1Model = this.GetLine1Model();

            var line2Model = this.GetLine2Model();

            var voiceProfileModel = new VoiceProfileModel
                                        {
                                            DigitMapEnable = this.cbDigitMapEnable.Checked,
                                            DigitMap = this.txtDigitMap.Text,
                                            UserAgentDomain = this.txtUserAgentDomain.Text,
                                            ProxyServer = this.txtProxyServer.Text,
                                            RegistrarServer = this.txtRegistrarServer.Text,
                                            OutboundProxy = this.txtOutboundProxy.Text,
                                            RegistrationPeriod = this.txtRegistrationPeriod.Text,
                                            Lines =
                                                new List<LineModel>(new[] { line1Model, line2Model })
                                        };
            return voiceProfileModel;
        }

        private LineModel GetLine2Model()
        {
            var line2Model = new LineModel
                                 {
                                     RegUserName = this.txtLine2RegUserName.Text,
                                     AuthUserName = this.txtLine2AuthUserName.Text,
                                     AuthPassword = this.txtLine2AuthPassword.Text
                                 };
            return line2Model;
        }

        private LineModel GetLine1Model()
        {
            var line1Model = new LineModel
                                 {
                                     RegUserName = this.txtLine1RegUserName.Text,
                                     AuthUserName = this.txtLine1AuthUserName.Text,
                                     AuthPassword = this.txtLine1AuthPassword.Text
                                 };
            return line1Model;
        }
    }
}