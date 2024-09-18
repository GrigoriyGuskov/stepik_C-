using System;
using System.Collections.Generic;

public class MainClass
{
    public static void Main()
    {
        string line = Console.ReadLine(); // ввод числа в десятеричной системе 

        int x = int.Parse(line);

        string answer = "";
        string end = "";

        //Запишите тут Ваш код
        var lets = new Dictionary<int, string>()
        {
            { 10, "a"},
            { 11, "b"},
            { 12, "c"},
            { 13, "d"},
            { 14, "e"},
            { 15, "f"},
            { 0, "10"},
        };

        int res = x;

        if (x > 0)
        {

            for (int i = 0; i < line.Length - 1; i++)
            {

                var ost = res % 16;

                if (ost >= 10 || ost == 0)
                {
                    answer += lets[ost];
                }
                else
                {
                    answer += ost.ToString();
                }

                res /= 16;
            }

            if (answer != "10")
            {
                char[] arr = answer.ToCharArray();
                Array.Reverse(arr);
                string a = new string(arr);
                end += a;
            }
            else
            {
                end += answer;
            }

        }
        else
        {
            end = "0";
        }

        Console.WriteLine(end);
    }
}