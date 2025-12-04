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
using YourProject.Models;
using YourProject.Services;

namespace ESP_Keyboard
{
    /// <summary>
    /// Логика взаимодействия для EditorDataBase.xaml
    /// </summary>
    public partial class EditorDataBase : Window
    {
        public EditorDataBase()
        {
            InitializeComponent();
            DataContext = new MainViewModel();


        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        public static event Action DataUpdated;

        private void SaveDataBase(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext; // Получаем ViewModel
            var dbService = new DatabaseService();

            foreach (var macro in viewModel.Macros)
            {
                if (macro.Id > 0) // Существующие
                    dbService.UpdateMacro(macro);
                else // Новые
                    dbService.AddMacro(macro);
            }
            DataUpdated?.Invoke();
        }

        private void DeleteMacro(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            if (button.DataContext is Macro macro)
            {
                var dbService = new DatabaseService();
                dbService.DeleteMacro(macro.Id);

                var viewModel = (MainViewModel)DataContext;
                viewModel.Macros.Remove(macro);
            }
        }
    }
}
