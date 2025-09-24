using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Edgeify
{
    public partial class Edgefiy : Form
    {
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;

        private MusicPlay backgroundMusic;

        public Edgefiy()
        {
            InitializeComponent();
            bannerPic.MouseDown += BannerPic_MouseDown;
            this.MaximizeBox = false;
            uninstallEdge.FlatStyle = FlatStyle.Flat;
            uninstallEdge.FlatAppearance.BorderSize = 0;
            uninstallEdge.BackColor = Color.FromArgb(90, 20, 20, 20);
            closeBtn.FlatStyle = FlatStyle.Flat;
            closeBtn.FlatAppearance.BorderSize = 0;

            // background music start
            backgroundMusic = new MusicPlay();
        }

        private void closeBtn_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            backgroundMusic?.Stop();
            base.OnFormClosing(e);
        }

        private void BannerPic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void uninstallEdge_Click(object sender, EventArgs e)
        {
            // trying to kill this bastard...
            try { Process.Start("taskkill", "/f /im msedge.exe")?.WaitForExit(); } catch { }
            try { Process.Start("taskkill", "/f /im MicrosoftEdgeUpdate.exe")?.WaitForExit(); } catch { }

            string tempExe = Path.Combine(Path.GetTempPath(), "install_wim_tweak.exe");
            string target = @"C:\Windows\install_wim_tweak.exe";
            try
            {
                using (Stream resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Edgeify.install_wim_tweak.exe"))
                {
                    if (resourceStream == null)
                    {
                        string[] resources = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                        string availableResources = string.Join(", ", resources);
                        MessageBox.Show($"Nie można znaleźć wbudowanego pliku install_wim_tweak.exe\nDostępne zasoby: {availableResources}",
                                      "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    using (FileStream fileStream = File.Create(tempExe))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }
                // removing Microsoft Edge Ghost Icon...
                File.Copy(tempExe, target, true);
                Process.Start(target, "/o /c \"Microsoft-Windows-Internet-Browser-Package\" /r")?.WaitForExit();
                File.Delete(target);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas pracy z install_wim_tweak.exe: {ex.Message}",
                              "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                try
                {
                    if (File.Exists(tempExe))
                        File.Delete(tempExe);
                }
                catch { }
            }

            // removing catalogs from os.
            string[] dirs =
            {
                @"C:\Windows\SystemApps\Microsoft.MicrosoftEdge_8wekyb3d8bbwe",
                @"C:\Program Files (x86)\Microsoft\Edge",
                @"C:\Program Files (x86)\Microsoft\EdgeUpdate",
                @"C:\Program Files (x86)\Microsoft\EdgeCore",
                @"C:\Program Files (x86)\Microsoft\EdgeWebView"
            };
            foreach (var d in dirs)
            {
                try
                {
                    if (Directory.Exists(d)) Directory.Delete(d, true);
                }
                catch { }
            }

            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft", writable: true))
                {
                    if (key != null)
                    {
                        foreach (var sub in key.GetSubKeyNames())
                        {
                            if (sub.Contains("Edge"))
                            {
                                try { key.DeleteSubKeyTree(sub); } catch { }
                            }
                        }
                    }
                }
            }
            catch { }
            // shortcuts
            string[] shortcuts =
            {
                @"C:\Users\Public\Desktop\Microsoft Edge.lnk",
                Environment.ExpandEnvironmentVariables(@"%ProgramData%\Microsoft\Windows\Start Menu\Programs\Microsoft Edge.lnk"),
                Environment.ExpandEnvironmentVariables(@"%APPDATA%\Microsoft\Internet Explorer\Quick Launch\User Pinned\TaskBar\Microsoft Edge.lnk"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Microsoft Edge.lnk")
            };
            foreach (var s in shortcuts)
            {
                try { if (File.Exists(s)) File.Delete(s); } catch { }
            }

            MessageBox.Show("Removing Microsoft Edge Completed!", "Edgeify", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}