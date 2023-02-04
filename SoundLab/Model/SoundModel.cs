using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace SoundLab.Model
{
    public class SoundModel
    {
        public string selectedFile { get; set; } = "";
        private string[]  allowedFiles = ".mp3,.m4a,.m4v,.mov,.mp4,.wav,.sami,.smi,.avi,.aac,.3g2,.3gp,.3gp2,.3gpp,.asf,.wma,.wmv".Split(',');
        public DirectoryInfo DocumentsDir = new DirectoryInfo(Environment.GetEnvironmentVariable("userprofile") + @"\Documents\SoundLab");
        private BufferedWaveProvider bufferStream;
        public int DeviceNumber
        {
            get => WaveOut.DeviceNumber;
            set
            {
                WaveOut.DeviceNumber = value;
                bufferStream = new BufferedWaveProvider(MicroPhone.WaveFormat);
                Stop();
            }
        }
        public WaveIn MicroPhone { get; set; }
        public WaveOut WaveOut = new WaveOut();
        public async void Stop()
        {
            await Task.Run(() => {
                WaveOut.Stop();
                WaveOut.Init(bufferStream);
                WaveOut.Play();
            });
        }
        public ObservableCollection<FileInfo> Files { get
            {
                ObservableCollection<FileInfo> Files = 
                    new ObservableCollection<FileInfo>(
                        DocumentsDir.EnumerateFiles()
                        .Where(file => allowedFiles.Contains(
                        file.Extension)));
                return Files;
            }
        }
        public SoundModel()
        {
            if (!DocumentsDir.Exists)
                DocumentsDir.Create();
            MicroPhone = new WaveIn();
            bufferStream = new BufferedWaveProvider(MicroPhone.WaveFormat);
            MicroPhone.DataAvailable += MicroPhone_DataAvailable;
            MicroPhone.StartRecording();
            DeviceNumber = 1;

        }

        private void MicroPhone_DataAvailable(object sender, WaveInEventArgs e)
        {
            bufferStream.AddSamples(e.Buffer, 0 , e.Buffer.Length);
        }

        ~SoundModel()
        {
            WaveOut?.Stop();
            WaveOut?.Dispose();
            MicroPhone?.StopRecording();
            MicroPhone?.Dispose();
        }
        public void Play()
        {
            if (selectedFile != "")
            using (var ar = new AudioFileReader(selectedFile))
            {
                    try
                    {
                        WaveOut.Init(ar);
                        WaveOut.Play();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            }
        }
        public void AddFile(object File)
        {

            foreach(var s in (File as string[]))
            {
                FileInfo f = new FileInfo(s);
                if (!f.Exists || !allowedFiles.Contains(f.Extension))
                    return;
                f.MoveTo($@"{DocumentsDir.FullName}\{f.Name}");
            }
        }
       
    }
}
