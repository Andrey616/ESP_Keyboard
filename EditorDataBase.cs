using System.Collections.ObjectModel;
using YourProject.Models;

namespace YourProject.ViewModels
{
    public class EditorDataBase
    {
        public ObservableCollection<Macro> Macros { get; set; } = new();
    }
}