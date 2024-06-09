/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using System.IO;
using OpenQA.Selenium.Chrome;

namespace SeleniumProject.Util
{
    public class VideoRecorder
    {
        private Process ffmpegProcess;
        private string outputDirectory;
        private string videoFileName;
        private string ffmpegPath;

        public VideoRecorder()
        {
            outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestVideos");
            Directory.CreateDirectory(outputDirectory);
            videoFileName = $"TestVideo_{DateTime.Now:yyyyMMdd_HHmmss}.mp4";
            ffmpegPath = @"C:\Users\kkaffe\source\repos\SeleniumProject\SeleniumProject\Drivers";
        }

        public void StartRecording()
        {
            string videoFilePath = Path.Combine(outputDirectory, videoFileName);

            ffmpegProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = $"-y -f gdigrab -framerate 30 -i desktop -c:v libx264 -r 30 \"{videoFilePath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            ffmpegProcess.Start();
        }

        public void StopRecording()
        {
            if (ffmpegProcess != null && !ffmpegProcess.HasExited)
            {
                ffmpegProcess.Kill();
                ffmpegProcess = null;
            }
        }

        public string GetVideoFilePath()
        {
            return Path.Combine(outputDirectory, videoFileName);
        }
    }

}
*/