using Microsoft.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using YourProject.Models;

namespace YourProject.Services
{
	public class DatabaseService
	{
		private string connectionString = "Data Source=macros.db";

		public void InitializeDatabase()
		{
			if (!File.Exists("macros.db"))
			{
				using (var connection = new SqliteConnection(connectionString))
				{
					connection.Open();
					var command = connection.CreateCommand();
					command.CommandText = @"
						CREATE TABLE IF NOT EXISTS Macros (
							Id INTEGER PRIMARY KEY AUTOINCREMENT,
							KeyCombination TEXT NOT NULL,
							Description TEXT NOT NULL,
							KeyName TEXT NOT NULL
						)";
					command.ExecuteNonQuery();
					// Проверяем пустая ли таблица и добавляем данные
					command.CommandText = "SELECT COUNT(*) FROM Macros";
					var count = Convert.ToInt64(command.ExecuteScalar());

					if (count == 0)
					{

						// Добавляем тестовые данные
						command.CommandText = @"
						INSERT INTO Macros (KeyName, KeyCombination, Description)
						VALUES  
						('Win', 'Win+D', 'Показать рабочий стол'),
						('Win', 'Win+E', 'Открыть Проводник'),
						('Win', 'Win+I', 'Открыть Параметры'),
						('Win', 'Win+L', 'Заблокировать компьютер'),
						('Win', 'Win+R', 'Открыть окно ""Выполнить""'),
						('Win', 'Win+S', 'Открыть Поиск'),
						('Win', 'Win+A', 'Открыть Центр уведомлений'),
						('Win', 'Win+X', 'Открыть меню быстрой ссылки'),
						('Win', 'Win+Tab', 'Открыть Представление задач'),
						('Win', 'Win+Пробел', 'Переключить язык ввода'),
						('Win', 'Win+Стрелка влево', 'Прикрепить окно к левому краю'),
						('Win', 'Win+Стрелка вправо', 'Прикрепить окно к правому краю'),
						('Win', 'Win+Стрелка вверх', 'Развернуть окно'),
						('Win', 'Win+Стрелка вниз', 'Свернуть окно'),
						('Win', 'Win+Shift + S', 'Создать скриншот области'),
						('Win', 'Win+V', 'Открыть буфер обмена'),
						('Win', 'Win+Plus', 'Увеличить (лупа)'),
						('Win', 'Win+Minus', 'Уменьшить (лупа)'),
						('Win', 'Win+Esc', 'Выйти из лупы'),
						('Win', 'Win+P', 'Переключить режим投影'),
						('Win', 'Win+Ctrl + D', 'Создать виртуальный рабочий стол'),
						('Win', 'Win+Ctrl + F4', 'Закрыть текущий виртуальный рабочий стол'),
						('Win', 'Win+Ctrl + Стрелка влево/вправо', 'Переключение между виртуальными столами'),
						('Win', 'Win+Number (1-9)', 'Запуск приложения с панели задач'),
						('Win', 'Win+T', 'Цикл по приложениям на панели задач'),
						('Win', 'Win+M', 'Свернуть все окна'),
						('Win', 'Win+Shift + M', 'Восстановить свернутые окна'),
						('Win', 'Win+Home', 'Свернуть/восстановить все неактивные окна'),
						('Win', 'Win+Pause', 'Открыть свойства системы'),
						('Ctrl', 'Ctrl+C', 'Копировать'),
						('Ctrl', 'Ctrl+X', 'Вырезать'),
						('Ctrl', 'Ctrl+V', 'Вставить'),
						('Ctrl', 'Ctrl+Z', 'Отменить'),
						('Ctrl', 'Ctrl+Y', 'Повторить'),
						('Ctrl', 'Ctrl+A', 'Выделить всё'),
						('Ctrl', 'Ctrl+S', 'Сохранить'),
						('Ctrl', 'Ctrl+P', 'Печать'),
						('Ctrl', 'Ctrl+F', 'Найти'),
						('Ctrl', 'Ctrl+N', 'Создать новый документ/окно'),
						('Ctrl', 'Ctrl+O', 'Открыть'),
						('Ctrl', 'Ctrl+W', 'Закрыть текущее окно/вкладку'),
						('Ctrl', 'Ctrl+Shift + N', 'Создать новую папку'),
						('Ctrl', 'Ctrl+Shift + Esc', 'Открыть Диспетчер задач'),
						('Ctrl', 'Ctrl+B', 'Сделать текст полужирным'),
						('Ctrl', 'Ctrl+I', 'Сделать текст курсивом'),
						('Ctrl', 'Ctrl+U', 'Подчеркнуть текст'),
						('Ctrl', 'Ctrl+Shift + <', 'Уменьшить размер шрифта'),
						('Ctrl', 'Ctrl+Shift + >', 'Увеличить размер шрифта'),
						('Ctrl', 'Ctrl+L', 'Выровнять текст по левому краю'),
						('Ctrl', 'Ctrl+E', 'Выровнять текст по центру'),
						('Ctrl', 'Ctrl+R', 'Выровнять текст по правому краю'),
						('Ctrl', 'Ctrl+J', 'Выровнять текст по ширине'),
						('Ctrl', 'Ctrl+Q', 'Убрать форматирование абзаца'),
						('Ctrl', 'Ctrl+M', 'Увеличить отступ абзаца'),
						('Ctrl', 'Ctrl+Shift + M', 'Уменьшить отступ абзаца'),
						('Ctrl', 'Ctrl+1', 'Одинарный межстрочный интервал'),
						('Ctrl', 'Ctrl+2', 'Двойной межстрочный интервал'),
						('Ctrl', 'Ctrl+5', 'Полуторный межстрочный интервал'),
						('Alt', 'Alt+Tab', 'Переключение между окнами'),
						('Alt', 'Alt+F4', 'Закрыть текущее приложение'),
						('Alt', 'Alt+Enter', 'Свойства файла'),
						('Alt', 'Alt+Пробел', 'Системное меню окна'),
						('Alt', 'Alt+Подчеркнутая буква', 'Выполнить команду меню'),
						('Alt', 'Alt+D', 'Выделить адресную строку в Проводнике'),
						('Alt', 'Alt+Стрелка влево', 'Назад'),
						('Alt', 'Alt+Стрелка вправо', 'Вперед'),
						('Alt', 'Alt+Стрелка вверх', 'Перейти на уровень выше'),
						('F', 'F2', 'Переименовать'),
						('F', 'F5', 'Обновить'),
						('F1', 'F11', 'Полноэкранный режим'),
						('Shift', 'Shift + Delete', 'Удалить без помещения в корзину'),
						('Print', 'Print Screen', 'Скриншот экрана'),
						('Alt', 'Alt + Print Screen', 'Скриншот активного окна'),
						('Ctrl', 'Ctrl + Alt + Delete', 'Открыть экран безопасности')";
					}
					command.ExecuteNonQuery();
				}
			}
		}

