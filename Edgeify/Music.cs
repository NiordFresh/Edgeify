using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace Edgeify
{
    public class MusicPlay
    {
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr winHandle);

        private string musFilePath = "";

        public MusicPlay()
        {
            StartBackgroundMusic();
        }

        private void StartBackgroundMusic()
        {
            try
            {
                musFilePath = Path.Combine(Path.GetTempPath(), "music.mp3");
                string[] possibleNames = { "Edgeify.music.mp3", "music.mp3", "Edgeify.Resources.music.mp3" };
                Stream resourceStream = null;
                foreach (string resourceName in possibleNames)
                {
                    resourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
                    if (resourceStream != null)
                    {
                        break;
                    }
                }

                if (resourceStream != null)
                {
                    using (resourceStream)
                    {
                        using (FileStream fileStream = File.Create(musFilePath))
                        {
                            resourceStream.CopyTo(fileStream);
                        }
                    }

                    if (File.Exists(musFilePath))
                    {
                        PlayThisShit();
                    }
                }
            }
            catch { }
        }

        private void PlayThisShit()
        {
            try
            {
                mciSendString("close", null, 0, IntPtr.Zero);
                mciSendString($"open \"{musFilePath}\" type mpegvideo alias music", null, 0, IntPtr.Zero);
                mciSendString("play music repeat", null, 0, IntPtr.Zero);
            }
            catch
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        // tbh, idk asf what is this...
                        FileName = "powershell",
                        Arguments = $"-WindowStyle Hidden -Command \"Add-Type -AssemblyName presentationCore; $mediaPlayer = New-Object system.windows.media.mediaplayer; $mediaPlayer.open([uri]'{musFilePath}'); $mediaPlayer.play(); while($true){{ Start-Sleep 1 }}\"",
                        UseShellExecute = false,
                        CreateNoWindow = true
                    };
                    Process.Start(startInfo);
                }
                catch
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo
                        {
                            FileName = musFilePath,
                            UseShellExecute = true,
                            WindowStyle = ProcessWindowStyle.Hidden
                        });
                    }
                    catch { }
                }
            }
        }

        public void Stop()
        {
            try
            {
                mciSendString("close", null, 0, IntPtr.Zero);

                if (File.Exists(musFilePath))
                {
                    Thread.Sleep(100);
                    File.Delete(musFilePath);
                }
            }
            catch { }
        }
    }
}