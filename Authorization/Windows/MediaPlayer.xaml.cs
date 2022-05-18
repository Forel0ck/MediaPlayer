using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Forms;

namespace Authorization.Windows
{
    /// <summary>
    /// Логика взаимодействия для MediaPlayer.xaml
    /// </summary>
    public partial class MediaPlayer : Window
    {
        DispatcherTimer tiempo = new DispatcherTimer();
        DispatcherTimer timer;
        bool isDragging;
        private String tipo_Media = "";
        private bool fullscreen = false;
        private bool mute = false;


        public MediaPlayer()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5);
            timer.Tick += new EventHandler(timer_Tick);

            tiempo.Interval = new TimeSpan(0, 0, 0);

        }




        private void timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                slideravance.Value = mpMyl.Position.TotalSeconds;
                lbTime.Content = mpMyl.Position.Minutes + ":" + mpMyl.Position.Seconds;

            }
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            MediaState ms = MediaState.Play;
            mpMyl.LoadedBehavior = ms;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            MediaState ss = MediaState.Stop;
            mpMyl.LoadedBehavior = ss;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            MediaState ps = MediaState.Pause;
            mpMyl.LoadedBehavior = ps;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "MP3 Files (*.mp3)|*.mp3|MP4 File (*.mp4)|*.mp4|3GP File (*.3gp)|*.3gp|Audio File (*.wma)|*.wma|MOV File (*.mov)|*.mov|AVI File (*.avi)|*.avi|Flash Video(*.wmv)|*.wmv|MPEG-2 File(*.mpeg)|*.mpeg|WebM Video (*.webm)|*.webm|All files (*.*)|*.*";
                ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                ofd.ShowDialog();
                string filename = ofd.FileName;
                if (filename != "")
                {

                    Uri uri = new Uri(filename);
                    mpMyl.Source = uri;
                    mpMyl.Volume = 100.5;
                    MediaState opt = MediaState.Play;
                    mpMyl.LoadedBehavior = opt;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No Selection", "Empty");
                }
            }
            catch (Exception e1)
            {
                System.Console.WriteLine("Error message: " + e1.Message);
                throw;
            }
        }

        string videoURL = @"C:\Users\Chebyrek\source\repos\Authorization\Authorization\Videos\100_0001.mov";
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri obj = new Uri(videoURL);
                mpMyl.Source = obj;
                MediaState opt = MediaState.Play;
                mpMyl.LoadedBehavior = opt;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error Message: " + ex.Message);
                throw;
            }
        }

        private void mpMyl_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimeSpan ts = mpMyl.NaturalDuration.TimeSpan;
            slideravance.Maximum = ts.TotalSeconds;
            timer.Start();
        }

        private void slideravance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mpMyl.Position = TimeSpan.FromSeconds(slideravance.Value);
        }

        private void Mute()
        {

            if (!mute)
            {


                Vol.Source = new BitmapImage(new Uri(@"/Images\Mute.png", UriKind.Relative));
                mpMyl.IsMuted = true;

            }
            else
            {
                mpMyl.IsMuted = false;
                Vol.Source = new BitmapImage(new Uri(@"/Images\Volume Up.png", UriKind.Relative));

            }

            mute = !mute;

        }

        private void Vol_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Mute();
        }

        private void SliderVol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mpMyl.Volume = SliderVol.Value / 200;

        }
        public WindowState lastWindowState;
        public void Fullpantalla()
        {
            if (!fullscreen)
            {

                RootWindow.Visibility = Visibility.Collapsed;
                RootWindow.WindowStyle = WindowStyle.None;
                RootWindow.ResizeMode = ResizeMode.NoResize;
                lastWindowState = RootWindow.WindowState;
                RootWindow.WindowState = WindowState.Maximized;
                RootWindow.Topmost = true;
                RootWindow.Visibility = Visibility.Visible;
            }
            else
            {
                RootWindow.Topmost = false;
                RootWindow.ResizeMode = ResizeMode.CanResize;
                RootWindow.WindowState = lastWindowState;
                RootWindow.Visibility = Visibility.Visible;

            }
            
        }

        private void Full_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Fullpantalla();
        }
    }  
}

