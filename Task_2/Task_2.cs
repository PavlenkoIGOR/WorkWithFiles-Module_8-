using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Task2
    {
        static void Main(string[] args)
        {
            try
            {
                string myPath = "D:\\для SF\\exercise";
                DirectoryInfo dirMain = new DirectoryInfo(myPath);
                Console.WriteLine($"Общий размер дириктории: {SizeOfFilesAndFolders(dirMain)} байт");
            }
            catch (Exception oops)
            {
                Console.WriteLine(oops.Message);
            }
            Console.ReadKey();
        }
        public static double SizeOfFilesAndFolders(DirectoryInfo directory)
        {
            double size = 0;
            DirectoryInfo[] dirsInfo = directory.GetDirectories();  // Получим все директории корневого каталога
            FileInfo[] fInfo = directory.GetFiles(); // Получим все файлы корневого каталога
            try
            {
                ///
                ///перебор файлов
                ///

                foreach (FileInfo file in fInfo)
                {
                    Console.WriteLine("Файл: ");
                    Console.WriteLine($"{file}, размер {file.Length}"); //выводим список файлов в целевой папке
                    size += file.Length;
                }
                ///
                ///перебор папок
                ///


                foreach (DirectoryInfo dirs in dirsInfo)
                {

                    Console.WriteLine("Папка: ");
                    Console.WriteLine($"{dirs}"); //выводим список папок в целевой папке
                    Console.Write("\t");
                    size += SizeOfFilesAndFolders(dirs);
                }
                return size;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
    }
}
