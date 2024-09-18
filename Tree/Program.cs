using System;

public class Program
{
    public static void Main()
    {
        var tree1 = new Tree("сосна", -6);
        var tree2 = new Tree("береза", 80);
    }
}

public class Tree
{
    private string name;
    private int heigth;

    public string Name { get { return name; } init { name = value; } }
    public int Heigth { get {  return heigth; } set { if (value < 1) heigth = 1; else heigth = value; } }
    
    public Tree(string nam, int heig)
    {
        name = nam;
        if (heig < 1) 
            heigth = 1; 
        else 
            heigth = heig;
       
        Console.Write("Вы создали ");
        Print();
    }

    public void Print()
    {
        Console.WriteLine("дерево \"" + name + "\" высотой " + heigth + " см");
    }
}