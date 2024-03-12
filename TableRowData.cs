using Hangfire.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApplab3
{
    public class TableRowData: INotifyPropertyChanged
    {
        private int _index;
        private int _dayofmonth;
        private string _month;
        private string _task;
        private bool _isdone;
        private char _type;
        private double _hard;
        [DisplayName("Номер задачи")]
        public int myIndex
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }
        [DisplayName("Тип задачи")]
        public char Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChanged();
            }
        }
        [DisplayName("День месяца")]
        public int DayOfMonth
        {
            get => _dayofmonth;
            set
            {
                _dayofmonth = value;
                OnPropertyChanged();
            }
        }
        [DisplayName("Месяц")]
        public string Month
        {
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged();
            }
        }
        [DisplayName("Задача")]
        public string Task
        {
            get => _task;
            set
            {
                _task = value;
                OnPropertyChanged();
            }
        }
        [DisplayName("Сложность задачи")]
        public double Hard
        {
            get => _hard;
            set
            {
                _hard = value;
                OnPropertyChanged();
            }
        }
        [DisplayName("Задача выполнена?")]
        public bool isDone
        {
            get => _isdone;
            set
            {
                _isdone = value;
                OnPropertyChanged();
            }
        }
        public TableRowData()
        {
            /*myIndex = 1;
            Type = '+';
            DayOfMonth = 1;
            Month = "Январь";
            Task = "Задача";
            Hard = 1;
            isDone = false;*/
        }
        public TableRowData(int index, char type, int dayofmonth, string month, string task, double hard, bool isdone)
        {
            myIndex = index;
            Type = type;
            DayOfMonth = dayofmonth;
            Month = month;
            Task = task;
            Hard = hard;
            isDone = isdone;
        }
        // Метод для копирования объекта
        public TableRowData Copy() => new TableRowData(myIndex, Type, DayOfMonth, Month, Task, Hard, isDone);
        // Метод копирования данного объекта в другой, указанный в параметре
        // (можно использовать для восстановления данных из резервной копии
        // при отказе пользователя от сохранения сделанных изменений)
        public void CopyTo(TableRowData? trd)
        {
            if (trd is not null)
            {
                trd.myIndex = myIndex;
                trd.Type = Type;
                trd.DayOfMonth = DayOfMonth;
                trd.Month = Month;
                trd.Task = Task;
                trd.Hard = Hard;
                trd.isDone = isDone;
            }
        }

        // Для автоматического обновления таблицы с данными


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
