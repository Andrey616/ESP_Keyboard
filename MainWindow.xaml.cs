using Microsoft.Win32;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using YourProject.Services;
using YourProject.Services;

namespace ESP_Keyboard
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	
	public partial class MainWindow : Window
	{
		public MainWindow()
		{

			InitializeComponent();
			EditorDataBase.DataUpdated += OnDataUpdated;
			LoadMacros();
			
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			LoadMacros();

		}

		private ComboBox[] comboBoxes;

		private void OnDataUpdated()
		{
			Dispatcher.Invoke(() => LoadMacros()); // Перезагрузка
		}

		protected override void OnClosed(EventArgs e)
		{
			EditorDataBase.DataUpdated -= OnDataUpdated; // Отписка
			base.OnClosed(e);
		}
		
		private void LoadMacros()
		{

			var dbService = new DatabaseService();
			dbService.InitializeDatabase(); // Создаст БД при первом запуске
			comboBoxes = new[] { ComboBoxBut1, ComboBoxBut2, ComboBoxBut3, ComboBoxBut4, ComboBoxBut5, ComboBoxBut6 };
			foreach (var comboBox in comboBoxes)
			{
				comboBox.ItemsSource = dbService.GetMacros();
				comboBox.DisplayMemberPath = "KeyCombination";
				comboBox.SelectedValuePath = "Id";
			}

		}
		private void OpenEditor_Click(object sender, RoutedEventArgs e)
		{
			EditorDataBase editor = new EditorDataBase();
			editor.Left = this.Left + 100;
			editor.Top = this.Top + 50;
			editor.Show();
		}

		

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.Filter = "JSON files (*.json)|*.json";

            if (saveDialog.ShowDialog() == true)
            {
                var dbService = new DatabaseService();
                dbService.ExportForESP(saveDialog.FileName);
                MessageBox.Show("Экспорт завершен!");
            }
        }

    }
}