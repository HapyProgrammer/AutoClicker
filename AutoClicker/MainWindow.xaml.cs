using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace AutoClicker
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		[DllImport("user32")]
		public static extern int SetCursorPos(int x, int y);

		private const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
		private const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
		private const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
		private const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */

		[DllImport("user32.dll",
			CharSet = CharSet.Auto, CallingConvention= CallingConvention.StdCall)]
		public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons,int dwExtraInfo);

		private int counter = 0;
		private double cPS = 1;
		private bool clicked = false;
		private DispatcherTimer dt = new DispatcherTimer();

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			clicked = true;
			if (clicked)
			{
				double seconds = 1 / cPS;
				dt.Interval = TimeSpan.FromSeconds(seconds);
				dt.Tick += dtTicker;
				dt.Start();
			}

		}

		private void ClicksPerSecond_Click(object sender, RoutedEventArgs e)
		{
			counter++;
			CPSCounter.Text = counter.ToString();
		}

		private void StopButton_Click(object sender, RoutedEventArgs e)
		{
			clicked = false;
			dt.Stop();
		}

		private void Windows_Loaded(object sender, RoutedEventArgs e)
		{
			//add shortcuts for outside window here
		}

		private void dtTicker(object sender, EventArgs e)
		{
			mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
			mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
			window.Topmost = true;

		}

		private void CPSSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			Slider cpsSlider = sender as Slider;
			cPS = cpsSlider.Value;
			int intCPS = (int)cPS;
			ClicksIndicatorText.Text = intCPS.ToString();
		}
	}
}
