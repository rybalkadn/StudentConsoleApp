using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentConsoleApp
{
    class Student
    {
        private string _name;
        private ushort _numberBook;
        private byte[] _marks = null;
        private int _countMarks;                       // максимальное кол-во оценок, заданных пользователем
        private int _countMarksReal = 0;               // счетчик для добавления оценок в массив
        private double _averageMark;


        public Student(string name, ushort number)
        {
            _name = name;
            _numberBook = number;
        }


        /// <summary>
        /// Копирования студента
        /// </summary>
        /// <param name="student"></param>
        public Student(Student student)
            : this(student._name, student._numberBook)
        {

            _countMarks = student.CountMarks;
            _countMarksReal = student.CountMarksReal;

            if (student._marks != null)
            {
                _marks = (byte[])student._marks.Clone();
            }
        }


       
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _name = value;
                }
            }
        }

        public ushort NumberBook
        {
            get
            {
                return _numberBook;
            }
            set
            {
                _numberBook = value;
            }
        }

        public int CountMarks
        {
            get
            {
                return _countMarks;
            }
            set
            {
                _countMarks = value;
            }

        }

        public int CountMarksReal
        {
            get
            {
                return _countMarksReal;
            }
            set
            {
                _countMarksReal = value;
            }

        }

        /// <summary>
        /// Средний балл
        /// </summary>
        public double AverageMark
        {
            get
            {


                int sum = 0;

                if (_marks == null)
                {
                    return _averageMark;
                }


                for (int i = 0; i < _countMarksReal; i++)
                {
                    sum += _marks[i];
                }
                _averageMark = (double)sum / _countMarksReal;

                return _averageMark;


            }

        }

    
        /// <summary>
        /// Поиск
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public byte this[int index]
        {
            get
            {
                if (index >= 0 && index < _countMarks)
                {
                    return _marks[index];
                }
                return 0;
            }
            set
            {
                if (index >= 0 && index < _countMarks)
                {
                    _marks[index] = value;
                }

            }
        }

       

        /// <summary>
        /// добавление отметки
        /// </summary>
        /// <param name="mark"></param>
        public void AddMark(byte mark)
        {
            // если еще нет массива то создаем
            if (_marks == null)
            {
                _marks = new byte[_countMarks];
                _marks[_countMarksReal] = mark;
                _countMarksReal++;
            }
            else
            {
                if (_countMarksReal < _marks.Length)
                {
                    _marks[_countMarksReal] = mark;
                    _countMarksReal++;
                }

            }
        }

    }
}
