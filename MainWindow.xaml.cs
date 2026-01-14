using InTheHand.Net;
using InTheHand.Net.Bluetooth;
using InTheHand.Net.Sockets;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.IO;
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
using YourProject.Models;
using YourProject.Services;
using YourProject.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ESP_Keyboard
{
	public partial class MainWindow : Window
	{
        public MainWindow()
		{

			InitializeComponent();
            
            EditorDataBase.DataUpdated += OnDataUpdated;
			
            var dbService = new DatabaseService();
            dbService.InitializeProfelDatabase();
            var profiles = dbService.GetProfile();

            ComboBoxProfile.ItemsSource = profiles;
            ComboBoxProfile.DisplayMemberPath = "Profile";
            ComboBoxProfile.SelectedValuePath = "Id";
            ComboBoxProfile.SelectedIndex = 0;

            ComboBoxProfile.SelectionChanged += ComboBoxProfile_SelectionChanged;

            if (profiles.Count > 0)
            {
                ComboBoxProfile.SelectedIndex = 0;
                ComboBoxProfile_SelectionChanged(null, null);
            }

            LoadMacros();

        }

        private void ComboBoxProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxProfile.SelectedItem is Porf selectedProfile)
            {
                string idMakros = selectedProfile.IdMakros;
                ProcessIdMakros(idMakros);
                LoadMacros();
            }
        }

        private List<int> currentNumbers = new List<int>();

        private void ProcessIdMakros(string idMakros)
        {
            if (!string.IsNullOrEmpty(idMakros))
            {
                try
                {
                    currentNumbers = idMakros.Split(' ').Select(int.Parse).ToList();
                    Console.WriteLine($"Обработаны числа: {string.Join(", ", currentNumbers)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка обработки IdMakros: {ex.Message}");
                    currentNumbers = new List<int>();
                }
            }
            else
            {
                currentNumbers = new List<int>();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			LoadMacros();

		}

		private void OnDataUpdated()
		{
			Dispatcher.Invoke(() => LoadMacros());
		}

		protected override void OnClosed(EventArgs e)
		{
			EditorDataBase.DataUpdated -= OnDataUpdated;
			base.OnClosed(e);
		}

        private ComboBox[] comboBoxes;


        private void LoadMacros()
		{

			var dbService = new DatabaseService();
			dbService.InitializeDatabase();
			comboBoxes = new[] { ComboBoxBut1, ComboBoxBut2, ComboBoxBut3, ComboBoxBut4, ComboBoxBut5, ComboBoxBut6 };
            var macros = dbService.GetMacros();
            for (int i = 0; i < comboBoxes.Length; i++)
            {
                var comboBox = comboBoxes[i];
                comboBox.ItemsSource = macros;
                comboBox.DisplayMemberPath = "KeyCombination";
                comboBox.SelectedValuePath = "KeyName";

                int indexToSet = 0;

                if (currentNumbers.Count > i)
                {
                    indexToSet = currentNumbers[i];
                }
                else if (currentNumbers.Count > 0)
                {
                    indexToSet = currentNumbers[0];
                }

                if (indexToSet < 0) indexToSet = 0;
                if (indexToSet >= macros.Count) indexToSet = macros.Count - 1;

                comboBox.SelectedIndex = indexToSet;
                
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
            
            try
            {
                string macStr = "88:57:21:96:6B:12";
                BluetoothAddress address = BluetoothAddress.Parse(macStr);
                using var client = new BluetoothClient();
                var endPoint = new BluetoothEndPoint(address, BluetoothService.SerialPort);
                client.Connect(endPoint);

                var stream = client.GetStream();
                var bytes = System.Text.Encoding.ASCII.GetBytes($"k0:{ComboBoxBut1.SelectedValue as string}\nk1:{ComboBoxBut2.SelectedValue as string}\nk2:{ComboBoxBut3.SelectedValue as string}\nk3:{ComboBoxBut4.SelectedValue as string}\nk4:{ComboBoxBut5.SelectedValue as string}\nk5:{ComboBoxBut6.SelectedValue as string}");
                stream.Write(bytes, 0, bytes.Length);

                Console.WriteLine("Отправлено");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

    }
}