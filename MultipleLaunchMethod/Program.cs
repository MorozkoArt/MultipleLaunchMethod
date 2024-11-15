using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_method_of_climbing_the_hill
{
    internal class Program
    {
        public static int L = 7;

        public static string DecimalToBinary(int dec)
        {
            string binary = "";
            while (dec > 0)
            {
                binary += (dec % 2).ToString();
                dec /= 2;
            }
            while (binary.Length < 7)
            {
                binary += "0";
            }
            char[] stringArray = binary.ToCharArray();
            Array.Reverse(stringArray);
            string reversedBinary = new string(stringArray);
            return reversedBinary;

        }
        public static int QuadraticAdaptationFunction(int dec)
        {
            return Convert.ToInt32(Math.Pow((dec - Convert.ToInt32(Math.Pow(2, L - 1))), 2));
        }

        public static int TheHammingDistance(string s1, string s2)
        {
            int distance = 0;
            if (s1.Length == s2.Length)
            {
                for (int i = 0; i < s1.Length; ++i)
                {
                    if (s1[i] != s2[i])
                        distance++;
                }
                return distance;
            }
            return -1;
        }
        public static int SearchIndice(string s, List<List<string>> strings)
        {
            for (int i = 0; i < strings.Count; ++i)
            {
                if (strings[i][0] == s)
                {
                    return i;
                }
            }
            return -1;

        }
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<List<string>> binary_encodings = new List<List<string>>();
            List<string> chek = new List<string>();
            bool flag2 = true;
            int n = 0;
            string metod = "";
            while (true)
            {
                int path = -1;
                Console.WriteLine("Меню");
                Console.WriteLine("1 - Сгенерировать список кодировок");
                Console.WriteLine("2 - Запустить алгоритм");
                Console.WriteLine("3 - Завершить работу программы");
                path = int.Parse(Console.ReadLine());
                switch (path)
                {
                    case 1:
                        Console.WriteLine("\n");
                        Console.WriteLine("Ландшафт:");
                        binary_encodings.Clear();
                        for (int i = 0; i < Math.Pow(2, L); ++i)
                        {
                            List<string> list = new List<string>();
                            list.Add(DecimalToBinary(i));
                            if (i == 0) list.Add("0");
                            else list.Add(Math.Round(5 * Math.Sin(i / (180 / Math.PI)) + Math.Log(i),2).ToString());
                            if(i <32) Console.WriteLine("Кодировка: " + list[0] + " Приспособленность: " + double.Parse(list[1]));                                                                              
                            binary_encodings.Add(list);
                        }                       
                        break;
                    case 2:                       
                        if (binary_encodings.Count > 0)
                        {
                            Console.WriteLine("Введите количество шагов от 1 до 2^7 для Метода многократного запуска");
                            int max_step = int.Parse(Console.ReadLine());
                            Console.WriteLine("Введите количество шагов от 1 до 2^7, для алгоритмов входящих в метод многократного запуска");
                            n = int.Parse(Console.ReadLine());                           
                            int number_1 = -1;
                            string MaxS = "";
                            double max = 0;
                            for (int step = 0; step < max_step; step++)
                            {                               
                                List<string> locality = new List<string>();
                                int chois = rnd.Next(0, 3);
                                if (chois == 0)
                                {
                                    Console.WriteLine("\n" + "^".PadRight(140, '^'));
                                    Console.WriteLine("Метод Монте-Карло");
                                    string MaxS1 = "0";
                                    string max1 = "0";
                                    flag2 = true;
                                    number_1 = -1;
                                    chek.Clear();
                                    for (int i = 0; i < n; ++i)
                                    {
                                        flag2 = true;
                                        while (flag2 == true)
                                        {
                                            number_1 = rnd.Next(0, binary_encodings.Count);                                            
                                            if(chek.Contains(binary_encodings[number_1][0]) != true)
                                            {
                                                Console.Write(" Номер шага: " + (i+1) + " Выбранная кодировка: " + binary_encodings[number_1][0] 
                                                    +  "  Максимальная приспособленность: " + max1 +  "  Приспособленность: " + double.Parse(binary_encodings[number_1][1]));
                                                if (double.Parse(max1) < double.Parse(binary_encodings[number_1][1]))
                                                {
                                                    max1 = binary_encodings[number_1][1];
                                                    MaxS1 = binary_encodings[number_1][0];
                                                    chek.Add(binary_encodings[number_1][0]);
                                                    Console.Write("  <--The maximum has changed!");
                                                }
                                                flag2 = false;
                                            }                                                                                   
                                        }
                                        Console.WriteLine();
                                    }
                                    Console.WriteLine("/".PadRight(140, '/'));
                                    Console.WriteLine("Результат работы алгоритма: максимальная приспособленность: " + double.Parse(max1) + " Максимальная кодировка: " + MaxS1);
                                    if (double.Parse(max1) > max)
                                    {
                                        max = double.Parse(max1);
                                        MaxS = MaxS1;
                                        metod = "Монте карло";
                                        Console.WriteLine("Был сменен общий максимум. Максимальная приспособленность: " + max + " Макс кодировка: " + MaxS);
                                    }
                                    else Console.WriteLine("Общий максимум не изменился. Максимальная приспособленность: " + max + " Макс кодировка: " + MaxS);
                                }
                                else if (chois == 1)
                                {                                    
                                    Console.WriteLine("\n" + "^".PadRight(140, '^'));
                                    Console.WriteLine("Метод поиска в глубину");
                                    string MaxS2 = DecimalToBinary(rnd.Next(0, binary_encodings.Count));
                                    string max_Si2 = MaxS2;
                                    string max2 = binary_encodings[SearchIndice(MaxS2, binary_encodings)][1];
                                    string s1 = "";
                                    string s2 = "";
                                    locality.Clear();
                                    for (int i = 0; i < Math.Pow(2, L); ++i)
                                    {
                                        if (TheHammingDistance(MaxS2, binary_encodings[i][0]) == 1)
                                        {
                                            locality.Add(binary_encodings[i][0]);
                                        }
                                    }                                  
                                    for (int i = 0; i < n; ++i)
                                    {
                                        max_Si2 = MaxS2;
                                        Console.WriteLine("Номер шага: " + (i + 1) + " Максимальная приспособленность: " + double.Parse(max2) + " Максимальная кодировка: " + MaxS2);
                                        Console.WriteLine("|Окрестность|" + "Приспособленность|");
                                        Console.WriteLine("-".PadRight(31, '-'));
                                        for (int k = 0; k < locality.Count; k++)
                                        {
                                            s1 = locality[k].PadRight(9);
                                            s2 = binary_encodings[SearchIndice(locality[k], binary_encodings)][1];                              
                                            Console.WriteLine("|".PadRight(3) + s1 + "|".PadRight(7) + double.Parse(s2).ToString().PadRight(11) + "|");
                                            Console.WriteLine("-".PadRight(31, '-'));
                                        }
                                        if (locality.Count > 0)
                                        {
                                            string Si = locality[rnd.Next(0, locality.Count)];
                                            Console.WriteLine(" Выбираемая кодировка (Si): " + Si + " Её приспособленность: " + double.Parse(binary_encodings[SearchIndice(Si, binary_encodings)][1]));
                                            locality.Remove(Si);
                                            if (double.Parse(max2) < double.Parse(binary_encodings[SearchIndice(Si, binary_encodings)][1]))
                                            {
                                                MaxS2 = Si;
                                                max2 = binary_encodings[SearchIndice(Si, binary_encodings)][1];
                                                Console.WriteLine("Произошла смена максимума. Максимальная приспособленность: " + Math.Round(double.Parse(max2), 2)
                                                    + " Максимальная кодировка: " + MaxS2);
                                                locality.Clear();
                                                for (int j = 0; j < Math.Pow(2, L); ++j)
                                                {
                                                    if (TheHammingDistance(MaxS2, binary_encodings[j][0]) == 1 && binary_encodings[j][0] != max_Si2)
                                                    {
                                                        locality.Add(binary_encodings[j][0]);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                            break;
                                        Console.WriteLine("/".PadRight(140, '/'));
                                    }
                                    Console.WriteLine("Результат работы алгоритма: максимальная приспособленность: " + double.Parse(max2) + " Максимальная кодировка: " + MaxS2);
                                    if (double.Parse(max2) > max)
                                    {
                                        max = double.Parse(max2);
                                        MaxS = MaxS2;
                                        metod = "в глубину";
                                        Console.WriteLine("Был сменен общий максимум. Максимальная приспособленность: " + max + " Макс кодировка: " + MaxS);
                                    }
                                    else Console.WriteLine("Общий максимум не изменился. Максимальная приспособленность: " + max + " Макс кодировка: " + MaxS);
                                }
                                else if (chois == 2)
                                {
                                    Console.WriteLine("\n" + "^".PadRight(140, '^'));
                                    Console.WriteLine("Метод поиска в ширину");
                                    string MaxS3 = DecimalToBinary(rnd.Next(0, binary_encodings.Count));
                                    string max_Si3 = MaxS3;
                                    string max3 = binary_encodings[SearchIndice(MaxS3, binary_encodings)][1];
                                    string s3 = "";
                                    string s4 = "";
                                    locality.Clear();
                                    for (int i = 0; i < Math.Pow(2, L); ++i)
                                    {
                                        if (TheHammingDistance(MaxS3, binary_encodings[i][0]) == 1)
                                            locality.Add(binary_encodings[i][0]);
                                    }                                   
                                    for (int i = 0; i < n; ++i)
                                    {
                                        max_Si3 = MaxS3;
                                        Console.WriteLine("Номер шага: " + (i + 1) + " Максимальная приспособленность: " + double.Parse(max3) + " Максимальная кодировка: " + MaxS3);
                                        Console.WriteLine("|Окрестность|" + "Приспособленность|");
                                        Console.WriteLine("-".PadRight(31, '-'));
                                        for (int k = 0; k < locality.Count; k++)
                                        {
                                            s3 = locality[k].PadRight(9);
                                            s4 = binary_encodings[SearchIndice(locality[k], binary_encodings)][1];                                            
                                            Console.WriteLine("|".PadRight(3) + s3 + "|".PadRight(7) + double.Parse(s4).ToString().PadRight(11) + "|");
                                            Console.WriteLine("-".PadRight(31, '-'));
                                        }
                                        string Si = "";
                                        double MaxLocation = -1;
                                        for (int f = 0; f < locality.Count; f++)
                                        {
                                            if (MaxLocation < double.Parse(binary_encodings[SearchIndice(locality[f], binary_encodings)][1]))
                                            {
                                                Si = locality[f];
                                                MaxLocation = double.Parse(binary_encodings[SearchIndice(locality[f], binary_encodings)][1]);
                                            }
                                        }
                                        Console.WriteLine(" Выбираемая кодировка (Si): " + Si + " Её приспособленность: " + double.Parse(binary_encodings[SearchIndice(Si, binary_encodings)][1]));
                                        locality.Remove(Si);
                                        if (double.Parse(max3) < double.Parse(binary_encodings[SearchIndice(Si, binary_encodings)][1]))
                                        {
                                            MaxS3 = Si;
                                            max3 = binary_encodings[SearchIndice(Si, binary_encodings)][1];
                                            Console.WriteLine("Произошла смена максимума. Максимальная приспособленность: " + double.Parse(max3)
                                                + " Максимальная кодировка: " + MaxS3);
                                            locality.Clear();
                                            for (int j = 0; j < Math.Pow(2, L); ++j)
                                            {
                                                if (TheHammingDistance(MaxS3, binary_encodings[j][0]) == 1 && binary_encodings[j][0] != max_Si3)
                                                    locality.Add(binary_encodings[j][0]);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("/".PadRight(140, '/'));
                                            break;
                                        }
                                        Console.WriteLine("/".PadRight(140, '/'));
                                    }
                                    Console.WriteLine("Результат работы алгоритма: максимальная приспособленность: " + double.Parse(max3) + " Максимальная кодировка: " + MaxS3);
                                    if (double.Parse(max3) > max)
                                    {
                                        max = double.Parse(max3);
                                        MaxS = MaxS3;
                                        metod = "в ширину";
                                        Console.WriteLine("Был сменен общий максимум. Максимальная приспособленность: " + max + " Макс кодировка: " + MaxS);
                                    }
                                    else Console.WriteLine("Общий максимум не изменился. Максимальная приспособленность: " + max + " Макс кодировка: " + MaxS);
                                }
                            }
                            Console.WriteLine("\n" + "zvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzvzv");
                            Console.WriteLine("Итоговый результат работы алгоритма: максимальная приспособленность: " + max + " Максимальная кодировка: " + MaxS + ".  Метод - " + metod);
                        }
                        else
                        {
                            Console.WriteLine("Список пустой!!!");
                        }
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неккоректный ввод");
                        break;
                }
            }
        }
    }
}

