
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace UltraWideScreenShare.WinForms
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            magnifierPanel = new Panel();
            TitleBar = new FlowLayoutPanel();
            dragButton = new Button();
            titleButton = new Button();
            toggleButton = new Button();
            minimizeButton = new Button();
            maximizeButton = new Button();
            closeButton = new Button();
            TitleBar.SuspendLayout();
            SuspendLayout();
            // 
            // magnifierPanel
            // 
            magnifierPanel.BackColor = Color.Transparent;
            magnifierPanel.Dock = DockStyle.Fill;
            magnifierPanel.Location = new Point(7, 8);
            magnifierPanel.Margin = new Padding(0);
            magnifierPanel.Name = "magnifierPanel";
            magnifierPanel.Size = new Size(1357, 884);
            magnifierPanel.TabIndex = 0;
            // 
            // TitleBar
            // 
            TitleBar.Anchor = AnchorStyles.Top;
            TitleBar.BackColor = Color.White;
            TitleBar.Controls.Add(dragButton);
            TitleBar.Controls.Add(titleButton);
            TitleBar.Controls.Add(toggleButton);
            TitleBar.Controls.Add(minimizeButton);
            TitleBar.Controls.Add(maximizeButton);
            TitleBar.Controls.Add(closeButton);
            TitleBar.Location = new Point(392, 6);
            TitleBar.Margin = new Padding(0);
            TitleBar.Name = "TitleBar";
            TitleBar.Size = new Size(633, 44);
            TitleBar.TabIndex = 1;
            TitleBar.WrapContents = false;
            TitleBar.Paint += TitleBar_Paint;
            // 
            // dragButton
            // 
            dragButton.Anchor = AnchorStyles.None;
            dragButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            dragButton.BackColor = Color.FromArgb(229, 229, 229);
            dragButton.BackgroundImageLayout = ImageLayout.None;
            dragButton.FlatAppearance.BorderColor = Color.FromArgb(229, 229, 229);
            dragButton.FlatAppearance.BorderSize = 0;
            dragButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(229, 229, 229);
            dragButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 229, 229);
            dragButton.FlatStyle = FlatStyle.Flat;
            dragButton.Image = Properties.Resources.drag;
            dragButton.Location = new Point(0, 0);
            dragButton.Margin = new Padding(0);
            dragButton.Name = "dragButton";
            dragButton.Size = new Size(30, 44);
            dragButton.TabIndex = 3;
            dragButton.UseVisualStyleBackColor = false;
            dragButton.MouseMove += DragButton_MouseMove;
            // 
            // titleButton
            // 
            titleButton.BackColor = Color.FromArgb(229, 229, 229);
            titleButton.BackgroundImageLayout = ImageLayout.None;
            titleButton.FlatAppearance.BorderColor = Color.FromArgb(229, 229, 229);
            titleButton.FlatAppearance.BorderSize = 0;
            titleButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(229, 229, 229);
            titleButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 229, 229);
            titleButton.FlatStyle = FlatStyle.Flat;
            titleButton.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            titleButton.ImageAlign = ContentAlignment.MiddleRight;
            titleButton.Location = new Point(30, 0);
            titleButton.Margin = new Padding(0);
            titleButton.Name = "titleButton";
            titleButton.Size = new Size(363, 44);
            titleButton.TabIndex = 4;
            titleButton.Text = "Ultra Wide Screen Share 2.0";
            titleButton.UseVisualStyleBackColor = false;
            titleButton.MouseDown += TittleButton_MouseDown;
            // 
            // toggleButton
            // 
            toggleButton.BackColor = Color.White;
            toggleButton.BackgroundImageLayout = ImageLayout.None;
            toggleButton.FlatAppearance.BorderColor = Color.White;
            toggleButton.FlatAppearance.BorderSize = 0;
            toggleButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(229, 229, 229);
            toggleButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 229, 229);
            toggleButton.FlatStyle = FlatStyle.Flat;
            toggleButton.Image = Properties.Resources.toggle_up;
            toggleButton.Location = new Point(393, 0);
            toggleButton.Margin = new Padding(0);
            toggleButton.Name = "toggleButton";
            toggleButton.Size = new Size(60, 44);
            toggleButton.TabIndex = 5;
            toggleButton.UseVisualStyleBackColor = false;
            toggleButton.Click += toggleButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.BackColor = Color.White;
            minimizeButton.BackgroundImageLayout = ImageLayout.None;
            minimizeButton.FlatAppearance.BorderColor = Color.White;
            minimizeButton.FlatAppearance.BorderSize = 0;
            minimizeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(229, 229, 229);
            minimizeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 229, 229);
            minimizeButton.FlatStyle = FlatStyle.Flat;
            minimizeButton.Image = Properties.Resources.minimize;
            minimizeButton.Location = new Point(453, 0);
            minimizeButton.Margin = new Padding(0);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.Size = new Size(60, 44);
            minimizeButton.TabIndex = 2;
            minimizeButton.UseVisualStyleBackColor = false;
            minimizeButton.Click += minimizeButton_Click;
            // 
            // maximizeButton
            // 
            maximizeButton.BackColor = Color.White;
            maximizeButton.BackgroundImageLayout = ImageLayout.None;
            maximizeButton.FlatAppearance.BorderColor = Color.White;
            maximizeButton.FlatAppearance.BorderSize = 0;
            maximizeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(229, 229, 229);
            maximizeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(229, 229, 229);
            maximizeButton.FlatStyle = FlatStyle.Flat;
            maximizeButton.Image = Properties.Resources.maximize;
            maximizeButton.Location = new Point(513, 0);
            maximizeButton.Margin = new Padding(0);
            maximizeButton.Name = "maximizeButton";
            maximizeButton.Size = new Size(60, 44);
            maximizeButton.TabIndex = 1;
            maximizeButton.UseVisualStyleBackColor = false;
            maximizeButton.Click += maximizeButton_Click;
            // 
            // closeButton
            // 
            closeButton.BackColor = Color.White;
            closeButton.BackgroundImageLayout = ImageLayout.None;
            closeButton.FlatAppearance.BorderColor = Color.White;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(232, 17, 35);
            closeButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 17, 35);
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.Image = Properties.Resources.dismiss;
            closeButton.Location = new Point(573, 0);
            closeButton.Margin = new Padding(0);
            closeButton.Name = "closeButton";
            closeButton.Size = new Size(60, 44);
            closeButton.TabIndex = 0;
            closeButton.UseVisualStyleBackColor = false;
            closeButton.Click += closeButton_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Magenta;
            ClientSize = new Size(1371, 900);
            ControlBox = false;
            Controls.Add(TitleBar);
            Controls.Add(magnifierPanel);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(5, 6, 5, 6);
            MinimumSize = new Size(857, 400);
            Name = "MainWindow";
            Padding = new Padding(7, 8, 7, 8);
            Text = "Ultra Wide Screen Share 2.0";
            TopMost = true;
            TransparencyKey = Color.Magenta;
            Load += MainWindow_Load;
            ResizeBegin += MainWindow_ResizeBegin;
            ResizeEnd += MainWindow_ResizeEnd;
            Paint += MainWindow_Paint;
            TitleBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel magnifierPanel;
        private FlowLayoutPanel TitleBar;
        private Button closeButton;
        private Button maximizeButton;
        private Button minimizeButton;
        private Button titleButton;
        private Button dragButton;
        private Button toggleButton;
    }
}