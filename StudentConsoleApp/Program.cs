using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Group groupsource1 = Group.CreateGroup();

            UI.PrintGroup(groupsource1);

            string name = UI.EnterNameForSearch();
            UI.RezultSearchByName(groupsource1[name]);

            Student student;
            student = groupsource1.SearchByNumberBookStudent();
            UI.PrintStudent(student);

            UI.EditByNumberBookStudent(groupsource1);

            // копирование через конструктор
            Group groupsource2 = new Group(groupsource1);
            groupsource2.Name = "Copy group";

            UI.DelStudent(groupsource2);

            Console.ReadKey();
            Console.Clear();

            UI.PrintGroup(groupsource2);

            Console.ReadKey();
        }
    }
}
