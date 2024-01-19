using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Contact
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillDataGridView();
        }
        string connectionString = ("Server=localhost;Port=5432;Database=db;Username=postgres;Password=0000;");
        private void FillDataGridView()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM contact";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        adapter.Fill(table);
                        dataGridView1.DataSource = table;
                    }
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string name_contact = textBox2.Text;
            string phone = textBox3.Text;
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name_contact) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO contact (id_contact, name_contact, phone) VALUES (@id, @name_contact, @phone)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@name_contact", name_contact);
                        if (long.TryParse(phone, out long courseValue))
                        {
                            cmd.Parameters.AddWithValue("@phone", courseValue);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка преобразования значения 'phone' в число.");
                            return;
                        }
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Данные успешно добавлены в таблицу.");
                    }
                }
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении данных: {ex.Message}");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string id = textBox6.Text;
            string name_contact = textBox5.Text;
            string phone = textBox4.Text;
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name_contact) || string.IsNullOrEmpty(phone))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = "UPDATE contact SET name_contact = @name, phone = @phone WHERE id_contact = @id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                        cmd.Parameters.AddWithValue("@name", name_contact);

                        if (long.TryParse(phone, out long courseValue))
                        {
                            cmd.Parameters.AddWithValue("@phone", courseValue);
                        }
                        else
                        {
                            MessageBox.Show("Ошибка преобразования значения 'phone' в число.");
                            return;
                        }
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Данные успешно обновлены в таблице.");
                    }
                }

                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении данных: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string idToDelete = textBox9.Text;

            if (string.IsNullOrEmpty(idToDelete))
            {
                MessageBox.Show("Пожалуйста, введите ID для удаления записи.");
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string deleteQuery = "DELETE FROM contact WHERE id_contact = @id";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(deleteQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", Convert.ToInt32(idToDelete));
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Запись успешно удалена из таблицы.");
                    }
                }

                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении записи: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string nameToSearch = textBox10.Text;

            if (string.IsNullOrEmpty(nameToSearch))
            {
                MessageBox.Show("Пожалуйста, введите имя для поиска контакта.");
                return;
            }

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string searchQuery = "SELECT * FROM contact WHERE name_contact LIKE @name";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(searchQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", "%" + nameToSearch + "%");

                        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            if (dataTable.Rows.Count > 0)
                            {
                                dataGridView2.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("Контакт с указанным именем не найден.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при поиске контакта: {ex.Message}");
            }
        }
    }
}
