using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Notepad
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

		public void Open_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true)
			{
				string filePath = openFileDialog.FileName;
				txtEditor.Text = File.ReadAllText(filePath);
			}
		}

		public void Save_Click(object sender, RoutedEventArgs e)
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			if (saveFileDialog.ShowDialog() == true)
			{
				string filePath = saveFileDialog.FileName;
				File.WriteAllText(filePath, txtEditor.Text);
			}
		}

		private void Exit_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}