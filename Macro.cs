namespace YourProject.Models
{
	public class Macro
	{
        public int Id { get; set; }               // Первичный ключ БД (1, 2, 3...)
        public string KeyCombination { get; set; }// Сочетание ("ctrl+c", "alt+tab")
        public string Description { get; set; }   // Описание ("скопировать", "вставить")
        public string KeyName { get; set; }       // Имя клавиши ("ctrl", "alt", "win")
    }
}