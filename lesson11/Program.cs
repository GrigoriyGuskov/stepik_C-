using System;
using System.Collections.Generic;
public class MainClass
{
    public static void Main()
    {
        string text = "";
        int mc = 0, fc = 0;
        List<Panda> pandas = new List<Panda>();
        List<Pair> pairs = new List<Pair>();
        while (true)
        {
            text = Console.ReadLine();
            if (text == "end")
                break;
            var arr = text.Split(" ");
            if (Enum.TryParse(arr[2], out Gender po))
            {
                if (po == Gender.male)
                    ++mc;
                else
                    ++fc;
                pandas.Add(new Panda(int.Parse(arr[0]), int.Parse(arr[1]), po));
            }
        }
        Gender p = Gender.female;
        if (mc < fc)
            p = Gender.male;

        bool fl = true;
        while(fl)
        {
            fl = false;
            double minDist = double.PositiveInfinity, res;
            int min = -1, minPair = -1;
            for (int i = 0; i < pandas.Count; ++i)
            {
                if (pandas[i].pol == p)
                {
                    pandas[i].findMinDistanse(pandas, out int index, out res);
                    if (minDist > res)
                    {
                        min = i;
                        minPair = index;
                        minDist = res;
                    }
                }
            }
            if (min >= 0)
            {
                fl = true;
                pairs.Add(new Pair(pandas[min], pandas[minPair]));
                pandas.RemoveAt(Math.Max(min, minPair));
                pandas.RemoveAt(Math.Min(min, minPair));
            }
        }


        Console.WriteLine($"Total pandas count: {fc + mc}");
        foreach (var pan in pandas)
            Console.WriteLine($"Lonely {pan.pol} panda at X: {pan.x}, Y: {pan.y}");


        foreach (var pair in pairs)         
            Console.WriteLine($"Pandas pair at distance {Math.Round(pair.distanse, 2)}, male panda at X: {pair.male.x}, Y: {pair.male.y}, female panda at X: {pair.female.x}, Y: {pair.female.y}");
            

    }
}

public enum Gender
{
    male,
    female,
}
public class Panda
{

    public int x;
    public int y;
    public Gender pol;

    public Panda(int X, int Y, Gender g)
    {
        x=X; 
        y=Y; 
        pol=g;
    }
    public double distance(Panda p2)
    {
        return Math.Sqrt(Math.Pow(x - p2.x, 2) + Math.Pow(y - p2.y, 2));
    }

    public void findMinDistanse(List<Panda> pandas, out int index, out double minDist)
    {
        minDist = double.PositiveInfinity;
        index = -1;
        double res;
        for (int i = 0; i < pandas.Count; ++i)
        {
            if (pol != pandas[i].pol)
            {
                res = distance(pandas[i]);
                if (res < minDist)
                {
                    index = i;
                    minDist = distance(pandas[i]);
                }
            }
        }
        
    }


}

public class Pair
{
    public Panda male;
    public Panda female;
    public double distanse;

    public Pair(Panda panda1, Panda panda2)
    {
        if(panda1.pol == Gender.male)
        {
            male = panda1;
            female = panda2;
        }
        else
        {
            male = panda2;
            female = panda1;
        }
        distanse = panda1.distance(panda2);
    }
}
