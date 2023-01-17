using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using NAudio.Wave;
using SoundLab.Utilities;
using System.Windows;
using System.Text;
using System.Threading.Tasks;

namespace SoundLab.Model
{
    public class SoundModel
    {
        private string _startKey = "j";
        private KeyBinding start;
        public string StartKey { get { return _startKey; } set { _startKey = value; } } 

        RelayCommand stopCommand;
        private KeyBinding stop;
        private string _stopKey;
        public string StopKey { get; set; } = "k";
        private WaveOut WaveOut { get; set; }
        public string selectedFile { get; set; } = "";
        private string[]  allowedFiles = ".mp3,.m4a,.m4v,.mov,.mp4,.wav,.sami,.smi,.avi,.aac,.3g2,.3gp,.3gp2,.3gpp,.asf,.wma,.wmv".Split(',');
        public DirectoryInfo DocumentsDir = new DirectoryInfo(Environment.GetEnvironmentVariable("userprofile") + @"\Documents\SoundLab");
        public int DeviceNumber { get; set; } = 0;
        public ObservableCollection<FileInfo> Files { get
            {
                ObservableCollection<FileInfo> Files = new ObservableCollection<FileInfo>();
                foreach (var f in 
                    DocumentsDir.EnumerateFiles()
                    .Where(file=> allowedFiles.Contains(
                       file.Extension)))
                    Files.Add(f);
                return Files;
            }
        }
        public void Play()
        {
            if (selectedFile != "")
            using (var ar = new AudioFileReader(selectedFile))
            {
                    try
                    {
                        WaveOut = new WaveOut();
                        WaveOut.DeviceNumber = DeviceNumber;
                        WaveOut.Init(ar);
                        WaveOut.Play();
                        WaveOut.PlaybackStopped += WaveOut_PlaybackStopped;
                        ar.Dispose();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            }
        }

        private void WaveOut_PlaybackStopped(object sender, StoppedEventArgs e)
        {
           Stop();
        }

        public void Stop()
        {
            WaveOut?.Stop();
            WaveOut?.Dispose(); 
        }
    }
}
