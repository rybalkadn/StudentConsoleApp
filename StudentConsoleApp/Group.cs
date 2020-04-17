using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentConsoleApp
{
    class Group
    {
        private string _name;
        private string _specialty;
        private Student[] _students = null;
        private int _countStudents;                    // кол-во студентов, заданных пользователем
        private int _countStudentsReal = 0;            // счетчик для добавления студентов

        // номера зачеток
        private ushort _startBookNum;
        private ushort _nextBookNum;


        static Random rand = new Random();

        public Group()
        {
            _startBookNum = (ushort)rand.Next(100000, 999999);
            _nextBookNum = _startBookNum;
        }


        public Group(string name, string specialty, Student[] students)
        {
            _name = name;
            _specialty = specialty;
            _students = students;
        }


        /// <summary>
        /// Копирования Группы
        /// </summary>
        /// <param name="group"></param>
        public Group(Group group)
        {
            _name = group.Name;
            _specialty = group.Specialty;
            _countStudents = group.CountStudents;
            _countStudentsReal = group.CountStudentsReal;

            if (group._students != null)
            {
                _students = new Student[_countStudents];

                for (int i = 0; i < _countStudentsReal; i++)
                {
                   
                    _students[i] = new Student(group._students[i]);
                }
            }
        }


       

        public int CountStudents
        {
            get
            {
                return _countStudents;
            }
            private set
            {
                _countStudents = value;
            }

        }

        public int CountStudentsReal
        {
            get
            {
                return _countStudentsReal;
            }


        }

        public ushort NextBookNum
        {
            get
            {
                return _nextBookNum;
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

        public string Specialty
        {
            get
            {
                return _specialty;
            }
            set
            {
                if (!String.IsNullOrEmpty(value))
                {
                    _specialty = value;
                }

            }
        }



        public Student this[int index]
        {
            get
            {
                if (IsValidIndex(index))
                {
                    return _students[index];
                }
                else
                {
                    return null;
                }

            }
            set
            {
                if (IsValidIndex(index))
                {
                    _students[index] = value;
                }

            }
        }

        private bool IsValidIndex(int index)
        {
            return index >= 0 && index < _countStudentsReal;
        }

        public Student this[ushort numberBook]
        {
            get
            {
                Student retStud = null;

                for (int i = 0; i < _countStudentsReal; i++)
                {
                    if ((_students[i].NumberBook == numberBook))
                    {
                        retStud = new Student(_students[i]);  
                        break;
                    }
                }

                return retStud;
            }
            set
            {
                for (int i = 0; i < _countStudentsReal; i++)
                {
                    if ((_students[i].NumberBook == numberBook))
                    {
                        _students[i] = new Student(value); 
                        break;
                    }

                }

            }
        }

        public bool this[string name]
        {
            get
            {
                name = name.ToLower();
                bool found = false;

                for (int i = 0; i < _countStudentsReal; i++)
                {
                    if ((_students[i].Name.ToLower() == name))
                    {
                        found = true;
                    }
                }

                return found;
            }

        }

    

        /// <summary>
        /// создаем группу
        /// </summary>
        /// <returns></returns>
        public static Group CreateGroup()
        {
            Group group = new Group();
            UI.GetNameGroup(group);

            group.CountStudents = UI.GetCountStudents();

            EnterStudents(group);
            UI.EnterMarksForStudents(group);

            return group;
        }

        /// <summary>
        /// ввод студентов в группу
        /// </summary>
        /// <param name="group"></param>
        public static void EnterStudents(Group group)
        {
            string name;
            ConsoleKey use;

            Console.WriteLine(Environment.NewLine + "[Add Students]" + Environment.NewLine);
            int countMarks = UI.GetCountMarks();
            do
            {
                Console.WriteLine("Student with book number {0}", group.NextBookNum);

                Console.Write("Name: ");
                name = Console.ReadLine();

                Student stud = new Student(name, group.NextBookNum);
                stud.CountMarks = countMarks;
                group.AddStudent(stud);
                group._nextBookNum++;

                Console.WriteLine(Environment.NewLine + "Anykey - next student. Esc - stop." + Environment.NewLine);
                use = Console.ReadKey(true).Key;


            } while (use != ConsoleKey.Escape && group.CountStudentsReal < group.CountStudents);
        }

        /// <summary>
        /// добавление студента
        /// </summary>
        /// <param name="newStudent"></param>
        public void AddStudent(Student newStudent)
        {

            // если еще нет массива студентов то создаем
            if (_students == null)
            {
                _students = new Student[_countStudents];
                _students[_countStudentsReal] = newStudent;
                _countStudentsReal++;


            }
            else
            {
                if (_countStudentsReal < _students.Length)
                {
                    _students[_countStudentsReal] = newStudent;
                    _countStudentsReal++;


                }

            }

        }

        /// <summary>
        /// Средней оценки по группе
        /// </summary>
        /// <returns></returns>
        public double AverageMark()
        {
            double x = 0;
            double sum = 0;
            double rezult = 0;


            for (int i = 0; i < _countStudentsReal; i++)
            {
                x = _students[i].AverageMark;
                sum += x;
            }
            rezult = sum / _countStudentsReal;

            return rezult;
        }

        /// <summary>
        /// проверка есть ли студенты с указанным именем
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public bool SearchByNameStudent(string name)
        {
            //string name;
            //bool nameSt = false;
            //name = UI.EnterNameForSearch();

            bool nameSt = this[name];

            return nameSt;


        }

        /// <summary>
        /// поиск студента по номеру зачетки
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public Student SearchByNumberBookStudent()
        {

            string number;
            ushort numberBook = 0;
            Student st;

            number = UI.EnterNumberBookForSearch();
            ushort.TryParse(number, out numberBook);
            st = this[numberBook];

            return st;

        }


        /// <summary>
        /// удаление студента по номеру зачетки
        /// </summary>
        /// <returns></returns>
        public bool DeleteStudentByNumberBook()
        {
            Student st = SearchByNumberBookStudent();
            bool delete = false;
            Student[] students = new Student[CountStudents];

            if (st != null)
            {
                int j = 0;
                for (int i = 0; i < CountStudentsReal; i++)
                {

                    if (_students[i].NumberBook != st.NumberBook)
                    {
                        students[j] = new Student(_students[i]);
                        j++;
                    }

                }
                _students = students;
                _countStudentsReal--;
                delete = true;
            }

            return delete;

        }


    }
}
