using System;
using System.Text;
using System.IO;
using static System.Console;
using System.Text.RegularExpressions;

namespace Streams
{
    internal class Program
    {
        static void Main(string[] args)
        {
            short key;
            l1:
            WriteLine("Выберите действие 1 - записать, 2 - считать");
            try
            {
                key = short.Parse(Console.ReadLine());
            }
            catch (FormatException ex)
            {
                WriteLine($"{ex.Message}");
                goto l1;
            }

            switch (key)
            {
                case 1:
                    {
                        //var str = @"152155 Московская обл, г Пушкино, ул Колотушкина, д 99, кв 7";
                        string adress_pattern = @"(?<индекс>\d{6})\s(?<область>.*?),\s(?<город>г\s.*?),\s(?<улица>.+?),\s(?<дом>д\s.+?),\s(?<квартира>кв\s.+)";
                        WriteLine("Введите адрес");
                        string adress = ReadLine();
                        Regex r_adress = new Regex(adress_pattern);
                        if (r_adress.IsMatch(adress))
                        {
                            WriteLine("Введите путь к фалу");
                            string fPath = ReadLine();
                            using (FileStream fs = new FileStream(fPath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                            {
                                byte[] str_byte = Encoding.UTF8.GetBytes(adress);
                                fs.Write(str_byte, 0, str_byte.Length);
                                WriteLine("Ok");
                            }
                        }
                        else
                        {
                            WriteLine("Adress is not correct");
                            goto l1;
                        }

                    }
                    break;
                case 2:
                    {   //111.doc
                        WriteLine("Введите путь к фалу");
                        string fPath = ReadLine();
                        using (FileStream fs = new FileStream(fPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            byte[] str_byte = new byte[(int)fs.Length];
                            fs.Read(str_byte, 0, str_byte.Length);
                            string str_new = Encoding.UTF8.GetString(str_byte);
                            WriteLine(str_new);
                        }
                    }
                    break;
                default:
                    {
                        WriteLine("Error number");
                        goto l1;
                    }
            }
        }
    }
}
