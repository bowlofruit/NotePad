using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notepad;
using System.IO;
using System.Threading;
using System.Windows.Controls;

namespace WPFNotepad.Tests
{
	[TestClass]
	public class MainWindowTests
	{
		private MainWindow mainWindow;
		private ManualResetEvent mainWindowInitializedEvent = new ManualResetEvent(false);

		[TestInitialize]
		public void Initialize()
		{
			Thread thread = new Thread(() =>
			{
				mainWindow = new MainWindow();
				mainWindowInitializedEvent.Set(); // Сигналізує, що головне вікно ініціалізовано
				System.Windows.Threading.Dispatcher.Run();
			});

			thread.SetApartmentState(ApartmentState.STA);
			thread.Start();

			// Чекаємо, доки головне вікно буде повністю ініціалізовано
			mainWindowInitializedEvent.WaitOne();
		}

		[TestMethod]
		public void OpenFileTest()
		{
			string testFilePath = "test.txt";

			mainWindow.Dispatcher.Invoke(() =>
			{
				File.WriteAllText(testFilePath, "Test content");

			 	TextBox textBox = (TextBox)mainWindow.FindName("txtEditor");
				Assert.IsNotNull(textBox);
				mainWindow.Open_Click(null, null);
				Assert.AreEqual("Test content", textBox.Text);
			});
		}

		[TestMethod]
		public void SaveFileTest()
		{
			string testFilePath = "test.txt";
			
			mainWindow.Dispatcher.Invoke(() =>
			{
				TextBox textBox = (TextBox)mainWindow.FindName("txtEditor");
				Assert.IsNotNull(textBox);

				textBox.Text = "Test content";
				mainWindow.Save_Click(null, null);
			});

			Assert.AreEqual("Test content", File.ReadAllText(testFilePath));

			File.Delete(testFilePath);
		}
	}
}