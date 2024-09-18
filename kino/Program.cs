using System;

public class Viewer 
{
    private int _numberOfVisits;

    public int NumerOfVisits {  get { return _numberOfVisits; } set { _numberOfVisits = value; } }

    public Viewer(int n) 
    {
        NumerOfVisits = n;
    }

    public double Cost(double price, int n)
    {
        NumerOfVisits += n;
        return price * n;
    }
}

public class Regular : Viewer
{
    public Regular(int n) : base(n) 
    { 
        NumerOfVisits = n;
    }
    public double Cost(double price, int n)
    {
        double sum = 0;
        for (int i = 0; i < n; ++i)
        {
            ++NumerOfVisits;
            if (NumerOfVisits < 200)
                sum += price * (100 - NumerOfVisits / 10) / 100;
            else
                sum += price * 0.8;
        }
        return sum;
    }
}

public class Student : Viewer
{
    public Student(int n) : base(n)
    {
        NumerOfVisits = n;
    }
    public double Cost(double price, int n)
    {
        double sum = 0;
        for (int i = 0; i < n; ++i)
        {
            ++NumerOfVisits;
            if(NumerOfVisits % 3 == 0)
                sum += price*0.5;
            else
                sum += price;
        }
        return sum;
    }
}


public class Pensioner : Viewer
{
    public Pensioner(int n) : base(n)
    {
        NumerOfVisits = n;
    }
    public double Cost(double price, int n)
    {
        double sum = 0;
        for (int i = 0; i < n; ++i)
        {
            ++NumerOfVisits;
            if(NumerOfVisits % 5 != 0) 
                sum += price * 0.5;
        }
        return sum;
    }
}

class Program
{
    static void Main()
    {
        var text = Console.ReadLine();
        var array = text.Split(" ");
        int NoV = int.Parse(array[1]);
        double price = double.Parse(array[2]);
        int n = int.Parse(array[3]);
        double sum = 0;
        switch (array[0])
        {
            case "viewer":
                sum = (new Viewer(NoV)).Cost(price, n);
                break;
            case "regular":
                sum = (new Regular(NoV)).Cost(price, n);
                break;
            case "student":
                sum = (new Student(NoV)).Cost(price, n);
                break;
            case "pensioner":
                sum = (new Pensioner(NoV)).Cost(price,n);
                break;
        }
        Console.WriteLine(Math.Round(sum, 2));
    }
}