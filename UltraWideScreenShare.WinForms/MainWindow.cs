using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace UltraWideScreenShare.WinForms
{
    public partial class MainWindow : Form
    {
        private const int TOGGLE_COLLAPSED_WIDTH = 16;


        private readonly Timer _dispatcherTimer = new Timer() { Interval = 2 }; //30fps
        private Point _tittleBarLocation = new Point();
        private Magnifier _magnifier;
        private bool _isTransparent = false;
        private Color _frameColor = Color.FromArgb(255, 53, 89, 224); //#3559E0
        const int _borderWidth = 4;
        private bool _showMagnifierScheduled = true;
        private bool _isFocused = true;
        private bool _isInitialized = false;

        private bool _titleBarCollapsed = false;
        private int _titleBarExpandedWidth;
        private int _toggleButtonExpandedWidth;
        public MainWindow()
        {
            InitializeComponent();
            TitleBar.BringToFront();
            InitializePaddingsForBorders();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.InitializeMainWindowStyle();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            _isFocused = true;
            TitleBar.Visible = true;
            Padding = new Padding(_borderWidth, _borderWidth, _borderWidth, _borderWidth);
            Invalidate();
        }

        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            _isFocused = false;
            TitleBar.Visible = false;
            Padding = new Padding(0);
            Invalidate();
        }

        private void InitializePaddingsForBorders()
        {
            Padding = new Padding(_borderWidth, _borderWidth, _borderWidth, _borderWidth);
            TitleBar.Width += (_borderWidth * 2);
            TitleBar.Height += (_borderWidth * 2);
            TitleBar.Padding = new Padding(_borderWidth, _borderWidth, _borderWidth, _borderWidth);
        }

        protected override void OnMove(EventArgs e)
        {
            MaximizedBounds = new Rectangle(Point.Empty, Screen.GetWorkingArea(Location).Size);
            base.OnMove(e);
            if (_isInitialized && WindowState == FormWindowState.Normal)
            {
                Settings.SaveWindowPosition(this);
            }
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            Settings.RestoreWindowPosition(this);
            _magnifier = new Magnifier(magnifierPanel.Handle);
            _isInitialized = true;
            _dispatcherTimer.Start();
            _dispatcherTimer.Tick += (s, a) =>
            {
                _magnifier.UpdateMagnifierWindow();
                if (magnifierPanel.Bounds.Contains(PointToClient(Cursor.Position)) && !TitleBar.Bounds.Contains(PointToClient(Cursor.Position)))
                {
                    if (!_isTransparent)
                    { this.SetTransparency(_isTransparent = true); Trace.WriteLine("enter"); }
                }
                else
                {
                    if (_isTransparent)
                    { this.SetTransparency(_isTransparent = false); Trace.WriteLine("leave"); }
                }
                if (_showMagnifierScheduled)
                {
                    _magnifier.ShowMagnifier();
                    _showMagnifierScheduled = false;
                }
            };
        }

        private void MainWindow_ResizeBegin(object sender, EventArgs e) => _magnifier.HideMagnifier();

        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            _showMagnifierScheduled = true;
            if (_isInitialized && WindowState == FormWindowState.Normal)
            {
                Settings.SaveWindowPosition(this);
            }
        }


        private void TittleButton_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Drag();
                SetupMaximizeButton();
            }
        }


        const int WM_NCCALCSIZE = 0x0083;
        const int WM_NCACTIVATE = 0x0086;
        const int WM_NCHITTEST = 0x0084;

        protected override void WndProc(ref Message m)
        {
            var message = m.Msg;
            if (message == WM_NCCALCSIZE)
            {
                return;
            }

            if (message == WM_NCACTIVATE)
            {
                m.Result = new IntPtr(-1);
                return;
            }

            base.WndProc(ref m);

            if (message == WM_NCHITTEST && _isFocused)
            {
                this.TryResize(ref m, _borderWidth);
            }
        }

        private void MainWindow_Paint(object sender, PaintEventArgs e)
        {
            if (_isFocused)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                    _frameColor, _borderWidth, ButtonBorderStyle.Solid,
                    _frameColor, _borderWidth, ButtonBorderStyle.Solid,
                    _frameColor, _borderWidth, ButtonBorderStyle.Solid,
                    _frameColor, _borderWidth, ButtonBorderStyle.Solid);
            }
        }

        private void TitleBar_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, TitleBar.ClientRectangle,
               _frameColor, _borderWidth, ButtonBorderStyle.Solid,
               _frameColor, _borderWidth, ButtonBorderStyle.Solid,
               _frameColor, _borderWidth, ButtonBorderStyle.Solid,
               _frameColor, _borderWidth, ButtonBorderStyle.Solid);
        }

        private void closeButton_Click(object sender, EventArgs e) => Close();

        protected override void OnClosing(CancelEventArgs e)
        {
            Settings.SaveWindowPosition(this);
            _dispatcherTimer.Stop();
            _dispatcherTimer.Dispose();
            base.OnClosing(e);
        }
        private void minimizeButton_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void maximizeButton_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal : FormWindowState.Maximized;
            SetupMaximizeButton();
        }

        private void SetupMaximizeButton()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                maximizeButton.Image = Properties.Resources.restore;
            }
            else
            {
                maximizeButton.Image = Properties.Resources.maximize;
            }
        }



        private void DragButton_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TitleBar.Left = Math.Clamp(value: e.X + TitleBar.Left - _tittleBarLocation.X,
                    min: _borderWidth / 2, max: Width - TitleBar.Width - _borderWidth / 2);

            }
        }


        private void toggleButton_Click(object sender, EventArgs e)
        {
            ToggleTitleBarCollapsed();
        }

        private void ToggleTitleBarCollapsed()
        {
            dragButton.Visible = _titleBarCollapsed;
            titleButton.Visible = _titleBarCollapsed;
            minimizeButton.Visible = _titleBarCollapsed;
            maximizeButton.Visible = _titleBarCollapsed;
            closeButton.Visible = _titleBarCollapsed;

            if (_titleBarCollapsed)
            {
                toggleButton.Image = Properties.Resources.toggle_up;

                TitleBar.Width = _titleBarExpandedWidth;
                TitleBar.Height = dragButton.Height + _borderWidth * 2;
                TitleBar.Left = Math.Clamp(TitleBar.Left - (TitleBar.Width - toggleButton.Width)/2, 0, Width - TitleBar.Width);

                toggleButton.Width = _toggleButtonExpandedWidth;
                toggleButton.Height = titleButton.Height;

                _titleBarCollapsed = false;
            }
            else
                {
                    _toggleButtonExpandedWidth = toggleButton.Width;
                    _titleBarExpandedWidth = TitleBar.Width;

                    bool isRightAligned = TitleBar.Left >= Width - TitleBar.Width - _borderWidth;
                    bool isLeftAligned = TitleBar.Left <= _borderWidth;

                    toggleButton.Image = Properties.Resources.toggle_dn;

                    toggleButton.Width = 16;
                    toggleButton.Height = 10;

                    TitleBar.Height = toggleButton.Height + _borderWidth * 2;
                    TitleBar.Width = toggleButton.Width + _borderWidth * 2;

                    if (isRightAligned)
                        TitleBar.Left = Width - TitleBar.Width - _borderWidth / 2;
                    else if (!isLeftAligned)
                        TitleBar.Left += (_titleBarExpandedWidth - toggleButton.Width) / 2;

                    _titleBarCollapsed = true;
                }
        }

    }
}
