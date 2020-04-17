using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentConsoleApp
{
    class UI
    {

        /// <summary>
        /// Максимальное количество студентов у пользователя
        /// </summary>
        /// <returns></returns>
        public static int GetCountStudents()
        {
            int countStudents;
            string countSt;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Enter maximum count of students: ");
                countSt = Console.ReadLine();
            } while (int.TryParse(countSt, out countStudents) == false);
            return countStudents;
        }

        /// <summary>
        /// Максимальное количество оценок для одного студента у пользователя
        /// </summary>
        /// <returns></returns>
        public static int GetCountMarks()
        {
            int countMarks;
            string countMar;
            do
            {
                Console.WriteLine("Enter maximum marks for one student: ");
                countMar = Console.ReadLine();
            } while (int.TryParse(countMar, out countMarks) == false);
            return countMarks;
        }

        public static void GetNameGroup(Group groupStudents)
        {
            Console.WriteLine(Environment.NewLine + "[Group create]" + Environment.NewLine);

            Console.Write("Name: ");
            groupStudents.Name = Console.ReadLine();

            Console.Write("Spec: ");
            groupStudents.Specialty = Console.ReadLine();
        }



        /// <summary>
        /// добавление оценок студентам групы
        /// </summary>
        /// <param name="groupStudents"></param>
        public static void EnterMarksForStudents(Group groupStudents)
        {
            ConsoleKey use;
            byte mark = 0;

            Console.WriteLine(Environment.NewLine + "[Enter Students Assess]" + Environment.NewLine);


            for (int i = 0; i < groupStudents.CountStudentsReal; i++)
            {
                Console.Write("Assess for student {0} with book number {1} : ", groupStudents[i].Name, groupStudents[i].NumberBook);

                do
                {
                    use = Console.ReadKey().Key;

                    mark = Key2Number(use);

                    // проверяем что это число входит в диапазон от одного до пяти 
                    if (mark > 0 && mark <= 5)
                    {
                        groupStudents[i].AddMark(mark);
                        Console.Write(" ");
                    }
                    else
                    {
                        break;
                    }

                } while (mark != 0 && groupStudents[i].CountMarksReal < groupStudents[i].CountMarks);


                Console.WriteLine();
            }


        }

        /// <summary>
        /// по номеру клавиши получаем число
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte Key2Number(ConsoleKey key)
        {
            byte num = 0;
            switch (key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    num = 1;
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    num = 2;
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    num = 3;
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    num = 4;
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    num = 5;
                    break;
                case ConsoleKey.NumPad6:
                case ConsoleKey.D6:
                    num = 6;
                    break;
                case ConsoleKey.NumPad7:
                case ConsoleKey.D7:
                    num = 7;
                    break;
                case ConsoleKey.NumPad8:
                case ConsoleKey.D8:
                    num = 8;
                    break;
                case ConsoleKey.NumPad9:
                case ConsoleKey.D9:
                    num = 9;
                    break;
                default:
                    break;
            }

            return num;
        }

        /// <summary>
        /// Данных о студенте
        /// </summary>
        /// <param name="student"></param>
        public static void PrintStudent(Student student)
        {

            if (student != null)
            {
                Console.Write(Environment.NewLine + "Name: {0,25}, number: {1,10}, average score: {2}, assessments: ", student.Name, student.NumberBook, (float)student.AverageMark);

                if (student.CountMarksReal == 0)
                {
                    Console.Write("  no assessments!");
                    return;
                }

                for (int i = 0; i < student.CountMarksReal; i++)
                {
                    Console.Write("{0,3} ", student[i]);
                }

            }
            else
            {
                Console.WriteLine("No students");
            }


        }

        /// <summary>
        /// печать группы со студентами 
        /// </summary>
        /// <param name="groupStudents"></param>
        public static void PrintGroup(Group groupStudents)
        {
            Console.WriteLine(Environment.NewLine + "Group name: {0,10}, specialty: {1,15}, students: ", groupStudents.Name, groupStudents.Specialty);
            for (int i = 0; i < groupStudents.CountStudentsReal; i++)
            {
                PrintStudent(groupStudents[i]);
            }

            PrintAverGroup(groupStudents);
        }

        public static void PrintAverGroup(Group groupStudents)
        {
            Console.WriteLine();
            Console.Write(Environment.NewLine + "Average score for the group: {0} - {1}", groupStudents.Name, (float)groupStudents.AverageMark());
        }

        /// <summary>
        /// Поиск Студента по имент
        /// </summary>
        /// <returns></returns>
        public static string EnterNameForSearch()
        {
            string name;
            Console.WriteLine();
            Console.Write(Environment.NewLine + "Is there a student with that name?  ");
            name = Console.ReadLine();
            return name;
        }

        /// <summary>
        /// результат поиска по имени
        /// </summary>
        /// <param name="nameStudent"></param>
        public static void RezultSearchByName(bool nameStudent)
        {
            if (nameStudent)
            {
                Console.WriteLine("Yes");
            }
            else
            {
                Console.WriteLine("No");
            }

        }

        public static string EnterNumberBookForSearch()
        {
            string number;
            Console.WriteLine();
            Console.Write(Environment.NewLine + "Search by number book of student  ");
            number = Console.ReadLine();
            return number;
        }

        /// <summary>
        /// редактирование студента по номеру зачетки
        /// </summary>
        /// <param name="groupStudents"></param>
        public static void EditByNumberBookStudent(Group groupStudents)
        {
            Console.WriteLine();
            Console.WriteLine(Environment.NewLine + "[Edit student]");
            Student student = groupStudents.SearchByNumberBookStudent();
            if (student == null)
            {
                Console.WriteLine("No students");
                return;
            }

            string answer;
            byte mark;
            ConsoleKey use;


            Console.WriteLine();
            Console.Write(Environment.NewLine + "What needs to be edited?  ");

            answer = Console.ReadLine().ToLower();

            if (answer == "name")
            {
                Console.WriteLine();
                Console.Write(Environment.NewLine + "Enter name:  ");
                student.Name = Console.ReadLine();

            }
            else
            {
                Console.WriteLine();
                Console.Write(Environment.NewLine + "Enter marks for adding:  ");
                do
                {
                    use = Console.ReadKey().Key;

                    mark = Key2Number(use);

                    if (mark > 0 && mark <= 5)
                    {
                        student.AddMark(mark);
                        Console.Write(" ");
                    }
                    else
                    {
                        break;
                    }

                } while (mark != 0 && student.CountMarksReal < student.CountMarks);
            }

            PrintStudent(student);

        }

        /// <summary>
        /// Удаление студента
        /// </summary>
        /// <param name="groupStudents"></param>
        public static void DelStudent(Group groupStudents)
        {
            Console.WriteLine();
            Console.WriteLine(Environment.NewLine + "[Delete student]");
            if (groupStudents.DeleteStudentByNumberBook())
            {
                Console.WriteLine(Environment.NewLine + "Done delete");
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "No students");
            }


        }

    }
}
