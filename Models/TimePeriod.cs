using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timetracker.Models
{
    /// <summary>
    /// Отрезок времени
    /// </summary>
    public class TimePeriod
    {
        /// <summary>
        /// Уникальный идентификатор сотрудника
        /// </summary>
        private readonly int _employeeId;

        /// <summary>
        /// Табельный номер сотрудника
        /// </summary>
        private readonly string _personnelNumber;

        /// <summary>
        /// Имя сотрудника
        /// </summary>
        private readonly string _name;

        /// <summary>
        /// Фамилия сотрудника
        /// </summary>
        private readonly string _surname;

        /// <summary>
        /// Отчество сотрудника
        /// </summary>
        private readonly string _patronymic;

        /// <summary>
        /// Дата и время начала временного отрезка
        /// </summary>
        private readonly DateTime _dateTimeStart;

        /// <summary>
        /// Дата и время конца временного отрезка
        /// </summary>
        private readonly DateTime _dateTimeEnd;

        /// <summary>
        /// Конструктор
        /// </summary>
        public TimePeriod (int employeeId, string personnelNumber, string name, string surname, string patronymic, DateTime dateTimeStart, DateTime dateTimeEnd)
        {
            _employeeId = employeeId;
            _personnelNumber = personnelNumber;
            _name = name;
            _surname = surname;
            _patronymic = patronymic;
            _dateTimeStart = dateTimeStart;
            _dateTimeEnd = dateTimeEnd;
        }
    }
}