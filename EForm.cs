using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApplab3
{
    public partial class EForm : Form
    {
        // Свойство для хранения информации об объекте
        public TableRowData UserData { get; }

        // Резервная копия данных объекта, полученная из основной формы
        private readonly TableRowData _userBackupData;

        // Информация об исключении при редактировании данных (при наличии ошибок)
        private BindingException? _bindingException;
        public EForm(TableRowData? ud = null)
        {
            InitializeComponent();
            // Результат работы диалога по умолчанию (необходимо для закрытия окна по [x])
            DialogResult = DialogResult.Cancel;

            // Если переданые данные из главной формы...
            if (ud is not null)
                UserData = ud; // сохраняем их в свойстве с данными
            else UserData = new TableRowData(); // ... иначе - создаем новое свойство с данными

            // Делаем резервную копию исходно переданных данных на случай отмены радактирования
            _userBackupData = UserData.Copy();

            // Производим связывание данных между графическими элементами и свойством с хранимой информацией об объекте
            numericUpDown1.DataBindings.Add("Value", UserData, "myIndex");
            numericUpDown2.DataBindings.Add("Value", UserData, "DayOfMonth");
            var v2Binding = textBox2.DataBindings.Add("Text", UserData, "Task");
            // Включаем поддержку форматирования ввода
            // (обеспечивает контроль ошибок при вводе данных)
            v2Binding.FormattingEnabled = true; // !!!!
                                                // Назначаем метод, который будет вызываться для анализа
                                                // введенных в проверяемое поле данных
            v2Binding.BindingComplete += V2BindingComplete;
            checkBox1.DataBindings.Add("Checked", UserData, "isDone");
            textBox1.DataBindings.Add("Text", UserData, "Month");
            textBox3.DataBindings.Add("Text", UserData, "Hard");
            textBox4.DataBindings.Add("Text", UserData,"Type");
        }
        private void V2BindingComplete(object? sender, BindingCompleteEventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Не указан месяц!!");
            }
            else
            {
                errorProvider1.Clear();
            }
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider2.SetError(textBox2, "Не указана задача!");
            }
            else
            {
                errorProvider2.Clear();
            }
            if (String.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider3.SetError(textBox4, "Не указана срочность выполнения!");
            }
            else
            {
                errorProvider3.Clear();
            }
            // Вызывается при изменении данных
            if (e.BindingCompleteState != BindingCompleteState.Success)
            {
                // Если изменения прошли с ошибкой (сработало исключение в TableRowData)
                textBox1.BackColor = Color.OrangeRed;
                textBox1.Focus();
                MessageBox.Show("Вы ввели неверные данные!");
            }

            // Сохраняем информацию о произошедшем исключении в поле класса
            _bindingException = e.Exception as BindingException;
        }

        private void EForm_Load(object sender, EventArgs e)
        {

        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Кнопка "Сохранить"

            //Если было исключение при заполнении значений
            if (_bindingException is not null)
            {
                // Выводим сообщение об ошибке
                MessageBox.Show(
                    _bindingException.Message,
                    _bindingException.ErrorField,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                // и не даем окну закрыться. 
                return;
            }

            // Сюда попадем только если ошибок нет и данные можно сохранять
            // Установим результат работы с диалоговым окном
            DialogResult = DialogResult.OK;
            if (MessageBox.Show("Вы уверены, что хотите сохранить данное поле?", "Сохранить?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                // И закроем окно.
                Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Кнопка "Отменить".

            // Восстанавливаем данные об объекте из резервной копии, чтобы
            // отменить возможные изменения, которые успел сделать пользователь
            _userBackupData.CopyTo(UserData);
            if (MessageBox.Show("Вы уверены, что хотите отменить данные действия?", "Отменить?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {   
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {

            char number = e.KeyChar;
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) //цифры, клавиша BackSpace и запятая а ASCII
            {
                e.Handled = true;
            }
        }
    }
}
