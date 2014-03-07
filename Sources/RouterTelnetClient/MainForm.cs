using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using RouterTelnetClient.Business;
using RouterTelnetClient.Configuration;
using RouterTelnetClient.Forms;
using RouterTelnetClient.Models;
using RouterTelnetClient.Services;

namespace RouterTelnetClient
{
    public partial class MainForm : Form
    {
        private ITelnetService telnetService = null;

        private IValidationService validationService = null;

        private UserProfileConfiguration configuration = null;

        public MainForm()
        {
            this.InitializeComponent();
            this.Initialize();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitializeVoiceProfileView();
            this.InitializeLines();
        }

        private void InitializeLines()
        {
            this.InitializeLineOne();
            this.InitializeLineTwo();
        }

        private void InitializeLineOne()
        {
            var line = this.configuration.Lines[0];
            this.txtLine1RegUserName.Text = line.RegUserName;
            this.txtLine1AuthUserName.Text = line.AuthUserName;
            this.txtLine1AuthPassword.Text = line.AuthPassword;
        }

        private void InitializeLineTwo()
        {
            var line = this.configuration.Lines[1];
            this.txtLine2RegUserName.Text = line.RegUserName;
            this.txtLine2AuthUserName.Text = line.AuthUserName;
            this.txtLine2AuthPassword.Text = line.AuthPassword;
        }

        private void InitializeVoiceProfileView()
        {
            this.cbDigitMapEnable.Checked = this.configuration.DigitMapEnable;
            this.txtDigitMap.Text = this.configuration.DigitMap;
            this.txtUserAgentDomain.Text = this.configuration.UserAgentDomain;
            this.txtProxyServer.Text = this.configuration.ProxyServer;
            this.txtRegistrarServer.Text = this.configuration.RegistrarServer;
            this.txtOutboundProxy.Text = this.configuration.OutboundProxy;
            this.txtRegistrationPeriod.Text = this.configuration.RegistrationPeriod.ToString(CultureInfo.InvariantCulture);
        }

        private void Initialize()
        {
            this.validationService = new ValidationService();
            this.telnetService = new TelnetService();
            this.configuration = new UserServiceConfiguration().UserProfileConfiguration;
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

            var longOperationForm = new LongOperationForm();
            longOperationForm.Show(this);

            var x = longOperationForm.Owner.Location.X + (longOperationForm.Owner.Width - longOperationForm.Width) / 2;
            var y = longOperationForm.Owner.Location.Y + (longOperationForm.Owner.Height - longOperationForm.Height) / 2;
            longOperationForm.Location = new System.Drawing.Point(x, y);
            longOperationForm.SetDesktopLocation(x, y);
            try
            {
                this.telnetService.Submit(model);
            }
            finally
            {
                longOperationForm.Hide();
            }
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