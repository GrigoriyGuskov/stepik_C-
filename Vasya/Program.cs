using System;

public class Program
{
    public static void Main()
    {
        var text = Console.ReadLine();
        var arr = text.Split(" ");
        int n = int.Parse(arr[1]);
        var vas = new Vasya(arr[0], n);
        vas.Print();
    }
}

public class Vasya
{
    private string name;
    private int age;

    public string Name 
    {  
        get 
        { 
            return name; 
        } 
        init 
        {
            if (value != "Василий")
                name = "Я не " + value + ", а Василий!";
            else
                name = "Василий";
        }
    }
    public int Age 
    { 
        get
        { 
            return age; 
        }
        set
        {
            if (value < 0)
                age = 0;
            else if (value > 122)
                age = 122;
            else
                age = value;
        }
    }

    public Vasya(string nam, int ag)
    {
        Name = nam;
        Age = ag;
    }

    public void Print()
    {
        Console.WriteLine(name);
        Console.Write("Мне " + age);
        if (age % 10 > 0 && age % 10 < 5 && (age % 100 < 10 || age % 100 > 20))
            if(age % 10 == 1)
                Console.WriteLine(" год");
            else
                Console.WriteLine(" года");
        else
            Console.WriteLine(" лет");
    }

}