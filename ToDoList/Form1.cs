using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form1 : Form
    {
        private readonly TaskRepository _taskRepository;

        public Form1()
        {
            InitializeComponent();
            _taskRepository = new TaskRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadTasksFromDatabase();
        }

        private void addTaskButton_Click(object sender, EventArgs e)
        {
            string taskName = taskTextBox.Text.Trim();

            if (string.IsNullOrEmpty(taskName))
            {
                MessageBox.Show("Please enter a task.");
                return;
            }

            if (taskName.Length > 50)
            {
                MessageBox.Show("Task name is too long. Please enter a task with less than 50 characters.");
                return;
            }

            _taskRepository.AddTask(taskName);
            taskTextBox.Clear();
            LoadTasksFromDatabase();
        }

        private void LoadTasksFromDatabase()
        {
            fPanel.Controls.Clear();

            foreach (var task in _taskRepository.GetTasks())
            {
                Panel taskPanel = new Panel
                {
                    Width = fPanel.Width - 35,
                    Height = 30,
                    Margin = new Padding(5)
                };

                CheckBox taskCheckBox = new CheckBox
                {
                    Text = task.Name,
                    Width = taskPanel.Width - 80,
                    Location = new Point(5, 5),
                    Checked = task.IsCompleted,
                    Tag = task.Id
                };

                taskCheckBox.CheckedChanged += (s, e) =>
                {
                    _taskRepository.UpdateTaskCompletion((int)taskCheckBox.Tag, taskCheckBox.Checked);
                };

                Button deleteButton = new Button
                {
                    Text = "x",
                    Width = 60,
                    Height = 29,
                    Location = new Point(taskPanel.Width - 70, 2)
                };

                deleteButton.Click += (s, e) =>
                {
                    _taskRepository.DeleteTask((int)taskCheckBox.Tag);
                    LoadTasksFromDatabase();
                };

                taskPanel.Controls.Add(taskCheckBox);
                taskPanel.Controls.Add(deleteButton);
                fPanel.Controls.Add(taskPanel);
            }
        }
    }

    public class TaskRepository
    {
        private readonly string _connectionString = "Data Source=tasks.db";

        public TaskRepository()
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            string tableCommand = @"
                CREATE TABLE IF NOT EXISTS Tasks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    IsCompleted INTEGER
                )";

            var command = connection.CreateCommand();
            command.CommandText = tableCommand;
            command.ExecuteNonQuery();
        }

        public void AddTask(string name)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO Tasks (Name, IsCompleted) VALUES (@name, 0)";
            command.Parameters.AddWithValue("@name", name);
            command.ExecuteNonQuery();
        }

        public List<(int Id, string Name, bool IsCompleted)> GetTasks()
        {
            var tasks = new List<(int, string, bool)>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT Id, Name, IsCompleted FROM Tasks";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tasks.Add((
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetBoolean(2)
                ));
            }

            return tasks;
        }

        public void UpdateTaskCompletion(int id, bool isCompleted)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "UPDATE Tasks SET IsCompleted = @isCompleted WHERE Id = @id";
            command.Parameters.AddWithValue("@isCompleted", isCompleted ? 1 : 0);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public void DeleteTask(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM Tasks WHERE Id = @id";
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
