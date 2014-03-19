using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RouterTelnetClient.Forms
{
    /// <summary>
    /// Summary description for ProgressWindow.
    /// </summary>
    public class ProgressWindowForm : Form, IProgressCallback
    {
        #region private fields

        private Button _cancelButton;
        private Label _label;
        private ProgressBar _progressBar;
        private readonly SynchronizationContext _context;

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private readonly System.ComponentModel.Container _components;

        public delegate void SetTextInvoker(String text);
        public delegate void IncrementInvoker(int val);
        public delegate void StepToInvoker(int val);
        public delegate void RangeInvoker(int minimum, int maximum);

        private String _titleRoot = String.Empty;
        private String _titleRootPrefix = "Calix";

        private readonly ManualResetEvent _initEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent _abortEvent = new ManualResetEvent(false);
        private bool _requiresClose = true;

        private const int SC_CLOSE = 0xF060;
        private const int MF_GRAYED = 0x1;

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        private static extern int EnableMenuItem(IntPtr hMenu, int wIDEnableItem, int wEnable);

        #endregion private fields

        #region init\final

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressWindowForm"/> class.
        /// </summary>
        public ProgressWindowForm()
        {
            this._context = SynchronizationContext.Current ?? new SynchronizationContext();
            this.InitializeComponent();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressWindowForm"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public ProgressWindowForm(String text)
        {
            this.UpdateTitleRoot(text);
            this._context = SynchronizationContext.Current ?? new SynchronizationContext();
            this.InitializeComponent();
        }

        #endregion init\final

        #region public methods

        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback.
        /// </summary>
        /// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
        /// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
        public void Begin(int minimum, int maximum)
        {
            this._initEvent.WaitOne();
            this._context.Send(delegate
                              {
                                  this.DoBegin(minimum, maximum);
                              },
                          null);
        }

        /// <summary>
        /// Call this method from the worker thread to initialize
        /// the progress callback, without setting the range
        /// </summary>
        public void Begin()
        {
            this._initEvent.WaitOne();
            this._context.Send(delegate
                              {
                                  this.DoBegin();
                              },
                          null);
        }

        /// <summary>
        /// Call this method from the worker thread to reset the range in the progress callback
        /// </summary>
        /// <param name="minimum">The minimum value in the progress range (e.g. 0)</param>
        /// <param name="maximum">The maximum value in the progress range (e.g. 100)</param>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public void SetRange(int minimum, int maximum)
        {
            this._initEvent.WaitOne();

            this._context.Send(delegate
                              {
                                  this.DoSetRange(minimum, maximum);
                              },
                          null);
        }

        /// <summary>
        /// Sets the title.
        /// </summary>
        /// <param name="text">The text.</param>
        public void SetTitle(String text)
        {
            this._context.Send(delegate
            {
                this.DoSetTitle(text);
            },
                          null);
        }

        /// <summary>
        /// Call this method from the worker thread to update the progress text.
        /// </summary>
        /// <param name="text">The progress text to display</param>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public void SetText(String text)
        {
            this._context.Send(delegate
                              {
                                  this.DoSetText(text);
                              },
                          null);
        }

        /// <summary>
        /// Call this method from the worker thread to step the progress meter to a particular value.
        /// </summary>
        /// <param name="val">The value to which to step the meter</param>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public void Increment(int val)
        {
            this._context.Send(delegate
                              {
                                  this.DoIncrement(val);
                              },
                          null);
        }

        /// <summary>
        /// Call this method from the worker thread to increase the progress counter by a specified value.
        /// </summary>
        /// <param name="val">The amount by which to increment the progress indicator</param>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public void StepTo(int val)
        {
            this._context.Send(delegate
                              {
                                  this.DoStepTo(val);
                              },
                          null);
        }

        /// <summary>
        /// If this property is true, then you should abort work
        /// </summary>
        /// <value></value>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public bool IsAborting
        {
            get
            {
                return this._abortEvent.WaitOne(0, false);
            }
        }

        /// <summary>
        /// Call this method from the worker thread to finalize the progress meter
        /// </summary>
        /// <remarks>You must have called one of the Begin() methods prior to this call.</remarks>
        public void End()
        {
            if (!this._requiresClose)
                return;
            if (this.IsHandleCreated)
            {
                this.Invoke(new MethodInvoker(this.DoEnd));
            }
            else
            {
                this.DoEnd();
            }
        }

        #endregion public methods

        #region private methods

        private void UpdateTitleRoot(String text)
        {
            this._titleRoot = String.IsNullOrEmpty(text) ? this._titleRootPrefix : this._titleRootPrefix + " - " + text;
        }

        private static Int32 GetWParam(IntPtr wParam)
        {
            String deviceLetter = String.Empty;
            Int32 param = Marshal.ReadInt32(wParam, 4);
            return param;
        }

        private const Int32 WM_SYSCOMMAND = 0x112;
        private const Int32 SC_MINIMIZE = 0xf020;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_SYSCOMMAND)
            {
                switch (m.WParam.ToInt32())
                {
                    case (SC_MINIMIZE):
                        {
                            this.Hide();
                            this.Owner.WindowState = FormWindowState.Minimized;
                            this.Owner.Hide();
                        }
                        break;
                }
            }
        }

        private void DoSetTitle(string text)
        {
            this.UpdateTitleRoot(text);
        }

        private void DoSetText(String text)
        {

            this._label.Text = text;
        }

        private void DoIncrement(int val)
        {
            this._progressBar.Increment(val);
            this.UpdateStatusText();
        }

        private void DoStepTo(int val)
        {
            this._progressBar.Value = val;
            this.UpdateStatusText();
        }

        private void DoBegin(int minimum, int maximum)
        {
            this.DoBegin();
            this.DoSetRange(minimum, maximum);
        }

        private void DoBegin()
        {
            this._cancelButton.Enabled = true;
            // ControlBox = true;
        }

        private void DoSetRange(int minimum, int maximum)
        {
            this._progressBar.Minimum = minimum;
            this._progressBar.Maximum = maximum;
            this._progressBar.Value = minimum;
            this.Text = this._titleRoot;
        }

        private void DoEnd()
        {
            this.Close();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //ControlBox = false;
            //disable close button
            EnableMenuItem(GetSystemMenu(this.Handle, false), SC_CLOSE, MF_GRAYED);
            this.Owner.SizeChanged += this.Owner_SizeChanged;
            this._initEvent.Set();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (this.Owner.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this._components != null)
                {
                    this._components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.None)
            {
                e.Cancel = true;
                return;
            }

            this._requiresClose = false;
            this.AbortWork();
            this.Owner.SizeChanged -= this.Owner_SizeChanged;
            base.OnFormClosing(e);
        }

        private void UpdateStatusText()
        {
            this.Text = this._titleRoot + String.Format(" - {0}% complete", (this._progressBar.Value * 100) / (this._progressBar.Maximum - this._progressBar.Minimum));
        }

        private void AbortWork()
        {
            this._cancelButton.Enabled = false;
            this._abortEvent.Set();
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this._progressBar = new System.Windows.Forms.ProgressBar();
            this._label = new System.Windows.Forms.Label();
            this._cancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // _progressBar
            // 
            this._progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._progressBar.Location = new System.Drawing.Point(11, 53);
            this._progressBar.Name = "_progressBar";
            this._progressBar.Size = new System.Drawing.Size(617, 23);
            this._progressBar.TabIndex = 1;
            // 
            // _label
            // 
            this._label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this._label.Location = new System.Drawing.Point(12, 9);
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(616, 41);
            this._label.TabIndex = 0;
            this._label.Text = "Starting operation...";
            // 
            // _cancelButton
            // 
            this._cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Enabled = false;
            this._cancelButton.Location = new System.Drawing.Point(553, 82);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 2;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.Click += new System.EventHandler(this.CancelButtonClick);
            // 
            // ProgressWindowForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(640, 108);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._progressBar);
            this.Controls.Add(this._label);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(434, 119);
            this.Name = "ProgressWindowForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Submitting...";
            this.ResumeLayout(false);

        }

        void Owner_SizeChanged(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
        }

        #endregion

        private void CancelButtonClick(object sender, EventArgs e)
        {
            this.AbortWork();
        }

        #endregion private methods
    }
}