using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW1_Module_8
{
    internal class HW1_8
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Введите адрес папки: ");
            string myPath = /*Console.ReadLine();*/"D:\\для SF\\exercise";
            SeekAndDestroy(myPath);

            Console.ReadKey();

        }
        public static void SeekAndDestroy(string myPath)
        {
            int i = 0;
            int j = 0;
            if (Directory.Exists(myPath))
            {
                try
                {
                    ///
                    ///перебор папок
                    ///
                    Console.WriteLine(myPath);
                    string[] dirFolders = Directory.GetDirectories(myPath); //получаем список папок в целевой папке

                    foreach (string folderInDirFolders in dirFolders)
                    {
                        DateTime lastWriteTime = File.GetLastWriteTime(folderInDirFolders); ///проверка времени последнего редактирования папки

                        string[] dirFiles = Directory.GetFiles(folderInDirFolders); ///получаем список файлов в целевой папке
                        Console.WriteLine($"{folderInDirFolders}");
                        if (lastWriteTime < DateTime.Now.AddMinutes(-5))
                        {
                            i++;
                        }
                    }
                    ///
                    ///перебор файлов
                    ///
                    string[] dirFilesMain = Directory.GetFiles(myPath); //получаем список папок в целевой папке
                    foreach (string filesMain in dirFilesMain)
                    {
                        DateTime lastWriteTime = File.GetLastWriteTime(filesMain); ///проверка времени последнего редактирования папки
                        Console.WriteLine($"{filesMain}");
                        if (lastWriteTime < DateTime.Now.AddMinutes(-5))
                        {
                            j++;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            string[] filesMainForDelete = new string[j]; ///создаём массив для хранения файлов в корне на удаление в корневом каталоге
            string[] foldersMainForDelete = new string[i]; ///создаём массив для хранения папок в корне на удаление
            int d = 0;
            int n = 0;
            ///
            ///снова
            ///перебор папок
            ///
            string[] dirFolders2 = Directory.GetDirectories(myPath); //получаем список папок в целевой папке
            foreach (string folderInDirFolders in dirFolders2)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(folderInDirFolders); ///проверка времени последнего редактирования папки

                string[] dirFiles = Directory.GetFiles(folderInDirFolders); ///получаем список файлов в целевой папке
                if (lastWriteTime < DateTime.Now.AddMinutes(-5))
                {
                    foldersMainForDelete[d] = folderInDirFolders;
                    d++;
                }
                else
                {
                    Console.WriteLine("папки активно используются");
                    d = 0;
                }
                ///
                ///перебор файлов
                ///
            }
            string[] dirFilesMain2 = Directory.GetFiles(myPath); //получаем список папок в целевой папке
            foreach (string filesMain in dirFilesMain2)
            {
                DateTime lastWriteTime = File.GetLastWriteTime(filesMain); ///проверка времени последнего редактирования папки
                if (lastWriteTime < DateTime.Now.AddMinutes(-5))
                {
                    filesMainForDelete[n] = filesMain;
                    n++;
                }
                else
                {
                    Console.WriteLine("файлы активно используются");
                }
            }
            Console.WriteLine("--------------для удаления---------------");
            Console.WriteLine("Папки для удаления(которые не использовадись более 30 мин): ");
            if (filesMainForDelete.Length > 0)
            {
                for (int w = 0; w < foldersMainForDelete.Length; w++)
                {
                    Console.WriteLine($"{foldersMainForDelete[w]}");
                }
            }
            else
            {
                Console.WriteLine(0);
            }
            Console.WriteLine("Файлы для удаления(которые не использовадись более 30 мин): ");
            if (foldersMainForDelete.Length > 0)
            {
                for (int q = 0; q < filesMainForDelete.Length; q++)
                {
                    Console.WriteLine($"{filesMainForDelete[q]}");
                }
            }
            else
            {
                Console.WriteLine(0);
            }
            Console.WriteLine("Удалить файлы/папки(y/n): ");
            char ch = CorrectiveChoice(Console.ReadLine());
            if (ch == 'y' & filesMainForDelete.Length > 0 & foldersMainForDelete.Length > 0)
            {
                foreach (var itemF in foldersMainForDelete)
                {

                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(itemF);
                        dirInfo.Delete(true); // Удаление со всем содержимым
                        Console.WriteLine($"Каталог ''{itemF}''  удален");
                    }
                    catch (Exception oops)
                    {
                        Console.WriteLine(oops.Message);
                    }

                }

                foreach (var item in filesMainForDelete)
                {
                    try
                    {
                        var dirInfo = new FileInfo(item);
                        File.Delete(item);
                        //dirInfo.Delete(true); // Удаление со всем содержимым
                        Console.WriteLine($"Файл ''{item}'' удален");
                    }
                    catch (Exception oops)
                    {
                        Console.WriteLine(oops.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("штош...");
            }
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
    }
}
