using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Microsoft.Win32;


namespace Clock
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Storyboard seconds = (Storyboard)ssecond.FindResource("sbseconds");
            seconds.Begin();
            seconds.Seek(new TimeSpan(0, 0, 0, DateTime.Now.Second, 0));

            Storyboard minutes = (Storyboard)sminute.FindResource("sbminutes");
            minutes.Begin();
            minutes.Seek(new TimeSpan(0, 0, DateTime.Now.Minute, DateTime.Now.Second, 0));

            Storyboard hours = (Storyboard)shour.FindResource("sbhours");
            hours.Begin();
            hours.Seek(new TimeSpan(0, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, 0));
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        //private void SetStartup()
        //{
        //    RegistryKey rk = Registry.CurrentUser.OpenSubKey
        //        ("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        //    if (chkStartUp.Checked)
        //        rk.SetValue(AppName, Application.ExecutablePath);
        //    else
        //        rk.DeleteValue(AppName, false);

        //}
        private void clsbtn(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        Point _startPosition;
        bool _isResizing = false;
        private void resizeGrip_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Capture(resizeGrip))
            {
                _isResizing = true;
                _startPosition = Mouse.GetPosition(this);
            }
        }

        private void window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_isResizing)
            {
                Point currentPosition = Mouse.GetPosition(this);
                double diffX = currentPosition.X - _startPosition.X;
                double diffY = currentPosition.Y - _startPosition.Y;
                double currentLeft = gridResize.Margin.Left;
                double currentTop = gridResize.Margin.Top;
                gridResize.Margin = new Thickness(currentLeft + diffX, currentTop + diffY, 0, 0);
                _startPosition = currentPosition;
            }
        }

        private void resizeGrip_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_isResizing == true)
            {
                _isResizing = false;
                Mouse.Capture(null);
            }

        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            controlButtons.Opacity = 100;
        }
    }
}
