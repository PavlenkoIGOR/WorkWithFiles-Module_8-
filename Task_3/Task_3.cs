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
            string myPath = "D:\\для SF\\exercise";
            DirectoryInfo dirMain = new DirectoryInfo(myPath);
            try
            {
                Console.WriteLine($"Общий размер дириктории до удаления: {SizeOfFilesAndFolders(dirMain)} байт");
                int a = SeekForDestroy(dirMain);
                Console.WriteLine("Файлов на удаление: {0} ", a);
                if (a == 0)
                {
                    Console.WriteLine("Удалять нечего");
                }
                else
                {
                    Console.WriteLine("Удалить?(y/n): ");
                    char ch = CorrectiveChoice(Console.ReadLine());
                    if (ch == 'y')
                    {
                        SeekAndDestroy(dirMain);
                    }
                    else
                    {
                        Console.WriteLine("штош...");
                    }
                }
                Console.WriteLine($"Общий размер дириктории после удаления: {SizeOfFilesAndFolders(dirMain)} байт");
            }
            catch (Exception oops)
            {
                Console.WriteLine(oops.Message);
            }

            Console.ReadKey();
        }
        static char CorrectiveChoice(string choice)
        {
            char b;
            bool c = char.TryParse(choice, out char result);
            if (c == true & (result == 'y' | result == 'n'))
            {
                return result;
            }
            else
            {
                do
                {
                    Console.WriteLine("error!");
                    Console.Write("incorrect data! enter again!(y/n): ");
                    choice = Console.ReadLine();
                    c = char.TryParse(choice, out b);
                    if (b == 'y' | b == 'n')
                    {
                        return b;
                    }
                } while ((c == false) | (b != 'y' | b != 'n')); //повторяется, если условие в скобках выполнено
                return b;
            }
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

        public static int SeekForDestroy(DirectoryInfo directory)
        {
            int quantity = 0;
            int quantityDir = 0;

            DirectoryInfo[] dirsInfo = directory.GetDirectories();  // Получим все директории корневого каталога
            FileInfo[] fInfo = directory.GetFiles(); // Получим все файлы корневого каталога
            try
            {
                ///
                ///перебор файлов
                ///

                foreach (FileInfo file in fInfo)
                {
                    DateTime lastWriteTime = file.LastAccessTime; ///проверка времени последнего редактирования папки
                    Console.WriteLine($"Файл: {file}");
                    if (lastWriteTime < DateTime.Now.AddMinutes(-5))
                    {
                        quantity++;
                    }
                    else
                    {
                        Console.WriteLine("файлы активно используются");
                    }
                }
                ///
                ///перебор папок
                ///
                foreach (DirectoryInfo dirs in dirsInfo)
                {
                    DateTime lastWriteTime = dirs.LastAccessTime; ///проверка времени последнего редактирования папки
                    DirectoryInfo[] dirsInf = dirs.GetDirectories(); ///получаем список файлов в целевой папке
                    Console.WriteLine($"Папка: {dirs}");
                    if (lastWriteTime < DateTime.Now.AddMinutes(-5))
                    {
                        quantityDir++;
                    }
                    else
                    {
                        Console.WriteLine("папка активно используются");
                    }

                    quantity += SeekForDestroy(dirs);
                }
                return quantity;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        }
        public static void SeekAndDestroy(DirectoryInfo directory)
        {
            int quantity = 0;
            int quantityDir = 0;

            DirectoryInfo[] dirsInfo = directory.GetDirectories();  // Получим все директории корневого каталога
            FileInfo[] fInfo = directory.GetFiles(); // Получим все файлы корневого каталога
            try
            {
                ///
                ///перебор файлов
                ///

                foreach (FileInfo file in fInfo)
                {
                    DateTime lastWriteTime = file.LastAccessTime; ///проверка времени последнего редактирования папки
                    if (lastWriteTime < DateTime.Now.AddMinutes(-5))
                    {
                        quantity++;
                        file.Delete();
                    }
                }
                ///
                ///перебор папок
                ///
                foreach (DirectoryInfo dirs in dirsInfo)
                {
                    DateTime lastWriteTime = dirs.LastAccessTime; ///проверка времени последнего редактирования папки
                    DirectoryInfo[] dirsInf = dirs.GetDirectories(); ///получаем список файлов в целевой папке
                    if (lastWriteTime < DateTime.Now.AddMinutes(-5))
                    {
                        quantityDir++;
                        dirs.Delete(true);
                    }

                    quantity += SeekForDestroy(dirs);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            //Console.WriteLine($"удалено {quantity} файлов");
        }
    }
}

