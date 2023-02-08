using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using FigureClass;
using System.IO;
using System.Diagnostics.Tracing;
using System.Security.AccessControl;

namespace Redaktor
{
    public class Class2
    {
        public static Figure Pryamougolnik = new Figure("Прямоугольник", 20, 40);
        public static Figure Kvadratik = new Figure("Квадрат", 30, 30);
        public static void program()
        {
            Console.WriteLine("Введите путь до файла: ");

            string FilePath = Console.ReadLine();
            int CursPos = 4;

            List<Figure> FiguresList = new List<Figure>();
            FiguresList.Add(Pryamougolnik);
            FiguresList.Add(Kvadratik);

            List<Figure> HyPabotaiPLZ;

            if (File.Exists(FilePath))
            {
                if (Path.GetExtension(FilePath) == ".txt")
                {
                    Console.Clear();
                    Console.WriteLine("[ F1 ] - Сохранить файл.");
                    Console.WriteLine("[ F2 ] - Изменить текущий файл.");
                    Console.WriteLine("[ ESCAPE ] - Выйти");
                    Console.WriteLine("Содержимое этого TXT файла: ");
                    Console.WriteLine(File.ReadAllText(FilePath));
                }

                if (Path.GetExtension(FilePath) == ".xml")
                {

                    XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                    using (FileStream fs = new FileStream(FilePath, FileMode.Open))
                    {
                        HyPabotaiPLZ = (List<Figure>)xml.Deserialize(fs);
                        Console.Clear();
                        Console.WriteLine("[ F1 ] - Сохранить файл.");
                        Console.WriteLine("[ F2 ] - Изменить текущий файл.");
                        Console.WriteLine("[ ESCAPE ] - Выйти");
                        Console.WriteLine("Содержимое этого XML файла: ");
                        Console.WriteLine(HyPabotaiPLZ[0].Name + "\n" + HyPabotaiPLZ[0].Height + "\n" + HyPabotaiPLZ[0].Width +
                           "\n" + HyPabotaiPLZ[1].Name + "\n" + HyPabotaiPLZ[1].Height + "\n" + HyPabotaiPLZ[1].Width);
                    }
                }


if (Path.GetExtension(FilePath) == ".json")
                {
                    List<Figure> json = JsonConvert.DeserializeObject<List<Figure>>(File.ReadAllText(FilePath));
                    Console.Clear();
                    Console.WriteLine("[ F1 ] - Сохранить файл.");
                    Console.WriteLine("[ F2 ] - Изменить текущий файл.");
                    Console.WriteLine("[ ESCAPE ] - Выйти");
                    Console.WriteLine("Содержимое этого JSON файла: ");
                    Console.WriteLine(json[0].Name + "\n" + json[0].Height + "\n" + json[0].Width +
                           "\n" + json[1].Name + "\n" + json[1].Height + "\n" + json[1].Width);
                }

                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.F2)
                {
                    bool works = true;
                    Console.Clear();
                    while (works)
                    {
                        Console.WriteLine("С помощью стрелок ВВЕРХ и ВНИЗ выберите нужный эелемент.");
                        Console.WriteLine("[ ENTER ] - Выбрать");
                        Console.WriteLine("[ ESCAPE ] - Выйти");
                        Console.WriteLine("Прямоугольник");
                        Console.WriteLine("Квадрат");
                        Console.SetCursorPosition(0, CursPos);
                        Console.WriteLine("->");

                        ConsoleKeyInfo key2 = Console.ReadKey();
                        if (key2.Key == ConsoleKey.UpArrow)
                        {
                            if (CursPos > 4)
                            {
                                CursPos--;
                            }
                        }
                        if (key2.Key == ConsoleKey.DownArrow)
                        {
                            if (CursPos < 5)
                            {
                                CursPos++;
                            }
                        }
                        else if (key2.Key == ConsoleKey.Enter)
                        {
                            Console.Clear();
                            Console.WriteLine("РЕДАКТИРОВАНИЕ");

                            if (CursPos == 4)
                            {
                                Console.WriteLine("Введите новое имя: ");
                                Pryamougolnik.Name = Convert.ToString(Console.ReadLine());
                                Console.WriteLine("Введите новую высоту: ");
                                Pryamougolnik.Height = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Введите новую ширину: ");
                                Pryamougolnik.Width = Convert.ToInt32(Console.ReadLine());
                            }
                            else if (CursPos == 5)
                            {
                                Console.WriteLine("Введите новое имя: ");
                                Kvadratik.Name = Convert.ToString(Console.ReadLine());
                                Console.WriteLine("Введите новую высоту: ");
                                Kvadratik.Height = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Введите новую ширину: ");
                                Kvadratik.Width = Convert.ToInt32(Console.ReadLine());
                            }



                            Console.WriteLine("Для сохранения нажмите F1");
                            key = Console.ReadKey();
                            if (key.Key == ConsoleKey.F1)
                            {

                                if (Path.GetExtension(FilePath) == ".xml")
                                {
                                    Console.Clear();
                                    XmlSerializer xml = new(typeof(List<Figure>));
                                    using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
                                    {
                                        xml.Serialize(fs, FiguresList);
                                    }
                                    Console.WriteLine("Сохранено в XML файл по этому пути.");
                                }
                                else if (Path.GetExtension(FilePath) == ".json")
                                {
                                    Console.Clear();
                                    string json = JsonConvert.SerializeObject(FiguresList);
                                    File.WriteAllText(FilePath, json);
                                    Console.WriteLine("Сохранено в JSON файл по этому пути.");
                                }
                                else if (Path.GetExtension(FilePath) == ".txt")
                                {
                                    Console.Clear();
                                    string NapolnenieTXT = $"{FiguresList[0].Name} \n {FiguresList[0].Height} \n {FiguresList[0].Width} \n {FiguresList[1].Name} \n {FiguresList[1].Height} \n {FiguresList[1].Width}";
                                    File.WriteAllText(FilePath, NapolnenieTXT);
                                    Console.WriteLine("Сохранено в TXT файл по этому пути.");
                                }
                                break;
                            }
                        }
                        else if (key2.Key == ConsoleKey.Escape)
                        {
                            Console.Clear();
                            Console.WriteLine("Конец проги");
                            Environment.Exit(0);
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Конец проги");
                    Environment.Exit(0);
                }
            }

            else
            {
                if (Path.GetExtension(FilePath) == ".txt")
                {
                    File.WriteAllText(FilePath, (FiguresList[0].Name + "\n" + FiguresList[0].Height + "\n" + FiguresList[0].Width + "\n" +
                        FiguresList[1].Name + "\n" + FiguresList[1].Height + "\n" + FiguresList[1].Width));
                }

                if (Path.GetExtension(FilePath) == ".json")
                {
                    File.WriteAllText(FilePath, JsonConvert.SerializeObject(FiguresList));
                }

                if (Path.GetExtension(FilePath) == ".xml")
                {
                    XmlSerializer xml = new XmlSerializer(typeof(List<Figure>));
                    using (FileStream fs = new FileStream(FilePath, FileMode.OpenOrCreate))
                    {
                        xml.Serialize(fs, FiguresList);
                    }
                }
            }
        }


        public static void Main()
        {
            program();
        }
    }
}
