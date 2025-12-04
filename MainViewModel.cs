using System.Collections.ObjectModel;
using System.Windows;
using YourProject.Models;
using YourProject.Services;

public class MainViewModel
{
	public ObservableCollection<Macro> Macros { get; set; } = new ObservableCollection<Macro>();
    public MainViewModel()
    {
        var dbService = new DatabaseService();
        var macros = dbService.GetMacros();

        // Временная проверка
        foreach (var macro in macros)
        {
            Macros.Add(macro);
            System.Diagnostics.Debug.WriteLine($"KeyName: '{macro.KeyName}'");
            //System.Diagnostics.Debug.WriteLine($"Добавлен: {macro.KeyCombination}");
        }
        
    }
}