using System.Drawing;
using Microsoft.Win32;

namespace UltraWideScreenShare.WinForms
{
    internal static class Settings
    {
        private const string RegistryKeyPath = @"Software\UltraWideScreenShare";
        private const string WindowPositionXKey = "WindowPositionX";
        private const string WindowPositionYKey = "WindowPositionY";
        private const string WindowWidthKey = "WindowWidth";
        private const string WindowHeightKey = "WindowHeight";
        private const string WindowStateKey = "WindowState";

        public static void SaveWindowPosition(Form form)
        {
            try
            {
                using (RegistryKey? key = Registry.CurrentUser.CreateSubKey(RegistryKeyPath))
                {
                    if (key != null)
                    {
                        if (form.WindowState == FormWindowState.Normal)
                        {
                            key.SetValue(WindowPositionXKey, form.Location.X);
                            key.SetValue(WindowPositionYKey, form.Location.Y);
                            key.SetValue(WindowWidthKey, form.Width);
                            key.SetValue(WindowHeightKey, form.Height);
                        }
                        key.SetValue(WindowStateKey, form.WindowState.ToString());
                    }
                }
            }
            catch
            {
            }
        }

        public static void RestoreWindowPosition(Form form)
        {
            try
            {
                using (RegistryKey? key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
                {
                    if (key != null)
                    {
                        object? x = key.GetValue(WindowPositionXKey);
                        object? y = key.GetValue(WindowPositionYKey);
                        object? width = key.GetValue(WindowWidthKey);
                        object? height = key.GetValue(WindowHeightKey);
                        object? state = key.GetValue(WindowStateKey);

                        if (x != null && y != null)
                        {
                            int posX = (int)x;
                            int posY = (int)y;
                            
                            Point targetLocation = new Point(posX, posY);
                            
                            bool isLocationValid = false;
                            foreach (Screen screen in Screen.AllScreens)
                            {
                                if (screen.WorkingArea.Contains(targetLocation))
                                {
                                    isLocationValid = true;
                                    break;
                                }
                            }

                            if (isLocationValid)
                            {
                                form.StartPosition = FormStartPosition.Manual;
                                form.Location = targetLocation;
                            }
                        }

                        if (width != null && height != null)
                        {
                            int w = (int)width;
                            int h = (int)height;
                            
                            if (w > 0 && h > 0)
                            {
                                form.Size = new Size(w, h);
                            }
                        }

                        if (state != null && Enum.TryParse<FormWindowState>(state.ToString(), out FormWindowState windowState))
                        {
                            if (windowState != FormWindowState.Minimized)
                            {
                                form.WindowState = windowState;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}