using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApplab3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private BindingList<TableRowData> datalist = new();
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = datalist;
            datalist.Add(new TableRowData(1, '+', 10, "Январь", "Убрать дом", 10, true));
            datalist.Add(new TableRowData(5, '-', 5, "Октябрь", "Сделать домашнюю работу", 8.4, false));
            datalist.Add(new TableRowData(2, '+', 6, "Сентябрь", "Купить продукты", 5.3, true));
            datalist.Add(new TableRowData(10, '+', 8, "Июнь", "Прочитать книгу", 6.1, false));
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f2 = new EForm();
            if (f2.ShowDialog(this) == DialogResult.OK)
            {
                datalist.Add(f2.UserData);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var filename = saveFileDialog1.FileName;
                using var file = new FileStream(filename, FileMode.Create);
                using var sw = new StreamWriter(file, Encoding.UTF8);
                var jso = new JsonSerializerOptions();
                jso.WriteIndented = false;
                foreach (var elem in datalist)
                {
                    sw.WriteLine(JsonSerializer.Serialize<TableRowData>(elem, jso));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                var filename = openFileDialog1.FileName;
                using var sr = new StreamReader(filename, Encoding.UTF8);
                datalist.Clear();
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine() ?? "";
                    var obj = JsonSerializer.Deserialize<TableRowData>(line);
                    if (obj is not null)
                    {
                        datalist.Add(obj);
                    }
                }
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.SelectedRows[0].Index;
                var f2 = new EForm(datalist[i]);
                if (MessageBox.Show("Вы уверены, что хотите редактировать данное поле?", "Редактировать?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    f2.ShowDialog(this);
                }
            }
            catch
            {
                MessageBox.Show("Выбери строку для редактирования", "Ошибка!");
            }
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int i = dataGridView1.SelectedRows[0].Index;
                if (MessageBox.Show("Вы уверены, что хотите удалить данное поле?", "Удалить?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                { 
                    dataGridView1.Rows.RemoveAt(i); 
                }
            }
            catch
            {
                MessageBox.Show("Выбери строку для удаления", "Ошибка!");
            }
        }
    }
}