		public List<Macro> GetMacros()
		{
			var macros = new List<Macro>();
			using (var connection = new SqliteConnection(connectionString))
			{
				connection.Open();
				var command = connection.CreateCommand();
				command.CommandText = "SELECT Id, KeyCombination, Description, KeyName FROM Macros";

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						macros.Add(new Macro
						{
							Id = reader.GetInt32(0),
                            KeyCombination = reader.IsDBNull(1) ? null : reader.GetString(1),
                            Description = reader.IsDBNull(2) ? null : reader.GetString(2),
                            KeyName = reader.IsDBNull(3) ? null : reader.GetString(3)
                        });
					}
				}
			}
			return macros;
		}

		public void UpdateMacro(Macro macro)
		{
			try
			{
				using var connection = new SqliteConnection(connectionString);
				connection.Open();

				var command = connection.CreateCommand();
				command.CommandText = @"
					UPDATE Macros 
					SET KeyCombination = $combination, 
						Description = $description,
						KeyName = $keyname
					WHERE Id = $id";

				command.Parameters.AddWithValue("$combination", macro.KeyCombination ?? "");
				command.Parameters.AddWithValue("$description", macro.Description ?? "");
                command.Parameters.AddWithValue("$keyname", macro.KeyName ?? "");
                command.Parameters.AddWithValue("$id", macro.Id);

				int rowsAffected = command.ExecuteNonQuery();
				System.Diagnostics.Debug.WriteLine($"Обновлено строк: {rowsAffected} для ID: {macro.Id}");
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine($"Ошибка: {ex.Message}");
			}
		}

		public void AddMacro(Macro macro)
		{
			using var connection = new SqliteConnection(connectionString);
			connection.Open();

			var command = connection.CreateCommand();
			command.CommandText = @"
				INSERT INTO Macros (KeyCombination, Description, KeyName) 
				VALUES ($combination, $description, $keyname);
				SELECT last_insert_rowid();"; // Получаем новый ID


			command.Parameters.AddWithValue("$combination", macro.KeyCombination ?? "");
			command.Parameters.AddWithValue("$description", macro.Description ?? "");
            command.Parameters.AddWithValue("$keyname", macro.KeyName ?? "");

            var newId = (long)command.ExecuteScalar();
			macro.Id = (int)newId; // Обновляем ID в объекте
		}

        public void DeleteMacro(int id)
        {
            using var connection = new SqliteConnection(connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Macros WHERE Id = $id";
            command.Parameters.AddWithValue("$id", id);
            command.ExecuteNonQuery();
        }

        public void ExportForESP(string filePath)
        {
            var macros = GetMacros();

            // Выбираем только нужные поля для ESP
            var espConfig = macros.Select(m => new
            {
                id = m.Id,
                keys = m.KeyCombination
            }).ToList();

            // Сериализуем в JSON
            string json = JsonConvert.SerializeObject(espConfig, Formatting.Indented);

            // Сохраняем файл
            File.WriteAllText(filePath, json);
        }
    }
}