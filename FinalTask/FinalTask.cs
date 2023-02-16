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
    class Student
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
    internal class Task4
    {
        static void Main(string[] args)
        {
            string[] paths = { @"d:\", "для SF", "exercise", "Students.dat" };
            string fullFilePath = Path.Combine(paths);
            string studentsFolder = @"d:\для SF\exercise\Students";
            try
            {
                DirectoryInfo dir = new DirectoryInfo(studentsFolder);
                if (!dir.Exists)
                {
                    Console.WriteLine("директория создана");
                    dir.Create();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message); //вывод ошибки о несуществующей директории
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (var fs = new FileStream(fullFilePath, FileMode.Open))
            {
                var students = (Student[])binaryFormatter.Deserialize(fs);
                foreach (var student in students)
                {
                    using (StreamWriter sw = new StreamWriter($"{studentsFolder}\\{student.Group}.txt", false))
                        sw.WriteLine($"Имя: {student.Name}, Дата рождения: {student.DateOfBirth} ({student.Group})");
                }
            } 
        }
    }
}


