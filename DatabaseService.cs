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
						('0x83, 0xD9, 0x2C', 'Win+D', 'Показать рабочий стол'),
						('0x83, 0x8E', 'Win+E', 'Открыть Проводник'),
						('0x83, 0x73', 'Win+I', 'Открыть Параметры'),
						('0x83, 0x76', 'Win+L', 'Заблокировать компьютер'),
						('0x83, 0x82', 'Win+R', 'Открыть окно ""Выполнить""'),
						('0x83, 0x83', 'Win+S', 'Открыть Поиск'),
						('0x83, 0x65', 'Win+A', 'Открыть Центр уведомлений'),
						('0x83, 0x88', 'Win+X', 'Открыть меню быстрой ссылки'),
						('0x83, 0xB3', 'Win+Tab', 'Открыть Представление задач'),
						('0x83, 0x2C', 'Win+Space', 'Переключить язык ввода'),
						('0x83, 0xD8', 'Win+Left_Arrow', 'Прикрепить окно к левому краю'),
						('0x83, 0xD7', 'Win+Right_Arrow', 'Прикрепить окно к правому краю'),
						('0x83, 0xDA', 'Win+Up_Arrow', 'Развернуть окно'),
						('0x83, 0xD9', 'Win+Down_Arrow', 'Свернуть окно'),
						('0x83, 0x81, 0x83', 'Win+Shift+S', 'Создать скриншот области'),
						('0x83, 0x86', 'Win+V', 'Открыть буфер обмена'),
						('0x83, 0x87', 'Win+Plus', 'Увеличить (лупа)'),
						('0x83, 0x2D', 'Win+Minus', 'Уменьшить (лупа)'),
						('0x83, 0xB1', 'Win+Esc', 'Выйти из лупы'),
						('0x83, 0x80', 'Win+P', 'Переключить режим проекции'),
						('0x83, 0x80, 0x68', 'Win+Ctrl+D', 'Создать виртуальный рабочий стол'),
						('0x83, 0x80, 0xC5', 'Win+Ctrl+F4', 'Закрыть текущий виртуальный рабочий стол'),
						('0x83, 0x80, 0xD8', 'Win+Ctrl+Left_Arrow', 'Переключение между виртуальными столами влево'),
						('0x83, 0x80, 0xD7', 'Win+Ctrl+Right_Arrow', 'Переключение между виртуальными столами вправо'),
						('0x83, 0x31', 'Win+1', 'Запуск приложения с панели задач №1'),
						('0x83, 0x32', 'Win+2', 'Запуск приложения с панели задач №2'),
						('0x83, 0x33', 'Win+3', 'Запуск приложения с панели задач №3'),
						('0x83, 0x34', 'Win+4', 'Запуск приложения с панели задач №4'),
						('0x83, 0x35', 'Win+5', 'Запуск приложения с панели задач №5'),
						('0x83, 0x36', 'Win+6', 'Запуск приложения с панели задач №6'),
						('0x83, 0x37', 'Win+7', 'Запуск приложения с панели задач №7'),
						('0x83, 0x38', 'Win+8', 'Запуск приложения с панели задач №8'),
						('0x83, 0x39', 'Win+9', 'Запуск приложения с панели задач №9'),
						('0x83, 0x84', 'Win+T', 'Цикл по приложениям на панели задач'),
						('0x83, 0x77', 'Win+M', 'Свернуть все окна'),
						('0x83, 0x81, 0x77', 'Win+Shift+M', 'Восстановить свернутые окна'),
						('0x83, 0xD2', 'Win+Home', 'Свернуть/восстановить все неактивные окна'),
						('0x83, 0xD0', 'Win+Pause', 'Открыть свойства системы'),
						('0x80, 0x67', 'Ctrl+C', 'Копировать'),
						('0x80, 0x88', 'Ctrl+X', 'Вырезать'),
						('0x80, 0x86', 'Ctrl+V', 'Вставить'),
						('0x80, 0x7A', 'Ctrl+Z', 'Отменить'),
						('0x80, 0x79', 'Ctrl+Y', 'Повторить'),
						('0x80, 0x65', 'Ctrl+A', 'Выделить всё'),
						('0x80, 0x73', 'Ctrl+S', 'Сохранить'),
						('0x80, 0x70', 'Ctrl+P', 'Печать'),
						('0x80, 0x66', 'Ctrl+F', 'Найти'),
						('0x80, 0x6E', 'Ctrl+N', 'Создать новый документ/окно'),
						('0x80, 0x6F', 'Ctrl+O', 'Открыть'),
						('0x80, 0x77', 'Ctrl+W', 'Закрыть текущее окно/вкладку'),
						('0x80, 0x81, 0x6E', 'Ctrl+Shift+N', 'Создать новую папку'),
						('0x80, 0x81, 0xB1', 'Ctrl+Shift+Esc', 'Открыть Диспетчер задач'),
						('0x80, 0x66', 'Ctrl+B', 'Сделать текст полужирным'),
						('0x80, 0x69', 'Ctrl+I', 'Сделать текст курсивом'),
						('0x80, 0x75', 'Ctrl+U', 'Подчеркнуть текст'),
						('0x80, 0x81, 0x36', 'Ctrl+Shift+<', 'Уменьшить размер шрифта'),
						('0x80, 0x81, 0x37', 'Ctrl+Shift+>', 'Увеличить размер шрифта'),
						('0x80, 0x6C', 'Ctrl+L', 'Выровнять текст по левому краю'),
						('0x80, 0x65', 'Ctrl+E', 'Выровнять текст по центру'),
						('0x80, 0x72', 'Ctrl+R', 'Выровнять текст по правому краю'),
						('0x80, 0x6A', 'Ctrl+J', 'Выровнять текст по ширине'),
						('0x80, 0x71', 'Ctrl+Q', 'Убрать форматирование абзаца'),
						('0x80, 0x6D', 'Ctrl+M', 'Увеличить отступ абзаца'),
						('0x80, 0x81, 0x6D', 'Ctrl+Shift+M', 'Уменьшить отступ абзаца'),
						('0x80, 0x31', 'Ctrl+1', 'Одинарный межстрочный интервал'),
						('0x80, 0x32', 'Ctrl+2', 'Двойной межстрочный интервал'),
						('0x80, 0x35', 'Ctrl+5', 'Полуторный межстрочный интервал'),
						('0x82, 0xB3', 'Alt+Tab', 'Переключение между окнами'),
						('0x82, 0xC6', 'Alt+F4', 'Закрыть текущее приложение'),
						('0x82, 0xB0', 'Alt+Enter', 'Свойства файла'),
						('0x82, 0x2C', 'Alt+Space', 'Системное меню окна'),
						('0x82, 0x68', 'Alt+D', 'Выделить адресную строку в Проводнике'),
						('0x82, 0xD8', 'Alt+Left_Arrow', 'Назад'),
						('0x82, 0xD7', 'Alt+Right_Arrow', 'Вперед'),
						('0x82, 0xDA', 'Alt+Up_Arrow', 'Перейти на уровень выше'),
						('0xC3', 'F2', 'Переименовать'),
						('0xC6', 'F5', 'Обновить'),
						('0xCC', 'F11', 'Полноэкранный режим'),
						('0x81, 0xD4', 'Shift+Delete', 'Удалить без помещения в корзину'),
						('0xCE', 'Print_Screen', 'Скриншот экрана'),
						('0x82, 0xCE', 'Alt+Print_Screen', 'Скриншот активного окна'),
						('0x80, 0x82, 0xD4', 'Ctrl+Alt+Delete', 'Открыть экран безопасности')";
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