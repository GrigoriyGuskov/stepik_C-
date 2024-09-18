using System.Xml.Linq;
using System;

public class Program
{
    public static void Main()
    {
        var a = new Money("10", "р.", "15", "коп.");
        a.PrintTransferCost(0.05);
    }
}

public class Money
{
    public int rub;
    public int kop;


    public Money()
    {
        rub = 0;
        kop = 0;
    }
    public Money(string val, string name)
    {
        int num = int.Parse(val);
        if (num < 0)
            Console.WriteLine("Не может быть отрицательным!");
        else if (name == "р.")
        {
            kop = 0;
            rub = num;
        }
        else if (name == "коп.")
        {
            kop = num % 100;
            rub = num / 100;
        } 
    }

    public Money(string val1, string name1, string val2, string name2)
    {
        if (name2 == "р." || name1 == "коп.")
            Console.WriteLine("Рубли и копейки перепутаны местами!");
        else
        {
            int num1 = int.Parse(val1);
            int num2 = int.Parse(val2);
            if (num1 < 0 || num2 < 0)
                Console.WriteLine("Не может быть отрицательным!");
            else
            {
                kop = num2 % 100;
                rub = num1 + num2 / 100;
            }
        }
    }

    public void Print()
    {
        if (rub > 0)
            Console.Write(rub + " р. ");
        Console.Write(kop + " коп.\n");
    }

    public void PrintTransferCost(double n)
    {
        int num = (int)Math.Round((rub*100 +  kop) * (1 + n));
        if (num/100 > 0)
            Console.Write($"{num / 100} р. ");
        Console.Write($"{num % 100} коп.\n");
    }

    public static Money Sum(Money a, Money b)
    {
        var c = new Money();
        c.kop = (a.kop + b.kop)%100;
        c.rub = a.rub + b.rub + (a.kop + b.kop) / 100;
        return c;
    }

    public static Money Difference(Money a, Money b)
    {
        var c = new Money((Math.Abs(a.rub*100 + a.kop - b.rub*100 - b.kop)).ToString(), "коп.");
        return c;
    }

}
