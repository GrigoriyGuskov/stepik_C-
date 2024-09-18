using System;

public class Program
{
    public static void Main()
    {
        var text = Console.ReadLine();
        var array = text.Split('.');
        var date = new Date(int.Parse(array[0]), int.Parse(array[1]), int.Parse(array[2]));
        date.Print();
        date.Next().Print();
        date.Previous().Print();
        date.PrintForward(5);
        date.PrintBackward(5);
    }
}

public class Date
{
    enum Months
    {
        January = 1,
        February = 2,
        March = 3,
        April = 4,
        May = 5,
        June = 6,
        July = 7,
        August = 8,
        September = 9,
        October = 10,
        November = 11,
        December = 12
    }

    private static readonly int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31};
    private int day;
    private int month;
    private int year;

    public Date(int d, int m, int y)
    {
        year = y + (m-1)/12;
        month = (m - 1) % 12 + 1;
        int i = d;
        
        for (; i > days[month - 1];)
        {
            if (month == 2 && (year%400 == 0 || (year % 4 == 0 && year % 100 != 0)))
            {
                if (i == 29)
                    break;
                else
                    --i;
            }
            i -= days[month - 1];
            ++month;
            if (month > 12)
            {
                ++year;
                month -= 12;
            }

        }
        
        day = i;
        
    }

    public Date Next()
    {
        return new Date(day+1, month, year);
    }

    public Date Previous()
    {
        int d = day;
        int m = month;
        int y = year;
        if (d == 1)
        {
            if (m == 1)
            {
                --y;
                m = 12;
                d = 32;
            }
            else
            {
                --m;
                d += days[m-1] + 1;
            }
        }
        return new Date(d-1, m, y);
    }

    public void Print()
    {
        Console.WriteLine($"The {day} of {(Months)(month)} {year}");
    }

    public void PrintForward(int n)
    {
        var dat = this.Next();
        for(int i = 0; i < n; ++i)
        {
            dat.Print();
            dat = dat.Next();
        }
    }

    public void PrintBackward(int n)
    {
        var dat = this.Previous();
        for (int i = 0; i < n; ++i)
        {
            dat.Print();
            dat = dat.Previous();
        }
    }
}