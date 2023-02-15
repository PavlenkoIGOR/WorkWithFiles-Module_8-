

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name
        {
            get; set;
        }
        public string Group
        {
            get; set;
        }
        public DateTime DateOfBirth
        {
            get; set;
        }
        public Student(string name, string group, DateTime dateOfBirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
        }
    }
    class Program
    {
        public static void Main()
        {
            FolderCreator();
            FileCreator();
            Console.ReadKey();
        }
        public static void FileCreator()
        {
            try
            {
                Student[] students =
                {
                    new Student("Евгений", "Группа1", new DateTime(2009, 5, 29)),
                    new Student("Паша", "Группа1", new DateTime(2007, 5, 29)),
                    new Student("Ваня", "Группа2", new DateTime(2008, 5, 29)),
                    new Student("Гриша", "Группа1", new DateTime(2007, 5, 29)),
                    new Student("Света", "Группа3", new DateTime(2005, 5, 29)),
                    new Student("Вова", "Группа2", new DateTime(2007, 5, 30)),
                    new Student("Вася", "Группа3", new DateTime(2007, 5, 30)),
                    new Student("Кирилл", "Группа2", new DateTime(2007, 5, 30)),
                    new Student("Жора", "Группа1", new DateTime(2007, 5, 30)),
                };
                BinaryFormatter formatter = new BinaryFormatter();
                // сериализация
                using (FileStream fs = new FileStream("D:\\для SF\\exercise\\Students.dat", FileMode.OpenOrCreate))
                {
                    foreach (Student student in students)
                    {
                        formatter.Serialize(fs, students); //данные из Students.dat сериализованы
                    }
                }

                // десериализация
                using (FileStream fs = new FileStream("D:\\для SF\\exercise\\Students.dat", FileMode.Open))
                {
                    foreach (Student student in students)
                    {
                        formatter.Deserialize(fs);
                    }
                    using (StreamWriter sw1 = new StreamWriter("D:\\для SF\\exercise\\new\\Group1.txt"))
                        foreach (Student student in students)
                        {
                            if (student.Group == "Группа1")
                                sw1.WriteLine($"Имя: {student.Name} \n\tДата рождения: {student.DateOfBirth} ");
                        }

                    using (StreamWriter sw2 = new StreamWriter("D:\\для SF\\exercise\\new\\Group2.txt"))
                        foreach (Student student in students)
                        {
                            if (student.Group == "Группа2")
                                sw2.WriteLine($"Имя: {student.Name} \n\tДата рождения: {student.DateOfBirth} ");
                        }
                    using (StreamWriter sw3 = new StreamWriter("D:\\для SF\\exercise\\new\\Group3.txt"))
                        foreach (Student student in students)
                        {
                            if (student.Group == "Группа3")
                                sw3.WriteLine($"Имя: {student.Name} \n\tДата рождения: {student.DateOfBirth} ");
                        }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        public static void FolderCreator()
        {
            try
            {
                DirectoryInfo newDirectory = new DirectoryInfo("D:\\для SF\\exercise\\new");
                if (!newDirectory.Exists)
                    newDirectory.Create();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

}

