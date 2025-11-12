using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;
using TodoListApp;

namespace To_Do_List
{
    public partial class Form1 : Form
    {
        private List<TodoItem> tasks = new List<TodoItem>();
        private const string FILE_NAME = "tasks.json";

        public Form1()
        {
            InitializeComponent();

            // Закачане на събитията, ако Designer-ът не ги е добавил:
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnDelete.Click += btnDelete_Click;
            btnSave.Click += btnSave_Click;
            btnLoad.Click += btnLoad_Click;

            // (по желание) двойно кликване по списъка за отметка "изпълнена"
            lstTasks.DoubleClick += lstTasks_DoubleClick;

        }

        private void RefreshList()
        {
            lstTasks.Items.Clear();
            foreach (var task in tasks)
                lstTasks.Items.Add(task);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string text = txtTask.Text.Trim();
            if (string.IsNullOrEmpty(text))
            {
                MessageBox.Show("Please enter a task description.", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            tasks.Add(new TodoItem { Description = text, IsCompleted = false });
            txtTask.Clear();
            RefreshList();
        }

        private void lstTasks_DoubleClick(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex >= 0)
            {
                tasks[lstTasks.SelectedIndex].IsCompleted =
                    !tasks[lstTasks.SelectedIndex].IsCompleted;
                RefreshList();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex < 0)
            {
                MessageBox.Show("Select a task to edit.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string newText = Microsoft.VisualBasic.Interaction.InputBox(
                "Edit task description:", "Edit Task",
                tasks[lstTasks.SelectedIndex].Description);

            if (!string.IsNullOrWhiteSpace(newText))
            {
                tasks[lstTasks.SelectedIndex].Description = newText.Trim();
                RefreshList();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lstTasks.SelectedIndex < 0)
            {
                MessageBox.Show("Select a task to delete.", "Info",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Delete selected task?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tasks.RemoveAt(lstTasks.SelectedIndex);
                RefreshList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                File.WriteAllText(FILE_NAME, JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true }));
                MessageBox.Show("Tasks saved successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving tasks: " + ex.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(FILE_NAME))
                {
                    string json = File.ReadAllText(FILE_NAME);
                    tasks = JsonSerializer.Deserialize<List<TodoItem>>(json);
                    RefreshList();
                    MessageBox.Show("Tasks loaded successfully.", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No saved file found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tasks: " + ex.Message);
            }
        }

        private void add_Click(object sender, EventArgs e)
        {

        }

        private void lstTasks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
