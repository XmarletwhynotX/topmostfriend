﻿using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace TopMostFriend {
    public sealed class AboutWindow : Form {
        private const int BUTTON_SPACING = 6;
        private const int GENERAL_PADDING = 12;
        private const int BUTTON_HEIGHT = 23;
        private const int BUTTON_WIDTH = 70;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x02;

        public static void Display() {
            using (AboutWindow about = new AboutWindow())
                about.ShowDialog();
        }

        public AboutWindow() {
            Text = $@"About Top Most Friend";
            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            BackgroundImage = Properties.Resources.about;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = Properties.Resources.about.Size;
            MaximizeBox = MinimizeBox = false;
            MaximumSize = MinimumSize = Size;

            int tabIndex = 0;

            Button closeButton = new Button {
                Text = @"Close",
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
                TabIndex = ++tabIndex,
            };
            closeButton.Location = new Point(ClientSize.Width - closeButton.Size.Width - GENERAL_PADDING, ClientSize.Height - closeButton.Size.Height - GENERAL_PADDING);
            closeButton.Click += (s, e) => Close();
            Controls.Add(closeButton);

            Button websiteButton = new Button {
                Text = @"Website",
                Size = new Size(BUTTON_WIDTH, BUTTON_HEIGHT),
                TabIndex = ++tabIndex,
            };
            websiteButton.Location = new Point(closeButton.Left - websiteButton.Width - BUTTON_SPACING, closeButton.Top);
            websiteButton.Click += (s, e) => Process.Start(@"https://flash.moe/utils#tmf");
            Controls.Add(websiteButton);

            Button creditButton = new Button {
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top,
                Text = string.Empty,
                Size = new Size(300, 15),
                TabIndex = ++tabIndex,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.Transparent,
            };
            creditButton.FlatAppearance.BorderSize = 0;
            creditButton.FlatAppearance.MouseOverBackColor = Color.Transparent;
            creditButton.FlatAppearance.MouseDownBackColor = Color.Transparent;
            creditButton.Location = new Point(ClientSize.Width - creditButton.Size.Width - GENERAL_PADDING, 60);
            creditButton.Click += (s, e) => Process.Start(@"https://flash.moe");

            Controls.Add(creditButton);
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);

            Program.ReleaseCapture();
            Program.SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }
    }
}